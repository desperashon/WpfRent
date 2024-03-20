using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WpfRent.Models;

namespace WpfRent.View.Pages
{
    public partial class RentSearchPage : Page
    {
        private readonly RentGavrilinEntities _context;
        private List<Announcement> _filteredAnnouncements;
        public RentSearchPage(Users enteredUser)
        {
            InitializeComponent();
            LoadData();
            _context = new RentGavrilinEntities();
            LoadAnnouncements();

            GeolocationTb.Text = $"Ваше местоположение: {enteredUser.Location1.name}!";
        }

        private void LoadData()
        {
            using (var context = new RentGavrilinEntities())
            {
                var announcements = context.Announcement.ToList();
                announcements = context.Announcement.Include("Location1").ToList();
                basketLb.ItemsSource = announcements;
            }
        }

        private void LoadAnnouncements()
        {
            _filteredAnnouncements = _context.Announcement.ToList();
            basketLb.ItemsSource = _filteredAnnouncements;
        }

        public void ApplyFilter(decimal maxPrice, string selectedLocation, string selectedHouseType)
        {
            _filteredAnnouncements = _context.Announcement.ToList();

            if (maxPrice > 0)
                _filteredAnnouncements = _filteredAnnouncements.Where(a => a.price <= maxPrice).ToList();

            if (!string.IsNullOrWhiteSpace(selectedLocation))
                _filteredAnnouncements = _filteredAnnouncements.Where(a => a.Location1.name == selectedLocation).ToList();

            if (!string.IsNullOrWhiteSpace(selectedHouseType))
                _filteredAnnouncements = _filteredAnnouncements.Where(a => a.Characteristics.name == selectedHouseType).ToList();

            basketLb.ItemsSource = _filteredAnnouncements;
        }


        public void Refresh()
        {
            LoadAnnouncements();
        }
        







        private void basketLb_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (basketLb.SelectedItem != null)
            {
            
                Announcement selectedVacancy = (Announcement)basketLb.SelectedItem;



                InfoRentPage extendedWindow = new InfoRentPage(selectedVacancy);

                NavigationService.Navigate(extendedWindow);
            }
        }

      

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new ProfilPage());
        }

        private void FiltrBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new FiltrPage(this));
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
