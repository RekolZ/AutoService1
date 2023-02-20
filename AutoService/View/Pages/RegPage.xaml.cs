using AutoService.Models;
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
using System.Text.RegularExpressions;

namespace AutoService.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegPage.xaml
    /// </summary>
    public partial class RegPage : Page
    {
        string genderCode;
        Core db = new Core();
        public RegPage()
        {
            InitializeComponent();
        }

        private void RegButton_Click(object sender, RoutedEventArgs e)
        {
            if (GenderCombo.SelectedIndex == 0)
                genderCode = "1";
            else
                genderCode = "2";

            string regex = @"(?=.*[0 - 9])(?=.*[!@#$%^&*])(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z!@#$%^&*]{6,}";
            if (Regex.IsMatch(PasswordBox.Password, regex, RegexOptions.IgnoreCase))
            {
                var client = new Client()
                {
                    LastName = LastNameTBox.Text,
                    FirstName = FirstNameTBox.Text,
                    Patronymic = PatronymicTBox.Text,
                    Phone = PhoneTBox.Text,
                    Email = EmailTBox.Text,
                    PhotoPath = " ",
                    GenderCode = genderCode,
                    Birthday = BirthdayCalendar.SelectedDate,
                    RegistrationDate = DateTime.Now,
                };

                var user = new User()
                {
                    Login = LoginTBox.Text,
                    Password = PasswordBox.Password,
                    RoleId = 2
                };
                //он делает id нулевым и из за этого ошибка
                db.context.User.Add(user);
                db.context.Client.Add(client);
                db.context.SaveChanges();
                this.NavigationService.Navigate(new LoginPage());
            }
            else
                MessageBox.Show("ВЫ НУБЫ");
           
        }
    }
}
