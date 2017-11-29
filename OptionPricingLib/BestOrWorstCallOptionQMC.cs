using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Random;
using MathNet.Numerics.LinearAlgebra.Double;

namespace OptionPricingLib
{
    public class BestOrWorstCallOptionQMC
    {
        public static double GetST0(double S, double T, double b, double v, double fi)
        {
            return S * Math.Pow(Math.E, ((b - v * v / 2.0) * T + v * Math.Sqrt(T) * fi));
        }
        public static double GetST0_Antithetic(double S, double T, double b, double v, double fi)
        {
            return S * Math.Pow(Math.E, ((b - v * v / 2.0) * T - v * Math.Sqrt(T) * fi));
        }

        public static double GetPayOff(string BestOrWorstFlg, double[] S, double[] X, double T, double r, double[] b, double[] v, Matrix<double> cholesky, int p)
        {
            double option_price = double.NaN;
            int num = S.Length;
            double[] ST0_vec = new double[num];
            double[] ST0_antithetic_vec = new double[num];
            double[] po = new double[num];
            double[] fi_vec = new double[num];

            //double b = Normal.Sample(new MersenneTwister(), 0.0, 1.0);
            //double[] c = new double[num];
            //Normal.Samples(c, 0.0, 1.0);
            Halton ht = new Halton(num, p + 1);
            double[] c = ht.GetNextGaussian();
            for (var i = 0; i < num; i++)
            {
                double _fi = 0;
                for (var j = 0; j < num; j++)
                {
                    _fi = _fi + cholesky[i, j] * c[j];
                }
                fi_vec[i] = _fi;
                ST0_vec[i] = GetST0(S[i], T, b[i], v[i], _fi);
                //ST0_antithetic_vec[i] = (GetST0_Antithetic(S[i], T, b[i], v[i], _fi) + GetST0(S[i], T, b[i], v[i], _fi))/2;
                po[i] = Math.Max(ST0_vec[i] - X[i], 0);
                //po[i] = Math.Max(ST0_antithetic_vec[i] - X[i],0);
            }
            if (BestOrWorstFlg == "best")
            {
                option_price = po.Max();
            }
            else if (BestOrWorstFlg == "worst")
            {
                option_price = po.Min();
            }
            else
            {
                option_price = double.NaN;
            }
            return option_price;
        }
        public static double GetPrice(string BestOrWorstFlg, double[] S, double[] X, double T, double r, double[] b, double[] v, double[,] CorrMat, int SimTimes)
        {
            int num = S.Length;
            var matrix = new DenseMatrix(num);
            for (var i = 0; i < num; i++)
            {
                for (var j = 0; j < num; j++)
                {
                    matrix[i, j] = CorrMat[i, j];
                }
            }
            var cholesky = matrix.Cholesky().Factor;
            double option_price = 0;
            for (int i = 0; i < SimTimes; i++)
            {
                option_price = option_price + GetPayOff(BestOrWorstFlg, S, X, T, r, b, v, cholesky, i);
            }
            return option_price / SimTimes * Math.Exp(-T*r);
        }
    }
}
