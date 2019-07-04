using System;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using Microsoft.SqlServer.Management.Smo;
using Newtonsoft.Json;
using System.Net.Mail;

namespace TestTask
{
    class SQLServerData
    {
        public DateTime date;
        public string Server;
        public string Version;
    }

    class MsSQL
    {      
        //проверка доступности SQL Server
        public static void SQLConnection()
        {
            SQLServerData serverData = new SQLServerData();
            serverData.date = new DateTime();

            while (true)
            {
                try
                {                   
                    Console.WriteLine("\nВведите имя SQL сервера, к которому хотите подключиться, например DESKTOP-CODCI6J");
                    string ServerName = Convert.ToString(Console.ReadLine());

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

                catch
                {
                    Console.WriteLine("Введено неверное имя сервера или введённый вами сервер недоступен");
                }
            }
        }

        public static void DessirializeSQLData(string data)
        {
            SQLServerData SQLData = JsonConvert.DeserializeObject<SQLServerData>(data);
            Console.WriteLine("date " + SQLData.date + " Server " + SQLData.Server + " Version " + SQLData.Version);
        }
    }
}
