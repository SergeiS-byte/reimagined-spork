using System;

namespace InterfaceDLL
{
    abstract public class CheckAvailability
    {
        public abstract IAvailable Check_Object();

        public void CheckOperation()
        {
            var check = Check_Object();
            check.CheckAvailability();
        }
    }

    public interface IAvailable
    {
        void CheckAvailability();
    }

    public class Summon
    {
        public void call(CheckAvailability check)
        {
            check.CheckOperation();
        }
    }
}
