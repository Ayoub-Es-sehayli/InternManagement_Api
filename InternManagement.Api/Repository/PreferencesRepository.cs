using System.Threading.Tasks;
using InternManagement.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace InternManagement.Api.Repository
{
  public class PreferencesRepository : IPreferencesRepository
  {
    private readonly InternContext _context;
    public PreferencesRepository(InternContext context)
    {
      _context = context;
    }
    public async Task<Preferences> EditPreferencesAsync(Preferences config)
    {
      _context.Preferences.Update(config);
      await _context.SaveChangesAsync();
      return config;
    }

    public async Task<Preferences> GetPreferencesAsync()
    {
      var model = await _context.Preferences.FirstOrDefaultAsync();
      return model;
    }
  }
}