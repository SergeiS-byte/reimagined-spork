using System.Configuration;
using System.Collections.Specialized;
using Newtonsoft.Json;
using System;
using System.IO;
using InterfacePr;
using SitePr;
using MSqlPr;
using Unity;
using Microsoft.Practices.Unity.Configuration;

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
                    SetSettings.Settings();
                    //Console.WriteLine("Параметры файла конфигурации");
                    ConfigSettings.ReadAllSettings();
                    //Console.WriteLine();

                    Console.WriteLine("Вы хотите изменить параметры запуска?\nВведите: 1-да, 2-нет");
                    string param = Convert.ToString(Console.ReadLine());
                    if (param == "1")
                    {
                        //ConfigSettings.ChangeSettings();
                        new Summon().call(new SiteCeator());    //Sites.CheckAvailability();
                        new Summon().call(new MSQLCreator());   //MsSQL.CheckAvailability();

                        SendigToEmail.SendMessage("SiteFile.json", "FileSQLServer.json");
                    }
                    else if (param == "2")
                    {
                        new Summon().call(new SiteCeator());    //Sites.CheckAvailability();
                        new Summon().call(new MSQLCreator());   //MsSQL.CheckAvailability();

                        SendigToEmail.SendMessage("SiteFile.json", "FileSQLServer.json");
                    }
                    else Console.WriteLine("Должно быть введено 1 или 2");
                }
                else
                {
                    Console.WriteLine("Приложение было запущено с параметром");
                    Console.WriteLine("Данные последней проверки");

                    Sites.DessirializeData(File.ReadAllText("SiteFile.json"));
                    MsSQL.DessirializeData(File.ReadAllText("FileSQLServer.json"));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
