using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;

namespace OptionPricingLib
{
    public class SolveStrike
    {
        public static double SolveStrikeByDelta(string tpflg,double S,double delta,double t,double r,double b,double v)
        {
            double strike = 0;
            double d1;
            if (tpflg.Equals("c"))
            {
                d1 = Normal.InvCDF(0, 1, delta / (Math.Exp((b - r) * t)));
            }
            else if (tpflg.Equals("p"))
            {
                d1 = Normal.InvCDF(0, 1, delta / (Math.Exp((b - r) * t))+1);
            }
            else
            {
                d1 = 0;
            }
            double lnsn;
            lnsn = d1 * v * Math.Sqrt(t) - (b + v * v / 2) * t;
            strike = S / Math.Exp(lnsn);
            return strike;
        }
    }
}
