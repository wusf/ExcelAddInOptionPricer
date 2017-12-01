using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;

namespace OptionPricingLib
{
    public class SpreadOptionApprox
    {
        public static double pricer(string cpflg, double S1, double S2, double Q1, double Q2, double X, double T,
                double r, double b1, double b2, double v1, double v2, double rho)
        {
            double v;
            double S;
            double d1, d2;
            double F;
            double price = double.NaN;
            F = Q2 * S2 * Exp((b2 - r) * T) / (Q2 * S2 * Exp((b2 - r) * T) + X * Exp(-r * T));
            v = Sqr(v1 * v1 + (v2 * F) * (v2 * F) - 2 * rho * v1 * v2 * F);
            S = Q1 * S1 * Exp((b1 - r) * T) / (Q2 * S2 * Exp((b2 - r) * T) + X * Exp(-r * T));
            d1 = (Log(S) + v * v / 2 * T) / (v * Sqr(T));
            d2 = d1 - v * Sqr(T);

            if (cpflg.Equals("c"))
            {

                price = (Q2 * S2 * Exp((b2 - r) * T) + X * Exp(-r * T)) * (S * CND(d1) - CND(d2));
            }
            else
            {
                price = (Q2 * S2 * Exp((b2 - r) * T) + X * Exp(-r * T)) * (CND(-d2) - S * CND(-d1));
            }
                return price;               
        }

        public static double fdaDelta1(cpflg, s1, s2, Q1,Q2,X,Task,ref,b1,b2,v1,v2,rho,ds)
        {
            pricer(cpflg, S1 + dS, S2, Q1, Q2, X, T, r, b1, b2, v1, v2, rho) - pricer(cpflg, S1 - dS, S2, Q1, Q2, X, T, r, b1, b2, v1, v2, rho)) / (2 * dS)
        }

     ElseIf OutPutFlag = "d2" Then 'Delta S2
         ESpreadApproximation = (SpreadApproximation(CallPutFlag, S1, S2 + dS, Q1, Q2, X, T, r, b1, b2, v1, v2, rho) - SpreadApproximation(CallPutFlag, S1, S2 - dS, Q1, Q2, X, T, r, b1, b2, v1, v2, rho)) / (2 * dS)
    ElseIf OutPutFlag = "e1" Then 'Elasticity S1
         ESpreadApproximation = (SpreadApproximation(CallPutFlag, S1 + dS, S2, Q1, Q2, X, T, r, b1, b2, v1, v2, rho) - SpreadApproximation(CallPutFlag, S1 - dS, S2, Q1, Q2, X, T, r, b1, b2, v1, v2, rho)) / (2 * dS) * S1 / SpreadApproximation(CallPutFlag, S1, S2, Q1, Q2, X, T, r, b1, b2, v1, v2, rho)
    ElseIf OutPutFlag = "e2" Then 'Elasticity S2
         ESpreadApproximation = (SpreadApproximation(CallPutFlag, S1, S2 + dS, Q1, Q2, X, T, r, b1, b2, v1, v2, rho) - SpreadApproximation(CallPutFlag, S1, S2 - dS, Q1, Q2, X, T, r, b1, b2, v1, v2, rho)) / (2 * dS) * S2 / SpreadApproximation(CallPutFlag, S1, S2, Q1, Q2, X, T, r, b1, b2, v1, v2, rho)
    ElseIf OutPutFlag = "g1" Then 'Gamma S1
        ESpreadApproximation = (SpreadApproximation(CallPutFlag, S1 + dS, S2, Q1, Q2, X, T, r, b1, b2, v1, v2, rho) - 2 * SpreadApproximation(CallPutFlag, S1, S2, Q1, Q2, X, T, r, b1, b2, v1, v2, rho) + SpreadApproximation(CallPutFlag, S1 - dS, S2, Q1, Q2, X, T, r, b1, b2, v1, v2, rho)) / dS ^ 2
      ElseIf OutPutFlag = "g2" Then 'Gamma S2
        ESpreadApproximation = (SpreadApproximation(CallPutFlag, S1, S2 + dS, Q1, Q2, X, T, r, b1, b2, v1, v2, rho) - 2 * SpreadApproximation(CallPutFlag, S1, S2, Q1, Q2, X, T, r, b1, b2, v1, v2, rho) + SpreadApproximation(CallPutFlag, S1, S2 - dS, Q1, Q2, X, T, r, b1, b2, v1, v2, rho)) / dS ^ 2
    ElseIf OutPutFlag = "gv1" Then 'DGammaDVol S1
        ESpreadApproximation = (SpreadApproximation(CallPutFlag, S1 + dS, S2, Q1, Q2, X, T, r, b1, b2, v1 + dv, v2, rho) - 2 * SpreadApproximation(CallPutFlag, S1, S2, Q1, Q2, X, T, r, b1, b2, v1 + dv, v2, rho) + SpreadApproximation(CallPutFlag, S1 - dS, S2, Q1, Q2, X, T, r, b1, b2, v1 + dv, v2, rho) _
      - SpreadApproximation(CallPutFlag, S1 + dS, S2, Q1, Q2, X, T, r, b1, b2, v1 - dv, v2, rho) + 2 * SpreadApproximation(CallPutFlag, S1, S2, Q1, Q2, X, T, r, b1, b2, v1 - dv, v2, rho) - SpreadApproximation(CallPutFlag, S1 - dS, S2, Q1, Q2, X, T, r, b1, b2, v1 - dv, v2, rho)) / (2 * dv* dS ^ 2) / 100
     ElseIf OutPutFlag = "gv2" Then 'DGammaDVol S2
        ESpreadApproximation = (SpreadApproximation(CallPutFlag, S1, S2 + dS, Q1, Q2, X, T, r, b1, b2, v1, v2 + dv, rho) - 2 * SpreadApproximation(CallPutFlag, S1, S2, Q1, Q2, X, T, r, b1, b2, v1, v2 + dv, rho) + SpreadApproximation(CallPutFlag, S1, S2 - dS, Q1, Q2, X, T, r, b1, b2, v1, v2 + dv, rho) _
      - SpreadApproximation(CallPutFlag, S1, S2 + dS, Q1, Q2, X, T, r, b1, b2, v1, v2 - dv, rho) + 2 * SpreadApproximation(CallPutFlag, S1, S2, Q1, Q2, X, T, r, b1, b2, v1, v2 - dv, rho) - SpreadApproximation(CallPutFlag, S1, S2 - dS, Q1, Q2, X, T, r, b1, b2, v1, v2 - dv, rho)) / (2 * dv* dS ^ 2) / 100
    ElseIf OutPutFlag = "cgv1" Then 'Cross GammaDvol S1v2 Cross Zomma
        ESpreadApproximation = (SpreadApproximation(CallPutFlag, S1 + dS, S2, Q1, Q2, X, T, r, b1, b2, v1, v2 + dv, rho) - 2 * SpreadApproximation(CallPutFlag, S1, S2, Q1, Q2, X, T, r, b1, b2, v1, v2 + dv, rho) + SpreadApproximation(CallPutFlag, S1 - dS, S2, Q1, Q2, X, T, r, b1, b2, v1, v2 + dv, rho) _
      - SpreadApproximation(CallPutFlag, S1 + dS, S2, Q1, Q2, X, T, r, b1, b2, v1, v2 - dv, rho) + 2 * SpreadApproximation(CallPutFlag, S1, S2, Q1, Q2, X, T, r, b1, b2, v1, v2 - dv, rho) - SpreadApproximation(CallPutFlag, S1 - dS, S2, Q1, Q2, X, T, r, b1, b2, v1, v2 - dv, rho)) / (2 * dv* dS ^ 2) / 100
     ElseIf OutPutFlag = "cgv2" Then 'Cross GammaDvol S2v1 Cross Zomma
        ESpreadApproximation = (SpreadApproximation(CallPutFlag, S1, S2 + dS, Q1, Q2, X, T, r, b1, b2, v1 + dv, v2, rho) - 2 * SpreadApproximation(CallPutFlag, S1, S2, Q1, Q2, X, T, r, b1, b2, v1 + dv, v2, rho) + SpreadApproximation(CallPutFlag, S1, S2 - dS, Q1, Q2, X, T, r, b1, b2, v1 + dv, v2, rho) _
      - SpreadApproximation(CallPutFlag, S1, S2 + dS, Q1, Q2, X, T, r, b1, b2, v1 - dv, v2, rho) + 2 * SpreadApproximation(CallPutFlag, S1, S2, Q1, Q2, X, T, r, b1, b2, v1 - dv, v2, rho) - SpreadApproximation(CallPutFlag, S1, S2 - dS, Q1, Q2, X, T, r, b1, b2, v1 - dv, v2, rho)) / (2 * dv* dS ^ 2) / 100




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