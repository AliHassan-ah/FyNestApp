using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudAppProject.OrderDetails.Dto
{
    public class PagedOrderDetailsResultRequestDto
    {
       
            public string Keyword { get; set; }

            public string Sorting { get; set; }

            public int SkipCount { get; set; }

            public int MaxResultCount { get; set; }
        
    }
}
