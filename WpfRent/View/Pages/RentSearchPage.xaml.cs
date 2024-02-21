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
        public RentSearchPage(Users enteredUser)
        {
            InitializeComponent();
            LoadData();

            ;
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


        public void ApplyFilter(decimal maxPrice, string location, string houseType)
        {
            using (var context = new RentGavrilinEntities())
            {
                IQueryable<Announcement> filteredData = context.Announcement;

                if (maxPrice > 0)
                    filteredData = filteredData.Where(a => a.price <= maxPrice);

                if (!string.IsNullOrEmpty(location))
                    filteredData = filteredData.Where(a => a.Location1.name == location);

                if (!string.IsNullOrEmpty(houseType))
                    filteredData = filteredData.Where(a => a.Characteristics.name == houseType);

                var filteredList = filteredData.ToList();

                foreach (var announcement in filteredList)
                {
                    Console.WriteLine($"Location: {announcement.Location1?.name}, Title: {announcement.title}, Price: {announcement.price}");
                }

                basketLb.ItemsSource = filteredList;
            }
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
