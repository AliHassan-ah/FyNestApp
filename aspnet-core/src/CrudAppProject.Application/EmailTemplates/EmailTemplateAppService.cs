using Abp.Domain.Repositories;
using CrudAppProject.EmailSender.EmailSenderEntities;
using CrudAppProject.EmailTemplates.EmailTemplatesDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudAppProject.EmailTemplates
{
    public class EmailTemplateAppService : CrudAppProjectAppServiceBase, IEmailTemplateAppService
    {
        private readonly IRepository<EmailTemplate, long> _emailTemplateRepository;

        public EmailTemplateAppService(IRepository<EmailTemplate, long> emailTemplateRepository)
        {
            _emailTemplateRepository = emailTemplateRepository;
        }

        public async Task<List<EmailTemplateDto>> GetAllEmailTemplatesAsync()
        {
            var allEmailTemplates = await _emailTemplateRepository.GetAllListAsync();

            return ObjectMapper.Map<List<EmailTemplateDto>>(allEmailTemplates);
        }

        public async Task<EmailTemplateDto> CreateEmailTemplate(EmailTemplateDto input)
        {
            var emailTemplate = new EmailTemplate
            {
                Name = input.Name,
                Subject = input.Subject,
                Content = input.Content,
                Token = input.Token,
                TenantId = (int)(AbpSession.TenantId ?? null),
            };
            var createdEmailTemplate = await _emailTemplateRepository.InsertAsync(emailTemplate);
            return ObjectMapper.Map<EmailTemplateDto>(createdEmailTemplate);
        }
        public async Task<EmailTemplateDto> UpdateEmailTemplate(long id, EmailTemplateDto input)
        {
            var existingEmailTemplates = await _emailTemplateRepository.GetAsync(id);

            existingEmailTemplates.Subject = input.Subject;
            existingEmailTemplates.Content = input.Content;
            existingEmailTemplates.Token = input.Token;

            var updatedProduct = await _emailTemplateRepository.UpdateAsync(existingEmailTemplates);
            return ObjectMapper.Map<EmailTemplateDto>(updatedProduct);
        }
        public async Task DeleteEmailTemplate(long id)
        {
            await _emailTemplateRepository.DeleteAsync(id);
        }
        public async Task<EmailTemplateDto> GetSingleEmailTemplate(long id)
        {
            var singleEmailTemplate = await _emailTemplateRepository.GetAsync(id);
            return ObjectMapper.Map<EmailTemplateDto>(singleEmailTemplate);
        }
    }
}
