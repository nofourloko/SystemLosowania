using Microsoft.Extensions.Logging;

namespace SystemLosowania;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("SourceCodePro-VariableFont_wght.ttf", "SourceCodeProVariableFontwght");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}

