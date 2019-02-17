using System;
using System.Data;

namespace Scorecard
{
    public static class Extensions
    {
        public static double Evaluate(this string expression)
        {
            return Convert.ToDouble(new DataTable().Compute(expression, string.Empty));
        }
    }
}
