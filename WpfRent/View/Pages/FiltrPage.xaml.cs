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
            // Получение списка локаций из базы данных
            var locations = _context.Location.ToList();

            // Добавление локаций в ComboBox
            LocationComboBox.ItemsSource = locations;
            LocationComboBox.DisplayMemberPath = "name";
        }

        private void LoadHouseTypes()
        {
            // Получение списка типов домов из базы данных
            var houseTypes = _context.Characteristics.ToList();

            // Добавление типов домов в ComboBox
            HouseTypeComboBox.ItemsSource = houseTypes;
            HouseTypeComboBox.DisplayMemberPath = "name";
        }

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            // Обработка применения фильтрации
            decimal maxPrice = (decimal)PriceSlider.Value; // Получаем значение цены из слайдера
            string selectedLocation = LocationComboBox.SelectedValue as string; // Получаем выбранную локацию
            string selectedHouseType = (HouseTypeComboBox.SelectedValue as Characteristics)?.name; // Получаем выбранный тип дома

            // Передаем фильтры обратно на страницу RentSearchPage
            if (_previousPage is RentSearchPage rentSearchPage)
            {
                rentSearchPage.ApplyFilter(maxPrice, selectedLocation, selectedHouseType);
                NavigationService.Navigate(rentSearchPage); // Возвращаемся на RentSearchPage после применения фильтра
            }
            else
            {
                MessageBox.Show("Ошибка: Предыдущая страница не является RentSearchPage");
            }
        }





        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            // Сброс всех значений фильтрации
            PriceSlider.Value = 0;
            LocationComboBox.SelectedIndex = -1; // Сброс выбранной локации
            HouseTypeComboBox.SelectedIndex = -1; // Сброс выбранного типа дома

            // Сброс остальных элементов фильтрации (если есть)
            MessageBox.Show("Сброс фильтра");
        }

      
    }
}
