using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudAppProject.Categories.Dto
{
    public class CategoryDto:EntityDto<long>
    {
        public int TenantId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public string CategoryThumbnail { get; set; }
        public DateTime CreationTime { get; set; }
    }
    public class PagedResultDto<T>
    {
        public int TotalCount { get; set; }
        public List<T> Items { get; set; }
    }
}
