using SystemLosowania.Models;

namespace SystemLosowania.Views;

public partial class DodanieUcznia : ContentPage
{
    public OperacjeNaLiścieUczniów operacjeNaLiscie { get; set; }

    public DodanieUcznia(OperacjeNaLiścieUczniów _operacjeNaLiscie)
    {
        operacjeNaLiscie = _operacjeNaLiscie;
        InitializeComponent();
    }

    async void Button_DodajUcznia(System.Object sender, System.EventArgs e)
    {
        string imie, nazwisko, klasa;
        int numerek = 0;

        if (ImieEditor.Text != null)
        {
            imie = ImieEditor.Text;
        }
        else
        {
            ButtonDodajUcznia.Text = "Proszę podać imie i sprobowac ponownie";
            return;
        }

        if (NaziwskoEditor.Text != null)
        {
            nazwisko = NaziwskoEditor.Text;
        }
        else
        {
            ButtonDodajUcznia.Text = "Proszę podać nazwisko i sprobowac ponownie";
            return;
        }

        if (KlasaEditor.Text != null)
        {
            klasa = KlasaEditor.Text;
            
        }
        else
        {
            ButtonDodajUcznia.Text = "Proszę podać klase i sprobowac ponownie";
            return;
        }

        if (NumerEditor.Text != null)
        {
            if(Int32.TryParse(NumerEditor.Text, out int n))
            {
                numerek = n;
                if(numerek > 30)
                {
                    ButtonDodajUcznia.Text = "Numerek jest za duzy";
                    return;
                }
                else if(numerek < 0)
                {
                    ButtonDodajUcznia.Text = "Numerek jest za mały";
                    return;
                }
            }
            
        }
        else
        {
            ButtonDodajUcznia.Text = "Proszę podać numerek i sprobowac ponownie";
            return;
        }


        string czyUdane = operacjeNaLiscie.dodanieUcznia(imie, nazwisko, klasa, numerek);
        if(czyUdane == "")
        {
            await Navigation.PopAsync();
        }
        else
        {
            ButtonDodajUcznia.Text = czyUdane;
        }
        
    }
}