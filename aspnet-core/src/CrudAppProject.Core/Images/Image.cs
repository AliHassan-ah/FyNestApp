using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using CrudAppProject.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudAppProject.Images
{
    public class Image : FullAuditedEntity<long>
    {
        public string ImageUrl { get; set; }

        [ForeignKey("ProductId")]
        public long ProductId {  get; set; }
        public virtual Product Product { get; set; }
    }
}
