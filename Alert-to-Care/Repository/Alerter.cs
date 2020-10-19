
using System.Net;
using System.Net.Mail;

namespace Alert_to_Care.Repository
{
    public interface Alerter
    {
        bool Alert(string message);
    }
    public class EmailAlert : Alerter
    {
        public bool Alert(string message)
        {
          
            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new NetworkCredential("toofy136gotit@gmail.com", "toofy@1234"),
                EnableSsl = true,           

        };
            smtpClient.Send("toofy136gotit@gmail.com", "toofyrece@gmail.com", "ALERT", message);
            return true;
        }
    }
}
