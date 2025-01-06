using Abp.Domain.Entities.Auditing;
using CrudAppProject.Authorization.Users;
using CrudAppProject.Products;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudAppProject.ProductReviews
{
    public class ProductReview: FullAuditedEntity<long>
    {
        [ForeignKey("ProductId")]
        public long ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int? Rating { get; set; }
        public string Review { get; set; }
        public string ? UserName { get; set; }

        [ForeignKey("UserId")]
        public long UserId { get; set; }
        public virtual User User { get; set; }


    }
}
