using System.Configuration;
using System.Collections.Specialized;
using Newtonsoft.Json;
using System;
using System.IO;

namespace TestTask
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                //ExeConfigurationFileMap configMap = new ExeConfigurationFileMap();
                //configMap.ExeConfigFilename = @"App1.config";
                //Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configMap, ConfigurationUserLevel.None);

                if (args.Length == 0)
                {
                    Console.WriteLine("Приложение было запущено без параметра");
                    new Summon().call(new SiteCeator());    //Sites.CheckAvailability();
                    new Summon().call(new MSQLCreator());   //MsSQL.CheckAvailability();

                    SendigToEmail.SendMessage("SiteFile.json", "FileSQLServer.json");
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

    //
    abstract class CheckAvailability
    {
        public abstract IAvailable Check_Object();

        public void CheckOperation()
        {
            var check = Check_Object();
            check.CheckAvailability();
        }
    }

    class SiteCeator : CheckAvailability
    {
        public override IAvailable Check_Object()
        {
            return new Sites();
        }
    }

    class MSQLCreator : CheckAvailability
    {
        public override IAvailable Check_Object()
        {
            return new MsSQL();
        }
    }

    public interface IAvailable
    {
        void CheckAvailability();
    }

    class Summon
    {
        public void call(CheckAvailability check)
        {
            check.CheckOperation();
        }
    }
}
