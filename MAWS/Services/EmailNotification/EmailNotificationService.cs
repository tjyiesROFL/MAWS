using System;
using System.Threading.Tasks;
using System.Net.Mail;


namespace MAWS.Services.EmailNotification
{
    public class EmailNotificationService : IEmailNotification
    {
        private readonly string _testLocation = "/Users/kavichapagain/Projects/MAWS";

        public EmailNotificationService()
        {
        }

        public async Task<string> SendEmail(Message message)
        {
            try {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = new MailAddress(message.From);
                mailMessage.To.Add(message.To);
                mailMessage.Body = message.Body;

                //add CC BCC

                SmtpClient smtp = new SmtpClient();
                smtp.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                smtp.PickupDirectoryLocation = $@"{_testLocation}";
                await smtp.SendMailAsync(mailMessage);
                return "Email Sent";
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }
        
    }
}
