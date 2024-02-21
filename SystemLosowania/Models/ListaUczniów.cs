using System;
using System.ComponentModel;

using System;
namespace SystemLosowania.Models
{
	public class ListaUczniów : INotifyPropertyChanged
    {
        private string imie;
        public string Imie
        {
            get { return imie; }
            set
            {
                if(imie != value)
                {
                    imie = value;
                    NotifyPropertyChanged("Imie");
                }
            }
        }

        private string nazwisko;
        public string Nazwisko
        {
            get { return nazwisko; }
            set
            {
                if(nazwisko != value)
                {
                    nazwisko = value;
                    NotifyPropertyChanged("Nazwisko");
                }
            }
        }
		public int Numerek { get; set; }


        private string klasa;
        public string Klasa
        {
            get { return klasa; }
            set
            {
                if (klasa != value)
                {
                    klasa = value;
                    NotifyPropertyChanged("Klasa");
                }
            }
        }
        
		private bool czyZostałaOdpytana;
        

        public bool CzyZostałaOdpytana
		{
            get { return czyZostałaOdpytana; }
            set
            {
                if (czyZostałaOdpytana != value)
                {
                    czyZostałaOdpytana = value;
                    NotifyPropertyChanged("CzyZostałaOdpytana");
                }
            }
        }
        private int liczbaOsobWylosowanychPoOdpytaniu;

        public int LiczbaOsobWylosowanychPoOdpytaniu
        {
            get { return liczbaOsobWylosowanychPoOdpytaniu; }
            set
            {
                if (liczbaOsobWylosowanychPoOdpytaniu != value)
                {
                    liczbaOsobWylosowanychPoOdpytaniu = value;
                    NotifyPropertyChanged("LiczbaOsobWylosowanychPoOdpytaniu");
                }
            }
        }

        private bool obecny;
        public bool Obecny
        {
            get { return obecny; }
            set
            {
                if (obecny != value)
                {
                    obecny = value;
                    NotifyPropertyChanged("Obecny");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

