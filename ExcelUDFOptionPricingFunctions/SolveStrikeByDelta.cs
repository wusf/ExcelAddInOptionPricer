using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExcelDna.Integration;
using OPLib = OptionPricingLib;

namespace ExcelUDFOptionPricingFunctions
{
    public class SolveStrikeByDelta
    {
        [ExcelFunction(Description = "Returns multi-asset spread option price and greeks through Quasi Monte-Carlo method")]
        public static object dnetSolveStrikeByDelta(
                              [ExcelArgument(Name = "CallPutFlag", Description = Flag.VanillaStyle)] string CallPutFlag,
                              [ExcelArgument(Name = "S", Description = "Underlying price")] double S,
                              [ExcelArgument(Name = "Delta", Description = "Target delta")] double delta,
                              [ExcelArgument(Name = "T", Description = "Days to expiration")] double T,
                              [ExcelArgument(Name = "r", Description = "Interest rate")] double r,
                              [ExcelArgument(Name = "b", Description = "Carry")] double b,
                              [ExcelArgument(Name = "v", Description = "Volatility")] double v)
        {
            double result = double.NaN;

            result = OPLib.SolveStrike.SolveStrikeByDelta(CallPutFlag, S, delta, T, r, b, v);

            return result;
        }

    }
}
