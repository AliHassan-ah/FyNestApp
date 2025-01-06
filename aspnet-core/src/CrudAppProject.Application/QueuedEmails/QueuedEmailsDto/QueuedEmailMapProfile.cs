using AutoMapper;
using CrudAppProject.EmailSender.EmailSenderEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudAppProject.QueuedEmails.QueuedEmailsDto
{
    public class QueuedEmailMapProfile : Profile
    {
        public QueuedEmailMapProfile()
        {
            CreateMap<QueuedEmail, QueuedEmailDto>();
            CreateMap<QueuedEmailDto, QueuedEmail>();
        }
    }
}
