using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;

namespace OptionPricingLib
{
    public class OneTouchDoubleBarrierBinaryAnalyticMethod
    {
        public static double OneTouchDoubleBarrierBinaryOption(string TypeFlag, double S, double L, double U,
                                                       double k, double T, double r, double b, double v)
        {
            double price = double.NaN;
            double Alfa, Beta, Z, sum;
            int i;


            Alfa = -0.5 * (2 * b / v * v - 1);
            Beta = -0.25 * (2 * b / v * v - 1) * (2 * b / v * v - 1) - 2 * r / (v * v);

            Z = Log(U / L);
            sum = 0;
            for (i = 1; i <= 50; i++)
            {
                sum = sum + 2 * Math.PI * i * k / (Z * Z)
                    * ((Math.Pow(S / L, Alfa) - Math.Pow(-1, i) * Math.Pow(S / U, Alfa)) / (Alfa * Alfa + (i * Math.PI / Z) * (i * Math.PI / Z)))
                    * Math.Sin(i * Math.PI / Z * Log(S / L)) * Exp(-0.5 * ((i * Math.PI / Z) * (i * Math.PI / Z) - Beta) * v * v * T);
            }


            if (TypeFlag.Equals("o"))
            {// Knock-out
                price = sum;
            }
            else if (TypeFlag.Equals("i"))
            {// Knock-in
                price = k * Exp(-r * T) - sum;
            }
            return price;
        }

        public static double FDA_Delta(string TypeFlag, double S, double L, double U, double k, double T,
                               double r, double b, double v, double ds)
        {
            double delta = double.NaN;
            double bsr = OneTouchDoubleBarrierBinaryOption(TypeFlag, S + ds, L, U, k, T, r, b, v);
            double bs = OneTouchDoubleBarrierBinaryOption(TypeFlag, S, L, U, k, T, r, b, v);
            double bsl = OneTouchDoubleBarrierBinaryOption(TypeFlag, S - ds, L, U, k, T, r, b, v);
            delta = (bsr - bsl) / (2 * ds);
            return delta;
        }

        public static double FDA_DeltaR(string TypeFlag, double S, double L, double U, double k, double T,
                                       double r, double b, double v, double ds)
        {
            double delta = double.NaN;
            double bsr = OneTouchDoubleBarrierBinaryOption(TypeFlag, S + ds, L, U, k, T, r, b, v);
            double bs = OneTouchDoubleBarrierBinaryOption(TypeFlag, S, L, U, k, T, r, b, v);
            double bsl = OneTouchDoubleBarrierBinaryOption(TypeFlag, S - ds, L, U, k, T, r, b, v);
            delta = (bsr - bs) / (ds);
            return delta;
        }

        public static double FDA_DeltaL(string TypeFlag, double S, double L, double U, double k, double T,
                                       double r, double b, double v, double ds)
        {
            double delta = double.NaN;
            double bsr = OneTouchDoubleBarrierBinaryOption(TypeFlag, S + ds, L, U, k, T, r, b, v);
            double bs = OneTouchDoubleBarrierBinaryOption(TypeFlag, S, L, U, k, T, r, b, v);
            double bsl = OneTouchDoubleBarrierBinaryOption(TypeFlag, S - ds, L, U, k, T, r, b, v);
            delta = (bs - bsl) / (ds);
            return delta;
        }

        public static double FDA_GammaP(string TypeFlag, double S, double L, double U, double k, double T,
                                       double r, double b, double v, double ds)
        {
            double gammap = double.NaN;
            double bsr = OneTouchDoubleBarrierBinaryOption(TypeFlag, S + ds, L, U, k, T, r, b, v);
            double bs = OneTouchDoubleBarrierBinaryOption(TypeFlag, S, L, U, k, T, r, b, v);
            double bsl = OneTouchDoubleBarrierBinaryOption(TypeFlag, S - ds, L, U, k, T, r, b, v);
            gammap = S / 100 * (bsr - 2 * bs + bsl) / (ds * ds);
            return gammap;
        }

        public static double FDA_Vega(string TypeFlag, double S, double L, double U, double k, double T,
                                       double r, double b, double v, double ds)
        {
            double vega = double.NaN;
            double bsr = OneTouchDoubleBarrierBinaryOption(TypeFlag, S, L, U, k, T, r, b, v + 0.01);
            double bsl = OneTouchDoubleBarrierBinaryOption(TypeFlag, S, L, U, k, T, r, b, v - 0.01);
            vega = (bsr - bsl) / 2.0;
            return vega;
        }

        public static double FDA_Theta(string TypeFlag, double S, double L, double U, double k, double T,
                                       double r, double b, double v, double ds)
        {
            double theta = double.NaN;
            double deltaT;
            if (T <= 1 / 365.0)
            {
                deltaT = 1 - 0.000005;
            }
            else
            {
                deltaT = 1 / 365.0;
            }
            double bsr = OneTouchDoubleBarrierBinaryOption(TypeFlag, S, L, U, k, T - deltaT, r, b, v);
            double bsl = OneTouchDoubleBarrierBinaryOption(TypeFlag, S, L, U, k, T, r, b, v);
            theta = bsr - bsl;
            return theta;
        }

        public static double DiscreteAdjustedBarrier(double S, double H, double v, double dt)
        {
            double barrier_adj = double.NaN;
            if (H > S)
            {
                barrier_adj = H * Exp(0.5826 * v * Sqr(dt));
            }
            else if (H < S)
            {
                barrier_adj = H * Exp(-0.5826 * v * Sqr(dt));
            }
            else
            {
                barrier_adj = double.NaN;
            }
            return barrier_adj;
        }

        public static double Log(double X)
        {
            return Math.Log(X);
        }

        public static double CND(double X)
        {
            return Normal.CDF(0, 1, X);
        }

        public static double Exp(double X)
        {
            return Math.Exp(X);
        }

        public static double Sqr(double x)
        {
            return Math.Sqrt(x);
        }
    }
}
