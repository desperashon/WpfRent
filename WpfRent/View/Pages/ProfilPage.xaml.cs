using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WpfRent.Models;

namespace WpfRent.View.Pages
{
    /// <summary>
    /// Interaction logic for ProfilPage.xaml
    /// </summary>
    public partial class ProfilPage : Page
    {
        public ProfilPage()
        {
            InitializeComponent();
            LocationTb.DisplayMemberPath = "name";
            LocationTb.SelectedValuePath = "id";
            LocationTb.ItemsSource = App.context.Location.ToList();

            var user = App.context.Users.FirstOrDefault(u => u.user_id == App.enteredUser.user_id);

            if (user != null)
            {
                LastnameTb.Text = user.last_name ?? "";
                FirstNameTB.Text = user.first_name ?? "";
                PatronymicTb.Text = user.middle_name ?? "";
                EmailTb.Text = user.email ?? "";
                PasswordPb.Password = user.password ?? "";

               
              
                LocationTb.SelectedItem = user.Location1?.name ?? "";
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var userToUpdate = App.context.Users.FirstOrDefault(u => u.user_id == App.enteredUser.user_id);

                if (userToUpdate != null)
                {
                    userToUpdate.last_name = LastnameTb.Text;
                    userToUpdate.first_name = FirstNameTB.Text;
                    userToUpdate.middle_name = PatronymicTb.Text;
                    userToUpdate.email = EmailTb.Text;
                    userToUpdate.password = PasswordPb.Password;

                 
                    var selectedLocationName = LocationTb.SelectedItem?.ToString() ?? "";
                    var location = App.context.Location.FirstOrDefault(l => l.name == selectedLocationName);
                    userToUpdate.Location1 = location;

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
