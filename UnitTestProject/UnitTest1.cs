using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pizzeria;
using System.Data.SqlClient;
using System.Windows.Controls;
using System.Windows;
namespace UnitTestProject
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void test_polaczenia_z_baza()
        {
            // arrange  
            Projekt Test = new Projekt();
            bool polaczonot = true;
            // act  
            Test.SQLopen();

            // assert  
            bool polaczono = Test.polaczono;
            Assert.AreEqual(polaczonot,polaczono, "Nie polaczono z baza danych");
        }

        [TestMethod]
        public void content_labela_sql()
        {
            // arrange  
            Projekt Test = new Projekt();
            Label pole=new Label();
            Label polet = new Label();
            int id=2;

            // act  
            Test.SQLopen();
            Test.SQL_label(pole, "Select Cena_sredniej, Cena_duzej from pizze where ID_Pizzy =" + id + "", 0);

            // assert  
            polet.Content = "17";
            Assert.AreEqual(polet.Content,pole.Content, "Nie przypisuje prawidłowo");
        }

        [TestMethod]
        public void dodawanie_zamowienia_do_sql()
        {
            // arrange  
            Projekt Test = new Projekt();
            int max_id_zamt = 1;
            string[,] zamowieniat = new string[10, 15];
            zamowieniat[2, 2] = null;
        // act  
        Test.SQLopen();
            Test.dodaj_zamowienie();

            // assert  
            
            Assert.AreEqual(max_id_zamt, Test.max_id_zam, "ID nie jest inkrementowane prawidłowo");
            Assert.AreEqual(zamowieniat[2, 2], Test.zamowienia[2, 2], "Nie prawidlowo dodaje zamowienie");
        }

        [TestMethod]
        public void contenty_buttonow()
        {
            // arrange  
            Projekt Test = new Projekt();
            string dla0 = "Margherita";
            string dla1 = "Capricciosa";
            string dla7 = "Familijna";
            string dla8 = "Vesuvio";


            // act  
            Test.SQLopen();
            Test.SQL_buttony_z_nazwami("SELECT Nazwa from Pizze");

            // assert  

            Assert.AreEqual(dla0, Test.tab_pizz[0].Content, "Niepoprawnie przypisuje");
            Assert.AreEqual(dla1, Test.tab_pizz[1].Content, "Niepoprawnie przypisuje");
            Assert.AreEqual(dla7, Test.tab_pizz[7].Content, "Niepoprawnie przypisuje");
            Assert.AreEqual(dla8, Test.tab_pizz[8].Content, "Niepoprawnie przypisuje");
        }

        [TestMethod]
        public void wyswietlanie_listy_skladnikow()
        {
            // arrange  
            Projekt Test = new Projekt();
            string id = "2";
            string[] skladnik = new string[9];
            for (int i = 0; i < 9; i++)
                skladnik[i] = "";

            skladnik[0] = "sos pomidorowy";
            skladnik[1] = "ser";
            skladnik[2] = "pieczarki";
            skladnik[3] = "szynka";

            // act  
            Test.SQLopen();
            Test.wybor_pizzy(id);

            // assert  
            for (int i = 0; i < 9; i++)
            {
                Assert.AreEqual(skladnik[i], Test.lab_pizz[i].Content, "Niepoprawnie przypisuje");
            }
        }

        [TestMethod]
        public void zastepowanie_zamowien_i_kwota_do_zaplaty()
        {
            // arrange  
            Projekt Test = new Projekt();
            string zamowienie1 = "";
            int kwota = 0;

            // act  
            Test.Usun_zam();

            // assert  
                Assert.AreEqual(zamowienie1, Test.zamowienie_lab[0].Content, "Niepoprawnie dziala");
            Assert.AreEqual(kwota, Test.suma_zam, "Niepoprawnie dziala");
        }
        [TestMethod]
        public void przewijanie_listy_w_prawo()
        {
            // arrange  
            Projekt Test = new Projekt();
            RoutedEventArgs e = null;
            int licznik = 1;
            Test.licznik = 4;

            // act  
            Test.Dalej(Test.prawo, e);

            // assert 
            
            Assert.AreEqual(licznik, Test.licznik, "Niepoprawnie dziala");
        }

        [TestMethod]
        public void przewijanie_listy_w_lewo()
        {
            // arrange  
            Projekt Test = new Projekt();
            RoutedEventArgs e = null;
            int licznik = 1;
            Test.licznik = 6;

            // act  
            Test.Wstecz(Test.lewo, e);

            // assert 

            Assert.AreEqual(licznik, Test.licznik, "Niepoprawnie dziala");
        }

        [TestMethod]
        public void suma_sklanikow()
        {
            // arrange  
            Projekt Test = new Projekt();
            int dlasredniej = 10;
            int dladuzej = 13;

            // act  
            Test.SQLopen();
            Test.licz_sum_skl(5);

            // assert  
                Assert.AreEqual(dlasredniej, Test.suma_skladnikow_sr, "Niepoprawnie dziala");
            Assert.AreEqual(dladuzej, Test.suma_skladnikow_d, "Niepoprawnie dziala");
        }
    }
}
