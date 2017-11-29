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
            string ver = "v 2017-03-24";
            return ver;
        }
        public static object GetUpdateInfo()
        {
            string info = "Add ImpVol";
            return info;
        }

    }
}
