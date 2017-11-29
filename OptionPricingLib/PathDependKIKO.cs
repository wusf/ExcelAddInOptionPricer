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
    class PathDependKIKO
    {
        public static double Main()
        {
            Matrix<double> z = Matrix<double>.Build.Random(30, 40);
            z.Row(1).ToString();
            return 0;
        }



    }
}
