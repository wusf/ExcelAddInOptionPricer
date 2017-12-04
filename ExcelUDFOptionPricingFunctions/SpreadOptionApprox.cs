using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelDna.Integration;
using OPLib = OptionPricingLib;

namespace CsOptionPricerAddIn
{
    public class SpreadOptionApprox
    {
        [ExcelFunction(Description = "Returns two assets spread option price and greeks")]
        public static object csSpreadOptionApprox([ExcelArgument(Name = "OutPutFlag", Description = Flag.OutputFlag)] string OutPutFlag,
                                               [ExcelArgument(Name = "CallPutFlag", Description = Flag.VanillaStyle)] string CallPutFlag,
                                               [ExcelArgument(Name = "S1", Description = "Spot price")] double S1,
                                               [ExcelArgument(Name = "S2", Description = "Spot price")] double S2,
                                               [ExcelArgument(Name = "Q1", Description = "Spot price")] double Q1,
                                               [ExcelArgument(Name = "Q2", Description = "Spot price")] double Q2,
                                               [ExcelArgument(Name = "X", Description = "Strike price")] double X,
                                               [ExcelArgument(Name = "T", Description = "Days to expiration")] double T,
                                               [ExcelArgument(Name = "r", Description = "Interest rate")] double r,
                                               [ExcelArgument(Name = "b1", Description = "Cost of carry1")] double b1,
                                               [ExcelArgument(Name = "b2", Description = "Cost of carry2")] double b2,
                                               [ExcelArgument(Name = "v1", Description = "Volatility1")] double v1,
                                               [ExcelArgument(Name = "v2", Description = "Volatility2")] double v2,
                                               [ExcelArgument(Name = "rho", Description = "Correlation")] double rho,
                                               [ExcelArgument(Name = "dS", Description = "Step size if S")] double dS)
        {
            double result = double.NaN;
            if (OutPutFlag.Equals("price"))
            {
                result = OPLib.SpreadOptionApprox.pricer(CallPutFlag, S1, S2, Q1, Q2,  X,  T, r, b1, b2, v1, v2, rho);
            }

            else if (OutPutFlag.Equals("delta1"))
            {
                result = OPLib.SpreadOptionApprox.fdaDelta1(CallPutFlag, S1, S2, Q1, Q2, X, T, r, b1, b2, v1, v2, rho, dS);
            }

            else if (OutPutFlag.Equals("delta2"))
            {
                result = OPLib.SpreadOptionApprox.fdaDelta2(CallPutFlag, S1, S2, Q1, Q2, X, T, r, b1, b2, v1, v2, rho, dS);
            }

            else if (OutPutFlag.Equals("gammap1"))
            {
                result = OPLib.SpreadOptionApprox.fdaGammaP1(CallPutFlag, S1, S2, Q1, Q2, X, T, r, b1, b2, v1, v2, rho, dS);
            }

            else if (OutPutFlag.Equals("gammap2"))
            {
                result = OPLib.SpreadOptionApprox.fdaGammaP2(CallPutFlag, S1, S2, Q1, Q2, X, T, r, b1, b2, v1, v2, rho, dS);
            }

            else if (OutPutFlag.Equals("vega1"))
            {
                result = OPLib.SpreadOptionApprox.fdaVega1(CallPutFlag, S1, S2, Q1, Q2, X, T, r, b1, b2, v1, v2, rho, dS);
            }

            else if (OutPutFlag.Equals("vega2"))
            {
                result = OPLib.SpreadOptionApprox.fdaVega2(CallPutFlag, S1, S2, Q1, Q2, X, T, r, b1, b2, v1, v2, rho, dS);
            }

            else if (OutPutFlag.Equals("theta"))
            {
                result = OPLib.SpreadOptionApprox.fdaTheta(CallPutFlag, S1, S2, Q1, Q2, X, T, r, b1, b2, v1, v2, rho, dS);
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
