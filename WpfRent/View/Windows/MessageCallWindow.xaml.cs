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
using System.Windows.Shapes;
using WpfRent.Models;

namespace WpfRent.View.Windows
{
    /// <summary>
    /// Логика взаимодействия для MessageCallWindow.xaml
    /// </summary>
    public partial class MessageCallWindow : Window
    {
        public string RealtorPhoneNumber { get; set; }

        public MessageCallWindow(string realtorPhoneNumber)
        {
            InitializeComponent();
            RealtorPhoneNumber = realtorPhoneNumber;
            NumberPhone.Text = RealtorPhoneNumber;
        }

        private void CopyNumber_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(RealtorPhoneNumber))
            {
                Clipboard.SetText(RealtorPhoneNumber);
            }

            Close();
        }
    

    }
}
