using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InternManagement.Api.Enums;
using InternManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace InternManagement.Api.Repository
{
  public class InternRepository : IInternRepository
  {
    private readonly InternContext _context;

    public InternRepository(InternContext context)
    {
      _context = context;
    }
    public async Task<Intern> AddInternAsync(Intern model)
    {
      model.Id = await GetInternCountAsync() + 1;
      model.State = eInternState.ApplicationFilled;
      await _context.AddAsync<Intern>(model);
      await SaveChangesAsync();
      return await _context.Interns.OrderBy(intern => intern.Id).LastAsync();
    }

    public async Task<Intern> GetInternAsync(int id)
    {
      var intern = await _context.Interns
      .Where(i => i.Id == id)
      .FirstOrDefaultAsync();
      if (intern != null)
      { await _context.Entry(intern).Navigation("Documents").LoadAsync(); }
      return intern;
    }

    public async Task<Intern> GetInternWithAttendanceAndDivision(int id)
    {
      var intern = await this.GetInternAsync(id);
      if (intern != null)
      {
        await _context.Entry(intern).Collection(i => i.Attendance).LoadAsync();
        await _context.Entry(intern).Navigation("Division").LoadAsync();
        await _context.Entry(intern).Navigation("Decision").LoadAsync();
      }
      return intern;
    }

    public async Task<Intern> GetInternWithDecision(int id)
    {
      var intern = await this.GetInternAsync(id);
      if (intern != null)
      {
        await _context.Entry(intern).Navigation("Decision").LoadAsync();
      }
      return intern;
    }

    public async Task<Intern> GetInternWithDepartment(int id)
    {
      var intern = await this.GetInternAsync(id);
      if (intern != null)
      {
        await _context.Entry(intern).Navigation("Division").LoadAsync();
        await _context.Entry(intern.Division).Navigation("Department").LoadAsync();
      }
      return intern;
    }

    public async Task<Intern> GetInternWithDepartmentAndLocation(int id)
    {
      var intern = await this.GetInternAsync(id);
      if (intern != null)
      {
        await _context.Entry(intern).Navigation("Division").LoadAsync();
        await _context.Entry(intern.Division).Navigation("Department").LoadAsync();
        await _context.Entry(intern.Division.Department).Navigation("Location").LoadAsync();
      }
      return intern;
    }

    public async Task<int> GetInternCountAsync()
    {
      return await _context.Interns.CountAsync();
    }

    public async Task<bool> InternExistsAsync(int id)
    {
      var count = await _context.Interns.CountAsync(intern => intern.Id == id);

      return count != 0;
    }

    private async Task SaveChangesAsync()
    {
      await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Intern>> GetInternsAsync()
    {
      return await _context.Interns
      .Include(i => i.Decision)
      .Select(intern => new Intern
      {
        Id = intern.Id,
        FirstName = intern.FirstName,
        LastName = intern.LastName,
        Division = intern.Division,
        Decision = intern.Decision,
        State = intern.State
      })
      .ToListAsync();
    }

    public async Task<bool> SetDecisionForIntern(int id, Decision decision)
    {
      if (await this.InternExistsAsync(id))
      {
        var intern = await this.GetInternAsync(id);
        if (intern.Decision == null)
        {
          await _context.AddAsync<Decision>(decision);
        }
        else
        {
          intern.Decision.Code = decision.Code;
          intern.Decision.Date = decision.Date;
        };
        switch (intern.State)
        {
          case eInternState.ApplicationFilled:
          case eInternState.AssignedDecision:
            intern.State = eInternState.AssignedDecision;
            break;
        }
        await SaveChangesAsync();
        return true;
      }
      return false;
    }

    public async Task<bool> SetAttestationForIntern(int id, Attestation attestation)
    {
      if (await this.InternExistsAsync(id))
      {
        var intern = await this.GetInternAsync(id);
        if (intern.State == eInternState.Finished)
        {
          if (intern.Attestation == null)
          {
            await _context.AddAsync(attestation);
          }
          else
          {
            intern.Attestation = attestation;
          }

          intern.State = eInternState.FileClosed;

          await _context.SaveChangesAsync();
          return true;
        }
      }
      return false;
    }

    public async Task<bool> SetCancellationForIntern(int id, Cancellation cancellation)
    {
      if (await this.InternExistsAsync(id))
      {
        var intern = await this.GetInternAsync(id);
        switch (intern.State)
        {
          case eInternState.AssignedDecision:
          case eInternState.Started:
          case eInternState.Finished:
            intern.State = eInternState.Cancelled;
            await _context.AddAsync(cancellation);
            await _context.SaveChangesAsync();
            return true;
        }
      }
      return false;
    }

    public async Task<bool> UpdateInternAsync(int id, Intern model)
    {
      if (await this.InternExistsAsync(id))
      {
        model.State = await _context.Interns.Where(x => x.Id == id).Select(x => x.State).SingleOrDefaultAsync();
        _context.Update(model);
        await _context.SaveChangesAsync();
        return true;
      }
      return false;
    }

    public async Task<bool> UpdateDocumentsAsync(int id, eDocumentState reportState, eDocumentState evalFormState)
    {
      if (await this.InternExistsAsync(id))
      {
        var intern = (await this.GetInternAsync(id));
        var duration = (intern.EndDate - intern.StartDate).TotalDays / 30;

        intern.Documents.Report = reportState;
        intern.Documents.EvaluationForm = evalFormState;
        intern.FileAlarmState = this.ProcessFile((int)duration, intern.Documents);
        _context.Update(intern);
        await _context.SaveChangesAsync();
        return true;
      }

      return false;
    }
    private eFileAlarmState ProcessFile(int duration, Documents documents)
    {
      return ((duration > 1 && documents.Convention == eDocumentState.Submitted)
      && (documents.Report == eDocumentState.Valid)
      && (documents.EvaluationForm == eDocumentState.Submitted)
      && (documents.CV == eDocumentState.Submitted)
      && (documents.Letter == eDocumentState.Submitted)
      && (documents.Insurance == eDocumentState.Submitted))
      ?
         eFileAlarmState.None
      :
         eFileAlarmState.IncompleteFile;

    }
  }
}