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
    /// Логика взаимодействия для InfoCompany.xaml
    /// </summary>
    public partial class InfoCompany : Window
    {
        EnterpriseRatingAppraiserContainer db = new EnterpriseRatingAppraiserContainer();
        MainForm mainForm;
        CompanyPerfomanceForm companyPerfomanceForm;
        public InfoCompany(Company company)
        {
            var _company = company;
            InitializeComponent();
            btn_Collapse.Click += (s, e) => WindowState = WindowState.Minimized;
            btn_Exit.Click += (s, e) => { mainForm = new MainForm(); mainForm.Show(); this.Close(); };
            btn_CompanyPerfomance.Click += (s, e) => { companyPerfomanceForm = new CompanyPerfomanceForm(_company); companyPerfomanceForm.Show(); this.Close(); };
            BlockNameCompany.Text = company.NameCompany;
            BlockDateOfFoundation.Text = company.DateOfFoundation.ToString("dd.MM.yyyy");
            BlockTypeOfEconomicActivity.Text = company.TypeOfEconomicActivity.Name;
            BlockOwnershipForm.Text = company.OwnershipForm.Name;
            BlockDescriptionCompany.Text = company.CompanyDescription;
        }
    }
}
