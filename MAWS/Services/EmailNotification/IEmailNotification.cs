using System;
using System.Threading.Tasks;

namespace MAWS.Services.EmailNotification
{
    public interface IEmailNotification
    {
        public Task<string> SendEmail(Message message);
    }
}
