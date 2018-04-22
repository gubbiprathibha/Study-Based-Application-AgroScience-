using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;

namespace EmailServiceLibrary
{
    public class EmailService 
    {
        public bool SendEmail(Mail email)
        {
            MailMessage msg = new MailMessage(email.FromAddress, email.ToAddress);
            msg.Body = email.Body;
            msg.Subject = email.Subject;
            msg.IsBodyHtml = false;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;
            System.Net.NetworkCredential nwCredential = new System.Net.NetworkCredential();
            nwCredential.UserName = "StudyBasedApplication@gmail.com";
            nwCredential.Password = "lkspplkspp";
            smtp.UseDefaultCredentials = true;
            smtp.Credentials = nwCredential;
            try
            {
                smtp.Send(msg);
                return true;
            }
            catch
            {
                return false;
            }


        }
    }
}
