using Unity;

namespace InterfaceDLL
{
    abstract public class CheckAvailability
    {
        public abstract IAvailable Check_Object(string data, UnityContainer container);

        public void CheckOperation(string data, UnityContainer container)
        {
            var check = Check_Object(data, container);
            check.CheckAvailability();
        }
    }

    public interface IAvailable 
    {
        void CheckAvailability();
    }

    public class Summon
    {
        public void call(CheckAvailability check, string data, UnityContainer container)
        {
            check.CheckOperation(data, container);
        }
    }
}
