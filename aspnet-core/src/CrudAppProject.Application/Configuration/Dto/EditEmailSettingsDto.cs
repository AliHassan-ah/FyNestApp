using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrudAppProject.Configuration.Dto
{
    public class EditEmailSettingsDto
    {
        public string DefaultSenderEmail { get; set; }
        public string SMTPHost { get; set; }
        public int SMTPPort { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string TestEmail { get; set; }
    }
}
