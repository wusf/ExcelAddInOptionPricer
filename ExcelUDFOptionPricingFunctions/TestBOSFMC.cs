using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelDna.Integration;
using OPLib = OptionPricingLib;

namespace ExcelUDFOptionPricingFunctions
{
    public class TestBOSFMC
    {
        [ExcelFunction(Description = "Returns multi-asset spread option price and greeks through Quasi Monte-Carlo method")]
        public static object CsTestBOSFCallMC([ExcelArgument(Name = "OutPutFlag", Description = Flag.OutputFlag)] string OutPutFlag,
                               [ExcelArgument(Name = "CallPutFlag", Description = Flag.OutputFlag)] string CallPutFlag,
                               [ExcelArgument(Name = "Wgt", Description = "Weights")] double[] Wgt,
                               [ExcelArgument(Name = "S", Description = "Spot price array")] double[] S,
                               [ExcelArgument(Name = "X", Description = "Strike price")] double[] X,
                               [ExcelArgument(Name = "h", Description = "Barrier price")] double[] H,
                               [ExcelArgument(Name = "k", Description = "rebate")] double k,
                               [ExcelArgument(Name = "T", Description = "Days to expiration")] double T,
                               [ExcelArgument(Name = "r", Description = "Interest rate")] double r,
                               [ExcelArgument(Name = "b", Description = "Cost of carry array")] double[] b,
                               [ExcelArgument(Name = "v", Description = "Volatility array")] double[] v,
                               [ExcelArgument(Name = "CorrMat", Description = "Correlation matrix")] double[,] mat,
                               [ExcelArgument(Name = "SimTimes", Description = "Simulation times")] int times,
                               [ExcelArgument(Name = "dS", Description = "Delta S")] double ds)
        {
            double result = double.NaN;

            if (OutPutFlag.Equals("p"))
            {
                result = OPLib.MultiAssetPathDependentMC.BestOfCallSFMC(CallPutFlag, Wgt, S, X, H, k, T, r, b, v, mat, times);
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
