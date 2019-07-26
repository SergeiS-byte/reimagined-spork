using System;
using System.IO;
using InterfaceDLL;
using MSQLDLL;
using SiteDLL;
using UserStructureDLL;
using SendDLL;
using System.Configuration;
using System.Collections.Generic;

namespace TestTask
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                StartupFoldersConfigSection section = (StartupFoldersConfigSection)ConfigurationManager.GetSection("StartupFolders");

                if (args.Length == 0)
                {                    
                    Console.WriteLine("Приложение было запущено без параметра");
                    //SetSettings.Settings();//UserStructure - убрать
                    //Console.WriteLine("Параметры файла конфигурации");
                    List<string> SendingData = new List<string>();

                    SendingData[0] = section.FolderItems[2].Path;
                    SendingData[1] = section.FolderItems[3].Path;
                    SendingData[2] = section.FolderItems[4].Path;
                    SendingData[3] = section.FolderItems[5].Path;

                    ConfigSettings.ReadAllSettings();
                    //Console.WriteLine();

                    Console.WriteLine("Вы хотите изменить параметры запуска?\nВведите: 1-да, 2-нет");
                    string param = Convert.ToString(Console.ReadLine());
                    if (param == "1")
                    {
                        //ConfigSettings.ChangeSettings();
                        new Summon().call(new SiteCeator(), section.FolderItems[0].Path);    //Sites.CheckAvailability();
                        new Summon().call(new MSQLCreator(), section.FolderItems[1].Path);   //MsSQL.CheckAvailability();

                        SendigToEmail.SendMessage("SiteFile.json", "FileSQLServer.json", SendingData);
                    }
                    else if (param == "2")
                    {
                        new Summon().call(new SiteCeator(), section.FolderItems[0].Path);    //Sites.CheckAvailability();
                        new Summon().call(new MSQLCreator(), section.FolderItems[1].Path);   //MsSQL.CheckAvailability();

                        SendigToEmail.SendMessage("SiteFile.json", "FileSQLServer.json", SendingData);
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
