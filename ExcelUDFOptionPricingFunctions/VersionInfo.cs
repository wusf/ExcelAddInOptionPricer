using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelDna.Integration;
using OPLib = OptionPricingLib;

namespace ExcelUDFOptionPricingFunctions
{
    public class VersionInfo
    {
        public static object GetVersion()
        {
            string ver = "v 2017-15-05";
            return ver;
        }
        public static object GetUpdateInfo()
        {
            string info = "1 Add spread option approximation method; 2 Change function names from dnet... to cs...";
            return info;
        }
    }
}
