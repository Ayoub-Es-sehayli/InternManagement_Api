using System.Collections.Generic;
using System.Threading.Tasks;
using InternManagement.Api.Dtos;

namespace InternManagement.Api.Services
{
    public interface IInternService
    {
        Task<InternDto> AddInternAsync(InternDto dto);
        Task<InternDto> GetInternByIdAsync(int id);

<<<<<<< HEAD
        Task<IEnumerable<InternListItemDto>> GetInternsAsync();
        Task<DecisionDto> PrintDecisionAsync(int id);
    }
=======
    Task<IEnumerable<InternListItemDto>> GetInternsAsync();
    Task<bool> SetDecisionAsync(DecisionFormDto dto);
  }
>>>>>>> 47801ec4cf26401bd773445f933b7ab08c4d869e
}