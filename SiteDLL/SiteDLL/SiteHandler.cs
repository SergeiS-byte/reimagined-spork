using InterfaceDLL;
using System;
using System.Net.NetworkInformation;
using Newtonsoft.Json;
using System.IO;
using RemoveWriteLines;
using Unity;

namespace SiteDLL
{
    public class SiteHandler
    {

    }

    public class SiteCeator : CheckAvailability
    {

        public override IAvailable Check_Object(string Data, UnityContainer container)
        {
            return new Sites(Data, container);
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
        private string processingData;
        UnityContainer _container;

        public string SitePingData
        {
            get { return SiteID; }
        }

        public Sites(string data, UnityContainer container)
        {
            processingData = data;

            Ping ping = new Ping();
            PingReply pingReply = ping.Send(data);
            SiteID = pingReply.Address.ToString();

            _container = container;
        }        

        public void CheckAvailability()
        {
            SiteData data = new SiteData();
            data.date = new DateTime();

            Ping ping = new Ping();

            //while (true)
            //{
            //section = (StartupFoldersConfigSection)ConfigurationManager.GetSection("StartupFolders");

            //string siteName = ConfigurationManager.AppSettings["siteName"];//Convert.ToString(Console.ReadLine());
            //Console.WriteLine("section"+section.FolderItems[0].Path);
            try
            {
                PingReply pingReply = ping.Send((processingData));

                if (pingReply != null)
                {
                    data.date = DateTime.Now;
                    data.Address = pingReply.Address.ToString();
                    data.Status = pingReply.Status.ToString();

                    string json = JsonConvert.SerializeObject(data);
                    File.WriteAllText("SiteFile.json", "");
                    File.AppendAllText("SiteFile.json", json);
                }
            }
            catch 
            {
                //Недоступно - www.euroset.ru
                //Доступно - www.yandex.ru
                _container.Resolve<Bootstrapper>().WriteAndGo("Введено неверное имя сайта или введенный сайт недоступен. Введите корректное имя сайта");
            }
            //}
        }

        public static void DessirializeData(string data, UnityContainer container)
        {
            SiteData siteData = JsonConvert.DeserializeObject<SiteData>(data);
            container.Resolve<Bootstrapper>().WriteAndGo("date " + siteData.date + " Address " + siteData.Address + " Status " + siteData.Status);
        }
    }
}
