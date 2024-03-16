using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SahibindenCrawlingApp.Helpers;

namespace SahibindenCrawlingApp
{
    public static class CalculateAverage
    {
        public static void Calculate(List<decimal> allAverages)
        {
            decimal average = allAverages.Average();
            Console.WriteLine($"Average of all averages: {average}");
        }
    }
}