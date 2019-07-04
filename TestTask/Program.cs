using Newtonsoft.Json;
using System;
using System.IO;

namespace TestTask
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args.Length == 0)
                {

                    Console.WriteLine("Приложение было запущено без параметра");
                    Sites.PingAddress();
                    MsSQL.SQLConnection();
                    SendigToEmail.SendMessage("SiteFile.json", "FileSQLServer.json");
                }
                else
                {
                    Console.WriteLine("Приложение было запущено с параметром");
                    Console.WriteLine("Данные последней проверки");
                    Sites.DessirializeSiteData(File.ReadAllText("SiteFile.json"));
                    MsSQL.DessirializeSQLData(File.ReadAllText("FileSQLServer.json"));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
