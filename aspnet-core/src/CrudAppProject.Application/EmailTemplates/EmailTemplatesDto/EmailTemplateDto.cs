using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudAppProject.EmailTemplates.EmailTemplatesDto
{
    public class EmailTemplateDto : EntityDto<long>
    {
        public int? TenantId { get; set; }
        public string? Name { get; set; }
        public string? Subject { get; set; }
        public string? Content { get; set; }
        public string? Token { get; set; }
    }
}
