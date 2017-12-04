using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelDna.Integration;
using OPLib = OptionPricingLib;


namespace CsOptionPricerAddIn
{
    public class OneTouchDoubleBarrierBinaryAnalyticMethod
    {
        [ExcelFunction(Description = "Returns standard barrier option price and greeks through analytic method")]
        public static object dnetOneTouchDoubleBarrierBinaryNGreeks([ExcelArgument(Name = "OutPutFlag", Description = Flag.OutputFlag)] string OutPutFlag,
                                              [ExcelArgument(Name = "TypeFlag", Description = Flag.OneTouchDoubleBarrierBinaryStyle)] string TypeFlag,
                                              [ExcelArgument(Name = "S", Description = "Spot price")] double S,
                                              [ExcelArgument(Name = "L", Description = "Lower barrier")] double L,
                                              [ExcelArgument(Name = "U", Description = "Upper barrier")] double U,
                                              [ExcelArgument(Name = "k", Description = "Rebate cash")] double k,
                                              [ExcelArgument(Name = "T", Description = "Days to expiration")] double T,
                                              [ExcelArgument(Name = "r", Description = "Interest rate")] double r,
                                              [ExcelArgument(Name = "b", Description = "Cost of carry")] double b,
                                              [ExcelArgument(Name = "v", Description = "Volatility")] double v,
                                              [ExcelArgument(Name = "dS", Description = "Delta S")] double ds)
        {
            double result = double.NaN;

            if (S >= U || S <= L)
            {
                if (TypeFlag.Equals("i") && OutPutFlag.Equals("price")) 
                    {
                    result = k * Math.Exp(-r * T);
                }
                else
                {
                    result = 0;
                }
                return result;
            }
        
            if (OutPutFlag.Equals("price"))
            {
                result = OPLib.OneTouchDoubleBarrierBinaryAnalyticMethod.OneTouchDoubleBarrierBinaryOption(TypeFlag, S, L, U, k, T, r, b, v);
            }

            else if (OutPutFlag.Equals("delta"))
            {
                result = OPLib.OneTouchDoubleBarrierBinaryAnalyticMethod.FDA_Delta(TypeFlag, S, L, U, k, T, r, b, v, ds);
            }

            else if (OutPutFlag.Equals("delta+"))
            {
                result = OPLib.OneTouchDoubleBarrierBinaryAnalyticMethod.FDA_DeltaR(TypeFlag, S, L, U, k, T, r, b, v, ds);
            }

            else if (OutPutFlag.Equals("delta-"))
            {
                result = OPLib.OneTouchDoubleBarrierBinaryAnalyticMethod.FDA_DeltaL(TypeFlag, S, L, U, k, T, r, b, v, ds);
            }


            else if (OutPutFlag.Equals("gammap"))
            {
                result = OPLib.OneTouchDoubleBarrierBinaryAnalyticMethod.FDA_GammaP(TypeFlag, S, L, U, k, T, r, b, v, ds);
            }

            else if (OutPutFlag.Equals("vega"))
            {
                result = OPLib.OneTouchDoubleBarrierBinaryAnalyticMethod.FDA_Vega(TypeFlag, S, L, U, k, T, r, b, v, ds);
            }

            else if (OutPutFlag.Equals("theta"))
            {
                result = OPLib.OneTouchDoubleBarrierBinaryAnalyticMethod.FDA_Theta(TypeFlag, S, L, U, k, T, r, b, v, ds);
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
