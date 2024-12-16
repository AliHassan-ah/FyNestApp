using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using CrudAppProject.Images;
using CrudAppProject.ProductDetails;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudAppProject.Products
{
    public class Product : FullAuditedEntity<long>,IMustHaveTenant
    {
        public int TenantId {  get; set; }
        public string ProductName {  get; set; }
        public string ProductDescription { get; set; }
        public string ProductThumbnail {  get; set; }
        public  ICollection<Image> Images { get; set; }
        public virtual ICollection<ProductDetail> ProductDetails { get; set; }

    }
}
