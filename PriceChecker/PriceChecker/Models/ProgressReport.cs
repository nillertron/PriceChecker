using System;
using System.Collections.Generic;
using System.Text;

namespace PriceChecker.Models
{
    public class ProgressReport
    {
        private static ProgressReport instans = new ProgressReport();
        public static List<Vare> VareListe { get; set; } = new List<Vare>();

        public static double Progress { get; set; } = 0;
        public static double TotalVare { get; set; } = 0;

        public static double CurrentVare { get; set; } = 0;
        private ProgressReport()
        {

        }

        public static ProgressReport GetInstans()
        {
            return instans;
        }


    }
}
