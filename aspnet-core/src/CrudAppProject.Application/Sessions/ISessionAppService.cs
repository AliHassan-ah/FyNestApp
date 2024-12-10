using System.Threading.Tasks;
using Abp.Application.Services;
using CrudAppProject.Sessions.Dto;

namespace CrudAppProject.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
