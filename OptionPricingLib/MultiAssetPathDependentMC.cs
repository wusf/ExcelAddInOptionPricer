using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.Distributions;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.Random;
using MathNet.Numerics.LinearAlgebra.Double;
using System;

namespace OptionPricingLib
{
    public class MultiAssetPathDependentMC
    {
        public static double[] GetPath(double S, double X, double H, double T, double b, double v, double[] fi)
        {
            int num_days = (int)(T * 250);
            double pt = 0;
            int mark = 0;
            double[] _price = new double[num_days];
            double dt = 1 / 250.0;
            int i;
            _price[0] = S;
            for (i=1; i<num_days; i++)
            {
                _price[i] =_price[i - 1] * Math.Pow(Math.E, (b - 0.5 * v * v) * dt + v * Math.Sqrt(dt) * fi[i]);
                if (_price[i] >= H)
                {
                    mark = 1;
                    break;
                }
            }
            if (mark == 1)
            {
                pt = 0;
            }
            else
            {
                pt = _price[num_days - 1];
            }

            double[] res = new double[2];
            res[0] = mark;
            res[1] = pt;
            return res;
        }

        public static double GetPayOff(string cpflg, double[] Wgt,double[] S, double[] X, double[] H, double k, double T, double r, double[] b, double[] v, Matrix<double> cholesky, int p, int sims)
        {
            double price = double.NaN;
            int num = S.Length;
            int days = (int)(T * 365);
            double[,] fi_mat = new double[days, num];
            double[] fi_list = new double[days];
            double[] pt = new double[num];
            double[] mk = new double[num];
            double[] _pt = new double[2];
            double[] po = new double[num];

            //double b = Normal.Sample(new MersenneTwister(), 0.0, 1.0);
            double[] c = new double[num];
            //Normal.Samples(c, 0.0, 1.0);

            //double[] c = new double[num];
            for (int d = 0; d < days; d++)
            {
                Normal.Samples(c, 0.0, 1.0);
                //Halton ht = new Halton(num, p + d*sims + 1);
                //c = ht.GetNextGaussian();
                for (var i = 0; i < num; i++)
                {
                    double _fi = 0;
                    for (var j = 0; j < num; j++)
                    {
                        _fi = _fi + cholesky[i, j] * c[j];
                    }
                    fi_mat[d, i] = _fi;
                }
            }

            for (int nn = 0; nn < num; nn++)
            {

                for (int fn = 0; fn < days; fn++)
                {
                    fi_list[fn] = fi_mat[fn, nn];
                }

                _pt = GetPath(S[nn], X[nn], H[nn], T, b[nn], v[nn], fi_list);
                pt[nn] = _pt[1];
                mk[nn] = _pt[0];
                if (cpflg.Equals("c"))
                {
                    po[nn] = pt[nn] - X[nn];
                }
                if (cpflg.Equals("p"))
                {
                    po[nn] = X[nn] - pt[nn];
                }
            }


            if (mk.Max() == 1)
            {
               price = k;
            }
            else
            {
                    price = Math.Max(po.Max(),0);
            }
            return price;
        }
        public static double BestOfCallSFMC(string cpflg, double[] Wgt, double[] S, double[] X, double[] H ,double k,double T, double r, double[] b, double[] v, double[,] CorrMat, int SimTimes)
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
                option_price = option_price + GetPayOff(cpflg, Wgt, S, X, H, k,T, r, b, v, cholesky, i, SimTimes);
            }
            return option_price / SimTimes;
        }
    }
}
