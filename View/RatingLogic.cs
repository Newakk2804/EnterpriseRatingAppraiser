using EnterpriseRatingAppraiser.EF;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnterpriseRatingAppraiser.View
{
    public class RatingLogic
    {
        EnterpriseRatingAppraiserContainer db = new EnterpriseRatingAppraiserContainer();
        private decimal GetPercentDifferenceBetweenTwoNumbers(decimal value1, decimal value2)
        {
            return Math.Abs((value1 - value2) / ((value1 + value2) / 2)) * 100;
        }

        /// <summary>
        /// Выручка
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public decimal GetRevenueInSpecificYear(string month, int year)
        {
                var revenue = db.CompanyPerfomanceSet
                    .Include(_ => _.Criterion)
                    .Where(_ =>_.Month == month &&
                    _.Year == year &&
                    _.Criterion.NameCriterion == "Выручка")
                    .FirstOrDefault();

                return revenue == null ? 0 : revenue.Value;
        }

        /// <summary>
        /// Прирост выручки в %
        /// </summary>
        /// <param name="value1">прошлый показатель</param>
        /// <param name="value2">настоящий показатель</param>
        /// <returns></returns>
        public decimal GetRevenueGrowthInTimestamp(string month1, int year1, string month2, int year2)
        {
            var revenue1 = GetRevenueInSpecificYear(month1, year1);
            var revenue2 = GetRevenueInSpecificYear(month2, year2);
            var revenueGrowth = GetPercentDifferenceBetweenTwoNumbers(revenue1, revenue2);
            return revenueGrowth;
        }

        /// <summary>
        /// Чистая прибыль
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public decimal GetNetProfitInSpecificYear(string month, int year)
        {
                var netProfit = db.CompanyPerfomanceSet
                    .Include(_ => _.Criterion)
                    .Where(_ =>_.Month == month &&
                    _.Year == year &&
                    _.Criterion.NameCriterion == "Чистая прибыль")
                    .FirstOrDefault();

                return netProfit == null ? 0 : netProfit.Value;
        }

        /// <summary>
        /// Прирост чистой прибыли в %
        /// </summary>
        /// <param name="value1">прошлый показатель</param>
        /// <param name="value2">настоящий показатель</param>
        /// <returns></returns>
        public decimal GetNetProfitGrowthInTimestamp(string month1, int year1, string month2, int year2)
        {
            var netProfit1 = GetNetProfitInSpecificYear(month1, year1);
            var netProfit2 = GetNetProfitInSpecificYear(month2, year2);
            var netProfitGrowth = GetPercentDifferenceBetweenTwoNumbers(netProfit1, netProfit2);
            return netProfitGrowth;
        }

        /// <summary>
        /// Капитал и резервы
        /// </summary>
        /// <param name="month"></param>
        /// <param name="year"></param>
        /// <returns></returns>
        public decimal GetCapitalAndReservesInSpecificYear(string month, int year)
        {
                var capitalAndReserves = db.CompanyPerfomanceSet
                    .Include(_ => _.Criterion)
                    .Where(_ => _.Month == month && _.Year == year &&
                    _.Criterion.NameCriterion == "Капитал и резервы")
                    .FirstOrDefault();

                return capitalAndReserves == null ? 0 : capitalAndReserves.Value;
        }

        /// <summary>
        /// Прирост капитала и резервов в %
        /// </summary>
        /// <param name="value1">прошлый показатель</param>
        /// <param name="value2">настоящий показатель</param>
        /// <returns></returns>
        public decimal GetCapitalAndReservesGrowthInTimestamp(string month1, int year1, string month2, int year2)
        {
            var capitalAndReserves1 = GetCapitalAndReservesInSpecificYear(month1, year1);
            var capitalAndReserves2 = GetCapitalAndReservesInSpecificYear(month2, year2);
            var capitalAndReservesGrowth = GetPercentDifferenceBetweenTwoNumbers(capitalAndReserves1, capitalAndReserves2);
            return capitalAndReservesGrowth;
        }

        /// <summary>
        /// Динамика выручки на капитал в %
        /// </summary>
        /// <param name="value1">выручка</param>
        /// <param name="value2">капитал и резервы</param>
        /// <returns></returns>
        public decimal GetEquityDynamicsInTimestamp(string month1, int year1, string month2, int year2)
        {
            var revenue = GetRevenueGrowthInTimestamp(month1, year1, month2, year2);
            var capitalAndReserves = GetCapitalAndReservesGrowthInTimestamp(month1, year1, month2, year2);

            var equityDynamics = GetPercentDifferenceBetweenTwoNumbers(revenue, capitalAndReserves);
            return equityDynamics;
        }

        /// <summary>
        /// Рейтинг предприятия за указанные месяцы
        /// </summary>
        /// <param name="value1">предыдущий месяц</param>
        /// <param name="value2">настоящий месяц</param>
        /// <returns></returns>
        public decimal GetCompanyRaiting(string month1, int year1, string month2, int year2)
        {
            var equityDynamics = GetEquityDynamicsInTimestamp(month1, year1, month2, year2);
            var raiting = db.RatingSet
                .Where(_ => (int)equityDynamics >= _.ValueFrom &&
                equityDynamics <= _.ValueTo)
                .FirstOrDefault();

            return raiting == null ? 0 : raiting.Value;
        }
    }
}
