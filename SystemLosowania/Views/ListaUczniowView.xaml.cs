using System.Collections.ObjectModel;
using SystemLosowania.Controls;
using SystemLosowania.Models;

namespace SystemLosowania.Views
{
    public partial class ListaUczniowView : ContentPage
    {
        public OperacjeNaLiścieUczniów operacjeNaLiscie { get; set; } = new OperacjeNaLiścieUczniów();
        public ObservableCollection<ListaUczniów> listaObecnosci { get; set; } = new ObservableCollection<ListaUczniów>();
        public ListaUczniów wylosowanyUczen { get; set; } = new ListaUczniów();
        public bool radioButtonObeconosc { get; set; } = false;

        public ListaUczniowView()
        {
            InitializeComponent();
            BindingContext = this;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (operacjeNaLiscie.uczniowieKlasy.Count == 0)
            {
                powitanie.IsVisible = true;
                WybranaKlasaGrid.IsVisible = false;
            }
        }

        void Button_Clicked_ZapiszObecność(System.Object sender, System.EventArgs e)
        {
            Button button = (Button)sender;

            if(listaObecnosci.Count() < 3)
            {
                wylosowanyUczneLabel.IsVisible = true;
                wylosowanyUczneLabel.Text = "Wybierz większą liczbę uczniów";
            }

            else if (button.Text == "Zapisz obecność")
            {
                operacjeNaLiscie.sprawdzenieObecnosci(listaObecnosci);
                radioButtonObeconosc = true;
                button.Text = "Wylosuj ucznia";
                wylosowanyUczneLabel.Text = "";


            }
            else if(listaObecnosci.Count() > 0)
            {
                wylosowanyUczneLabel.IsVisible = true;
                wylosowanyUczen = operacjeNaLiscie.LosowyNumerek();
                wylosowanyUczneLabel.Text = $"Wylosowany uczen - {wylosowanyUczen.Nazwisko} \n Numer - {wylosowanyUczen.Numerek}";
            }
           
        }

        void ListView_WybranaKlasa(System.Object sender, Microsoft.Maui.Controls.SelectedItemChangedEventArgs e)
        {
            if(e.SelectedItem != null)
            {
                listaObecnosci.Clear();

                Klasa klasa = (Klasa)e.SelectedItem;
                nazwaKlasy.Text = $"Klasa : {operacjeNaLiscie.wybranieKlasy(klasa.nazwaKlasy)}";

                powitanie.IsVisible = false;
                WybranaKlasaGrid.IsVisible = true;

                wylosowanyUczneLabel.Text = "";
                buttonObecnoscLosowanie.Text = "Zapisz obecność";

            }
            
        }

        void RadioButton_Obecnosc(System.Object sender, System.EventArgs e)
        {
            Button btn = (Button)sender;
            ListaUczniów uczenObecny = (ListaUczniów)btn.BindingContext;



            if (listaObecnosci.Contains(uczenObecny) == false)
            {
                btn.BackgroundColor = Colors.Green;
                listaObecnosci.Add(uczenObecny);
            }
            else
            {
                btn.BackgroundColor = Color.FromHex("#ac99ea");
                listaObecnosci.Remove(uczenObecny);
            }

        }

        async void ImageButton_ZobaczInformacje(System.Object sender, System.EventArgs e)
        {
            ListaUczniów uczen = (ListaUczniów)((ImageButton)sender).BindingContext;
            await Navigation.PushAsync(new DaneUcznia(uczen, operacjeNaLiscie));
        }

        async void Button_Clicked_DodajUcznia(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new DodanieUcznia(operacjeNaLiscie));
        }

    }
}


