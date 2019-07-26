using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using UserStructureDLL;

namespace SendDLL
{
    public class SendHandler
    {
    }

    public class SendigToEmail
    {
        //отправка файлов с данными на почту
        public static void SendMessage(string SiteFile, string SQLFile, List<string> Data)
        {
            try
            {
                Console.WriteLine("Количество " + Data.Count);
                //StartupFoldersConfigSection section = (StartupFoldersConfigSection)ConfigurationManager.GetSection("StartupFolders");

                string SenderEmail = Data[0];//"serega.sann@mail.ru"; // (section.FolderItems[2].Path);//ConfigurationManager.AppSettings["SenderEmail"];   //Convert.ToString(Console.ReadLine());
                Console.WriteLine("email с которго будет отправлен результат проверки: " + SenderEmail);

                string SenderName = Data[1];//"Test"; // (section.FolderItems[3].Path);//ConfigurationManager.AppSettings["SenderName"];
                Console.WriteLine("имя, которым будет подписано письмо: " + SenderName);

                MailAddress from = new MailAddress(SenderEmail, SenderName);

                string Receiver = Data[2];//"serega.sann@mail.ru"; // (section.FolderItems[4].Path);//ConfigurationManager.AppSettings["Receiver"];
                Console.WriteLine("email, которому будет отправлено письмо: " + Receiver);

                MailAddress to = new MailAddress(Receiver);

                MailMessage message = new MailMessage(from, to);
                message.IsBodyHtml = true;

                message.Attachments.Add(new Attachment(SiteFile));
                message.Attachments.Add(new Attachment(SQLFile));

                string smtpServ = Data[3]; //"smtp.mail.ru"; // (section.FolderItems[5].Path);//ConfigurationManager.AppSettings["smtpServ"];
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
