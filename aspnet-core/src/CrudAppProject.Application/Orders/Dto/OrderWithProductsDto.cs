using CrudAppProject.Products.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudAppProject.Orders.Dto
{
    public class OrderWithProductsDto
    {
        public long OrderId { get; set; } // The ID of the order
        public List<ProductDetailsDto> Products { get; set; }
    }
    public class ProductDetailsDto
    {
        public long ProductId { get; set; } // The ID of the product
        public string ProductName { get; set; } // The name of the product
        public string ProductDescription { get; set; }
        public string ProductThumbnail { get; set; }
    }
}
