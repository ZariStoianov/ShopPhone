namespace ShopPhone.Services.Statistics
{
    using ShopPhone.Data;
    using System;
    using System.Linq;

    public class StatisticsService : IStatisticsService
    {

        private readonly ApplicationDbContext data;

        public StatisticsService(ApplicationDbContext data)
        {
            this.data = data;
        }

        public StatisticsServiceModel Total()
        {
            var totalPhones = this.data.Phones.Count();
            var totalUsers = this.data.Users.Count();

            return new StatisticsServiceModel
            {
                TotalPhones = totalPhones,
                TotalUsers = totalUsers
            };
        }
    }
}
