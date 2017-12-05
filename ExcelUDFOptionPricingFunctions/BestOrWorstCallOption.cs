using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelDna.Integration;
using OPLib = OptionPricingLib;

namespace ExcelUDFOptionPricingFunctions
{
    public class BestOrWorstCallOptionQMC
    {
        [ExcelFunction(Description = "Returns multi-asset spread option price and greeks through Quasi Monte-Carlo method")]
        public static object dnetBestOrWorstCallOptionQMC([ExcelArgument(Name = "OutPutFlag", Description = Flag.OutputFlag)] string OutPutFlag,
                               [ExcelArgument(Name = "BestWorstFlag", Description = Flag.BestOrWorst)] string BestOrWorstFlag,
                               [ExcelArgument(Name = "S", Description = "Spot price array")] double[] S,
                               [ExcelArgument(Name = "X", Description = "Strike price")] double[] X,
                               [ExcelArgument(Name = "T", Description = "Days to expiration")] double T,
                               [ExcelArgument(Name = "r", Description = "Interest rate")] double r,
                               [ExcelArgument(Name = "b", Description = "Cost of carry array")] double[] b,
                               [ExcelArgument(Name = "v", Description = "Volatility array")] double[] v,
                               [ExcelArgument(Name = "CorrMat", Description = "Correlation matrix")] double[,] mat,
                               [ExcelArgument(Name = "SimTimes", Description = "Simulation times")] int times,
                               [ExcelArgument(Name = "dS", Description = "Delta S")] double ds)
        {
            double result = double.NaN;
            if (OutPutFlag.Equals("price"))
            {
                result = OPLib.BestOrWorstCallOptionQMC.GetPrice(BestOrWorstFlag, S, X, T, r, b, v, mat, times);
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
