    using Abp.Application.Services.Dto;
    using CrudAppProject.Images;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace CrudAppProject.Products.Dto
    {
        public class ProductDto: EntityDto<long>
        {
            public int TenantId { get; set; }
            public string ProductName { get; set; }
            public string ProductDescription { get; set; }
            public string ProductThumbnail { get; set; }
            public ICollection<Image> Images { get; set; }

        }
    public class PagedResultDto<T>
    {
        public int TotalCount { get; set; }
        public List<T> Items { get; set; }
    }
}
