using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelDna.Integration;
using OPLib = OptionPricingLib;

namespace ExcelUDFOptionPricingFunctions
{
    public class BSImpVol
    {
        [ExcelFunction(Description = "Returns ImpVol Bisec Method")]
        public static object CsImpVolBisec(  [ExcelArgument(Name = "CallPutFlag", Description = Flag.VanillaStyle)] string CallPutFlag,
                                               [ExcelArgument(Name = "S", Description = "Spot price")] double S,
                                               [ExcelArgument(Name = "X", Description = "Strike price")] double X,
                                               [ExcelArgument(Name = "T", Description = "Days to expiration")] double T,
                                               [ExcelArgument(Name = "r", Description = "Interest rate")] double r,
                                               [ExcelArgument(Name = "b", Description = "Cost of carry")] double b,
                                               [ExcelArgument(Name = "cm", Description = "option market price")] double cm)
        {
            double result = double.NaN;
            result = OPLib.BSImpVol.BSImpVolBisec(CallPutFlag, S, X, T, r, b, cm);
            return result;
        }

        [ExcelFunction(Description = "Returns ImpVol Newton Ralph")]
        public static object CsImpVolNR([ExcelArgument(Name = "CallPutFlag", Description = Flag.VanillaStyle)] string CallPutFlag,
                                       [ExcelArgument(Name = "S", Description = "Spot price")] double S,
                                       [ExcelArgument(Name = "X", Description = "Strike price")] double X,
                                       [ExcelArgument(Name = "T", Description = "Days to expiration")] double T,
                                       [ExcelArgument(Name = "r", Description = "Interest rate")] double r,
                                       [ExcelArgument(Name = "b", Description = "Cost of carry")] double b,
                                       [ExcelArgument(Name = "v", Description = "option market price")] double cm,
                                       [ExcelArgument(Name = "epsilon", Description = "option market price")] double epsilon)
        {
            double result = double.NaN;
            result = OPLib.BSImpVol.BSImpVolNR(CallPutFlag, S, X, T, r, b, cm, epsilon);
            return result;
        }
    }
}
