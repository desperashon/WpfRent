using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using WpfRent.Models;

namespace WpfRent.View.Pages
{
    public partial class FiltrPage : Page
    {
        private readonly RentGavrilinEntities _context;
        private Page _previousPage;
        public FiltrPage(Page previousPage)
        {
            InitializeComponent();
            _context = new RentGavrilinEntities();
            LoadLocations();
            LoadHouseTypes();
            _previousPage = previousPage;
        }

        private void LoadLocations()
        {
            var locations = _context.Location.ToList();
            LocationComboBox.ItemsSource = locations;
            LocationComboBox.DisplayMemberPath = "name";
        }

        private void LoadHouseTypes()
        {
            var houseTypes = _context.Characteristics.ToList();
            HouseTypeComboBox.ItemsSource = houseTypes;
            HouseTypeComboBox.DisplayMemberPath = "name";
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            decimal maxPrice = (decimal)PriceSlider.Value;
            string selectedLocation = LocationComboBox.SelectedValue as string;
            string selectedHouseType = (HouseTypeComboBox.SelectedValue as Characteristics)?.name;

            if (_previousPage is RentSearchPage rentSearchPage)
            {
                rentSearchPage.ApplyFilter(maxPrice, selectedLocation, selectedHouseType);
                NavigationService.Navigate(rentSearchPage);
            }
            else
            {
                MessageBox.Show("Ошибка: Предыдущая страница не является RentSearchPage");
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            PriceSlider.Value = 0;
            LocationComboBox.SelectedIndex = -1;
            HouseTypeComboBox.SelectedIndex = -1;
            MessageBox.Show("Сброс фильтра");
        }
    }
}
