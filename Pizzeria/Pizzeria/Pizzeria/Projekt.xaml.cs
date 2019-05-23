﻿using System;
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
        int licznik = 1; //do kreatora
        string nazwa_wyb_pizzy; //przypisanie nazwy wybranej pizzy
        string id_wyb_pizzy="0"; //przypisanie id wybranej pizzy
        int id_wyb_skladnika = 0; //przypisanie id wybranemu skladnikowi
        string rozmiar_pizzy = "0"; // przypisanie rozmiaru pizzy
        int cena_sr = 0; // cena aktualnego skladnika dla sredniej pizzy
        int cena_d = 0; // cena aktualnego skladnika dla duzej pizzy
        int suma_zam = 0; //przypisanie sumy kosztow zamowienia
        int suma_skladnikow_sr = 8; //suma cen skladnikow + cena startowa dla sr pizzy
        int suma_skladnikow_d = 10; //suma cen skladnikow + cena startowa dla duzej pizzy
        int max_id_zam = 0; //maksymalne id zapisanych w bazie zamowien
        string[,]zamowienia = new string[10,15]; //tablica parametrow zamowienia
        Label[] zamowienie_lab = new Label[10]; //tablica nazw zamowien dla menu
        Label[] zamowienie_lab1 = new Label[10];//tablica nazw zamowien dla zamow
        Button[] delzam = new Button[10];//tablica buttonow usuniecia dla menu
        Button[] delzam1 = new Button[10];// tablica buttonow usuniecia dla zamow
        SqlConnection polacz;// obiekt polaczenia z baza danych

        public Projekt()
        {
            InitializeComponent();
            SQLopen(); // otworz polaczenie z baza danych
            tworz_tablice();



        }

        //SQL

        public void SQLopen() // otworz polaczenie z baza danych
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
        public void SQL_label(Label pole, string sql, int wartosc) //przypisanie Contentowi labela (pole), tekstu komorki (kolejnosc komorek wedlug wartosc) wedlug zapytania sql 
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
        public void dodaj_zamowienie()//dodanie wszystkich zamowien jako jedno.
        {
            SqlCommand odczyt;
            SqlDataReader dataReader;
            max_id_zam++;
            try
            {
                for(int i=0;i<10;i++)
                {
                    string sql = "INSERT INTO Zamowienia VALUES("+max_id_zam+",'" + zamowienia[i, 0] + "','" + zamowienia[i, 1] + "','" + zamowienia[i, 2] + "','" + zamowienia[i, 3] + "','" + zamowienia[i, 4] + "','" + zamowienia[i, 5] + "','" + zamowienia[i, 6] + "','" + zamowienia[i, 7] + "','" + zamowienia[i, 8] + "','" + zamowienia[i, 9] + "','" + zamowienia[i, 10] + "','" + zamowienia[i, 11] + "','" + zamowienia[i, 12] + "','" + zamowienia[i, 13] + "','" + zamowienia[i, 14] + "')";
                    odczyt = new SqlCommand(sql, polacz);
                    dataReader = odczyt.ExecuteReader();
                    odczyt.Dispose();
                    dataReader.Close();
                }
                

                // while (dataReader.Read())
                // {
                //  dataReader.Read();
                //pole.Content = Convert.ToString(dataReader.GetValue(wartosc));
                // }


            }
            catch
            {
                MessageBox.Show("Niepoprawna składnia");
            }
        }
        public void SQL_buttony_z_nazwami(string sql)// przypisanie contentow buttonom w menu z wyborem pizzy
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
        public void SQLend()// zakonczenie polaczenia z baza danych
        {
            polacz.Close();
        }

        private void wybor_pizzy(string id)//przypisanie contentow labelom skladnikow w menu wyboru pizzy
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
        public void SQL_max_id() //przypisanie Contentowi labela (pole), tekstu komorki (kolejnosc komorek wedlug wartosc) wedlug zapytania sql 
        {
            SqlCommand odczyt;
            SqlDataReader dataReader;
            string sql = "Select MAX(ID_zam) from Zamowienia";
            try
            {
                odczyt = new SqlCommand(sql, polacz);
                dataReader = odczyt.ExecuteReader();
                // while (dataReader.Read())
                // {
                dataReader.Read();
                max_id_zam= Convert.ToInt32(dataReader.GetValue(0));
                // }
                dataReader.Close();
                odczyt.Dispose();
            }
            catch
            {
                MessageBox.Show("Niepoprawna składnia");
            }
        }
        //inne funkcje

        private void Usun_zam()
        {
            for (int i = 0; i < 9; i++)
            {
                if (zamowienia[i, 0] is null)
                {
                    for (int k = i; k < 9; k++)
                    {
                        zamowienia[k, 0] = zamowienia[k + 1, 0];
                        zamowienia[k, 1] = zamowienia[k + 1, 1];
                        zamowienia[k, 2] = zamowienia[k + 1, 2];
                        zamowienie_lab[k].Content = zamowienia[k, 0] + " " + zamowienia[k, 1] + "(" + zamowienia[k, 2] + "zl)";
                        zamowienie_lab1[k].Content = zamowienie_lab[k].Content;
                        delzam[k].Visibility = Visibility.Visible;
                        delzam1[k].Visibility = Visibility.Visible;
                    }
                }

                if (zamowienia[i, 0] is null)
                {
                    zamowienie_lab[i].Content = "";
                    zamowienie_lab1[i].Content = "";
                    delzam[i].Visibility = Visibility.Hidden;
                    delzam1[i].Visibility = Visibility.Hidden;
                }
                // break;*/
            }
            suma_label.Content = suma_zam + "zl";
            suma1_label.Content = suma_zam + "zl";

        }
        private void tworz_tablice()
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

            zamowienie_lab1[0] = zam11;
            zamowienie_lab1[1] = zam12;
            zamowienie_lab1[2] = zam13;
            zamowienie_lab1[3] = zam14;
            zamowienie_lab1[4] = zam15;
            zamowienie_lab1[5] = zam16;
            zamowienie_lab1[6] = zam17;
            zamowienie_lab1[7] = zam18;
            zamowienie_lab1[8] = zam19;
            zamowienie_lab1[9] = zam110;


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

            delzam1[0] = del_zam11_button;
            delzam1[1] = del_zam12_button;
            delzam1[2] = del_zam13_button;
            delzam1[3] = del_zam14_button;
            delzam1[4] = del_zam15_button;
            delzam1[5] = del_zam16_button;
            delzam1[6] = del_zam17_button;
            delzam1[7] = del_zam18_button;
            delzam1[8] = del_zam19_button;
            delzam1[9] = del_zam110_button;
        }

        //kreator
        public void SQL_buttony_z_nazwami_kreator(string sql)// przypisanie contentow buttonom w menu z wyborem pizzy
        {
            SqlCommand odczyt;
            SqlDataReader dataReader;

            try
            {
                odczyt = new SqlCommand(sql, polacz);
                dataReader = odczyt.ExecuteReader();
                Button[] tab_skladnikow = new Button[12];
                tab_skladnikow[0] = nr1;
                tab_skladnikow[1] = nr2;
                tab_skladnikow[2] = nr3;
                tab_skladnikow[3] = nr4;
                tab_skladnikow[4] = nr5;
                tab_skladnikow[5] = nr6;
                tab_skladnikow[6] = nr7;
                tab_skladnikow[7] = nr8;
                tab_skladnikow[8] = nr9;
                tab_skladnikow[9] = nr10;
                tab_skladnikow[10] = nr11;
                tab_skladnikow[11] = nr12;



                while (dataReader.Read())
                {
                    for (int i = 0; i < 12; i++)
                    {

                        tab_skladnikow[i].Content = Convert.ToString(dataReader.GetValue(0));
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

            private void Dalej(object sender, RoutedEventArgs e)
        {
            licznik++;
            if (licznik == 0)
            {
                licznik = 4;
            }
            if (licznik == 5)
            {
                licznik = 1;
            }
            if (licznik == 1)
            {
                opcja4.Visibility = Visibility.Hidden;
                opcja2.Visibility = Visibility.Hidden;
                opcja3.Visibility = Visibility.Hidden;
                opcja1.Visibility = Visibility.Visible;
                
                
            }
            if (licznik == 2)
            {
                opcja4.Visibility = Visibility.Hidden;
                opcja1.Visibility = Visibility.Hidden;
                opcja3.Visibility = Visibility.Hidden;
                opcja2.Visibility = Visibility.Visible;
                
                
            }
            if (licznik == 3)
            {
                opcja4.Visibility = Visibility.Hidden;
                opcja1.Visibility = Visibility.Hidden;
                opcja2.Visibility = Visibility.Hidden;
                opcja3.Visibility = Visibility.Visible;
                
            }
            if (licznik == 4)
            {
                opcja4.Visibility = Visibility.Visible;
                opcja1.Visibility = Visibility.Hidden;
                opcja2.Visibility = Visibility.Hidden;
                opcja3.Visibility = Visibility.Hidden;
            
            }
        }

        private void Wstecz(object sender, RoutedEventArgs e)
        {
            licznik--;
            if (licznik==0)
            {
                licznik = 4;
            }
            if (licznik == 5)
            {
                licznik = 1;
            }
            if (licznik == 1)
            {
                opcja4.Visibility = Visibility.Hidden;
                opcja2.Visibility = Visibility.Hidden;
                opcja3.Visibility = Visibility.Hidden;
                opcja1.Visibility = Visibility.Visible;
            }
            if (licznik == 2)
            {
                opcja4.Visibility = Visibility.Hidden;
                opcja1.Visibility = Visibility.Hidden;
                opcja3.Visibility = Visibility.Hidden;
                opcja2.Visibility = Visibility.Visible;
            }
            if (licznik == 3)
            {
                opcja4.Visibility = Visibility.Hidden;
                opcja1.Visibility = Visibility.Hidden;
                opcja2.Visibility = Visibility.Hidden;
                opcja3.Visibility = Visibility.Visible;
            }
            if (licznik == 4)
            {
                opcja4.Visibility = Visibility.Visible;
                opcja1.Visibility = Visibility.Hidden;
                opcja2.Visibility = Visibility.Hidden;
                opcja3.Visibility = Visibility.Hidden;
            }
            
        }

        private void Rozmiar_srednia2_Click(object sender, RoutedEventArgs e)
        {
            rozmiar_cm2.Content ="32cm";
            rozmiar_pizzy = "(srednia)";
            kwota_skl.Content = suma_skladnikow_sr;
        }
        private void Rozmiar_duza2_Click(object sender, RoutedEventArgs e)
        {
            rozmiar_cm2.Content ="48cm";
            rozmiar_pizzy = "(duza)";
            kwota_skl.Content = suma_skladnikow_d;


        }
        private void licz_sum_skl(int id_skladnika)
        {
            
                SqlCommand odczyt;
                SqlDataReader dataReader;
            string sql = "Select Cena_sr, Cena_d from skladniki where IDskladniku =" + id_skladnika + "";
                try
                {
                    odczyt = new SqlCommand(sql, polacz);
                    dataReader = odczyt.ExecuteReader();
                    // while (dataReader.Read())
                    // {
                    dataReader.Read();
                    cena_sr= Convert.ToInt32(dataReader.GetValue(0));
                    cena_d = Convert.ToInt32(dataReader.GetValue(1));
                suma_skladnikow_sr += cena_sr;
                suma_skladnikow_d += cena_d;
                // }
                dataReader.Close();
                    odczyt.Dispose();
                }
                catch
                {
                    MessageBox.Show("Niepoprawna składnia");
                }
            
            //  SQL_label(kwota_skl, "Select Cena_sr, Cena_d from skladniki where IDskladniku =" + id_wyb_skladnika + "", 1);
            //  zl_wyb2.Visibility = Visibility.Visible;

        }




        //menu wyboru skladnikow {pokazanie wartosci )



        private void Nr1_Click(object sender, RoutedEventArgs e)
        {
            if (prod1.Visibility == Visibility.Collapsed)
            {
                skladniki2.Height += 37;
                prod1.Visibility = Visibility.Visible;
                nr1.Opacity = 0.4;
                prod1.Content = nr1.Content;
                id_wyb_skladnika = 1;
                licz_sum_skl(id_wyb_skladnika);
                if (rozmiar_pizzy == "(duza)")
                    kwota_skl.Content = suma_skladnikow_d;
                else
                    kwota_skl.Content = suma_skladnikow_sr;

            }
            else
            {
                skladniki2.Height -= 37;
                prod1.Visibility = Visibility.Collapsed;
                nr1.Opacity = 1;
                suma_skladnikow_d -= cena_d;
                suma_skladnikow_sr -= cena_sr;
                if (rozmiar_pizzy == "(duza)")
                    kwota_skl.Content = suma_skladnikow_d;
                else
                    kwota_skl.Content = suma_skladnikow_sr;
            }
        }
        private void Nr2_Click(object sender, RoutedEventArgs e)
        {
            if (prod2.Visibility == Visibility.Collapsed)
            {
                skladniki2.Height += 37;
                prod2.Visibility = Visibility.Visible;
                nr2.Opacity = 0.4;
                prod2.Content = nr2.Content;
                id_wyb_skladnika = 2;
                licz_sum_skl(id_wyb_skladnika);
                if (rozmiar_pizzy == "(duza)")
                    kwota_skl.Content = suma_skladnikow_d;
                else
                    kwota_skl.Content = suma_skladnikow_sr;
            }
            else
            {
                skladniki2.Height -= 37;
                prod2.Visibility = Visibility.Collapsed;
                nr2.Opacity = 1;
                suma_skladnikow_d -= cena_d;
                suma_skladnikow_sr -= cena_sr;
                if (rozmiar_pizzy == "(duza)")
                    kwota_skl.Content = suma_skladnikow_d;
                else
                    kwota_skl.Content = suma_skladnikow_sr;
            }
        }
        private void Nr3_Click(object sender, RoutedEventArgs e)
        {
            if (prod3.Visibility == Visibility.Collapsed)
            {
                skladniki2.Height += 37;
                prod3.Visibility = Visibility.Visible;
                nr3.Opacity = 0.4;
                prod3.Content = nr3.Content;
                id_wyb_skladnika = 3;
                licz_sum_skl(id_wyb_skladnika);
                if (rozmiar_pizzy == "(duza)")
                    kwota_skl.Content = suma_skladnikow_d;
                else
                    kwota_skl.Content = suma_skladnikow_sr;
            }
            else
            {
                skladniki2.Height -= 37;
                prod3.Visibility = Visibility.Collapsed;
                nr3.Opacity = 1;
                suma_skladnikow_d -= cena_d;
                suma_skladnikow_sr -= cena_sr;
                if (rozmiar_pizzy == "(duza)")
                    kwota_skl.Content = suma_skladnikow_d;
                else
                    kwota_skl.Content = suma_skladnikow_sr;

            }
        }
        private void Nr4_Click(object sender, RoutedEventArgs e)
        {
            if (prod4.Visibility == Visibility.Collapsed)
            {
                skladniki2.Height += 37;
                prod4.Visibility = Visibility.Visible;
                nr4.Opacity = 0.4;
                prod4.Content = nr4.Content;
                id_wyb_skladnika = 4;
                licz_sum_skl(id_wyb_skladnika);
                if (rozmiar_pizzy == "(duza)")
                    kwota_skl.Content = suma_skladnikow_d;
                else
                    kwota_skl.Content = suma_skladnikow_sr;
            }
            else
            {
                skladniki2.Height -= 37;
                prod4.Visibility = Visibility.Collapsed;
                nr4.Opacity = 1;
                suma_skladnikow_d -= cena_d;
                suma_skladnikow_sr -= cena_sr;
                if (rozmiar_pizzy == "(duza)")
                    kwota_skl.Content = suma_skladnikow_d;
                else
                    kwota_skl.Content = suma_skladnikow_sr;

            }
        }
        private void Nr5_Click(object sender, RoutedEventArgs e)
        {
            if (prod5.Visibility == Visibility.Collapsed)
            {
                skladniki2.Height += 37;
                prod5.Visibility = Visibility.Visible;
                nr5.Opacity = 0.4;
                prod5.Content = nr5.Content;
                id_wyb_skladnika = 5;
                licz_sum_skl(id_wyb_skladnika);
                if (rozmiar_pizzy == "(duza)")
                    kwota_skl.Content = suma_skladnikow_d;
                else
                    kwota_skl.Content = suma_skladnikow_sr;
            }
            else
            {
                skladniki2.Height -= 37;
                prod5.Visibility = Visibility.Collapsed;
                nr5.Opacity = 1;
                suma_skladnikow_d -= cena_d;
                suma_skladnikow_sr -= cena_sr;
                if (rozmiar_pizzy == "(duza)")
                    kwota_skl.Content = suma_skladnikow_d;
                else
                    kwota_skl.Content = suma_skladnikow_sr;
            }
        }
        private void Nr6_Click(object sender, RoutedEventArgs e)
        {
            if (prod6.Visibility == Visibility.Collapsed)
            {
                skladniki2.Height += 37;
                prod6.Visibility = Visibility.Visible;
                nr6.Opacity = 0.4;
                prod6.Content = nr6.Content;
                id_wyb_skladnika = 6;
                licz_sum_skl(id_wyb_skladnika);
                if (rozmiar_pizzy == "(duza)")
                    kwota_skl.Content = suma_skladnikow_d;
                else
                    kwota_skl.Content = suma_skladnikow_sr;
            }
            else
            {
                skladniki2.Height -= 37;
                prod6.Visibility = Visibility.Collapsed;
                nr6.Opacity = 1;
                suma_skladnikow_d -= cena_d;
                suma_skladnikow_sr -= cena_sr;
                if (rozmiar_pizzy == "(duza)")
                    kwota_skl.Content = suma_skladnikow_d;
                else
                    kwota_skl.Content = suma_skladnikow_sr;
            }
        }
        private void Nr7_Click(object sender, RoutedEventArgs e)
        {
            if (prod7.Visibility == Visibility.Collapsed)
            {
                skladniki2.Height += 37;
                prod7.Visibility = Visibility.Visible;
                nr7.Opacity = 0.4;
                prod7.Content = nr7.Content;
                id_wyb_skladnika = 7;
                licz_sum_skl(id_wyb_skladnika);
                if (rozmiar_pizzy == "(duza)")
                    kwota_skl.Content = suma_skladnikow_d;
                else
                    kwota_skl.Content = suma_skladnikow_sr;
            }
            else
            {
                skladniki2.Height -= 37;
                prod7.Visibility = Visibility.Collapsed;
                nr7.Opacity = 1;
                suma_skladnikow_d -= cena_d;
                suma_skladnikow_sr -= cena_sr;
                if (rozmiar_pizzy == "(duza)")
                    kwota_skl.Content = suma_skladnikow_d;
                else
                    kwota_skl.Content = suma_skladnikow_sr;
            }
        }
        private void Nr8_Click(object sender, RoutedEventArgs e)
        {
            if (prod8.Visibility == Visibility.Collapsed)
            {
                skladniki2.Height += 37;
                prod8.Visibility = Visibility.Visible;
                nr8.Opacity = 0.4;
                prod8.Content = nr8.Content;
                id_wyb_skladnika = 8;
                licz_sum_skl(id_wyb_skladnika);
                if (rozmiar_pizzy == "(duza)")
                    kwota_skl.Content = suma_skladnikow_d;
                else
                    kwota_skl.Content = suma_skladnikow_sr;
            }
            else
            {
                skladniki2.Height -= 37;
                prod8.Visibility = Visibility.Collapsed;
                nr8.Opacity = 1;
                suma_skladnikow_d -= cena_d;
                suma_skladnikow_sr -= cena_sr;
                if (rozmiar_pizzy == "(duza)")
                    kwota_skl.Content = suma_skladnikow_d;
                else
                    kwota_skl.Content = suma_skladnikow_sr;
            }
        }
        private void Nr9_Click(object sender, RoutedEventArgs e)
        {
            if (prod9.Visibility == Visibility.Collapsed)
            {
                skladniki2.Height += 37;
                prod9.Visibility = Visibility.Visible;
                nr9.Opacity = 0.4;
                prod9.Content = nr9.Content;
                id_wyb_skladnika = 10;
                licz_sum_skl(id_wyb_skladnika);
                if (rozmiar_pizzy == "(duza)")
                    kwota_skl.Content = suma_skladnikow_d;
                else
                    kwota_skl.Content = suma_skladnikow_sr;
            }
            else
            {
                skladniki2.Height -= 37;
                prod9.Visibility = Visibility.Collapsed;
                nr9.Opacity = 1;
                suma_skladnikow_d -= cena_d;
                suma_skladnikow_sr -= cena_sr;
                if (rozmiar_pizzy == "(duza)")
                    kwota_skl.Content = suma_skladnikow_d;
                else
                    kwota_skl.Content = suma_skladnikow_sr;
            }
        }
        private void Nr10_Click(object sender, RoutedEventArgs e)
        {
            if (prod10.Visibility == Visibility.Collapsed)
            {
                skladniki2.Height += 37;
                prod10.Visibility = Visibility.Visible;
                nr10.Opacity = 0.4;
                prod10.Content = nr10.Content;
                id_wyb_skladnika = 11;
                licz_sum_skl(id_wyb_skladnika);
                if (rozmiar_pizzy == "(duza)")
                    kwota_skl.Content = suma_skladnikow_d;
                else
                    kwota_skl.Content = suma_skladnikow_sr;
            }
            else
            {
                skladniki2.Height -= 37;
                prod10.Visibility = Visibility.Collapsed;
                nr10.Opacity = 1;
                suma_skladnikow_d -= cena_d;
                suma_skladnikow_sr -= cena_sr;
                if (rozmiar_pizzy == "(duza)")
                    kwota_skl.Content = suma_skladnikow_d;
                else
                    kwota_skl.Content = suma_skladnikow_sr;
            }
        }
        private void Nr11_Click(object sender, RoutedEventArgs e)
        {
            if (prod11.Visibility == Visibility.Collapsed)
            {
                skladniki2.Height += 37;
                prod11.Visibility = Visibility.Visible;
                nr11.Opacity = 0.4;
                prod11.Content = nr11.Content;
                id_wyb_skladnika = 12;
                licz_sum_skl(id_wyb_skladnika);
                if (rozmiar_pizzy == "(duza)")
                    kwota_skl.Content = suma_skladnikow_d;
                else
                    kwota_skl.Content = suma_skladnikow_sr;
            }
            else
            {
                skladniki2.Height -= 37;
                prod11.Visibility = Visibility.Collapsed;
                nr11.Opacity = 1;
                suma_skladnikow_d -= cena_d;
                suma_skladnikow_sr -= cena_sr;
                if (rozmiar_pizzy == "(duza)")
                    kwota_skl.Content = suma_skladnikow_d;
                else
                    kwota_skl.Content = suma_skladnikow_sr;
            }
        }
        private void Nr12_Click(object sender, RoutedEventArgs e)
        {
            if (prod12.Visibility == Visibility.Collapsed)
            {
                skladniki2.Height += 37;
                prod12.Visibility = Visibility.Visible;
                nr12.Opacity = 0.4;
                prod12.Content = nr12.Content;
                id_wyb_skladnika = 13;
                licz_sum_skl(id_wyb_skladnika);
                if (rozmiar_pizzy == "(duza)")
                    kwota_skl.Content = suma_skladnikow_d;
                else
                    kwota_skl.Content = suma_skladnikow_sr;
            }
            else
            {
                skladniki2.Height -= 37;
                prod12.Visibility = Visibility.Collapsed;
                nr12.Opacity = 1;
                suma_skladnikow_d -= cena_d;
                suma_skladnikow_sr -= cena_sr;
                if (rozmiar_pizzy == "(duza)")
                    kwota_skl.Content = suma_skladnikow_d;
                else
                    kwota_skl.Content = suma_skladnikow_sr;
            }
        }


        //buttony

        private void Wybierz_Pizze_button_Click(object sender, RoutedEventArgs e)
        {
            menu.Visibility = Visibility.Collapsed;
            SQL_buttony_z_nazwami("SELECT Nazwa from Pizze");
            Pizza_button_Click(sender, e);
            
            wybor.Visibility = Visibility.Visible;  
        }

        private void kompozycja_Click_1(object sender, RoutedEventArgs e)
        {
            menu.Visibility = Visibility.Collapsed;
            kreator.Visibility = Visibility.Visible;
            SQL_buttony_z_nazwami_kreator("SELECT Nazwa from skladniki");
            rozmiar_pizzy = "(duza)";
                kwota_skl.Content = suma_skladnikow_d;
        }

        private void zamow_Click_2(object sender, RoutedEventArgs e)
        {
            menu.Visibility = Visibility.Collapsed;
            zamowienie.Visibility = Visibility.Visible;
         /*   zam11.Content = zam.Content;
            zam12.Content = zam1.Content;
            zam13.Content = zam2.Content;
            zam14.Content = zam3.Content;
            zam15.Content = zam4.Content;
            zam16.Content = zam5.Content;
            zam17.Content = zam6.Content;
            zam18.Content = zam7.Content;
            zam19.Content = zam8.Content;
            zam110.Content = zam9.Content;*/
        }

        private void Anuluj_button_Click(object sender, RoutedEventArgs e)
        {
            wybor.Visibility = Visibility.Collapsed;
            menu.Visibility = Visibility.Visible;
        }

        private void Dodaj_zamowienie_button_Click(object sender, RoutedEventArgs e)
        {
            for (int i=0;i<10;i++)
            {
                if (zamowienia[i, 0] is null)
                {
                    zamowienia[i, 0] = nazwa_wyb_pizzy;
                    zamowienia[i, 1] = rozmiar_pizzy;
                    zamowienia[i, 2] = Convert.ToString(kwota_kr.Content);
                    zamowienie_lab[i].Visibility = Visibility.Visible;
                    zamowienie_lab1[i].Visibility = Visibility.Visible;
                    zamowienie_lab[i].Content = zamowienia[i, 0] + " " + zamowienia[i, 1] + "(" + zamowienia[i, 2] + "zl)";
                    zamowienie_lab1[i].Content = zamowienie_lab[i].Content;
                    delzam[i].Visibility = Visibility.Visible;
                    delzam1[i].Visibility = delzam[i].Visibility;
                    suma_zam += int.Parse(zamowienia[i, 2]);
                    break;
                }
                
            }
            suma_label.Content = suma_zam+"zl";
            suma1_label.Content = suma_label.Content;
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
            for (int i = 0; i < 10; i++)
            {
                if (zamowienia[i, 0] is null)
                {
                    zamowienia[i, 0] = "wlasna pizza";
                    zamowienia[i, 1] = rozmiar_pizzy;
                    zamowienia[i, 2] = Convert.ToString(kwota_skl.Content);
                    zamowienia[i, 3] = Convert.ToString(prod1.Content);
                    zamowienia[i, 4] = Convert.ToString(prod2.Content);
                    zamowienia[i, 5] = Convert.ToString(prod3.Content);
                    zamowienia[i, 6] = Convert.ToString(prod4.Content);
                    zamowienia[i, 7] = Convert.ToString(prod5.Content);
                    zamowienia[i, 8] = Convert.ToString(prod6.Content);
                    zamowienia[i, 9] = Convert.ToString(prod7.Content);
                    zamowienia[i, 10] = Convert.ToString(prod8.Content);
                    zamowienia[i, 11] = Convert.ToString(prod9.Content);
                    zamowienia[i, 12] = Convert.ToString(prod10.Content);
                    zamowienia[i, 13] = Convert.ToString(prod11.Content);
                    zamowienia[i, 14] = Convert.ToString(prod12.Content);
                    zamowienie_lab[i].Visibility = Visibility.Visible;
                    zamowienie_lab1[i].Visibility = Visibility.Visible;
                    zamowienie_lab[i].Content = zamowienia[i, 0] + " " + zamowienia[i, 1] + "(" + zamowienia[i, 2] + "zl)";
                    zamowienie_lab1[i].Content = zamowienie_lab[i].Content;
                    delzam[i].Visibility = Visibility.Visible;
                    delzam1[i].Visibility = delzam[i].Visibility;
                    suma_zam += int.Parse(zamowienia[i, 2]);
                    break;
                }

            }
            suma_label.Content = suma_zam + "zl";
            suma1_label.Content = suma_label.Content;
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
            SQL_max_id();
            dodaj_zamowienie();
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
            Rozmiar_duza_Click(sender, e);
        }

        private void Pizza_button1_Click(object sender, RoutedEventArgs e)
        {
            wybor_pizzy("2");
            nazwa_wyb_pizzy = Convert.ToString(pizza_button1.Content);
            Rozmiar_duza_Click(sender, e);
        }

        private void Pizza_button2_Click(object sender, RoutedEventArgs e)
        {
            wybor_pizzy("3");
            nazwa_wyb_pizzy = Convert.ToString(pizza_button2.Content);
            Rozmiar_duza_Click(sender, e);
        }

        private void Pizza_button3_Click(object sender, RoutedEventArgs e)
        {
            wybor_pizzy("4");
            nazwa_wyb_pizzy = Convert.ToString(pizza_button3.Content);
            Rozmiar_duza_Click(sender, e);
        }

        private void Pizza_button4_Click(object sender, RoutedEventArgs e)
        {
            wybor_pizzy("5");
            nazwa_wyb_pizzy = Convert.ToString(pizza_button4.Content);
            Rozmiar_duza_Click(sender, e);
        }

        private void Pizza_button5_Click(object sender, RoutedEventArgs e)
        {
            wybor_pizzy("6");
            nazwa_wyb_pizzy = Convert.ToString(pizza_button5.Content);
            Rozmiar_duza_Click(sender, e);
        }

        private void Pizza_button6_Click(object sender, RoutedEventArgs e)
        {
            wybor_pizzy("7");
            nazwa_wyb_pizzy = Convert.ToString(pizza_button6.Content);
            Rozmiar_duza_Click(sender, e);
        }

        private void Pizza_button7_Click(object sender, RoutedEventArgs e)
        {
            wybor_pizzy("8");
            nazwa_wyb_pizzy = Convert.ToString(pizza_button7.Content);
            Rozmiar_duza_Click(sender, e);
        }

        private void Pizza_button8_Click(object sender, RoutedEventArgs e)
        {
            wybor_pizzy("10");
            nazwa_wyb_pizzy = Convert.ToString(pizza_button8.Content);
            Rozmiar_duza_Click(sender, e);
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
            del_zam11_button.Visibility = Visibility.Hidden;
            suma_zam -= Convert.ToInt32(zamowienia[0, 2]);
            zamowienia[0, 0] = null;
            zamowienia[0, 1] = null;
            zamowienia[0, 2] = null;
            zamowienie_lab[0].Content = "";
            zamowienie_lab1[0].Content = "";
            Usun_zam();


           // suma_label.Content = suma_zam+"zl";
        }
        private void Del_zam2_button_Click(object sender, RoutedEventArgs e)
        {
            del_zam2_button.Visibility = Visibility.Hidden;
            del_zam12_button.Visibility = Visibility.Hidden;
            suma_zam -= Convert.ToInt32(zamowienia[1, 2]);
            zamowienia[1, 0] = null;
            zamowienia[1, 1] = null;
            zamowienia[1, 2] = null;
            zamowienie_lab[1].Content = "";
            zamowienie_lab1[1].Content = "";
            Usun_zam();
          //  suma_label.Content = suma_zam + "zl";
        }
        private void Del_zam3_button_Click(object sender, RoutedEventArgs e)
        {
            del_zam3_button.Visibility = Visibility.Hidden;
            del_zam13_button.Visibility = Visibility.Hidden;
            suma_zam -= Convert.ToInt32(zamowienia[2, 2]);
            zamowienia[2, 0] = null;
            zamowienia[2, 1] = null;
            zamowienia[2, 2] = null;
            zamowienie_lab[2].Content = "";
            zamowienie_lab1[2].Content = "";
            Usun_zam();
          //  suma_label.Content = suma_zam + "zl";
        }
        private void Del_zam4_button_Click(object sender, RoutedEventArgs e)
        {
            del_zam4_button.Visibility = Visibility.Hidden;
            del_zam14_button.Visibility = Visibility.Hidden;
            suma_zam -= Convert.ToInt32(zamowienia[3, 2]);
            zamowienia[3, 0] = null;
            zamowienia[3, 1] = null;
            zamowienia[3, 2] = null;
            zamowienie_lab[3].Content = "";
            zamowienie_lab1[3].Content = "";
            Usun_zam();
           // suma_label.Content = suma_zam + "zl";
        }
        private void Del_zam5_button_Click(object sender, RoutedEventArgs e)
        {
            del_zam5_button.Visibility = Visibility.Hidden;
            del_zam15_button.Visibility = Visibility.Hidden;
            suma_zam -= Convert.ToInt32(zamowienia[4, 2]);
            zamowienia[4, 0] = null;
            zamowienia[4, 1] = null;
            zamowienia[4, 2] = null;
            zamowienie_lab[4].Content = "";
            zamowienie_lab1[4].Content = "";
            Usun_zam();
           // suma_label.Content = suma_zam + "zl";
        }
        private void Del_zam6_button_Click(object sender, RoutedEventArgs e)
        {
            del_zam6_button.Visibility = Visibility.Hidden;
            del_zam16_button.Visibility = Visibility.Hidden;
            suma_zam -= Convert.ToInt32(zamowienia[5, 2]);
            zamowienia[5, 0] = null;
            zamowienia[5, 1] = null;
            zamowienia[5, 2] = null;
            zamowienie_lab[5].Content = "";
            zamowienie_lab1[5].Content = "";
            Usun_zam();
           // suma_label.Content = suma_zam + "zl";
        }
        private void Del_zam7_button_Click(object sender, RoutedEventArgs e)
        {
            del_zam7_button.Visibility = Visibility.Hidden;
            del_zam17_button.Visibility = Visibility.Hidden;
            suma_zam -= Convert.ToInt32(zamowienia[6, 2]);
            zamowienia[6, 0] = null;
            zamowienia[6, 1] = null;
            zamowienia[6, 2] = null;
            zamowienie_lab[6].Content = "";
            zamowienie_lab1[6].Content = "";
            Usun_zam();
           // suma_label.Content = suma_zam + "zl";
        }
        private void Del_zam8_button_Click(object sender, RoutedEventArgs e)
        {
            del_zam8_button.Visibility = Visibility.Hidden;
            del_zam18_button.Visibility = Visibility.Hidden;
            suma_zam -= Convert.ToInt32(zamowienia[7, 2]);
            zamowienia[7, 0] = null;
            zamowienia[7, 1] = null;
            zamowienia[7, 2] = null;
            zamowienie_lab[7].Content = "";
            zamowienie_lab1[7].Content = "";
            Usun_zam();
           // suma_label.Content = suma_zam + "zl";
        }
        private void Del_zam9_button_Click(object sender, RoutedEventArgs e)
        {
            del_zam9_button.Visibility = Visibility.Hidden;
            del_zam19_button.Visibility = Visibility.Hidden;
            suma_zam -= Convert.ToInt32(zamowienia[8, 2]);
            zamowienia[8, 0] = null;
            zamowienia[8, 1] = null;
            zamowienia[8, 2] = null;
            zamowienie_lab[8].Content = "";
            zamowienie_lab1[8].Content = "";
            Usun_zam();
           // suma_label.Content = suma_zam + "zl";
        }
        private void Del_zam10_button_Click(object sender, RoutedEventArgs e)
        {
            del_zam10_button.Visibility = Visibility.Hidden;
            del_zam110_button.Visibility = Visibility.Hidden;
            suma_zam -= Convert.ToInt32(zamowienia[9, 2]);
            zamowienia[9, 0] = null;
            zamowienia[9, 1] = null;
            zamowienia[9, 2] = null;
            zamowienie_lab[9].Content = "";
            zamowienie_lab1[9].Content = "";
            Usun_zam();
            
        }

        
    }
}