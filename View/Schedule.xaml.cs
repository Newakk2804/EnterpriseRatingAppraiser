using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using EnterpriseRatingAppraiser.EF;
using LiveCharts;
using LiveCharts.Charts;
using LiveCharts.Definitions.Charts;
using LiveCharts.Helpers;
using LiveCharts.Wpf;
using LiveCharts.Wpf.Charts.Base;
using LiveCharts.Wpf.Components;

namespace EnterpriseRatingAppraiser.View
{
    /// <summary>
    /// Логика взаимодействия для Schedule.xaml
    /// </summary>
    public partial class Schedule : Window
    {
        EnterpriseRatingAppraiserContainer db = new EnterpriseRatingAppraiserContainer();
        Criterion Criterion = new Criterion();
        CompanyPerfomanceForm CompanyPerfomanceForm;
        public Schedule(Company company)
        {
            InitializeComponent();
            btn_Collapse.Click += (s, e) => WindowState = WindowState.Minimized;
            btn_Revenue.Click += (s, e) =>
            {
                ChartRevenue.LegendLocation = LegendLocation.Bottom;
                SeriesCollection series = new SeriesCollection();
                ChartValues<decimal> valueDecimal = new ChartValues<decimal>();
                List<string> dates = new List<string>();
                foreach (var item in db.CompanyPerfomanceSet)
                {
                    if (item.IdCompany == company.Id && item.Criterion.NameCriterion == "Выручка")
                    {
                        valueDecimal.Add(item.Value);
                        dates.Add(item.Month + " " + item.Year.ToString());
                    }
                }
                ChartRevenue.AxisX.Clear();
                ChartRevenue.AxisX.Add(new Axis()
                {
                    Title = "Даты",
                    Labels = dates
                });
                LineSeries line = new LineSeries();
                line.Title = $"{company.NameCompany}";
                line.Values = valueDecimal;
                series.Add(line);
                ChartRevenue.Series = series;
            };
            btn_Profit.Click += (s, e) =>
            {
                ChartRevenue.LegendLocation = LegendLocation.Bottom;
                SeriesCollection series = new SeriesCollection();
                ChartValues<decimal> valueDecimal = new ChartValues<decimal>();
                List<string> dates = new List<string>();
                foreach (var item in db.CompanyPerfomanceSet)
                {
                    if (item.IdCompany == company.Id && item.Criterion.NameCriterion == "Чистая прибыль")
                    {
                        valueDecimal.Add(item.Value);
                        dates.Add(item.Month + " " + item.Year.ToString());
                    }
                }
                ChartRevenue.AxisX.Clear();
                ChartRevenue.AxisX.Add(new Axis()
                {
                    Title = "Даты",
                    Labels = dates
                });
                LineSeries line = new LineSeries();
                line.Title = $"{company.NameCompany}";
                line.Values = valueDecimal;
                series.Add(line);
                ChartRevenue.Series = series;
            };
            btn_CapitalAndReserves.Click += (s, e) =>
            {
                ChartRevenue.LegendLocation = LegendLocation.Bottom;
                SeriesCollection series = new SeriesCollection();
                ChartValues<decimal> valueDecimal = new ChartValues<decimal>();
                List<string> dates = new List<string>();
                foreach (var item in db.CompanyPerfomanceSet)
                {
                    if (item.IdCompany == company.Id && item.Criterion.NameCriterion == "Капитал и резервы")
                    {
                        valueDecimal.Add(item.Value);
                        dates.Add(item.Month + " " + item.Year.ToString());
                    }
                }
                ChartRevenue.AxisX.Clear();
                ChartRevenue.AxisX.Add(new Axis()
                {
                    Title = "Даты",
                    Labels = dates
                });
                LineSeries line = new LineSeries();
                line.Title = $"{company.NameCompany}";
                line.Values = valueDecimal;
                series.Add(line);
                ChartRevenue.Series = series;
            };
            btn_Exit.Click += (s, e) => { CompanyPerfomanceForm = new CompanyPerfomanceForm(company); CompanyPerfomanceForm.Show(); this.Close(); };
        }
    }
}
