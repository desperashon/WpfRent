using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using WpfRent.Models;
using WpfRent.View.Windows;

namespace WpfRent.View.Pages
{
    public partial class InfoRentPage : Page
    {
        public Announcement selectedAnnouncement;
        public InfoRentPage(Announcement announcement)
        {
            InitializeComponent();

            selectedAnnouncement = announcement;

            var realtor = App.context.Realtor.FirstOrDefault(r => r.realtor_id == selectedAnnouncement.realtor_id);

            if (realtor != null)
            {
                if (!string.IsNullOrEmpty(realtor.photo))
                {
                    try
                    {
                        BitmapImage bitmapImage = new BitmapImage(new Uri(realtor.photo));
                        PhotoRelt.Source = bitmapImage;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка загрузки фотографии риэлтора: {ex.Message}");
                    }
                }

                NameReltTb.Text = realtor.name;
            }

            if (!string.IsNullOrEmpty(selectedAnnouncement.photo))
            {
                try
                {
                    BitmapImage bitmapImage = new BitmapImage(new Uri(selectedAnnouncement.photo));
                    ImageIm.Source = bitmapImage;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка загрузки изображения: {ex.Message}");
                }
            }

            StringBuilder characteristicsBuilder = new StringBuilder();

            CharacteristicsTb.Text = characteristicsBuilder.ToString();

            NameTb.Text = $" Название: {selectedAnnouncement.title}";
            LocationTb.Text = $" Местоположение: {selectedAnnouncement.Location1.name}";
            DescriptionTb.Text = $" Описание: {selectedAnnouncement.description}";
        }

        public string GetPhoneNumberFromDatabase(int announcementId)
        {
            using (var context = new RentGavrilinEntities())
            {
                int? realtorIdNullable = context.Announcement
                                                .Where(a => a.announcement_id == announcementId)
                                                .Select(a => a.realtor_id)
                                                .FirstOrDefault();

                if (realtorIdNullable.HasValue)
                {
                    int realtorId = realtorIdNullable.Value;

                    var realtor = context.Realtor.FirstOrDefault(r => r.realtor_id == realtorId);

                    return realtor?.phone;
                }
                else
                {
                    return null;
                }
            }
        }

        private void CallBtn_Click(object sender, RoutedEventArgs e)
        {
            if (selectedAnnouncement != null)
            {
                int announcementId = selectedAnnouncement.announcement_id;
                string realtorPhoneNumber = GetPhoneNumberFromDatabase(announcementId);

                MessageCallWindow messageWindow = new MessageCallWindow(realtorPhoneNumber);
                messageWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Выберите объявление.");
            }
        }

        private void MessageBtn_Click(object sender, RoutedEventArgs e)
        {
            string realtorPhoneNumber = null;
            MessageCallWindow messageWindow = new MessageCallWindow(realtorPhoneNumber);
            messageWindow.ShowDialog();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}
