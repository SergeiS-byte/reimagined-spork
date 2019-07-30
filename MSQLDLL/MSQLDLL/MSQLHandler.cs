using System;
using System.Data.SqlClient;
using System.IO;
using InterfaceDLL;
using Newtonsoft.Json;
using RemoveWriteLines;
using Unity;

namespace MSQLDLL
{
    public class MSQLHandler
    {

    }

    public class MSQLCreator : CheckAvailability
    {
        public override IAvailable Check_Object(string Data, UnityContainer container)
        {
            return new MsSQL(Data, container);
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
        private string processingData;
        private string ServData;
        UnityContainer _container;

        public MsSQL(string data, UnityContainer container)
        {
            processingData = data;
            _container = container;
        }

        public string MSQlRequest
        {
            get { return ServData; }
        }

        //проверка доступности SQL Server
        public void CheckAvailability()
        {
            SQLServerData serverData = new SQLServerData();
            serverData.date = new DateTime();

            while (true)
            {
                try
                {

                    string ServerName = processingData;//(section.FolderItems[1].Path);//ConfigurationManager.AppSettings["ServerName"]; //Convert.ToString(Console.ReadLine());
                    _container.Resolve<Bootstrapper>().WriteAndGo("\nИмя SQL сервера, к которому вы подключаетесь" + ServerName);

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
                            //_container.Resolve<Bootstrapper>().WriteAndGo("Версия сервера: " + Convert.ToString(sConn.ServerVersion));
                            //_container.container.Resolve<Bootstrapper>().WriteAndGo("Подключение к SQL Server установлено, сервер доступен");
                            ServData = sConn.ServerVersion;

                            serverData.date = DateTime.Now;
                            serverData.Server = sConn.DataSource;
                            serverData.Version = sConn.ServerVersion;

                            string json = JsonConvert.SerializeObject(serverData);
                            File.WriteAllText("FileSQLServer.json", "");
                            File.AppendAllText("FileSQLServer.json", json);

                            break;
                        }
                    }
                    //else
                    //_container.Resolve<Bootstrapper>().WriteAndGo("Имя не введено");
                }

                catch (Exception ex)
                {
                    //_container.Resolve<Bootstrapper>().WriteAndGo("Введено неверное имя сервера или введённый вами сервер недоступен \n" + ex);
                    break;
                }
            }
        }

        public static void DessirializeData(string data, UnityContainer container)
        {
            SQLServerData SQLData = JsonConvert.DeserializeObject<SQLServerData>(data);
            container.Resolve<Bootstrapper>().WriteAndGo("date " + SQLData.date + " Server " + SQLData.Server + " Version " + SQLData.Version);
        }
    }
}
