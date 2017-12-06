using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;

namespace OptionPricingLib
{
    public class ThreeAssetsSpreadOptionApprox
    {
        public static double pricer(string cpflg, double S1, double S2, double S3, double Q1, double Q2, double Q3, double X, double T,
                double r, double b1, double b2, double b3, double v1, double v2, double v3, double rho1, double rho2, double rho3)
        {
            double F1, F2, F3, LogF;
            double a, b2, b3;
            double temp1, temp2, temp3;
            double sigma;
            double S;
            double d1, d2, d3 ,d4;
            double price = double.NaN;

            F1 = Q1 * S1;
            F2 = Q2 * S2;
            F3 = Q3 * S3;

            a = F2 + F3 + X;
            b2 = F2 / a;
            b3 = F3 / a;
            temp1 = v1 * v1 + b2 * b2 * v2 * v2 + b3 * b3 * v3 * v3;
            temp2 = b2 * v1 * v2 * rho1 + b3 * v1 * v3 * rho2 - b2 * b3 * v2 * v3 * rho3;
            temp3 = 0.5 * (b2 * b2 * v2 * v2 + b3 * b3 * v3 * v3 - v1 * v1);
            sigma = Sqr(temp1 - 2 * temp2);
            LogF = Math.Log(F1 / a);
            d1 = (LogF + (0.5 * temp1 - temp2) * T) / (sigma * Sqr(T));
            d2 = (LogF + (temp3 - b2 * v2*v2 + b2 * b3 * v2 * v3 * rho3 - b3 * v2 * v3 * rho3 + v1 * v2 * rho1) * T) / (sigma * Sqr(T));
            d3 = (LogF + (temp3 - b3 * v3*v3 + b2 * b3 * v2 * v3 * rho3 - b2 * v2 * v3 * rho3 + v1 * v3 * rho2) * T) / (sigma * Sqr(T));
            d4 = (LogF + (temp3 + b2 * b3 * v2 * v3 * rho3) * T) / (sigma * Sqr(T));
            price = (F1 * CND(d1) - F2 * CND(d2) - F3 * CND(d3) - X * CND(d4)) * Exp(-r * T);
            return price;
        }

        public static double fdaDelta1(string cpflg, double S1, double S2, double Q1, double Q2, double X, double T,
                double r, double b1, double b2, double v1, double v2, double rho, double dS)
        {
            double result = double.NaN;
            result = (pricer(cpflg, S1 + dS, S2, Q1, Q2, X, T, r, b1, b2, v1, v2, rho) - pricer(cpflg, S1 - dS, S2, Q1, Q2, X, T, r, b1, b2, v1, v2, rho)) / (2 * dS);
            return result;
        }

        public static double fdaDelta2(string cpflg, double S1, double S2, double Q1, double Q2, double X, double T,
        double r, double b1, double b2, double v1, double v2, double rho, double dS)
        {
            double result = double.NaN;
            result = (pricer(cpflg, S1, S2 + dS, Q1, Q2, X, T, r, b1, b2, v1, v2, rho) - pricer(cpflg, S1, S2 - dS, Q1, Q2, X, T, r, b1, b2, v1, v2, rho)) / (2 * dS);
            return result;
        }

        ///ElseIf OutPutFlag = "e1" Then 'Elasticity S1
        ///    ESpreadApproximation = (SpreadApproximation(CallPutFlag, S1 + dS, S2, Q1, Q2, X, T, r, b1, b2, v1, v2, rho) - SpreadApproximation(CallPutFlag, S1 - dS, S2, Q1, Q2, X, T, r, b1, b2, v1, v2, rho)) / (2 * dS) * S1 / SpreadApproximation(CallPutFlag, S1, S2, Q1, Q2, X, T, r, b1, b2, v1, v2, rho)
        ///ElseIf OutPutFlag = "e2" Then 'Elasticity S2
        ///     ESpreadApproximation = (SpreadApproximation(CallPutFlag, S1, S2 + dS, Q1, Q2, X, T, r, b1, b2, v1, v2, rho) - SpreadApproximation(CallPutFlag, S1, S2 - dS, Q1, Q2, X, T, r, b1, b2, v1, v2, rho)) / (2 * dS) * S2 / SpreadApproximation(CallPutFlag, S1, S2, Q1, Q2, X, T, r, b1, b2, v1, v2, rho)

        public static double fdaGammaP1(string cpflg, double S1, double S2, double Q1, double Q2, double X, double T,
                                        double r, double b1, double b2, double v1, double v2, double rho, double dS)
        {
            double result = double.NaN;
            result = S1 / 100 * (pricer(cpflg, S1 + dS, S2, Q1, Q2, X, T, r, b1, b2, v1, v2, rho)
                - 2 * pricer(cpflg, S1, S2, Q1, Q2, X, T, r, b1, b2, v1, v2, rho)
                + pricer(cpflg, S1 - dS, S2, Q1, Q2, X, T, r, b1, b2, v1, v2, rho)) / (dS * dS);
            return result;
        }

        public static double fdaGammaP2(string cpflg, double S1, double S2, double Q1, double Q2, double X, double T,
                                        double r, double b1, double b2, double v1, double v2, double rho, double dS)
        {
            double result = double.NaN;
            result = S2 / 100 * (pricer(cpflg, S1, S2 + dS, Q1, Q2, X, T, r, b1, b2, v1, v2, rho)
                    - 2 * pricer(cpflg, S1, S2, Q1, Q2, X, T, r, b1, b2, v1, v2, rho)
                    + pricer(cpflg, S1, S2 - dS, Q1, Q2, X, T, r, b1, b2, v1, v2, rho)) / (dS * dS);
            return result;
        }

        public static double fdaVega1(string cpflg, double S1, double S2, double Q1, double Q2, double X, double T,
                                      double r, double b1, double b2, double v1, double v2, double rho, double dS)
        {
            double result = double.NaN;
            double dv = 0.01;
            result = (pricer(cpflg, S1, S2, Q1, Q2, X, T, r, b1, b2, v1 + dv, v2, rho)
                - pricer(cpflg, S1, S2, Q1, Q2, X, T, r, b1, b2, v1 - dv, v2, rho)) / 2;
            return result;
        }

        public static double fdaVega2(string cpflg, double S1, double S2, double Q1, double Q2, double X, double T,
                              double r, double b1, double b2, double v1, double v2, double rho, double dS)
        {
            double result = double.NaN;
            double dv = 0.01;
            result = (pricer(cpflg, S1, S2, Q1, Q2, X, T, r, b1, b2, v2 + dv, v2, rho)
                - pricer(cpflg, S1, S2, Q1, Q2, X, T, r, b1, b2, v2 - dv, v2, rho)) / 2;
            return result;
        }

        public static double fdaCorr(string cpflg, double S1, double S2, double Q1, double Q2, double X, double T,
                      double r, double b1, double b2, double v1, double v2, double rho, double dS)
        {
            double result = double.NaN;
            double dRho = 0.01;
            result = (pricer(cpflg, S1, S2, Q1, Q2, X, T, r, b1, b2, v1, v2, rho + dRho) - pricer(cpflg, S1, S2, Q1, Q2, X, T, r, b1, b2, v1, v2, rho - dRho)) / 2;
            return result;
        }

        public static double fdaTheta(string cpflg, double S1, double S2, double Q1, double Q2, double X, double T,
                      double r, double b1, double b2, double v1, double v2, double rho, double dS)
        {
            double result = double.NaN;
            if (T <= 1 / 365)
            {
                result = pricer(cpflg, S1, S2, Q1, Q2, X, 0.00001, r, b1, b2, v1, v2, rho) - pricer(cpflg, S1, S2, Q1, Q2, X, T, r, b1, b2, v1, v2, rho);
            }
            else
            {
                result = pricer(cpflg, S1, S2, Q1, Q2, X, T - 1 / 365, r, b1, b2, v1, v2, rho) - pricer(cpflg, S1, S2, Q1, Q2, X, T, r, b1, b2, v1, v2, rho);
            }
            return result;
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