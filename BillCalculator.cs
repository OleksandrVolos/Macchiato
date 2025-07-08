using System;
using System.Collections.Generic;
using System.Linq;

namespace WpfApp1
{
    public static class BillCalculator
    {
        private const decimal GstRate = 0.05m;

        public static decimal CalculateNetTotal(IEnumerable<MenuItem> items)
        {
            return items.Sum(item => item.Price);
        }

        public static decimal CalculateGst(decimal netTotal)
        {
            return Math.Round(netTotal * GstRate, 2);
        }

        public static decimal CalculateTip(decimal netTotal, decimal tipValue, bool isPercentage)
        {
            if (isPercentage)
                return Math.Round(netTotal * tipValue / 100m, 2);
            return tipValue;
        }

        public static decimal CalculateTotal(decimal net, decimal tip, decimal gst)
        {
            return Math.Round(net + tip + gst, 2);
        }
    }
}
