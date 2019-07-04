using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Mail;
using System.Net;

namespace TestTask
{
    class SendigToEmail
    {
        //отправка файлов с данными на почту
        public static void SendMessage(string SiteFile, string SQLFile)
        {
            try
            {
                Console.WriteLine("Введите email с которго будет отправлен результат проверки, например somemail@gmail.com");
                string SenderEmail = Convert.ToString(Console.ReadLine());

                Console.WriteLine("Введите имя, которым будет подписано письмо");
                string SenderName = Convert.ToString(Console.ReadLine());

                MailAddress from = new MailAddress(SenderEmail, SenderName);

                Console.WriteLine("Введите email, которому будет отправлено письмо");
                string Receiver = Convert.ToString(Console.ReadLine());
                MailAddress to = new MailAddress(Receiver);

                MailMessage message = new MailMessage(from, to);
                message.IsBodyHtml = true;

                message.Attachments.Add(new Attachment(SiteFile));
                message.Attachments.Add(new Attachment(SQLFile));

                Console.WriteLine("Введите smtp Server, с которого будет произведена отправка, например smtp.mail.ru");
                string smtpServ = Convert.ToString(Console.ReadLine());

                SmtpClient smtp = new SmtpClient(smtpServ, 587);
                Console.WriteLine("Введите пароль от почты");

                //string password = Convert.ToString(Console.ReadLine());
                string password = "";
                while (true)
                {
                    var key = Console.ReadKey(true);

                    if (key.Key == ConsoleKey.Enter) break; 

                    Console.Write("*");
                    password += key.KeyChar;
                }
                Console.WriteLine();

                smtp.Credentials = new NetworkCredential(SenderEmail, password);
                smtp.EnableSsl = true;
                smtp.Send(message);

            }
            catch //(Exception ex)
            {
                Console.WriteLine("Сообщение не было отправлено ");
            }
        }
    }
}
