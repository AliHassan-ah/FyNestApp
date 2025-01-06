using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudAppProject.BackgroundJobs.EmailJobs
{
    public class OrderEmailJobArgs
    {
        public string CustomerEmail { get; set; }
        public string OrderNumber { get; set; }
        public DateTime OrderDate { get; set; }
        public int TenantId { get; set; }
    }
}
