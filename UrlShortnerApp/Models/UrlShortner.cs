using System;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;

namespace UrlShortnerApp.Models
{
    public class UrlShortner
    {

        public static void  SendMail(string mail)
        {
            MailMessage message = new MailMessage();
            SmtpClient smtpClient = new SmtpClient();

            smtpClient.UseDefaultCredentials = true;
            smtpClient.Credentials = new System.Net.NetworkCredential("koustajyonetim@gmail.com", "ilknuarpdakicwpg");
            smtpClient.Port = 587;
            smtpClient.Host = "smtp.gmail.com";
          
            smtpClient.EnableSsl = true;
            message.To.Add(mail);
            message.From = new MailAddress("koustajyonetim@gmail.com");
            message.Subject = "Url Shortner Hesap Bilgilendirmesi";
            message.Body = "Tebrikler !.Hesabınız ücretsiz bir şekilde oluşturuldu.Bütün hizmetlerimizden ücretsiz olarak yararlanabilirsiniz";
            smtpClient.Send(message);
        }

        private const string Alphabet = "23456789bcdfghjkmnpqrstvwxyzBCDFGHJKLMNPQRSTVWXYZ-_";
        private static readonly int Base = Alphabet.Length;

        public static string Encode(int num, string customUrl)
        {
            var sb = new StringBuilder();
            while (num > 0)
            {
                sb.Insert(0, Alphabet.ElementAt(num % Base));
                num = num / Base;
            }
            return sb.ToString() + customUrl;
        }

        public static int Decode(string str)
        {
            var num = 0;
            for (var i = 0; i < str.Length; i++)
            {
                num = num * Base + Alphabet.IndexOf(str.ElementAt(i));
            }
            return num;
        }

        public static string GetMd5(string password)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(password));

            byte[] result = md5.Hash;

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                builder.Append(result[i].ToString("x2"));
            }

            return builder.ToString();
        }
    }
}
