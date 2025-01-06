using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudAppProject.EmailSender.EmailSenderEntities
{
    public class EmailTemplate : FullAuditedEntity<long>, IMayHaveTenant
    {
        public int ? TenantId { get; set; }
        public string ? Name { get; set; }
        public string  ? Subject { get; set; }
        public string ? Content { get; set; }
        public string  ? Token { get; set; }
    }
}
