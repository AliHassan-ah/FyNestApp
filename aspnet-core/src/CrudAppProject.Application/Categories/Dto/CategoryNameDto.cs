using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudAppProject.Categories.Dto
{
    public  class CategoryNameDto: EntityDto<long>
    {
        public string CategoryName { get; set; }

    }
}
