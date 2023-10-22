using EnterpriseRatingAppraiser.EF;
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

namespace EnterpriseRatingAppraiser.View
{
    /// <summary>
    /// Логика взаимодействия для CompanyPerfomanceForm.xaml
    /// </summary>
    public partial class CompanyPerfomanceForm : Window
    {
        EnterpriseRatingAppraiserContainer db = new EnterpriseRatingAppraiserContainer();
        InfoCompany infoCompany;
        AddPerfomance AddPerfomance;
        EditPerfomance EditPerfomance;
        Schedule schedule;
        RatingLogic rat = new RatingLogic();
        public CompanyPerfomanceForm(Company company)
        {
            var _company = company;
            InitializeComponent();
            btn_Collapse.Click += (s, e) => WindowState = WindowState.Minimized;
            btn_Exit.Click += (s, e) => { infoCompany = new InfoCompany(_company); infoCompany.Show(); this.Close(); };
            btn_EditPerfomance.Click += (s, e) => 
            {
                if (Test.SelectedItem != null)
                {
                    EditPerfomance = new EditPerfomance(_company, (CompanyPerfomance)Test.SelectedItem); EditPerfomance.Show(); this.Close();
                }
                else
                {
                    MessageBox.Show("Критерий не выбран", "Внимание");
                }
            };
            btn_AddNewPerfomance.Click += (s, e) => { AddPerfomance = new AddPerfomance(_company); AddPerfomance.Show(); this.Close(); };
            List<CompanyPerfomance> companies = new List<CompanyPerfomance>();
            foreach (var item in db.CompanyPerfomanceSet) 
            {
                if (item.IdCompany == _company.Id)
                {
                    companies.Add(item);
                }
            }
            Test.ItemsSource = companies;
            BlockNameCompany.Text = _company.NameCompany;
            btn_DeletePerfomance.Click += (s, e) =>
            {
                if (Test.SelectedItem != null)
                {
                    if (MessageBox.Show("Вы уверены, что хотите удалить показатель?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        db.CompanyPerfomanceSet.Remove((CompanyPerfomance)Test.SelectedItem);
                        db.SaveChanges();
                        List<CompanyPerfomance> companies1 = new List<CompanyPerfomance>();
                        foreach (var item in db.CompanyPerfomanceSet)
                        {
                            if (item.IdCompany == _company.Id)
                            {
                                companies1.Add(item);
                            }
                        }
                        Test.ItemsSource = companies1;
                    }
                }
                else
                {
                    MessageBox.Show("Критерий не выбран", "Внимание");
                }
            };
            btn_Schedule.Click += (s, e) => { schedule = new Schedule(_company); schedule.Show(); this.Close(); };
            List<decimal> companyRating = new List<decimal>();
            List<string> monthRating = new List<string>();
            foreach (var item in db.CompanyPerfomanceSet)
            {
                if (item.IdCompany == company.Id)
                {
                    monthRating.Add(item.Month);
                }
            }
            List<int> yearRating = new List<int>();
            foreach (var item in db.CompanyPerfomanceSet)
            {
                if (item.IdCompany == company.Id)
                {
                    yearRating.Add(item.Year);
                }
            }
            try
            {
                for (int i = 0; i < monthRating.Count; i++)
                {
                    if (i + 1 < yearRating.Count)
                    companyRating.Add(rat.GetCompanyRaiting(monthRating[i], yearRating[i], monthRating[i + 1], yearRating[i + 1]));
                }
                decimal result = 0;
                foreach (var item in companyRating)
                {
                    result += item;
                }
                result /= companyRating.Count;
                BlockRatingCompany.Text = Convert.ToString(string.Format("{0:F2}", result));
            }
            catch (Exception)
            {
                BlockRatingCompany.Text = string.Empty;
            }
        }
    }
}
