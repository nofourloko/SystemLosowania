namespace SystemLosowania.Controls;

public partial class UczenKontrol : ContentView
{
	public static readonly BindableProperty ImieProperty = BindableProperty.Create(nameof(Imie), typeof(string), typeof(UczenKontrol), String.Empty);
	public static readonly BindableProperty NazwiskoProperty = BindableProperty.Create(nameof(Nazwisko), typeof(string), typeof(UczenKontrol), String.Empty);
	public static readonly BindableProperty NumerekProperty = BindableProperty.Create(nameof(Numerek), typeof(int), typeof(UczenKontrol),0);

    public string Imie
	{
		get => "Imie: " + (string)GetValue(UczenKontrol.ImieProperty);
		set => SetValue(UczenKontrol.ImieProperty, value);
	}

	public string Nazwisko
	{
		get => "Nazwisko: " + (string)GetValue(UczenKontrol.NazwiskoProperty);
		set => SetValue(UczenKontrol.NazwiskoProperty, value);
	}

	public string Numerek
	{
		get => "Numerek: " + (int)GetValue(UczenKontrol.NumerekProperty);
		set => SetValue(UczenKontrol.NumerekProperty, value);
	}


	public UczenKontrol()
	{
		InitializeComponent();
	}
}
