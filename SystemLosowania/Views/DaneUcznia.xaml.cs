using System.Collections.ObjectModel;
using SystemLosowania.Models;
namespace SystemLosowania.Views;

public partial class DaneUcznia : ContentPage
{
	public ListaUczniów uczen { get; set; }
    public OperacjeNaLiścieUczniów operacjeNaLiscieUczniow { get; set; }
	public DaneUcznia(ListaUczniów uczenDane, OperacjeNaLiścieUczniów operacjeNaLiscie)
	{
        uczen = uczenDane;
        operacjeNaLiscieUczniow = operacjeNaLiscie;
        InitializeComponent();
		BindingContext = this;
	}

    async void Button_Zapisz(System.Object sender, System.EventArgs e)
    {
        bool zmianaKlasy = false;

        if(ZmianaImieniaEditor.Text != null)
        {
            uczen.Imie = ZmianaImieniaEditor.Text;
            ZmianaImieniaEditor.Text = "";
        }

        if(ZmianaNazwiskaEditor.Text != null)
        {
            uczen.Nazwisko = ZmianaNazwiskaEditor.Text;
            ZmianaNazwiskaEditor.Text = "";
        }

        if(ZmianaKlaseEditor.Text != null)
        {
            uczen.Klasa = ZmianaKlaseEditor.Text;
            ZmianaKlaseEditor.Text = "";
            zmianaKlasy = true;
        }


        operacjeNaLiscieUczniow.zmianaDanychUcznia(uczen, zmianaKlasy);

        if(zmianaKlasy == true)
        {
            await Navigation.PopAsync();
        }
    }

    async void Button_Usun(System.Object sender, System.EventArgs e)
    {
        operacjeNaLiscieUczniow.usniecieUcznia(uczen);

        await Navigation.PopAsync();
    }
}
