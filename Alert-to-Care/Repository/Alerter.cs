using System.Net;
using System.Net.Mail;

namespace Alert_to_Care.Repository
{
    public interface IAlerter
    {
        void Alert(string message);
    }

    public class EmailAlert : IAlerter
    {
        public void Alert(string message)
        {
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("toofy136gotit@gmail.com", "toofy@1234"),
                EnableSsl = true
            };
            smtpClient.Send("toofy136gotit@gmail.com", "toofyrece@gmail.com", "ALERT", message);
            
        }
    }
}