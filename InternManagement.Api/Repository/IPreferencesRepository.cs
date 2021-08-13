using System.Threading.Tasks;
using InternManagement.Api.Models;

namespace InternManagement.Api.Repository
{
    public interface IPreferencesRepository
    {
        Task<Preferences> EditPreferencesAsync(Preferences config);
        Task SaveChangesAsync();
    }
}