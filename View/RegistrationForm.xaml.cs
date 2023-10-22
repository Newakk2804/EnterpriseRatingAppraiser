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
    /// Логика взаимодействия для RegistrationForm.xaml
    /// </summary>
    public partial class RegistrationForm : Window
    {
        EnterpriseRatingAppraiserContainer db = new EnterpriseRatingAppraiserContainer();
        AuthorizationForm AForm;
        public RegistrationForm()
        {

            InitializeComponent();
            string login = string.Empty;
            string password = string.Empty;
            btn_Collapse.Click += (s, e) => WindowState = WindowState.Minimized;
            btn_Exit.Click += (s, e) => { AForm = new AuthorizationForm(); AForm.Show(); this.Close(); };
            btn_Cancellation.Click += (s, e) => { AForm = new AuthorizationForm(); AForm.Show(); this.Close(); };
            btn_Registration.Click += (s, e) =>
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
                        login = string.Empty;
                    }
                }
                if (login != string.Empty)
                {
                    MessageBox.Show("Такой пользователь существует!", "Внимание");
                }
                else
                {
                    db.UserSet.Add(new User() { Login = BoxLogin.Text, Password = BoxPassword.Password });
                    db.SaveChanges();
                    MessageBox.Show("Вы успешно зарегистрировались!", "Внимание");
                    AForm = new AuthorizationForm();
                    AForm.Show();
                    this.Close();
                }
            };
        }
    }
}
