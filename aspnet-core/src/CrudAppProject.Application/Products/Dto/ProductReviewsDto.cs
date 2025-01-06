using Abp.Application.Services.Dto;
using CrudAppProject.Authorization.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudAppProject.Products.Dto
{
    public class ProductReviewsDto : EntityDto<long>
    {
   
        public long ProductId { get; set; }
        public int? Rating { get; set; }
        public string Review { get; set; }
        public long UserId { get; set; }
        public string UserName { get; set;
        }
    
    }
}
