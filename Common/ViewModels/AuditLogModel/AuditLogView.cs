using System;

namespace Common.ViewModels.AuditLogModel
{
    public class AuditLogView
    {
        public long UserId { get; set; }
        public DateTime LoggedDate { get; set; } = DateTime.UtcNow;
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime EndDate { get; set; } = DateTime.UtcNow;
        public string ProcessedTime { get; set; }
        public bool IsSuccess { get; set; }
        public string Url { get; set; }
        public string MethodName { get; set; }
        public string MethodType { get; set; }
        public string Message { get; set; }
        public long ModuleId { get; set; }
    }
}
