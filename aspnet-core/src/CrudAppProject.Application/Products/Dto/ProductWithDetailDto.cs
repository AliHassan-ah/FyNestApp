using Abp.Application.Services.Dto;
using CrudAppProject.Images;
using CrudAppProject.ProductDetails;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudAppProject.Products.Dto
{
    public class ProductWithDetailDto:EntityDto<long>
    {
        public int TenantId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductThumbnail { get; set; }
        public DateTime CreationTime { get; set; }
        //public ICollection<Image> Images { get; set; }
        public List<string> Images { get; set; } 


        public ProductStatus Status { get; set; }
        public long Quantity { get; set; }
        public string? BarCode { get; set; }

        public int ? Rating { get; set; }
        public string Price { get; set; }
        public long CategoryId { get; set; }

    }
}
