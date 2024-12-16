using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudAppProject.Categories
{
    public class Category : FullAuditedEntity<long>,IMustHaveTenant
    {
        public int TenantId {  get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string CategoryThumbnail {  get; set; }
    }
}
