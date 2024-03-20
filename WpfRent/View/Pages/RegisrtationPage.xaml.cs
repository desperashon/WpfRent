using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using WpfRent.Models;

namespace WpfRent.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegistrationPage.xaml
    /// </summary>
    public partial class RegistrationPage : Page
    {
        public RegistrationPage()
        {
            InitializeComponent();
            LocationCmb.DisplayMemberPath = "name";
            LocationCmb.SelectedValuePath = "id";
            LocationCmb.ItemsSource = App.context.Location.ToList();
        }

        private void RegistrationBtn_Click(object sender, RoutedEventArgs e)
        {
            string mes = "";

            if (string.IsNullOrWhiteSpace(FirstNameTb.Text))
            {
                mes += "Введите имя\n";
            }
            else if (!Regex.IsMatch(FirstNameTb.Text, @"^[\p{L}\p{M}' \.\-]+$"))
            {
                mes += "Имя должно содержать только буквы русского алфавита\n";
            }

            if (string.IsNullOrWhiteSpace(LastNameTb.Text))
            {
                mes += "Введите фамилию\n";
            }
            else if (!Regex.IsMatch(LastNameTb.Text, @"^[\p{L}\p{M}' \.\-]+$"))
            {
                mes += "Фамилия должна содержать только буквы русского алфавита\n";
            }

            if (string.IsNullOrWhiteSpace(EmailTb.Text))
            {
                mes += "Введите почту\n";
            }
            else if (!IsValidEmail(EmailTb.Text))
            {
                mes += "Введите корректный email\n";
            }

            if (string.IsNullOrWhiteSpace(PasswordPb.Password))
            {
                mes += "Введите пароль\n";
            }
            else if (PasswordPb.Password.Length < 6)
            {
                mes += "Пароль должен содержать минимум 6 символов\n";
            }

            if (mes != "")
            {
                MessageBox.Show(mes);
                return;
            }

            Users user = new Users()
            {
                email = EmailTb.Text,
                first_name = FirstNameTb.Text,
                password = PasswordPb.Password,
                last_name = LastNameTb.Text,
                Location1 = LocationCmb.ItemsSource as Location,
            };

            App.context.Users.Add(user);
            App.enteredUser = user;
            App.context.SaveChanges();

            MessageBox.Show("Успешная регистрация!");
            NavigationService.Navigate(new RentSearchPage(user));
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}
