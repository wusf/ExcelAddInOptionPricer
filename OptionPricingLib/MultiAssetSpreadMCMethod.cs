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
    public class MultiAssetSpreadMCMethod
    {
        public static double GetST0(double S, double T, double b, double v, double fi)
        {
            return S * Math.Pow(Math.E, ((b - v * v / 2.0) * T + v * Math.Sqrt(T) * fi));
        }
        public static double GetST0_Antithetic(double S, double T, double b, double v, double fi)
        {
            return S * Math.Pow(Math.E, ((b - v * v / 2.0) * T - v * Math.Sqrt(T) * fi));
        }

        public static double GetPayOff(string cpflg, double[] Wgt, double[] S, double X, double T, double r, double[] b, double[] v, Matrix<double> cholesky)
        {
            double option_price = double.NaN;
            int num = S.Length;
            double[] ST0_vec = new double[num];
            double[] ST0_antithetic_vec = new double[num];
            double ST0_sum = 0;
            double ST0_antithetic_sum = 0;
            double[] fi_vec = new double[num];

            //double b = Normal.Sample(new MersenneTwister(), 0.0, 1.0);
            double[] c = new double[num];
            Normal.Samples(c, 0.0, 1.0);
            //Halton ht = new Halton(num, p + 1);
            //double[] c = ht.GetNextGaussian();
            for (var i = 0; i < num; i++)
            {
                double _fi = 0;
                for (var j = 0; j < num; j++)
                {
                    _fi = _fi + cholesky[i, j] * c[j];
                }
                fi_vec[i] = _fi;
                ST0_vec[i] = Wgt[i]*GetST0(S[i], T, b[i], v[i], _fi);
                ST0_antithetic_vec[i] = Wgt[i] * GetST0_Antithetic(S[i], T, b[i], v[i], _fi);
                ST0_sum = ST0_sum + ST0_vec[i];
                ST0_antithetic_sum = ST0_antithetic_sum + ST0_antithetic_vec[i];
            }
            if (cpflg == "c")
            {
                option_price = (Math.Max(ST0_sum - X, 0) + Math.Max(ST0_antithetic_sum - X, 0)) / 2.0;
            }
            else if (cpflg == "p")
            {
                option_price = (Math.Max(-ST0_sum + X, 0) + Math.Max(-ST0_antithetic_sum + X, 0)) / 2.0;
            }
            else
            {
                option_price = double.NaN;
            }
            return option_price;
        }
        public static double SpreadOptionMC(string cpflg, double[] Wgt, double[] S, double X, double T, double r, double[] b, double[] v, double[,] CorrMat, int SimTimes)
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
                option_price = option_price + GetPayOff(cpflg, Wgt, S, X, T, r, b, v, cholesky);
            }
            return option_price / SimTimes;
        }
    }
}
