using System.Collections.Generic;

namespace UniverseSso.Email.Implementation
{
    public class EmailNotification
    {
        public string SentFrom { get; set; }
        public string SentTo { get; set; }
        public string SentCc { get; set; }
        public string SentBcc { get; set; }
        public string EmailSubject { get; set; }
        public string EmailMessage { get; set; }
        public List<EmailAttachment> EmailAttachments { get; set; }
        public bool IsBodyHtml { get; set; }
    }
}