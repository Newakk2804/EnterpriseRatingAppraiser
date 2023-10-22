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
using static System.Net.Mime.MediaTypeNames;

namespace EnterpriseRatingAppraiser.View
{
    /// <summary>
    /// Логика взаимодействия для EditCompany.xaml
    /// </summary>
    public partial class EditCompany : Window
    {
        EnterpriseRatingAppraiserContainer db = new EnterpriseRatingAppraiserContainer();
        MainForm mainForm;
        public EditCompany(Company company)
        {
            InitializeComponent();
            btn_Collapse.Click += (s, e) => WindowState = WindowState.Minimized;
            List<TypeOfEconomicActivity> economicActivities = new List<TypeOfEconomicActivity>();
            foreach (var item in db.TypeOfEconomicActivitySet)
            {
                economicActivities.Add(item);
            }
            BoxTypeOfEconomicActivity.ItemsSource = economicActivities;
            List<OwnershipForm> ownerships = new List<OwnershipForm>();
            foreach (var item in db.OwnershipFormSet)
            {
                ownerships.Add(item);
            }
            BoxOwnershipForm.ItemsSource = ownerships;
            btn_Exit.Click += (s, e) => { mainForm = new MainForm(); mainForm.Show(); this.Close(); };
            btn_Cancellation.Click += (s, e) => { mainForm = new MainForm(); mainForm.Show(); this.Close(); };
            BoxName.Text = company.NameCompany.ToString();
            PickerDate.Text = company.DateOfFoundation.ToString();
            BoxDescription.Text = company.CompanyDescription.ToString();
            BoxTypeOfEconomicActivity.SelectedIndex = company.IdTypeOfEconomicActivity - 1;
            BoxOwnershipForm.SelectedIndex = company.IdOwnershipForm - 1;
            btn_Confirm.Click += (s, e) =>
            {
                foreach (var item in db.CompanySet)
                {
                    if (item.Id == company.Id)
                    {
                        item.NameCompany = BoxName.Text;
                        item.DateOfFoundation = DateTime.Parse(PickerDate.Text);
                        item.CompanyDescription = BoxDescription.Text;
                        item.IdTypeOfEconomicActivity = BoxTypeOfEconomicActivity.SelectedIndex + 1;
                        item.IdOwnershipForm = BoxOwnershipForm.SelectedIndex + 1;
                    }
                }
                db.SaveChanges();
                mainForm = new MainForm();
                mainForm.Show();
                this.Close();
            };
        }
    }
}
