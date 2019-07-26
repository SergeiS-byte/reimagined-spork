using System;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using InterfaceDLL;
using Newtonsoft.Json;
using UserStructureDLL;

namespace MSQLDLL
{
    public class MSQLHandler
    {

    }

    public class MSQLCreator : CheckAvailability
    {
        public override IAvailable Check_Object()
        {
            return new MsSQL();
        }
    }

    class SQLServerData
    {
        public DateTime date;
        public string Server;
        public string Version;
    }

    public class MsSQL : IAvailable
    {
        //проверка доступности SQL Server
        public void CheckAvailability()
        {
            SQLServerData serverData = new SQLServerData();
            serverData.date = new DateTime();

            while (true)
            {
                try
                {
                    //StartupFoldersConfigSection section = (StartupFoldersConfigSection)ConfigurationManager.GetSection("StartupFolders");

                    string ServerName = "DESKTOP-CODCI6J";//(section.FolderItems[1].Path);//ConfigurationManager.AppSettings["ServerName"]; //Convert.ToString(Console.ReadLine());
                    Console.WriteLine("\nИмя SQL сервера, к которому вы подключаетесь {0}", ServerName);

                    if (ServerName != null)
                    {
                        string sConnStr = new SqlConnectionStringBuilder
                        {
                            DataSource = @"" + ServerName,
                            //InitialCatalog = DBName,
                            IntegratedSecurity = true,
                        }.ConnectionString;

                        //как индикатор того, что подключение к серверу установлено здесь выводится версия сервера, в моем случае это 14.00.1000

                        using (var sConn = new SqlConnection(sConnStr))
                        {
                            sConn.Open();
                            Console.WriteLine("Версия сервера: " + Convert.ToString(sConn.ServerVersion));
                            Console.WriteLine("Подключение к SQL Server установлено, сервер доступен");

                            serverData.date = DateTime.Now;
                            serverData.Server = sConn.DataSource;
                            serverData.Version = sConn.ServerVersion;

                            string json = JsonConvert.SerializeObject(serverData);
                            File.WriteAllText("FileSQLServer.json", "");
                            File.AppendAllText("FileSQLServer.json", json);

                            break;
                        }
                    }
                    else
                        Console.WriteLine("Имя не введено");
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Введено неверное имя сервера или введённый вами сервер недоступен \n" + ex);
                    break;
                }
            }
        }

        public static void DessirializeData(string data)
        {
            SQLServerData SQLData = JsonConvert.DeserializeObject<SQLServerData>(data);
            Console.WriteLine("date " + SQLData.date + " Server " + SQLData.Server + " Version " + SQLData.Version);
        }
    }
}
