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
using WpfRent.Models;

namespace WpfRent.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public static Announcement filtrAnnouncement;
        public LoginPage()
        {
            InitializeComponent();
        }

        private void AuthorizationBtn_Click(object sender, RoutedEventArgs e)
        {
            Users user = App.context.Users
                   .FirstOrDefault(w => w.password == PasswordPb.Password && w.email == LoginTb.Text);

            if (user != null)
            {

                App.enteredUser = user;
                NavigationService.Navigate(new RentSearchPage(user));

            }
            else
            {
                MessageBox.Show("Неправильный логин или пароль!");
                PasswordPb.Clear();
                LoginTb.Clear();
            }
        }

        private void RegistretionBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrationPage());
        }
    }
}
