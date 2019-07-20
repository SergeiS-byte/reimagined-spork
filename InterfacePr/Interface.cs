using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using SitePr;
//using MSqlPr;

namespace InterfacePr
{
    class Interface
    {

    }

    abstract public class CheckAvailability
    {
        public abstract IAvailable Check_Object();

        public void CheckOperation()
        {
            var check = Check_Object();
            check.CheckAvailability();
        }
    }

    //class SiteCeator : CheckAvailability
    //{
    //    public override IAvailable Check_Object()
    //    {
    //        return new Sites();
    //    }
    //}

    //class MSQLCreator : CheckAvailability
    //{
    //    public override IAvailable Check_Object()
    //    {
    //        return new MsSQL();
    //    }
    //}

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
