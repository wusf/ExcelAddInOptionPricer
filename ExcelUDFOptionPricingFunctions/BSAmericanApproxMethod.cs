using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelDna.Integration;
using OPLib = OptionPricingLib;

namespace ExcelUDFOptionPricingFunctions
{
    public class BSAmericanApproxMethod
    {
        [ExcelFunction(Description = "Returns Ameican option price and greeks through BS approximation method")]
        public static object csGBSAmericanApproxNGreeks([ExcelArgument(Name = "OutPutFlag", Description = Flag.OutputFlag)] string OutPutFlag,
                                       [ExcelArgument(Name = "CallPutFlag", Description = Flag.VanillaStyle)] string CallPutFlag,
                                       [ExcelArgument(Name = "S", Description = "Spot price")] double S,
                                       [ExcelArgument(Name = "X", Description = "Strike price")] double X,
                                       [ExcelArgument(Name = "T", Description = "Days to expiration")] double T,
                                       [ExcelArgument(Name = "r", Description = "Interest rate")] double r,
                                       [ExcelArgument(Name = "b", Description = "Cost of carry")] double b,
                                       [ExcelArgument(Name = "v", Description = "Volatility")] double v,
                                       [ExcelArgument(Name = "dS", Description = "Delta S")] double ds)
        {
            double result = double.NaN;
            if (OutPutFlag.Equals("p"))
            {
                result = OPLib.BSAmericanApproxMethod.BSAmericanApprox2002(CallPutFlag, S, X, T, r, b, v);
            }
            else
            {
                result = double.NaN;
            }
            if (double.IsNaN(result))
            {
                return ExcelError.ExcelErrorValue;
            }
            else
            {
                return result;
            }
        }
    }
}
