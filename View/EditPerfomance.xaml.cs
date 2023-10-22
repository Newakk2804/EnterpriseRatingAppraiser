using EnterpriseRatingAppraiser.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EnterpriseRatingAppraiser.View
{
    /// <summary>
    /// Логика взаимодействия для EditPerfomance.xaml
    /// </summary>
    public partial class EditPerfomance : Window
    {
        EnterpriseRatingAppraiserContainer db = new EnterpriseRatingAppraiserContainer();
        CompanyPerfomanceForm form;
        private List<string> AddMonth()
        {
            List<string> Months = new List<string>();
            Months.Add("Январь");
            Months.Add("Февраль");
            Months.Add("Март");
            Months.Add("Апрель");
            Months.Add("Май");
            Months.Add("Июнь");
            Months.Add("Июль");
            Months.Add("Август");
            Months.Add("Сентябрь");
            Months.Add("Октябрь");
            Months.Add("Ноябрь");
            Months.Add("Декабрь");
            return Months;
        }
        private List<int> AddYears()
        {
            List<int> Years = new List<int>();
            Years.Add(2018);
            Years.Add(2019);
            Years.Add(2020);
            Years.Add(2021);
            Years.Add(2022);
            Years.Add(2023);
            return Years;
        }
        public EditPerfomance(Company company, CompanyPerfomance companyPerfomance)
        {
            InitializeComponent();
            btn_Collapse.Click += (s, e) => WindowState = WindowState.Minimized;
            btn_Exit.Click += (s, e) => { form = new CompanyPerfomanceForm(company); form.Show(); this.Close(); };
            btn_Cancellation.Click += (s, e) => { form = new CompanyPerfomanceForm(company); form.Show(); this.Close(); };
            BlockNameCompany.Text = company.NameCompany;
            List<Criterion> criteria = new List<Criterion>();
            foreach (var item in db.CriterionSet)
            {
                criteria.Add(item);
            }
            BoxCriterion.ItemsSource = criteria;
            BoxCriterion.SelectedIndex = companyPerfomance.IdCriterion - 1;
            BoxMonth.ItemsSource = AddMonth();
            BoxMonth.SelectedItem = companyPerfomance.Month;
            BoxYear.ItemsSource = AddYears();
            BoxYear.SelectedItem = companyPerfomance.Year;
            BoxValue.Text = companyPerfomance.Value.ToString();
            btn_EditCriterion.Click += (s, e) =>
            {
                var date = new DateTime(Convert.ToInt32(BoxYear.Text), BoxMonth.SelectedIndex + 1, 1);
                if (date < company.DateOfFoundation)
                {
                    MessageBox.Show($"Невозможно выставить показатели за период времени когда компания не существовала!\nдата основания компании: {company.DateOfFoundation.ToShortDateString()}", "Внимание");
                }
                else if (date > DateTime.Today)
                {
                    MessageBox.Show($"Невозможно выставить показатели за период времени, который еще не наступил!\nСегодняшняя дата: {DateTime.Today.ToShortDateString()}", "Внимание");
                }
                else
                {
                    foreach (var item in db.CompanyPerfomanceSet)
                    {
                        if (item.Id == companyPerfomance.Id)
                        {
                            item.IdCriterion = BoxCriterion.SelectedIndex + 1;
                            item.Month = BoxMonth.Text;
                            item.Year = Convert.ToInt32(BoxYear.Text);
                            item.Value = Convert.ToDecimal(BoxValue.Text);
                        }
                    }
                    db.SaveChanges();
                    form = new CompanyPerfomanceForm(company);
                    form.Show();
                    this.Close();
                }
            };
        }
    }
}
