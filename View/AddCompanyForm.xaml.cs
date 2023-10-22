using EnterpriseRatingAppraiser.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
    /// Логика взаимодействия для AddCompanyForm.xaml
    /// </summary>
    public partial class AddCompanyForm : Window
    {
        EnterpriseRatingAppraiserContainer db = new EnterpriseRatingAppraiserContainer();
        MainForm mainForm;
        public AddCompanyForm()
        {
            InitializeComponent();
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
            btn_Collapse.Click += (s, e) => WindowState = WindowState.Minimized;
            bool Flag = false;
            btn_AddCompany.Click += (s, e) =>
            {
                if (BoxName.Text != string.Empty)
                {
                    foreach (var item in db.CompanySet)
                    {
                        if (BoxName.Text == item.NameCompany)
                        {
                            Flag = true;
                        }
                    }
                    if (Flag == false)
                    {
                        db.CompanySet.Add(new Company() { NameCompany = BoxName.Text, DateOfFoundation = DateTime.Parse(PickerDate.Text), CompanyDescription = BoxDescription.Text, IdTypeOfEconomicActivity = BoxTypeOfEconomicActivity.SelectedIndex + 1, IdOwnershipForm = BoxOwnershipForm.SelectedIndex + 1 });
                        db.SaveChanges();
                        mainForm = new MainForm();
                        mainForm.Show();
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Компания с таким именем уже существует!","Внимание");
                    }
                }
                else
                {
                    MessageBox.Show("Введите название компании!", "Внимание");
                }
            };
        }
    }
}
