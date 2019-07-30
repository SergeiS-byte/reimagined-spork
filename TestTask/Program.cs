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
                    Udata.container.Resolve<Bootstrapper>().WriteAndGo("Приложение было запущено без параметра");

                    SetSettings.Settings(Udata.container);//UserStructure - убрать
                    //Console.WriteLine("Параметры файла конфигурации");
                    List<string> SendingData = new List<string>();

                    SendingData.Add(section.FolderItems[2].Path);
                    SendingData.Add(section.FolderItems[3].Path);
                    SendingData.Add(section.FolderItems[4].Path);
                    SendingData.Add(section.FolderItems[5].Path);

                    //ConfigSettings.ReadAllSettings();
                    //Console.WriteLine();

                    Udata.container.Resolve<Bootstrapper>().WriteAndGo("Вы хотите изменить параметры запуска?\nВведите: да/нет");
                    string param = Convert.ToString(Udata.container.Resolve<ReplaceConsole>().ReadD());
                    if (param == "да")
                    {
                        //ConfigSettings.ChangeSettings();
                        new Summon().call(new SiteCeator(), section.FolderItems[0].Path, Udata.container);    //Sites.CheckAvailability();
                        new Summon().call(new MSQLCreator(), section.FolderItems[1].Path, Udata.container);   //MsSQL.CheckAvailability();

                        SendigToEmail.SendMessage("SiteFile.json", "FileSQLServer.json", SendingData, Udata.container);
                    }
                    else if (param == "нет")
                    {
                        new Summon().call(new SiteCeator(), section.FolderItems[0].Path, Udata.container);    //Sites.CheckAvailability();
                        new Summon().call(new MSQLCreator(), section.FolderItems[1].Path, Udata.container);   //MsSQL.CheckAvailability();

                        SendigToEmail.SendMessage("SiteFile.json", "FileSQLServer.json", SendingData, Udata.container);
                    }
                    else Udata.container.Resolve<Bootstrapper>().WriteAndGo("Должно быть введено 1 или 2");
                }
                else
                {
                    Udata.container.Resolve<Bootstrapper>().WriteAndGo("Приложение было запущено с параметром");
                    Udata.container.Resolve<Bootstrapper>().WriteAndGo("Данные последней проверки");

                    Sites.DessirializeData(File.ReadAllText("SiteFile.json"), Udata.container);
                    MsSQL.DessirializeData(File.ReadAllText("FileSQLServer.json"), Udata.container);
                }
            }
            catch 
            {
                Udata.container.Resolve<Bootstrapper>().WriteAndGo("Программа не сработала, попробуйте ее перезапустить");
            }
        }
    }
}
