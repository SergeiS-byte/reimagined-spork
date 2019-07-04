using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net.NetworkInformation;

namespace TestTask
{
    //проверка доступности сайта
    class SiteData
    {
        public DateTime date;
        public string Address;
        public string Status;
    }

    class Sites
    {
        public static void PingAddress()
        {
            SiteData data = new SiteData();
            data.date = new DateTime();

            Ping ping = new Ping();
            Console.WriteLine("\nВведите адрес сайта для пинга, например www.ya.ru");

            while (true)
            {
                string siteName = Convert.ToString(Console.ReadLine());
                try
                {
                    PingReply pingReply = ping.Send(siteName);

                    if (pingReply.Address != null)
                    {
                        Console.WriteLine("Address - " + pingReply.Address);
                        Console.WriteLine("Status - " + pingReply.Status);

                        data.date = DateTime.Now;
                        data.Address = pingReply.Address.ToString();
                        data.Status = pingReply.Status.ToString();

                        string json = JsonConvert.SerializeObject(data);
                        File.WriteAllText("SiteFile.json", "");
                        File.AppendAllText("SiteFile.json", json); 

                        break;
                    }
                }
                catch (Exception ex)
                {
                    //Недоступно - www.euroset.ru
                    //Доступно - www.yandex.ru
                    Console.WriteLine("Введено неверное имя сайта или введенный сайт недоступен. Введите корректное имя сайта");
                }                
            }
        }

        public static void DessirializeSiteData(string data)
        {
            SiteData siteData = JsonConvert.DeserializeObject<SiteData>(data);
            Console.WriteLine("date " + siteData.date + " Address " + siteData.Address + " Status " + siteData.Status);
        }
    }
}
