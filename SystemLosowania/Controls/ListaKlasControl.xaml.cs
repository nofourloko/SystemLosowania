namespace SystemLosowania.Controls;

public partial class ListaKlasControl : ContentView
{
	public static readonly BindableProperty NazwaKlasyProperty = BindableProperty.Create(nameof(NazwaKlasy), typeof(string), typeof(ListaKlasControl), String.Empty);

	public string NazwaKlasy
	{
		get => (string)GetValue(ListaKlasControl.NazwaKlasyProperty);
		set => SetValue(ListaKlasControl.NazwaKlasyProperty, value);
	}

	public ListaKlasControl()
	{
		InitializeComponent();
	}
}
