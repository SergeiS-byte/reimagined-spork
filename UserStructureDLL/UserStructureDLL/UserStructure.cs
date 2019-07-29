using System;
using System.Configuration;
using Unity;
using RemoveWriteLines;

namespace UserStructureDLL
{
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

        [ConfigurationProperty("path", DefaultValue = "", IsKey = /*true*/false, IsRequired = /*true*/false)]
        public string Path
        {
            get { return ((string)(base["path"])); }
            set { base["path"] = value; }
        }
    }

    public class SetSettings
    {
        private static void Show(int num, UnityContainer container)
        {
            
            Configuration cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            StartupFoldersConfigSection section = (StartupFoldersConfigSection)cfg.Sections["StartupFolders"]; //не может загрузить из конфига

            if (section != null)
            {
                container.Resolve<Bootstrapper>().WriteAndGo(section.FolderItems[num].FolderType + ": ");
                container.Resolve<Bootstrapper>().WriteAndGo(section.FolderItems[num].Path);

                //cfg.Save(); 
            }
        }

        private static void Set(int num, UnityContainer container)
        {
            Configuration cfg = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            StartupFoldersConfigSection section = (StartupFoldersConfigSection)cfg.Sections["StartupFolders"];

            if (section != null)
            {
                container.Resolve<Bootstrapper>().WriteAndGo("Старое значение ");

                Show(num, container);

                container.Resolve<Bootstrapper>().WriteAndGo("Введите новое значение ");
                section.FolderItems[num].Path = Convert.ToString(container.Resolve<ReplaceConsole>().ReadD());

                cfg.Save(); //устанавливает перенос на новую строку и производит проверку <exename>.vshost.exe.config файла в вашей отладочной папке.
            }
        }

        public static void Settings(UnityContainer container)
        {

            container.Resolve<Bootstrapper>().WriteAndGo("Для изменения в файле конфигурации доступны следующие параметры:\n\n" +
                "1. siteName - имя сайта для парса,\n" +
                "2. ServerName - имя сервера MSSQL,\n" +
                "3. SenderEmail - почта, с которой будет отправлено письмо,\n" +
                "4. SenderName - имя, которым будет подписано письмо,\n" +
                "5. Receiver - почта-получатель,\n" +
                "6. smtpServ - smtp-Server отправителя\n");

            while (true)
            {
                container.Resolve<Bootstrapper>().WriteAndGo("Вы хотите изменить какой-либо параметр? да/нет\nПри выборе 'нет' будут выведены текущие настройки");
                string param = Convert.ToString(container.Resolve<ReplaceConsole>().ReadD());

                if (param == "да")
                {
                    container.Resolve<Bootstrapper>().WriteAndGo("Какой параметр вы хотите изменить? 1-6");
                    int ConfigNum = Convert.ToInt16(container.Resolve<ReplaceConsole>().ReadD());
                    if ((ConfigNum <= 6) && (ConfigNum >= 1)) Set(ConfigNum - 1, container);
                    else container.Resolve<Bootstrapper>().WriteAndGo("Введено неправильное значение, нужно ввести номер нужного параметра (1-6)");
                }
                else if (param == "нет")
                {
                    for (int j = 0; j < 6; j++)
                    {
                        Show(j, container);
                    }
                    break;
                }
                else container.Resolve<Bootstrapper>().WriteAndGo("Введите да или нет");
            }
        }
    }

    //***
}
