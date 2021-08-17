using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using InternManagement.Api.Dtos;
using InternManagement.Api.Enums;
using InternManagement.Api.Helpers;
using InternManagement.Api.Models;
using InternManagement.Api.Repository;

namespace InternManagement.Api.Services
{
  public class InternService : IInternService
  {
    private readonly IInternRepository _repository;
    private readonly IMapper _mapper;
    private readonly IPrintHelper _print;

    public InternService(IInternRepository repository, IMapper mapper, IPrintHelper print)
    {
      this._repository = repository;
      this._mapper = mapper;
      this._print = print;
    }

    public async Task<InternDto> AddInternAsync(InternDto dto)
    {
      var intern = await _repository.AddInternAsync(_mapper.Map<Intern>(dto));
      dto.Id = intern.Id;
      return dto;
    }

    public async Task<InternDto> GetInternByIdAsync(int id)
    {
      if (await _repository.InternExistsAsync(id))
      {
        var intern = await _repository.GetInternAsync(id);
        return _mapper.Map<InternDto>(intern);
      }
      return null;
    }

    public async Task<IEnumerable<InternListItemDto>> GetInternsAsync()
    {
      var interns = await _repository.GetInternsAsync();

      var dtos = _mapper.Map<IEnumerable<InternListItemDto>>(interns);

      return dtos;
    }
    public async Task<DecisionDto> PrintDecisionAsync(int id)
    {
      var intern = await _repository.GetInternWithDepartment(id);
      if (intern != null)
      {
        var templateDecision = _print.PrintDecision(intern.Gender);
        var decisiondto = _mapper.Map<DecisionDto>(intern);
        decisiondto.Template = templateDecision;
        return decisiondto;
      }
      return null;
    }
    public async Task<AttestationDto> PrintAttestationAsync(int id)
    {
      var intern = await _repository.GetInternWithDepartmentAndLocation(id);
      if (intern != null)
      {
        var templateAttestation = _print.PrintCertificate(intern.Gender);
        var attestationdto = _mapper.Map<AttestationDto>(intern);
        attestationdto.Template = templateAttestation;
        return attestationdto;
      }
      return null;
    }
    public async Task<AnnulationDto> PrintAnnulationAsync(int id, AnnulationReasonsDto reasons)
    {
      var intern = await _repository.GetInternWithDecision(id);
      if (intern != null)
      {
        var templateAnnulation = _print.PrintCancel(intern.Gender);
        var annulationdto = _mapper.Map<AnnulationDto>(intern);
        var genderModifier = intern.Gender == eGender.Female ? "e" : "";

        annulationdto.Absence =
          "<br /> -Vu le nombre d’absence injustifiées de l’intéressé" + genderModifier;
        annulationdto.Contact =
          "<br /> -Vu l’appel téléphonique de l’intéressé" + genderModifier
          + " stipulant l’annulation du stage.";

        if (reasons.Absence && reasons.Contact)
        {
          annulationdto.Absence += ";";
        }
        else if (reasons.Absence)
        {
          annulationdto.Absence += ".";
          annulationdto.Contact = "";
        }
        else if (reasons.Contact)
        {
          annulationdto.Absence = "";
        }
        else
        {
          annulationdto.Absence = "";
          annulationdto.Contact = "";
        }
        annulationdto.Template = templateAnnulation;
        return annulationdto;
      }
      return null;
    }
    public async Task<bool> SetDecisionAsync(DecisionFormDto dto)
    {
      var model = _mapper.Map<Decision>(dto);
      return await _repository.SetDecisionForIntern(dto.InternId, model);
    }


    public async Task<bool> SetAttestationAsync(AttestationFormDto dto)
    {
      var model = _mapper.Map<Attestation>(dto);
      return await _repository.SetAttestationForIntern(dto.InternId, model);
    }

    public async Task<bool> SetCancellationAsync(CancellationFormDto dto)
    {
      var model = _mapper.Map<Cancellation>(dto);
      return await _repository.SetCancellationForIntern(dto.InternId, model);
    }

    public async Task<InternInfoDto> GetInternInfoByIdAsync(int id)
    {
      var intern = await _repository.GetInternWithAttendanceAndDivision(id);
      return _mapper.Map<InternInfoDto>(intern);
    }
  }
}