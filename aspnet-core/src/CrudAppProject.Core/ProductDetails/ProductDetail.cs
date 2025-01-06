using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using CrudAppProject.Categories;
using CrudAppProject.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudAppProject.ProductDetails
{
    public class ProductDetail : FullAuditedEntity<long>,IMustHaveTenant
    {
        public ProductStatus Status { get; set; }
        public int TenantId { get; set; }
        public long Quantity {  get; set; }
        public string ? BarCode { get; set; }
        public string Price { get; set; }

        [ForeignKey("CategoryId")]
        public long CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [ForeignKey("ProductId")]
        public long ProductId {  get; set; }
        public virtual Product Product { get; set; }
        public int? Rating { get; set; }
    }
    public enum ProductStatus
    {
        Published =1,
        //LowStock =2,
        Draft=2
    }
}
