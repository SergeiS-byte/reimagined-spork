using System.Configuration;
using System.Collections.Specialized;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.NetworkInformation;
using InterfacePr;
using UserStructure;

namespace SitePr
{

    public class SiteCeator : CheckAvailability
    {
        public override IAvailable Check_Object()
        {
            return new Sites();
        }
    }

    //проверка доступности сайта
    class SiteData
    {
        public DateTime date;
        public string Address;
        public string Status;
    }

    public class Sites : IAvailable
    {
        private string SiteID;

        public string SitePingData
        {
            get { return SiteID; }
        }

        public Sites()
        {
            Ping ping = new Ping();
            section = (StartupFoldersConfigSection)ConfigurationManager.GetSection("StartupFolders");
            PingReply pingReply = ping.Send(("www.ya.ru"));
            SiteID = pingReply.Address.ToString();
        }

        StartupFoldersConfigSection section;

        public void CheckAvailability()
        {
            SiteData data = new SiteData();
            data.date = new DateTime();

            Ping ping = new Ping();
            //Console.WriteLine("\nсайта для пинга:");

            //while (true)
            //{
                section = (StartupFoldersConfigSection)ConfigurationManager.GetSection("StartupFolders");

                //string siteName = ConfigurationManager.AppSettings["siteName"];//Convert.ToString(Console.ReadLine());
                //Console.WriteLine("section"+section.FolderItems[0].Path);
                try
                {
                    PingReply pingReply = ping.Send((section.FolderItems[0].Path));

                    if (pingReply != null)
                    {
                        //Console.WriteLine("Address - " + pingReply.Address);
                        //Console.WriteLine("Status - " + pingReply.Status);

                        data.date = DateTime.Now;
                        data.Address = pingReply.Address.ToString();
                        data.Status = pingReply.Status.ToString();

                        string json = JsonConvert.SerializeObject(data);
                        File.WriteAllText("SiteFile.json", "");
                        File.AppendAllText("SiteFile.json", json);

                        //break;
                    }
                }
                catch (Exception ex)
                {
                    //Недоступно - www.euroset.ru
                    //Доступно - www.yandex.ru
                    Console.WriteLine("Введено неверное имя сайта или введенный сайт недоступен. Введите корректное имя сайта" + ex);
                }
            //}
        }

        public static void DessirializeData(string data)
        {
            SiteData siteData = JsonConvert.DeserializeObject<SiteData>(data);
            //Console.WriteLine("date " + siteData.date + " Address " + siteData.Address + " Status " + siteData.Status);
        }
    }
}
