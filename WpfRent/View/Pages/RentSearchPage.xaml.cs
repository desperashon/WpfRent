using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
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
    /// Логика взаимодействия для RentSearchPage.xaml
    /// </summary>
    public partial class RentSearchPage : Page
    {
   
        public RentSearchPage(Users enteredUser)
        {
            InitializeComponent();
            basketLb.ItemsSource = App.context.Announcement.ToList();
            GeolocationTb.Text = $"Ваше местоположение: {enteredUser.location}!";
        }

        private void basketLb_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (basketLb.SelectedItem != null)
            {
                // Получаем выбранный элемент
                Announcement selectedVacancy = (Announcement)basketLb.SelectedItem;

                

                InfoRentPage extendedWindow = new InfoRentPage(selectedVacancy);

                // Открываем новое окно
                NavigationService.Navigate(extendedWindow);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProfilPage());
        }

        private void FiltrBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FiltrPage());
        }

        private void SearchTb_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchTb.Text.Trim();

            if (string.IsNullOrEmpty(searchText))
            {

                basketLb.ItemsSource = App.context.Announcement.ToList();
            }
            else
            {

                var filteredVacancies = App.context.Announcement.Where(v => SqlFunctions.PatIndex("%" + searchText + "%", v.title) > 0).ToList();
                basketLb.ItemsSource = filteredVacancies;
            }
        }
    }
}
