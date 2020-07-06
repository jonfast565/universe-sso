using System.Threading;
using UniverseSso.Email.Implementation;

namespace UniverseSso.Email.Interfaces
{
    public interface IEmailSender
    {
        void SendEmail(EmailNotification email, CancellationToken ct);
    }
}