using System.Threading.Tasks;
using Abp.Application.Services;
using CrudAppProject.Authorization.Accounts.Dto;

namespace CrudAppProject.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
