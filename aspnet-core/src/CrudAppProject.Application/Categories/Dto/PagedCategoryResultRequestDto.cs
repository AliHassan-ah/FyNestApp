using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudAppProject.Categories.Dto
{
    public class PagedCategoryResultRequestDto : PagedAndSortedResultRequestDto
    {
        public string Keyword { get; set; }

    }
}
