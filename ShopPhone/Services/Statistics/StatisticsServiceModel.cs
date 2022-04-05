namespace ShopPhone.Services.Statistics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class StatisticsServiceModel
    {
        public int TotalUsers { get; set; }

        public double TotalPrice { get; set; }

        public int TotalPhones { get; set; }
    }
}
