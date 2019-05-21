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
using System.Data.SqlClient;

namespace Pizzeria
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class Projekt : Window
    {

        //zmienne
        int licznik = 1;
        string nazwa_wyb_pizzy;
        string id_wyb_pizzy="0";
        string rozmiar_pizzy = "0";
        int suma_zam = 0;
        string[,]zamowienia = new string[10,3];
        Label[] zamowienie_lab = new Label[10];
        Button[] delzam = new Button[10];
        SqlConnection polacz;

        public Projekt()
        {
            InitializeComponent();
            SQLopen();
            SQL_buttony_z_nazwami("SELECT Nazwa from Pizze");

        }

        //SQL

        public void SQLopen()
        {
            string connetionString = "Data Source=DESKTOP-O745A5P\\SQLEXPRESS;Initial Catalog=pizzeria;User ID=klient;Password=klient";
            SqlConnection polaczenie;
            polaczenie = new SqlConnection(connetionString);
            try
            {
                polaczenie.Open();
            }
            catch
            {
                MessageBox.Show("Nie uzyskano połączenia z bazą");
            }
            polacz = polaczenie;
        }
        public void SQL_label(Label pole, string sql, int wartosc)
        {
            SqlCommand odczyt;
            SqlDataReader dataReader;

            try
            {
                odczyt = new SqlCommand(sql, polacz);
                dataReader = odczyt.ExecuteReader();
                // while (dataReader.Read())
                // {
                dataReader.Read();
                pole.Content = Convert.ToString(dataReader.GetValue(wartosc));
                // }
                dataReader.Close();
                odczyt.Dispose();
            }
            catch
            {
                MessageBox.Show("Niepoprawna składnia");
            }
        }
        public void dodaj_zamowienie(string sql)
        {
            SqlCommand odczyt;
            SqlDataReader dataReader;

            try
            {
                odczyt = new SqlCommand(sql, polacz);
               dataReader = odczyt.ExecuteReader();
                // while (dataReader.Read())
                // {
              //  dataReader.Read();
                //pole.Content = Convert.ToString(dataReader.GetValue(wartosc));
                // }
              //  dataReader.Close();
                odczyt.Dispose();
            }
            catch
            {
                MessageBox.Show("Niepoprawna składnia");
            }
        }
        public void SQL_buttony_z_nazwami(string sql)
        {
            SqlCommand odczyt;
            SqlDataReader dataReader;

            try
            {
                odczyt = new SqlCommand(sql, polacz);
                dataReader = odczyt.ExecuteReader();
                Button[] tab_pizz = new Button[9];
                    tab_pizz[0] = pizza_button;
                    tab_pizz[1] = pizza_button1;
                    tab_pizz[2] = pizza_button2;
                    tab_pizz[3] = pizza_button3;
                    tab_pizz[4] = pizza_button4;
                    tab_pizz[5] = pizza_button5;
                    tab_pizz[6] = pizza_button6;
                    tab_pizz[7] = pizza_button7;
                    tab_pizz[8] = pizza_button8;
                    

                 while (dataReader.Read())
                 {
                    for (int i = 0; i < 9; i++)
                    {
                        
                        tab_pizz[i].Content = Convert.ToString(dataReader.GetValue(0));
                        dataReader.Read();
                    }
                 }
                dataReader.Close();
                odczyt.Dispose();
            }
            catch
            {
                MessageBox.Show("Niepoprawna składnia");
            }
        }
        public void SQLend()
        {
            polacz.Close();
        }

        private void wybor_pizzy(string id)
        {
            zl_wyb.Visibility = Visibility.Hidden;
            kwota_kr.Content = "";
            id_wyb_pizzy = id;

            SqlCommand odczyt;
            SqlDataReader dataReader;
            string sql = "SELECT skladniki.Nazwa from Pizze,skladniki where pizze.ID_Pizzy=" + id_wyb_pizzy + " AND (ID_skladniku1=skladniki.IDskladniku OR ID_skladniku2=skladniki.IDskladniku OR ID_skladniku3=skladniki.IDskladniku OR ID_skladniku4=skladniki.IDskladniku OR ID_skladniku5=skladniki.IDskladniku OR ID_skladniku6=skladniki.IDskladniku OR ID_skladniku7=skladniki.IDskladniku OR ID_skladniku8=skladniki.IDskladniku)  Order By skladniki.IDskladniku";
            try
            {
                odczyt = new SqlCommand(sql, polacz);
                dataReader = odczyt.ExecuteReader();
                Label[] lab_pizz = new Label[9];
                lab_pizz[0] = skladnik_lab;
                lab_pizz[1] = skladnik_lab1;
                lab_pizz[2] = skladnik_lab2;
                lab_pizz[3] = skladnik_lab3;
                lab_pizz[4] = skladnik_lab4;
                lab_pizz[5] = skladnik_lab5;
                lab_pizz[6] = skladnik_lab6;
                lab_pizz[7] = skladnik_lab7;
                lab_pizz[8] = skladnik_lab8;

                for (int i = 0; i < 9; i++)
                {
                    lab_pizz[i].Content = "";
                    lab_pizz[i].Visibility = Visibility.Hidden;
                }

                while (dataReader.Read())
                {
                    for (int i = 0; i < 9; i++)
                    {
                        lab_pizz[i].Content = Convert.ToString(dataReader.GetValue(0));
                        lab_pizz[i].Visibility = Visibility.Visible;
                        
                        if (dataReader.Read() == false) { break; }

                    }

                }
                dataReader.Close();
                odczyt.Dispose();
            }
            catch
            {
                MessageBox.Show("Niepoprawna składnia");
            }
        }

        //kreator

        private void Button_nasze_pizze_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Dalej(object sender, RoutedEventArgs e)
        {


            if (licznik >= 3)
            {
                licznik = 1;
            }
            else
            {
                licznik++;
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

        private void Rozmiar_srednia2_Click(object sender, RoutedEventArgs e)
        {
            rozmiar_cm2.Content ="32 cm";
        }

        private void Rozmiar_duza2_Click(object sender, RoutedEventArgs e)
        {
            rozmiar_cm2.Content ="48 cm";
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


        //buttony

        private void Wybierz_Pizze_button_Click(object sender, RoutedEventArgs e)
        {
            menu.Visibility = Visibility.Collapsed;
            wybor.Visibility = Visibility.Visible;  
        }

        private void kompozycja_Click_1(object sender, RoutedEventArgs e)
        {
            menu.Visibility = Visibility.Collapsed;
            kreator.Visibility = Visibility.Visible;
        }

        private void zamow_Click_2(object sender, RoutedEventArgs e)
        {
            menu.Visibility = Visibility.Collapsed;
            zamowienie.Visibility = Visibility.Visible;
        }

        private void Anuluj_button_Click(object sender, RoutedEventArgs e)
        {
            wybor.Visibility = Visibility.Collapsed;
            menu.Visibility = Visibility.Visible;
        }

        private void Dodaj_zamowienie_button_Click(object sender, RoutedEventArgs e)
        {
   
            
            zamowienie_lab[0] = zam;
            zamowienie_lab[1] = zam1;
            zamowienie_lab[2] = zam2;
            zamowienie_lab[3] = zam3;
            zamowienie_lab[4] = zam4;
            zamowienie_lab[5] = zam5;
            zamowienie_lab[6] = zam6;
            zamowienie_lab[7] = zam7;
            zamowienie_lab[8] = zam8;
            zamowienie_lab[9] = zam9;

            
            delzam[0] = del_zam1_button;
            delzam[1] = del_zam2_button;
            delzam[2] = del_zam3_button;
            delzam[3] = del_zam4_button;
            delzam[4] = del_zam5_button;
            delzam[5] = del_zam6_button;
            delzam[6] = del_zam7_button;
            delzam[7] = del_zam8_button;
            delzam[8] = del_zam9_button;
            delzam[9] = del_zam10_button;
            

            for (int i=0;i<10;i++)
            {
                if (zamowienia[i, 0] is null)
                {
                    zamowienia[i, 0] = nazwa_wyb_pizzy;
                    zamowienia[i, 1] = rozmiar_pizzy;
                    zamowienia[i, 2] = Convert.ToString(kwota_kr.Content);
                    zamowienie_lab[i].Content = zamowienia[i, 0] + " " + zamowienia[i, 1] + "(" + zamowienia[i, 2] + "zl)";
                    delzam[i].Visibility = Visibility.Visible;
                    suma_zam += int.Parse(zamowienia[i, 2]);
                    break;
                }
                
            }
            suma_label.Content = suma_zam+"zl";
            wybor.Visibility = Visibility.Collapsed;
            menu.Visibility = Visibility.Visible;
        }

        private void Anuluj_button1_Click(object sender, RoutedEventArgs e)
        {
            kreator.Visibility = Visibility.Collapsed;
            menu.Visibility = Visibility.Visible;
        }

        private void Dodaj_zamowienie_button_Click_1(object sender, RoutedEventArgs e)
        {
            kreator.Visibility = Visibility.Collapsed;
            menu.Visibility = Visibility.Visible;
        }

        private void Anuluj_button2_Click(object sender, RoutedEventArgs e)
        {
            zamowienie.Visibility = Visibility.Collapsed;
            menu.Visibility = Visibility.Visible;
        }

        private void Zamow_button_Click(object sender, RoutedEventArgs e)
        {
            zamowienie.Visibility = Visibility.Collapsed;
            finalizacja.Visibility = Visibility.Visible;
        }
        public async void restart()
        {
            await Task.Delay(3000);
            numerek.Visibility = Visibility.Collapsed;
            menu.Visibility = Visibility.Visible;
            InitializeComponent();
        }
        private void Gotowka_button_Click(object sender, RoutedEventArgs e)
        {
            finalizacja.Visibility = Visibility.Collapsed;
            numerek.Visibility = Visibility.Visible;
            restart();
        }

        private void Blik_button_Click(object sender, RoutedEventArgs e)
        {
            blik_grid.Visibility = Visibility.Visible;
        }

        private void Pizza_button_Click(object sender, RoutedEventArgs e)
        {
            wybor_pizzy("1");
            nazwa_wyb_pizzy = Convert.ToString(pizza_button.Content);
        }

        private void Pizza_button1_Click(object sender, RoutedEventArgs e)
        {
            wybor_pizzy("2");
            nazwa_wyb_pizzy = Convert.ToString(pizza_button1.Content);
        }

        private void Pizza_button2_Click(object sender, RoutedEventArgs e)
        {
            wybor_pizzy("3");
            nazwa_wyb_pizzy = Convert.ToString(pizza_button2.Content);
        }

        private void Pizza_button3_Click(object sender, RoutedEventArgs e)
        {
            wybor_pizzy("4");
            nazwa_wyb_pizzy = Convert.ToString(pizza_button3.Content);
        }

        private void Pizza_button4_Click(object sender, RoutedEventArgs e)
        {
            wybor_pizzy("5");
            nazwa_wyb_pizzy = Convert.ToString(pizza_button4.Content);
        }

        private void Pizza_button5_Click(object sender, RoutedEventArgs e)
        {
            wybor_pizzy("6");
            nazwa_wyb_pizzy = Convert.ToString(pizza_button5.Content);
        }

        private void Pizza_button6_Click(object sender, RoutedEventArgs e)
        {
            wybor_pizzy("7");
            nazwa_wyb_pizzy = Convert.ToString(pizza_button6.Content);
        }

        private void Pizza_button7_Click(object sender, RoutedEventArgs e)
        {
            wybor_pizzy("8");
            nazwa_wyb_pizzy = Convert.ToString(pizza_button7.Content);
        }

        private void Pizza_button8_Click(object sender, RoutedEventArgs e)
        {
            wybor_pizzy("10");
            nazwa_wyb_pizzy = Convert.ToString(pizza_button8.Content);
        }

        private void Rozmiar_srednia_Click(object sender, RoutedEventArgs e)
        {
            rozmiar_cm.Content ="32 cm";
            rozmiar_pizzy = "(srednia)";
            if (id_wyb_pizzy == "0")
            {
                kwota_kr.Content = "";
            }
            else
            {
                SQL_label(kwota_kr, "Select Cena_sredniej, Cena_duzej from pizze where ID_Pizzy =" + id_wyb_pizzy + "", 0);
                zl_wyb.Visibility = Visibility.Visible;
            }
        }

        private void Rozmiar_duza_Click(object sender, RoutedEventArgs e)
        {
            rozmiar_cm.Content ="48 cm";
            rozmiar_pizzy = "(duza)";
            if (id_wyb_pizzy == "0")
            {
                kwota_kr.Content = "";
            }
            else
            {
                SQL_label(kwota_kr, "Select Cena_sredniej, Cena_duzej from pizze where ID_Pizzy =" + id_wyb_pizzy + "", 1);
                zl_wyb.Visibility = Visibility.Visible;
            }
        }

        private void Del_zam1_button_Click(object sender, RoutedEventArgs e)
        {
            del_zam1_button.Visibility = Visibility.Hidden;
            suma_zam -= Convert.ToInt32(zamowienia[0, 2]);
            zamowienia[0, 0] = null;
            zamowienia[0, 1] = null;
            zamowienia[0, 2] = null;
            zamowienie_lab[0].Content = "";
            Usun_zam();
            
            suma_label.Content = suma_zam+"zl";
        }
        private void Del_zam2_button_Click(object sender, RoutedEventArgs e)
        {
            del_zam2_button.Visibility = Visibility.Hidden;
            suma_zam -= Convert.ToInt32(zamowienia[1, 2]);
            zamowienia[1, 0] = null;
            zamowienia[1, 1] = null;
            zamowienia[1, 2] = null;
            zamowienie_lab[1].Content = "";
            Usun_zam();

            suma_label.Content = suma_zam + "zl";
        }
        private void Del_zam3_button_Click(object sender, RoutedEventArgs e)
        {
            del_zam3_button.Visibility = Visibility.Hidden;
            suma_zam -= Convert.ToInt32(zamowienia[2, 2]);
            zamowienia[2, 0] = null;
            zamowienia[2, 1] = null;
            zamowienia[2, 2] = null;
            zamowienie_lab[2].Content = "";
            Usun_zam();

            suma_label.Content = suma_zam + "zl";
        }
        private void Del_zam4_button_Click(object sender, RoutedEventArgs e)
        {
            del_zam4_button.Visibility = Visibility.Hidden;
            suma_zam -= Convert.ToInt32(zamowienia[3, 2]);
            zamowienia[3, 0] = null;
            zamowienia[3, 1] = null;
            zamowienia[3, 2] = null;
            zamowienie_lab[3].Content = "";
            Usun_zam();

            suma_label.Content = suma_zam + "zl";
        }
        private void Del_zam5_button_Click(object sender, RoutedEventArgs e)
        {
            del_zam5_button.Visibility = Visibility.Hidden;
            suma_zam -= Convert.ToInt32(zamowienia[4, 2]);
            zamowienia[4, 0] = null;
            zamowienia[4, 1] = null;
            zamowienia[4, 2] = null;
            zamowienie_lab[4].Content = "";
            Usun_zam();

            suma_label.Content = suma_zam + "zl";
        }
        private void Del_zam6_button_Click(object sender, RoutedEventArgs e)
        {
            del_zam6_button.Visibility = Visibility.Hidden;
            suma_zam -= Convert.ToInt32(zamowienia[5, 2]);
            zamowienia[5, 0] = null;
            zamowienia[5, 1] = null;
            zamowienia[5, 2] = null;
            zamowienie_lab[5].Content = "";
            Usun_zam();

            suma_label.Content = suma_zam + "zl";
        }
        private void Del_zam7_button_Click(object sender, RoutedEventArgs e)
        {
            del_zam7_button.Visibility = Visibility.Hidden;
            suma_zam -= Convert.ToInt32(zamowienia[6, 2]);
            zamowienia[6, 0] = null;
            zamowienia[6, 1] = null;
            zamowienia[6, 2] = null;
            zamowienie_lab[6].Content = "";
            Usun_zam();

            suma_label.Content = suma_zam + "zl";
        }
        private void Del_zam8_button_Click(object sender, RoutedEventArgs e)
        {
            del_zam8_button.Visibility = Visibility.Hidden;
            suma_zam -= Convert.ToInt32(zamowienia[7, 2]);
            zamowienia[7, 0] = null;
            zamowienia[7, 1] = null;
            zamowienia[7, 2] = null;
            zamowienie_lab[7].Content = "";
            Usun_zam();

            suma_label.Content = suma_zam + "zl";
        }
        private void Del_zam9_button_Click(object sender, RoutedEventArgs e)
        {
            del_zam9_button.Visibility = Visibility.Hidden;
            suma_zam -= Convert.ToInt32(zamowienia[8, 2]);
            zamowienia[8, 0] = null;
            zamowienia[8, 1] = null;
            zamowienia[8, 2] = null;
            zamowienie_lab[8].Content = "";
            Usun_zam();

            suma_label.Content = suma_zam + "zl";
        }
        private void Del_zam10_button_Click(object sender, RoutedEventArgs e)
        {
            del_zam10_button.Visibility = Visibility.Hidden;
            suma_zam -= Convert.ToInt32(zamowienia[9, 2]);
            zamowienia[9, 0] = null;
            zamowienia[9, 1] = null;
            zamowienia[9, 2] = null;
            zamowienie_lab[9].Content = "";
            Usun_zam();

            suma_label.Content = suma_zam + "zl";
        }
       
        private void Usun_zam()
        {
            for (int i =0 ; i < 9; i++)
            {
                if (zamowienia[i, 0] is null )
                {
                    for (int k = i; k < 9; k++)
                    {
                        zamowienia[k, 0] = zamowienia[k + 1, 0];
                        zamowienia[k, 1] = zamowienia[k + 1, 1];
                        zamowienia[k, 2] = zamowienia[k + 1, 2];
                        zamowienie_lab[k].Content = zamowienia[k, 0] + " " + zamowienia[k, 1] + "(" + zamowienia[k, 2] + "zl)";
                        delzam[k].Visibility = Visibility.Visible;
                    }
                }
                  
                    if(zamowienia[i,0]is null)
                {
                    zamowienie_lab[i].Content = "";
                    delzam[i].Visibility = Visibility.Hidden;
                }
                    // break;*/
            }

        }

        
    }
}
