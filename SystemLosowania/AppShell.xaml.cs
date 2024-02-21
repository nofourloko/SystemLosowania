using SystemLosowania.Views;
namespace SystemLosowania;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(DaneUcznia), typeof(DaneUcznia));
		Routing.RegisterRoute(nameof(DodanieUcznia), typeof(DodanieUcznia));
	}
}

