using Abp.Application.Services;
using CrudAppProject.MultiTenancy.Dto;

namespace CrudAppProject.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

