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
      //zmienne
        int licznik = 1;
        int rozmiar_sr = 30;
        int rozmiar_duz = 42;
        
        
      //----------------------------------------  
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

        private void Rozmiar_srednia_Click(object sender, RoutedEventArgs e)
        {
            rozmiar_cm.Content = rozmiar_sr + " cm";
        }

        private void Rozmiar_duza_Click(object sender, RoutedEventArgs e)
        {
            rozmiar_cm.Content = rozmiar_duz + " cm";
        }
        


        //menu wyboru skladnikow {pokazanie wartosci )



        private void Nr1_Click(object sender, RoutedEventArgs e)
        {
            if (prod1.Visibility == Visibility.Collapsed)
            {
                prod1.Visibility = Visibility.Visible;
                nr1.Opacity = 0.4;
            }
            else
            {
                prod1.Visibility = Visibility.Collapsed;
                nr1.Opacity = 1;
            }
        }
        private void Nr2_Click(object sender, RoutedEventArgs e)
        {
            if (prod2.Visibility == Visibility.Collapsed)
            {
                prod2.Visibility = Visibility.Visible;
                nr2.Opacity = 0.4;
            }
            else
            {
                prod2.Visibility = Visibility.Collapsed;
                nr2.Opacity = 1;
            }
        }
        private void Nr3_Click(object sender, RoutedEventArgs e)
        {
            if (prod3.Visibility == Visibility.Collapsed)
            {
                prod3.Visibility = Visibility.Visible;
                nr3.Opacity = 0.4;
            }
            else
            {
                prod3.Visibility = Visibility.Collapsed;
                nr3.Opacity = 1;
            }
        }
        private void Nr4_Click(object sender, RoutedEventArgs e)
        {
            if (prod4.Visibility == Visibility.Collapsed)
            {
                prod4.Visibility = Visibility.Visible;
                nr4.Opacity = 0.4;
            }
            else
            {
                prod4.Visibility = Visibility.Collapsed;
                nr4.Opacity = 1;
            }
        }
        private void Nr5_Click(object sender, RoutedEventArgs e)
        {
            if (prod5.Visibility == Visibility.Collapsed)
            {
                prod5.Visibility = Visibility.Visible;
                nr5.Opacity = 0.4;
            }
            else
            {
                prod5.Visibility = Visibility.Collapsed;
                nr5.Opacity = 1;
            }
        }
        private void Nr6_Click(object sender, RoutedEventArgs e)
        {
            if (prod6.Visibility == Visibility.Collapsed)
            {
                prod6.Visibility = Visibility.Visible;
                nr6.Opacity = 0.4;
            }
            else
            {
                prod6.Visibility = Visibility.Collapsed;
                nr6.Opacity = 1;
            }
        }
        private void Nr7_Click(object sender, RoutedEventArgs e)
        {
            if (prod7.Visibility == Visibility.Collapsed)
            {
                prod7.Visibility = Visibility.Visible;
                nr7.Opacity = 0.4;
            }
            else
            {
                prod7.Visibility = Visibility.Collapsed;
                nr7.Opacity = 1;
            }
        }
        private void Nr8_Click(object sender, RoutedEventArgs e)
        {
            if (prod8.Visibility == Visibility.Collapsed)
            {
                prod8.Visibility = Visibility.Visible;
                nr8.Opacity = 0.4;
            }
            else
            {
                prod8.Visibility = Visibility.Collapsed;
                nr8.Opacity = 1;
            }
        }
        private void Nr9_Click(object sender, RoutedEventArgs e)
        {
            if (prod9.Visibility == Visibility.Collapsed)
            {
                prod9.Visibility = Visibility.Visible;
                nr9.Opacity = 0.4;
            }
            else
            {
                prod9.Visibility = Visibility.Collapsed;
                nr9.Opacity = 1;
            }
        }

    }
}
