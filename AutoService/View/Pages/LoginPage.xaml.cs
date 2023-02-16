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
using AutoService.Models;

namespace AutoService.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        Core db = new Core();
        public LoginPage()
        {
            InitializeComponent();

        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            User currentUser = db.context.User.Where(x => x.Login == TBoxLogin.Text).FirstOrDefault();
            if (currentUser != null)
            {
                if (currentUser.Login == TBoxLogin.Text && currentUser.Password == PBoxPassword.Password)
                {
                    App.CurrentUser = currentUser;
                    this.NavigationService.Navigate(new ServicePage());
                }
                else 
                {
                    MessageBox.Show("Неверный пароль");
                }
            }
            else
            {
                MessageBox.Show("Нет аккаунта с таким логином");
            }
        }

        private void BtnReg_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new RegPage());
        }
    }
}
