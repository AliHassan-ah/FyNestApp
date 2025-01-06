using Abp.Application.Services.Dto;


namespace CrudAppProject.QueuedEmails.QueuedEmailsDto
{
    public class QueuedEmailDto : EntityDto<long>
    {
        public int? TenantId { get; set; }
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool? IsSent { get; set; }
        public int? RetryCount { get; set; }
        public string ErrorMessage { get; set; }

    }
}
