using System;
using System.IO;
using InterfaceDLL;
using MSQLDLL;
using SiteDLL;
using UserStructureDLL;
using SendDLL;
using System.Configuration;
using System.Collections.Generic;
using Unity;
using RemoveWriteLines;

namespace TestTask
{
    class Program
    {
        static void Main(string[] args)
        {
            unityData Udata = new unityData();

            Udata.SetContainer();

            try
            {
                StartupFoldersConfigSection section = (StartupFoldersConfigSection)ConfigurationManager.GetSection("StartupFolders");

                if (args.Length == 0)
                {
                    unityData.container.Resolve<Bootstrapper>().WriteAndGo("Приложение было запущено без параметра");

                    SetSettings.Settings(unityData.container);//UserStructure - убрать
                    //Console.WriteLine("Параметры файла конфигурации");
                    List<string> SendingData = new List<string>();

                    SendingData.Add(section.FolderItems[2].Path);
                    SendingData.Add(section.FolderItems[3].Path);
                    SendingData.Add(section.FolderItems[4].Path);
                    SendingData.Add(section.FolderItems[5].Path);

                    //ConfigSettings.ReadAllSettings();
                    //Console.WriteLine();

                    unityData.container.Resolve<Bootstrapper>().WriteAndGo("Вы хотите изменить параметры запуска?\nВведите: да/нет");
                    string param = Convert.ToString(unityData.container.Resolve<ReplaceConsole>().ReadD());
                    if (param == "да")
                    {
                        //ConfigSettings.ChangeSettings();
                        new Summon().call(new SiteCeator(), section.FolderItems[0].Path);    //Sites.CheckAvailability();
                        new Summon().call(new MSQLCreator(), section.FolderItems[1].Path);   //MsSQL.CheckAvailability();

                        SendigToEmail.SendMessage("SiteFile.json", "FileSQLServer.json", SendingData);
                    }
                    else if (param == "нет")
                    {
                        new Summon().call(new SiteCeator(), section.FolderItems[0].Path);    //Sites.CheckAvailability();
                        new Summon().call(new MSQLCreator(), section.FolderItems[1].Path);   //MsSQL.CheckAvailability();

                        SendigToEmail.SendMessage("SiteFile.json", "FileSQLServer.json", SendingData);
                    }
                    else unityData.container.Resolve<Bootstrapper>().WriteAndGo("Должно быть введено 1 или 2");
                }
                else
                {
                    unityData.container.Resolve<Bootstrapper>().WriteAndGo("Приложение было запущено с параметром");
                    unityData.container.Resolve<Bootstrapper>().WriteAndGo("Данные последней проверки");

                    Sites.DessirializeData(File.ReadAllText("SiteFile.json"));
                    MsSQL.DessirializeData(File.ReadAllText("FileSQLServer.json"));
                }
            }
            catch 
            {
                unityData.container.Resolve<Bootstrapper>().WriteAndGo("Программа не сработала");
            }
        }
    }
}
