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
using System.Windows.Navigation;
using System.Windows.Shapes;
using EnterpriseRatingAppraiser;
using EnterpriseRatingAppraiser.EF;

namespace EnterpriseRatingAppraiser.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class AuthorizationForm : Window
    {
        EnterpriseRatingAppraiserContainer db = new EnterpriseRatingAppraiserContainer();
        RegistrationForm RForm;
        MainForm mainForm;
        public AuthorizationForm()
        {
            InitializeComponent();
            string login = string.Empty;
            string password = string.Empty;
            btn_Exit.Click += (s, e) => this.Close();
            btn_Collapse.Click += (s, e) => WindowState = WindowState.Minimized;
            btn_Cancellation.Click += (s, e) => this.Close();
            btn_Registration.Click += (s, e) => { RForm = new RegistrationForm(); RForm.Show(); this.Close(); };
            btn_Authorization.Click += (s, e) => 
            {
                foreach (var item in db.UserSet)
                {
                    if (BoxLogin.Text == item.Login)
                    {
                        login = item.Login;
                        break;
                    }
                    else
                    {
                        login = " ";
                    }
                }
                foreach (var item in db.UserSet)
                {
                    if (BoxPassword.Password == item.Password)
                    {
                        password = item.Password;
                        break;
                    }
                    else
                    {
                        password = " ";
                    }
                }

                if (BoxLogin.Text == login && BoxPassword.Password == password)
                {
                    mainForm = new MainForm();
                    mainForm.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль!", "Внимание");
                }
            };
        }
    }
}
