using CrudAppProject.EmailSender.EmailSenderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudAppProject.EmailSender.EmailTemplateManager
{
    public interface IEmailTemplateManager
    {
        Task<EmailTemplate> CreateTemplateAsync(EmailTemplate template);
        Task<EmailTemplate> GetTemplateByIdAsync(int id, string templateName);
    }
}
