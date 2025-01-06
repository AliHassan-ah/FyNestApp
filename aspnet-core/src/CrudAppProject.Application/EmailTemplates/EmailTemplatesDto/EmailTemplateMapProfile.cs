using AutoMapper;
using CrudAppProject.EmailSender.EmailSenderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudAppProject.EmailTemplates.EmailTemplatesDto
{
    public class EmailTemplateMapProfile : Profile
    {
        public EmailTemplateMapProfile()
        {
            CreateMap<EmailTemplate, EmailTemplateDto>();
            CreateMap<EmailTemplateDto, EmailTemplate>();

        }
    }
}
