using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace TESTBNI.Services
{
    public class Verification
    {
        public string EmailV = "your email";
        public string PasswordV = "your password";
        public string VerificationCode;
    }

    public class RandomGenerator
    {
        private Random random = new Random();
        public int GenerateRandom()
        {
            return random.Next(1000, 9999);
        }
    }
    public class MailService
    {
        public void SendEmail(string sendEmail, string theCode)
        {
            MailMessage mail = new MailMessage();
            SmtpClient StmpServer = new SmtpClient();
            mail.To.Add(new MailAddress(sendEmail));
            mail.From = new MailAddress("mambablack0905@gmail.com", "TEST BNI");
            mail.Subject = "VERIFICATION CODE" + DateTime.Now.ToString();
            mail.Body = "Dear User, <br><br>Please sign up using this code to your application :<br><br><b>" + theCode + "</b><br><br> Thank you, <br> BlackMamba";
            mail.IsBodyHtml = true;

            StmpServer.UseDefaultCredentials = false;
            StmpServer.Port = 587;
            StmpServer.Host = "smtp.gmail.com";
            StmpServer.Credentials = new NetworkCredential("mambablack0905@gmail.com", "082111432461");
            StmpServer.EnableSsl = true;
            // SmtpServer.UseDefaultCredentials = false;
            //StmpServer.UseDefaultCredentials = false;
           // StmpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            StmpServer.Send(mail);
        }
    }
}
