using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using CrudAppProject.EmailSender.EmailSenderEntities;
using Abp.ObjectMapping;
using System.Threading.Tasks;
using Abp.Dependency;


namespace CrudAppProject.EmailSender.EmailTemplateManager
{
    public class EmailTemplateManager : IEmailTemplateManager , ITransientDependency
    {
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<EmailTemplate, long> _templateRepository;
        private readonly IObjectMapper _objectMapper;
        public EmailTemplateManager(IUnitOfWorkManager unitOfWorkManager, IRepository<EmailTemplate, long> templateRepository, IObjectMapper objectMapper)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _templateRepository = templateRepository;
            _objectMapper = objectMapper;
        }
        public async Task<EmailTemplate> CreateTemplateAsync(EmailTemplate emailTemplate)
        {
            var template = _objectMapper.Map<EmailTemplate>(emailTemplate);
            template.TenantId = emailTemplate.TenantId;
            await _templateRepository.InsertAsync(emailTemplate);
            var savedTemplateDto = _objectMapper.Map<EmailTemplate>(template);
            return savedTemplateDto;

        }
        [UnitOfWork]
        public async Task<EmailTemplate> GetTemplateByIdAsync(int tenantId, string templateName)
        {

            var template = await _templateRepository.FirstOrDefaultAsync(t => t.TenantId == tenantId && t.Name == templateName);

            if (template == null)
            {
                return null;
            }

            // Map and return
            return _objectMapper.Map<EmailTemplate>(template);
        }
        public Task DeleteTemplateAsync(long id)
        {
            _templateRepository.DeleteAsync(id);
            return Task.CompletedTask;
        }
    }
}
