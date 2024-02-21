using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Логика взаимодействия для RegisrtationPage.xaml
    /// </summary>
    public partial class RegisrtationPage : Page
    {


        public RegisrtationPage()
        {
            InitializeComponent();
        }

        public static Announcement filtrAnnouncement;

        private void RegistretionBtn_Click(object sender, RoutedEventArgs e)
        {
            string mes = "";

            // Проверка имени
            if (string.IsNullOrWhiteSpace(NameTb.Text))
            {
                mes += "Введите имя\n";
            }
            else if (!Regex.IsMatch(NameTb.Text, @"^[\p{L}\p{M}' \.\-]+$"))
            {
                mes += "Имя должно содержать только буквы русского алфавита\n";
            }

            // Проверка email
            if (string.IsNullOrWhiteSpace(LoginTb.Text))
            {
                mes += "Введите почту \n";
            }
            else if (!IsValidEmail(LoginTb.Text))
            {
                mes += "Введите корректный email\n";
            }

            // Проверка пароля
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

            // Создание нового пользователя
            Users user = new Users()
            {
                email = LoginTb.Text,
                first_name = NameTb.Text,
                password = PasswordPb.Password,
                last_name = FirstnNameTb.Text,
            };

            // Добавление пользователя в контекст и сохранение изменений
            App.context.Users.Add(user);
            App.enteredUser = user;
            App.context.SaveChanges();
            
            MessageBox.Show("Успешная регистрация!");
            NavigationService.Navigate(new RentSearchPage(user));
        }

        // Проверка корректности email с использованием регулярного выражения
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
