using Abp.Application.Services;
using CrudAppProject.QueuedEmails.QueuedEmailsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudAppProject.QueuedEmails
{
    public interface IQueuedEmailAppService : IApplicationService
    {
        Task CreateQueueEmailAsync(QueuedEmailDto input);
        Task<Abp.Application.Services.Dto.PagedResultDto<QueuedEmailDto>> GetAllQueuedEmailAsync(PagedEmailResultRequestDto input);
        Task<QueuedEmailDto> GetSingleEmail(long id);
    }
}
