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
    /// Логика взаимодействия для MainForm.xaml
    /// </summary>
    public partial class MainForm : Window
    {
        EnterpriseRatingAppraiserContainer db = new EnterpriseRatingAppraiserContainer();
        AuthorizationForm authForm;
        AddCompanyForm addCompanyForm;
        EditCompany editCompanyForm;
        InfoCompany infoCompany;
        public MainForm()
        {
            InitializeComponent();
            btn_Collapse.Click += (s, e) => WindowState = WindowState.Minimized;
            List<Company> companies = new List<Company>();
            foreach (var item in db.CompanySet)
            {
                companies.Add(item);
            }
            Test.ItemsSource = companies;
            btn_Exit.Click += (s, e) => { authForm = new AuthorizationForm(); authForm.Show(); this.Close(); };
            btn_AddNewCompany.Click += (s, e) => { addCompanyForm = new AddCompanyForm(); addCompanyForm.Show(); this.Close(); };
            btn_EditCompany.Click += (s, e) =>
            {
                if(Test.SelectedItem != null)
                {
                    editCompanyForm = new EditCompany((Company)Test.SelectedItem); editCompanyForm.Show(); this.Close();
                }
                else
                {
                    MessageBox.Show("Компания не выбрана!", "Внимание");
                }
            };
            btn_DeleteCompany.Click += (s, e) =>
            {
                if (Test.SelectedItem != null)
                {
                    if (MessageBox.Show("Вы уверены, что хотите удалить предприятие?", "Внимание", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        db.CompanySet.Remove((Company)Test.SelectedItem);
                        db.SaveChanges();
                        List<Company> companies1 = new List<Company>();
                        foreach (var item in db.CompanySet)
                        {
                            companies1.Add(item);
                        }
                        Test.ItemsSource = companies1;
                    }
                }
                else
                {
                    MessageBox.Show("Предприятие не выбрана!", "Внимание");
                }
            };
            Test.MouseDoubleClick += (s, e) => { infoCompany = new InfoCompany((Company)Test.SelectedItem); infoCompany.Show(); this.Close(); };
        }
    }
}
