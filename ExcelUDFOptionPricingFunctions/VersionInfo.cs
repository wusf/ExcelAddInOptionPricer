using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelDna.Integration;
using OPLib = OptionPricingLib;

namespace CsOptionPricerAddIn
{
    public class VersionInfo
    {
        public static object GetVersion()
        {
            string ver = "v 2017-12-4";
            return ver;
        }
        public static object GetUpdateInfo()
        {
            string info = "1 Add spread option approx; 2 Change function name prefix; 3 Change package name";
            return info;
        }

    }
}
