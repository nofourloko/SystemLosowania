using System;
using SystemLosowania.Files;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls.PlatformConfiguration;



namespace SystemLosowania.Models
{
	public class OperacjeNaLiścieUczniów
	{
		public ObservableCollection<ListaUczniów> listaUczniow { get; set; } = new ObservableCollection<ListaUczniów>();
        public ObservableCollection<ListaUczniów> uczniowieKlasy { get; set; } = new ObservableCollection<ListaUczniów>();
        public ObservableCollection<Klasa> listaKlas { get; set; } = new ObservableCollection<Klasa>();
        public string aktualnaKlasa { get; set; } = "";

        public SzczesliwyNumerek szczesliwyNumerek { get; set; } = new SzczesliwyNumerek();

        public OperacjeNaPliku Plik { get; set; } = new OperacjeNaPliku();

		public OperacjeNaLiścieUczniów() {
			Plik.Odczyt(listaUczniow, szczesliwyNumerek);

            if(szczesliwyNumerek.DataWylosowania.Day != DateTime.Now.Day)
            {
                Random rnd = new Random();
                szczesliwyNumerek = new SzczesliwyNumerek { Numerek = rnd.Next(1, 30), DataWylosowania = DateTime.Now };
                Plik.Zapis(listaUczniow, szczesliwyNumerek);
            }

            aktualizacjaKlasy();
        }

        public ListaUczniów LosowyNumerek()
        {
            Random rnd = new Random();
            ListaUczniów uczenWybrany;

            while (true)
            {
                int indexLosowegoUcznia = rnd.Next(uczniowieKlasy.Count);
                ListaUczniów wybranyUczen = uczniowieKlasy.ElementAt(indexLosowegoUcznia);

                int wybranyUczenZeWszystkichindex = listaUczniow.IndexOf(wybranyUczen);
                ListaUczniów wybranyUczenZeWszystkich = listaUczniow.ElementAt(wybranyUczenZeWszystkichindex);

                if (wybranyUczen.CzyZostałaOdpytana == false && wybranyUczen.Numerek != szczesliwyNumerek.Numerek )
                {
                    wybranyUczen.CzyZostałaOdpytana = true;
                    wybranyUczenZeWszystkich.CzyZostałaOdpytana = true;

                    foreach(var uczen in uczniowieKlasy)
                    {
                        if(uczen.CzyZostałaOdpytana == true)
                        {
                            uczen.LiczbaOsobWylosowanychPoOdpytaniu++;

                            if(uczen.LiczbaOsobWylosowanychPoOdpytaniu > 3)
                            {
                                uczen.CzyZostałaOdpytana = false;
                                uczen.LiczbaOsobWylosowanychPoOdpytaniu = 0;
                            }
                        }
                    }

                    foreach (var uczen in listaUczniow)
                    {
                        if (uczen.CzyZostałaOdpytana == true)
                        {
                            uczen.LiczbaOsobWylosowanychPoOdpytaniu++;

                            if (uczen.LiczbaOsobWylosowanychPoOdpytaniu == 3)
                            {
                                uczen.CzyZostałaOdpytana = false;
                                uczen.LiczbaOsobWylosowanychPoOdpytaniu = 0;
                            }
                        }
                    }

                    uczenWybrany = wybranyUczen;
                    break;
                }
            }
            return uczenWybrany;
        }

		public string wybranieKlasy(string klasa)
		{
            uczniowieKlasy.Clear();
            foreach (var uczen in listaUczniow)
            {
                if (uczen.Obecny == false)
                {
                    uczen.Obecny = true;
                }
            }

            sortowanieUczniow(klasa);

            aktualnaKlasa = uczniowieKlasy[0].Klasa;
            return aktualnaKlasa;
        }

        public void sortowanieUczniow(string klasa)
        {
            ObservableCollection<ListaUczniów> obecni = new ObservableCollection<ListaUczniów>(
            (listaUczniow.Where((uczen) => uczen.Klasa == klasa).OrderBy((uczen) => uczen.Numerek))
                ) ;
            foreach (var item in obecni)
            {
                uczniowieKlasy.Add(item);
            }
        }

		public void sprawdzenieObecnosci(ObservableCollection<ListaUczniów> listaObecnosci)
		{
            uczniowieKlasy.Clear();
            foreach (var item in listaObecnosci)
            {
                item.Obecny = false;
                uczniowieKlasy.Add(item);
            }

        }

      

        public void zmianaDanychUcznia(ListaUczniów uczen, bool zmianaKlasy)
        {
            if(zmianaKlasy == true)
            {
                uczniowieKlasy.Remove(uczen);
                if(uczniowieKlasy.Count == 0)
                {
                    aktualizacjaKlasy();    
                }
            }

            Plik.Zapis(listaUczniow, szczesliwyNumerek);
        }

        public void usniecieUcznia(ListaUczniów uczen)
        {

            listaUczniow.Remove(uczen);
            uczniowieKlasy.Remove(uczen);

            aktualizacjaKlasy();

            Plik.Zapis(listaUczniow, szczesliwyNumerek);

        }

        public string dodanieUcznia(string imie, string nazwisko, string klasa, int numerek)
        {

            if (listaUczniow.Where(item =>  item.Klasa == klasa && item.Numerek == numerek).Count() > 0)
            {
                return "Podany numerek jest zajęty proszę wpisać inny";
            }

            if(listaUczniow.Where(item => item.Klasa == klasa).Count() > 30)
            {
                return "Ta klasa jest już pełna";
            }


            ListaUczniów nowyUcznen = new ListaUczniów
            {
                Imie = imie,
                Klasa = klasa,
                Nazwisko = nazwisko,
                CzyZostałaOdpytana = false,
                LiczbaOsobWylosowanychPoOdpytaniu = 0,
                Numerek = numerek,
                Obecny = true
            };

            

            listaUczniow.Add(nowyUcznen);

            if (aktualnaKlasa != "")
            {
                uczniowieKlasy.Clear();
                sortowanieUczniow(klasa);
            }


            if (listaKlas.Where(item => item.nazwaKlasy == klasa).Count() == 0 )
            {
                listaKlas.Add(new Klasa { nazwaKlasy = klasa});
            }

            aktualizacjaKlasy();
           
            Plik.Zapis(listaUczniow, szczesliwyNumerek);

            return "";
        }

        public void aktualizacjaKlasy()
        {
            var posortowaneKlasy = listaUczniow.OrderBy(uczen => uczen.Klasa)
                .GroupBy(uczen => uczen.Klasa)
                .Select(group => group.First())
                .ToList();
            listaKlas.Clear();

            foreach (var item in posortowaneKlasy)
            {
                listaKlas.Add(new Klasa { nazwaKlasy = item.Klasa });
            }
        }

    }
}

