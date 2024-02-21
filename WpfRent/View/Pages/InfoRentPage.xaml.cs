using System;
using System.Collections.Generic;
using System.IO;
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
using WpfRent.View.Windows;

namespace WpfRent.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для InfoRentPage.xaml
    /// </summary>
    public partial class InfoRentPage : Page
    {
        public Announcement selectedAnnouncement;
        public InfoRentPage(Announcement announcement)
        {
            InitializeComponent();

            selectedAnnouncement = announcement;

            var realtor = App.context.Realtor.FirstOrDefault(r => r.realtor_id == selectedAnnouncement.realtor_id);

            // Проверка, найден ли риэлтор
            if (realtor != null)
            {
                // Отображение фотографии риэлтора
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

                // Отображение имени риэлтора
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


            // Получение характеристик для выбранного объявления
            //var characteristics = from ac in App.context.Announcement_Characteristis
            //                      join c in App.context.Characteristics on ac.characteristic_id equals c.characteristic_id
            //                      where ac.announcement_id == selectedAnnouncement.announcement_id
            //                      select c;

            // Формирование строки характеристик
            StringBuilder characteristicsBuilder = new StringBuilder();
            //foreach (var characteristic in characteristics)
            //{
            //    characteristicsBuilder.AppendLine(characteristic.name);
            //}

            // Установка характеристик в TextBox
            CharacteristicsTb.Text = characteristicsBuilder.ToString();

            NameTb.Text = $" Название: {selectedAnnouncement.title}";
            LocationTb.Text = $" Местоположение: {selectedAnnouncement.Location1.name}";
            DescriptionTb.Text = $" Описание: {selectedAnnouncement.description}";
           
               
        }

        public string GetPhoneNumberFromDatabase(int announcementId)
        {
            using (var context = new RentGavrilinEntities())
            {
                // Находим объявление в базе данных по его идентификатору и получаем id риэлтора
                int? realtorIdNullable = context.Announcement
                                                .Where(a => a.announcement_id == announcementId)
                                                .Select(a => a.realtor_id)
                                                .FirstOrDefault();

                if (realtorIdNullable.HasValue) // Проверяем, есть ли значение
                {
                    int realtorId = realtorIdNullable.Value; // Получаем значение, если оно есть

                    // Получаем риэлтора из базы данных по его идентификатору
                    var realtor = context.Realtor.FirstOrDefault(r => r.realtor_id == realtorId);

                    // Возвращаем номер телефона риэлтора
                    return realtor?.phone;
                }
                else
                {
                    return null; // Или что-то другое, что указывает на отсутствие номера телефона
                }
            }
        }


        private void CallBtn_Click(object sender, RoutedEventArgs e)
        {
            // Получение номера телефона риэлтора из базы данных
            if (selectedAnnouncement != null)
            {
                int announcementId = selectedAnnouncement.announcement_id; // Получаем идентификатор объявления
                string realtorPhoneNumber = GetPhoneNumberFromDatabase(announcementId); // Передаем идентификатор объявления

                // Создание и открытие окна для звонка
                MessageCallWindow messageWindow = new MessageCallWindow(realtorPhoneNumber);
                messageWindow.ShowDialog(); // Открываем окно как модальное
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
            messageWindow.ShowDialog(); // Открываем окно как модальное
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }
    }
}
