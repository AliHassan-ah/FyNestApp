using Abp.Domain.Repositories;
using CrudAppProject.EmailSender.EmailSenderEntities;
using CrudAppProject.OrderDetails;
using CrudAppProject.QueuedEmails.QueuedEmailsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudAppProject.QueuedEmails
{
    public class QueuedEmailAppService : CrudAppProjectAppServiceBase, IQueuedEmailAppService
    {
        private readonly IRepository<QueuedEmail, long> _queuedEmailRepository;
        private readonly IRepository<OrderDetail, long> _orderDetailRepository;
        public QueuedEmailAppService(IRepository<QueuedEmail, long> queuedEmailRepository, IRepository<OrderDetail, long> orderDetailRepository)
        {
            _queuedEmailRepository = queuedEmailRepository;
            _orderDetailRepository = orderDetailRepository;
        }
        public async Task CreateQueueEmailAsync(QueuedEmailDto input)
        {
            var emailQueueEntity = ObjectMapper.Map<QueuedEmail>(input);
            await _queuedEmailRepository.InsertAsync(emailQueueEntity);
        }

        public async Task<Abp.Application.Services.Dto.PagedResultDto<QueuedEmailDto>> GetAllQueuedEmailAsync(PagedEmailResultRequestDto input)
        {
            var allEmails = await _queuedEmailRepository.GetAllListAsync();

            if (!string.IsNullOrWhiteSpace(input.Keyword))
            {
                allEmails = allEmails.Where(email =>
                email.Subject.Contains(input.Keyword) ||
                //email.Status.Contains(input.Keyword)||
                email.ToEmail.Contains(input.Keyword)
                ).ToList();

            }
            var totalCount = await _queuedEmailRepository.CountAsync();
            var pagedEmails = allEmails.OrderBy(email => email.CreationTime).Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            var result = ObjectMapper.Map<List<QueuedEmailDto>>(pagedEmails);

            return new Abp.Application.Services.Dto.PagedResultDto<QueuedEmailDto>(totalCount, result);
        }

        public async Task<QueuedEmailDto> GetSingleEmail(long id)
        {
            var singleEmail = _queuedEmailRepository.GetAsync(id).Result;
            return ObjectMapper.Map<QueuedEmailDto>(singleEmail);
        }

    }
}
