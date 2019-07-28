namespace InterfaceDLL
{
    abstract public class CheckAvailability
    {
        public abstract IAvailable Check_Object(string data);

        public void CheckOperation(string data)
        {
            var check = Check_Object(data);
            check.CheckAvailability();
        }
    }

    public interface IAvailable 
    {
        void CheckAvailability();
    }

    public class Summon
    {
        public void call(CheckAvailability check, string data)
        {
            check.CheckOperation(data);
        }
    }
}
