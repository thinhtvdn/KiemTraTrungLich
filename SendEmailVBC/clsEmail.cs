using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Windows.Forms;

namespace SendEmailVBC
{
    class clsEmail
    {
        public static string guiEmailHTML(string diaChiNhan, string tieude, string noidung)
        {
            string msg = "Đã gửi.";
            try
            {
               SmtpClient client = new SmtpClient();
               client.Port = 587;
               client.Host = "smtp.gmail.com";
               client.EnableSsl = true;
               client.Timeout = 10000;
               client.UseDefaultCredentials = false;
               client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Credentials = new System.Net.NetworkCredential("thinhtranvandanang@gmail.com", "xyfn dgmn myun hrue");
               MailMessage mm = new MailMessage();
                mm.IsBodyHtml = true;
                mm.BodyEncoding = UTF8Encoding.UTF8;
                mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                mm.From = new MailAddress("thinhtranvandanang@gmail.com");
                mm.To.Add(diaChiNhan);
                mm.Subject = tieude;
                mm.Body = noidung;
                client.Send(mm);
            }
            catch (SmtpException ex)
            {
                msg = "Email không thể gửi:";
                msg += ex.Message;
            }
            return msg;
        }



    }
}
