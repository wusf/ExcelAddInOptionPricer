using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelDna.Integration;
using OPLib = OptionPricingLib;


namespace ExcelUDFOptionPricingFunctions
{
    public class StandardBarrierOptionAnalyticMethod
    {
        [ExcelFunction(Description = "Returns standard barrier option price and greeks through analytic method")]
        public static object csStandardBarrierNGreeks([ExcelArgument(Name = "OutPutFlag", Description = Flag.OutputFlag)] string OutPutFlag,
                                               [ExcelArgument(Name = "TypeFlag", Description = Flag.StandardBarrierStyle)] string TypeFlag,
                                               [ExcelArgument(Name = "S", Description = "Spot price")] double S,
                                               [ExcelArgument(Name = "X", Description = "Strike price")] double X,
                                               [ExcelArgument(Name = "H", Description = "Barrier price")] double H,
                                               [ExcelArgument(Name = "k", Description = "Rebate cash")] double k,
                                               [ExcelArgument(Name = "T", Description = "Days to expiration")] double T,
                                               [ExcelArgument(Name = "r", Description = "Interest rate")] double r,
                                               [ExcelArgument(Name = "b", Description = "Cost of carry")] double b,
                                               [ExcelArgument(Name = "v", Description = "Volatility")] double v,
                                               [ExcelArgument(Name = "dS", Description = "Delta S")] double ds)
        {
            double result = double.NaN;

            string OutInFlag;
            string CallPutFlag;
            OutInFlag = TypeFlag.Substring(1, 2);
            CallPutFlag = TypeFlag.Substring(0, 1);

            if ((OutInFlag.Equals("do") && S <= H) || (OutInFlag.Equals("uo") && S >= H))
            {
                if (OutPutFlag.Equals("price"))
                {
                    result = k;
                }
                else
                {
                    result = 0;
                }
                return result;
            }
            else if ((OutInFlag.Equals("di") && S <= H) || (OutInFlag.Equals("ui") && S >= H))
            {
                result = (double)BlackScholesPricer.csGBlackScholesNGreeks(OutPutFlag, CallPutFlag, S, X, T, r, b, v, ds);
                return result;
            }
           

            if (OutPutFlag.Equals("price"))
            {
                result = OPLib.StandardBarrierAnalyticMethod.StandardBarrierOption(TypeFlag, S, X, H, k, T, r, b, v);
            }

            else if (OutPutFlag.Equals("delta"))
            {
                result = OPLib.StandardBarrierAnalyticMethod.FDA_Delta(TypeFlag, S, X, H, k, T, r, b, v, ds);
            }

            else if (OutPutFlag.Equals("delta+"))
            {
                result = OPLib.StandardBarrierAnalyticMethod.FDA_DeltaR(TypeFlag, S, X, H, k, T, r, b, v, ds);
            }

            else if (OutPutFlag.Equals("delta-"))
            {
                result = OPLib.StandardBarrierAnalyticMethod.FDA_DeltaL(TypeFlag, S, X, H, k, T, r, b, v, ds);
            }


            else if (OutPutFlag.Equals("gammap"))
            {
                result = OPLib.StandardBarrierAnalyticMethod.FDA_GammaP(TypeFlag, S, X, H, k, T, r, b, v, ds);
            }

            else if (OutPutFlag.Equals("vega"))
            {
                result = OPLib.StandardBarrierAnalyticMethod.FDA_Vega(TypeFlag, S, X,  H, k, T, r, b, v, ds);
            }

            else if (OutPutFlag.Equals("theta"))
            {
                result = OPLib.StandardBarrierAnalyticMethod.FDA_Theta(TypeFlag, S, X, H, k, T, r, b, v, ds);
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

        [ExcelFunction(Description = "Returns adjusted standard barrier")]
        public static object dnetDiscreteAdjustedBarrier([ExcelArgument(Name = "S", Description = "Spot price")] double S,
                                                         [ExcelArgument(Name = "H", Description = "Barrier price")] double H,
                                                         [ExcelArgument(Name = "v", Description = "Volatility")] double v,
                                                         [ExcelArgument(Name = "dT", Description = "Time between monitoring")] double dt)
        {
            double result = double.NaN;
            result = OPLib.StandardBarrierAnalyticMethod.DiscreteAdjustedBarrier(S, H, v, dt);
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
