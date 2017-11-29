using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelDna.Integration;
using OPLib = OptionPricingLib;

namespace ExcelUDFOptionPricingFunctions
{
    public class CashOrNothingAnalyticMethod
    {
        [ExcelFunction(Description = "Returns vanilla option price and greeks through Black-Sholes-Merton method")]
        public static object dnetCashOrNothingNGreeks([ExcelArgument(Name = "OutPutFlag", Description = Flag.OutputFlag)] string OutPutFlag,
                                               [ExcelArgument(Name = "CallPutFlag", Description = Flag.VanillaStyle)] string CallPutFlag,
                                               [ExcelArgument(Name = "S", Description = "Spot price")] double S,
                                               [ExcelArgument(Name = "x", Description = "Strike price")] double x,
                                               [ExcelArgument(Name = "k", Description = "Rebate cash")] double k,
                                               [ExcelArgument(Name = "T", Description = "Days to expiration")] double T,
                                               [ExcelArgument(Name = "r", Description = "Interest rate")] double r,
                                               [ExcelArgument(Name = "b", Description = "Cost of carry")] double b,
                                               [ExcelArgument(Name = "v", Description = "Volatility")] double v,
                                               [ExcelArgument(Name = "dS", Description = "Delta S")] double ds)
        {
            double result = double.NaN;
            if (OutPutFlag.Equals("price"))
            {
                result = OPLib.CashOrNothingAnalyticMethod.CashOrNothing(CallPutFlag, S, x, k,T, r, b, v);
            }

            else if (OutPutFlag.Equals("delta"))
            {
                result = OPLib.CashOrNothingAnalyticMethod.FDA_Delta(CallPutFlag, S, x, k, T, r, b, v, ds);
            }

            else if (OutPutFlag.Equals("delta+"))
            {
                result = OPLib.CashOrNothingAnalyticMethod.FDA_DeltaR(CallPutFlag, S, x, k, T, r, b, v, ds);
            }

            else if (OutPutFlag.Equals("delta-"))
            {
                result = OPLib.CashOrNothingAnalyticMethod.FDA_DeltaL(CallPutFlag, S, x, k, T, r, b, v, ds);
            }


            else if (OutPutFlag.Equals("gammap"))
            {
                result = OPLib.CashOrNothingAnalyticMethod.FDA_GammaP(CallPutFlag, S, x, k, T, r, b, v, ds);
            }

            else if (OutPutFlag.Equals("vega"))
            {
                result = OPLib.CashOrNothingAnalyticMethod.FDA_Vega(CallPutFlag, S, x, k, T, r, b, v, ds);
            }

            else if (OutPutFlag.Equals("theta"))
            {
                result = OPLib.CashOrNothingAnalyticMethod.FDA_Theta(CallPutFlag, S, x, k, T, r, b, v, ds);
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
