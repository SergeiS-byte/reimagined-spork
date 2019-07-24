using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace TestTask
{
    //***

    class ConfigSettings
    {
        public static void ReadAllSettings()
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;

                if (appSettings.Count == 0)
                {
                    Console.WriteLine("AppSettings is empty.");
                }
                else
                {
                    foreach (var key in appSettings.AllKeys)
                    {
                        Console.WriteLine("Key: {0} Value: {1}", key, appSettings[key]);
                    }
                }
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error reading app settings");
            }
        }

        static void AddUpdateAppSettings(string key, string value)
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                if (settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException)
            {
                Console.WriteLine("Error writing app settings");
            }
        }

        public static void ChangeSettings()
        {
            while (true)
            {
                Console.WriteLine("Введите параметр key, который хотите изменить или введите новый, если хотите его создать");
                string key = Convert.ToString(Console.ReadLine());

                Console.WriteLine("Введите параметр value");
                string value = Convert.ToString(Console.ReadLine());

                AddUpdateAppSettings(key, value);

                Console.WriteLine("Хотите изменить еще один параметр?\nВведите: 1 - да, 2 - нет");
                string param = Convert.ToString(Console.ReadLine());
                if (param == "2")
                {
                    Console.WriteLine("Параметры изменены\n");
                    break;
                }
                else if (param == "1") Console.WriteLine("Параметры изменены\nСледующий параметр: ");
                else Console.WriteLine("Введено неверное значение");
            }
        }
    }
}
