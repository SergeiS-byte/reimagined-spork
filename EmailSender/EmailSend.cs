using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using UserStructure;

namespace EmailSender
{
    class EmailSend
    {
    }

    public class SendigToEmail
    {
        //отправка файлов с данными на почту
        public static void SendMessage(string SiteFile, string SQLFile)
        {
            try
            {
                StartupFoldersConfigSection section = (StartupFoldersConfigSection)ConfigurationManager.GetSection("StartupFolders");

                string SenderEmail = (section.FolderItems[2].Path);//ConfigurationManager.AppSettings["SenderEmail"];   //Convert.ToString(Console.ReadLine());
                Console.WriteLine("email с которго будет отправлен результат проверки: " + SenderEmail);

                string SenderName = (section.FolderItems[3].Path);//ConfigurationManager.AppSettings["SenderName"];
                Console.WriteLine("имя, которым будет подписано письмо: " + SenderName);

                MailAddress from = new MailAddress(SenderEmail, SenderName);

                string Receiver = (section.FolderItems[4].Path);//ConfigurationManager.AppSettings["Receiver"];
                Console.WriteLine("email, которому будет отправлено письмо: " + Receiver);

                MailAddress to = new MailAddress(Receiver);

                MailMessage message = new MailMessage(from, to);
                message.IsBodyHtml = true;

                message.Attachments.Add(new Attachment(SiteFile));
                message.Attachments.Add(new Attachment(SQLFile));

                string smtpServ = (section.FolderItems[5].Path);//ConfigurationManager.AppSettings["smtpServ"];
                Console.WriteLine("smtp Server, с которого будет произведена отправка: " + smtpServ);

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
            catch (Exception ex)
            {
                Console.WriteLine("Сообщение не было отправлено " + ex);
            }
        }
    }
}
