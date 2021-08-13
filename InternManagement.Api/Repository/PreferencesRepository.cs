using System.Threading.Tasks;
using InternManagement.Api.Models;

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
            var result = new Preferences
            {
                nInterns = 0,
                nAttendanceDays = 0
            };
            result.nInterns = config.nInterns;
            result.nAttendanceDays = config.nAttendanceDays;
            _context.Preferences.Update(result);
            await SaveChangesAsync();
            return result;
        }
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}