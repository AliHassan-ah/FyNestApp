using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudAppProject.EmailSender.EmailSenderEntities
{
    public class QueuedEmail : FullAuditedEntity<long>,IMayHaveTenant
    {
        public int ? TenantId { get; set; }
        public string ? ToEmail { get; set; }
        public string ? Subject { get; set; }
        public string ? Body { get; set; }
        public bool ? IsSent { get; set; }
        public int ? RetryCount { get; set; }
        public string ? ErrorMessage { get; set; }

    }
}
