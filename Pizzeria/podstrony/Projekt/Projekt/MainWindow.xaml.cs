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

namespace Projekt
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        int licznik = 1;
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Dalej(object sender, RoutedEventArgs e)
        {
          

            if (licznik >=3)
                {
                    licznik = 1;
                }
            else
                {
                    licznik++;
                }
            
            if(licznik == 1)
            {
                opcja2.Visibility = Visibility.Hidden;
                opcja3.Visibility = Visibility.Hidden;
                opcja1.Visibility = Visibility.Visible;
            }
            if (licznik == 2)
            {
                opcja1.Visibility = Visibility.Hidden;
                opcja3.Visibility = Visibility.Hidden;
                opcja2.Visibility = Visibility.Visible;
            }
            if (licznik == 3)
            {
                opcja1.Visibility = Visibility.Hidden;
                opcja2.Visibility = Visibility.Hidden;
                opcja3.Visibility = Visibility.Visible;
            }
        }

        private void Wstecz(object sender, RoutedEventArgs e)
        {
            if (licznik <= 1)
            {
                licznik = 3;
            }
            else
            {
                licznik--;
            }
            
            if (licznik == 1)
            {
                opcja2.Visibility = Visibility.Hidden;
                opcja3.Visibility = Visibility.Hidden;
                opcja1.Visibility = Visibility.Visible;
            }
            if (licznik == 2)
            {
                opcja1.Visibility = Visibility.Hidden;
                opcja3.Visibility = Visibility.Hidden;
                opcja2.Visibility = Visibility.Visible;
            }
            if (licznik == 3)
            {
                opcja1.Visibility = Visibility.Hidden;
                opcja2.Visibility = Visibility.Hidden;
                opcja3.Visibility = Visibility.Visible;
            }
        }
    }
}
