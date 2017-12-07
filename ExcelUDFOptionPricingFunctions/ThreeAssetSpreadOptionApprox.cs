﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelDna.Integration;
using OPLib = OptionPricingLib;

namespace ExcelUDFOptionPricingFunctions
{
    public class ThreeAssetSpreadOptionApprox
    {
        [ExcelFunction(Description = "Returns two-asset spread option price and greeks")]
        public static object Cs3AssetSpreadOptionApprox([ExcelArgument(Name = "OutPutFlag", Description = Flag.OutputFlag)] string OutPutFlag,
                                               [ExcelArgument(Name = "CallPutFlag", Description = Flag.VanillaStyle)] string CallPutFlag,
                                               [ExcelArgument(Name = "S1", Description = "Spot price of asset 1")] double S1,
                                               [ExcelArgument(Name = "S2", Description = "Spot price of asset 2")] double S2,
                                               [ExcelArgument(Name = "S2", Description = "Spot price of asset 3")] double S3,
                                               [ExcelArgument(Name = "Q1", Description = "Quantity of asset 1")] double Q1,
                                               [ExcelArgument(Name = "Q2", Description = "Quantity of asset 2")] double Q2,
                                               [ExcelArgument(Name = "Q2", Description = "Quantity of asset 3")] double Q3,
                                               [ExcelArgument(Name = "X", Description = "Strike price")] double X,
                                               [ExcelArgument(Name = "T", Description = "Days to expiration")] double T,
                                               [ExcelArgument(Name = "r", Description = "Interest rate")] double r,
                                               [ExcelArgument(Name = "b1", Description = "Cost of carry of asset 1")] double b1,
                                               [ExcelArgument(Name = "b2", Description = "Cost of carry of asset 2")] double b2,
                                               [ExcelArgument(Name = "b3", Description = "Cost of carry of asset 3")] double b3,
                                               [ExcelArgument(Name = "v1", Description = "Volatility of asset 1")] double v1,
                                               [ExcelArgument(Name = "v2", Description = "Volatility of asset 2")] double v2,
                                               [ExcelArgument(Name = "v3", Description = "Volatility of asset 3")] double v3,
                                               [ExcelArgument(Name = "rho", Description = "Correlation of asset 1 and 2")] double rho1,
                                               [ExcelArgument(Name = "rho", Description = "Correlation of asset 1 and 3")] double rho2,
                                               [ExcelArgument(Name = "rho", Description = "Correlation of asset 2 and 3")] double rho3,
                                               [ExcelArgument(Name = "dS", Description = "Step size if S")] double dS)
        {
            double result = double.NaN;
            if (OutPutFlag.Equals("price"))
            {
                result = OPLib.ThreeAssetsSpreadOptionApprox.Pricer(CallPutFlag, S1, S2, S3,Q1, Q2, Q3,X, T, r, b1, b2,b3, v1, v2,v3, rho1,rho2,rho3);
            }

            else if (OutPutFlag.Equals("delta1"))
            {
                result = OPLib.ThreeAssetsSpreadOptionApprox.FdDelta1(CallPutFlag, S1, S2, S3, Q1, Q2, Q3, X, T, r, b1, b2, b3, v1, v2, v3, rho1, rho2, rho3, dS);
            }

            else if (OutPutFlag.Equals("delta2"))
            {
                result = OPLib.ThreeAssetsSpreadOptionApprox.FdDelta2(CallPutFlag, S1, S2, S3, Q1, Q2, Q3, X, T, r, b1, b2, b3, v1, v2, v3, rho1, rho2, rho3, dS);
            }

            else if (OutPutFlag.Equals("delta3"))
            {
                result = OPLib.ThreeAssetsSpreadOptionApprox.FdDelta3(CallPutFlag, S1, S2, S3, Q1, Q2, Q3, X, T, r, b1, b2, b3 ,v1, v2, v3, rho1, rho2, rho3, dS);
            }


            else if (OutPutFlag.Equals("gammap1"))
            {
                result = OPLib.ThreeAssetsSpreadOptionApprox.FdGammaP1(CallPutFlag, S1, S2, S3, Q1, Q2, Q3, X, T, r, b1, b2, b3, v1, v2, v3, rho1, rho2, rho3, dS);
            }

            else if (OutPutFlag.Equals("gammap2"))
            {
                result = OPLib.ThreeAssetsSpreadOptionApprox.FdGammaP2(CallPutFlag, S1, S2, S3, Q1, Q2, Q3, X, T, r, b1, b2, b3, v1, v2, v3, rho1, rho2, rho3, dS);
            }

            else if (OutPutFlag.Equals("gammap3"))
            {
                result = OPLib.ThreeAssetsSpreadOptionApprox.FdGammaP3(CallPutFlag, S1, S2, S3, Q1, Q2, Q3, X, T, r, b1, b2, b3, v1, v2, v3, rho1, rho2, rho3, dS);
            }

            else if (OutPutFlag.Equals("vega1"))
            {
                result = OPLib.ThreeAssetsSpreadOptionApprox.FdVega1(CallPutFlag, S1, S2, S3, Q1, Q2, Q3, X, T, r, b1, b2, b3, v1, v2, v3, rho1, rho2, rho3, dS);
            }

            else if (OutPutFlag.Equals("vega2"))
            {
                result = OPLib.ThreeAssetsSpreadOptionApprox.FdVega2(CallPutFlag, S1, S2, S3, Q1, Q2, Q3, X, T, r, b1, b2, b3, v1, v2, v3, rho1, rho2, rho3, dS);
            }

            else if (OutPutFlag.Equals("vega3"))
            {
                result = OPLib.ThreeAssetsSpreadOptionApprox.FdVega3(CallPutFlag, S1, S2, S3, Q1, Q2, Q3, X, T, r, b1, b2, b3, v1, v2, v3, rho1, rho2, rho3, dS);
            }

            else if (OutPutFlag.Equals("theta"))
            {
                result = OPLib.ThreeAssetsSpreadOptionApprox.FdTheta(CallPutFlag, S1, S2, S3, Q1, Q2, Q3, X, T, r, b1, b2, b3, v1, v2, v3, rho1, rho2, rho3, dS);
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
