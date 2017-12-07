using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelDna.Integration;
using OPLib = OptionPricingLib;

namespace ExcelUDFOptionPricingFunctions
{
    public class TwoAssetSpreadOptionApprox
    {
        [ExcelFunction(Description = "Returns two assets spread option price and greeks")]
        public static object Cs2AssetSpreadOptionApprox([ExcelArgument(Name = "OutPutFlag", Description = Flag.OutputFlag)] string OutPutFlag,
                                               [ExcelArgument(Name = "CallPutFlag", Description = Flag.VanillaStyle)] string CallPutFlag,
                                               [ExcelArgument(Name = "S1", Description = "Spot price of asset 1")] double S1,
                                               [ExcelArgument(Name = "S2", Description = "Spot price of asset 2")] double S2,
                                               [ExcelArgument(Name = "Q1", Description = "Quantity of asset 1")] double Q1,
                                               [ExcelArgument(Name = "Q2", Description = "Quantity of asset 2")] double Q2,
                                               [ExcelArgument(Name = "X", Description = "Strike price")] double X,
                                               [ExcelArgument(Name = "T", Description = "Days to expiration")] double T,
                                               [ExcelArgument(Name = "r", Description = "Interest rate")] double r,
                                               [ExcelArgument(Name = "b1", Description = "Cost of carry of asset 1")] double b1,
                                               [ExcelArgument(Name = "b2", Description = "Cost of carry of asset 2")] double b2,
                                               [ExcelArgument(Name = "v1", Description = "Volatility of asset 1")] double v1,
                                               [ExcelArgument(Name = "v2", Description = "Volatility of asset 2")] double v2,
                                               [ExcelArgument(Name = "rho", Description = "Correlation of 2 assets")] double rho,
                                               [ExcelArgument(Name = "dS", Description = "Step size if S")] double dS)
        {
            double result = double.NaN;
            if (OutPutFlag.Equals("price"))
            {
                result = OPLib.TwoAssetsSpreadOptionApprox.Pricer(CallPutFlag, S1, S2, Q1, Q2,  X,  T, r, b1, b2, v1, v2, rho);
            }

            else if (OutPutFlag.Equals("delta1"))
            {
                result = OPLib.TwoAssetsSpreadOptionApprox.FdDelta1(CallPutFlag, S1, S2, Q1, Q2, X, T, r, b1, b2, v1, v2, rho, dS);
            }

            else if (OutPutFlag.Equals("delta2"))
            {
                result = OPLib.TwoAssetsSpreadOptionApprox.FdDelta2(CallPutFlag, S1, S2, Q1, Q2, X, T, r, b1, b2, v1, v2, rho, dS);
            }

            else if (OutPutFlag.Equals("gammap1"))
            {
                result = OPLib.TwoAssetsSpreadOptionApprox.FdGammaP1(CallPutFlag, S1, S2, Q1, Q2, X, T, r, b1, b2, v1, v2, rho, dS);
            }

            else if (OutPutFlag.Equals("gammap2"))
            {
                result = OPLib.TwoAssetsSpreadOptionApprox.FdGammaP2(CallPutFlag, S1, S2, Q1, Q2, X, T, r, b1, b2, v1, v2, rho, dS);
            }

            else if (OutPutFlag.Equals("vega1"))
            {
                result = OPLib.TwoAssetsSpreadOptionApprox.FdVega1(CallPutFlag, S1, S2, Q1, Q2, X, T, r, b1, b2, v1, v2, rho, dS);
            }

            else if (OutPutFlag.Equals("vega2"))
            {
                result = OPLib.TwoAssetsSpreadOptionApprox.FdVega2(CallPutFlag, S1, S2, Q1, Q2, X, T, r, b1, b2, v1, v2, rho, dS);
            }

            else if (OutPutFlag.Equals("theta"))
            {
                result = OPLib.TwoAssetsSpreadOptionApprox.FdTheta(CallPutFlag, S1, S2, Q1, Q2, X, T, r, b1, b2, v1, v2, rho, dS);
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
