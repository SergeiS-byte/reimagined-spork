using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserStructure
{
    class CustomStructure
    {
    }

    //Этот объект вернет нам пользовательскую секцию.
    public class StartupFoldersConfigSection : ConfigurationSection
    {
        [ConfigurationProperty("Folders")]
        public FoldersCollection FolderItems
        {
            get { return ((FoldersCollection)(base["Folders"])); }
        }
    }

    //Это собственно коллекция элементов, которые мы определим в пользовательской секции.
    [ConfigurationCollection(typeof(FolderElement))]
    public class FoldersCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new FolderElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((FolderElement)(element)).FolderType;
        }

        public FolderElement this[int idx]
        {
            get { return (FolderElement)BaseGet(idx); }
        }
    }

    //Это сам элемент, описывающий какую-от определенную вами сущность.
    public class FolderElement : ConfigurationElement
    {

        [ConfigurationProperty("folderType", DefaultValue = "", IsKey = true, IsRequired = true)]
        public string FolderType
        {
            get { return ((string)(base["folderType"])); }
            set { base["folderType"] = value; }
        }

        [ConfigurationProperty("path", DefaultValue = "", IsKey = true/*false*/, IsRequired = true/*false*/)]
        public string Path
        {
            get { return ((string)(base["path"])); }
            set { base["path"] = value; }
        }
    }

    public class SetSettings
    {
        private static void Show(int num)
        {
            Configuration cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            StartupFoldersConfigSection section = (StartupFoldersConfigSection)cfg.Sections["StartupFolders"];

            if (section != null)
            {
                Console.Write(section.FolderItems[num].FolderType + ": ");
                Console.WriteLine(section.FolderItems[num].Path);

                //cfg.Save(); 
            }
        }

        private static void Set(int num)
        {
            Configuration cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            StartupFoldersConfigSection section = (StartupFoldersConfigSection)cfg.Sections["StartupFolders"];

            if (section != null)
            {
                Console.WriteLine("Старое значение ");

                Show(num);

                Console.WriteLine("Введите новое значение ");
                section.FolderItems[num].Path = Convert.ToString(Console.ReadLine());

                cfg.Save(); //устанавливает перенос на новую строку и производит проверку <exename>.vshost.exe.config файла в вашей отладочной папке.
            }
        }

        public static void Settings()
        {

            Console.WriteLine("Для изменения в файле конфигурации доступны следующие параметры:\n\n" +
                "1. siteName - имя сайта для парса,\n" +
                "2. ServerName - имя сервера MSSQL,\n" +
                "3. SenderEmail - почта, с которой будет отправлено письмо,\n" +
                "4. SenderName - имя, которым будет подписано письмо,\n" +
                "5. Receiver - почта-получатель,\n" +
                "6. smtpServ - smtp-Server отправителя\n");

            while (true)
            {
                Console.WriteLine("Вы хотите изменить какой-либо параметр? да/нет\nПри выборе 'нет' будут выведены текущие настройки");
                string param = Convert.ToString(Console.ReadLine());

                if (param == "да")
                {
                    Console.WriteLine("Какой параметр вы хотите изменить? 1-6");
                    int ConfigNum = Convert.ToInt16(Console.ReadLine());
                    if ((ConfigNum <= 6) && (ConfigNum >= 1)) Set(ConfigNum - 1);
                    else Console.WriteLine("Введено неправильное значение, нужно ввести номер нужного параметра (1-6)");
                }
                else if (param == "нет")
                {
                    for (int j = 0; j < 6; j++)
                    {
                        Show(j);
                    }
                    break;
                }
                else Console.WriteLine("Введите да или нет");
            }
        }
    }

    //***
}
