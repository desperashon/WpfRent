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

namespace WpfRent.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для ProfilPage.xaml
    /// </summary>
    public partial class ProfilPage : Page
    {
        public ProfilPage()
        {
            InitializeComponent();

            var user = App.context.Users.FirstOrDefault(u => u.user_id == App.enteredUser.user_id);

            // Проверяем, что пользователь не равен null
            if (user != null)
            {
                // Присваиваем значения текстовым полям, если они не равны null
                LastnameTb.Text = user.last_name ?? "";
                FirstNameTB.Text = user.first_name ?? "";
                PatronymicTb.Text = user.middle_name ?? "";
                EmailTb.Text = user.email ?? "";
                PasswordPb.Password = user.password ?? "";
                LocationTb.Text = user.location ?? "";


            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получаем экземпляр пользователя из контекста данных
                var userToUpdate = App.context.Users.FirstOrDefault(u => u.user_id == App.enteredUser.user_id);

                // Обновляем данные пользователя
                if (userToUpdate != null)
                {
                    userToUpdate.last_name = LastnameTb.Text;
                    userToUpdate.first_name = FirstNameTB.Text;
                    userToUpdate.middle_name = PatronymicTb.Text;
                    userToUpdate.email = EmailTb.Text;
                    userToUpdate.password = PasswordPb.Password;
                    userToUpdate.location = LocationTb.Text;

                    // Сохраняем изменения в базе данных
                    App.context.SaveChanges();

                    MessageBox.Show("Изменения успешно сохранены.");
                }
                else
                {
                    MessageBox.Show("Пользователь не найден.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении изменений: {ex.Message}");
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}
