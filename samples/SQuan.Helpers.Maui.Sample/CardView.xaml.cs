using CommunityToolkit.Maui.Markup;
using SQuan.Helpers.Maui.Mvvm;

namespace SQuan.Helpers.Maui.Sample;

public partial class CardView : ContentView
{
	[BindableProperty] public partial string CardTitle { get; set; } = string.Empty;
	[BindableProperty] public partial string CardDescription { get; set; } = string.Empty;
	[BindableProperty] public partial string CardTemplate { get; set; } = "CardViewDefault";
	[BindableProperty] public partial ImageSource? IconImageSource { get; set; } = null;
	[BindableProperty] public partial Color IconBackgroundColor { get; set; } = Colors.Transparent;
	[BindableProperty] public partial Color BorderColor { get; set; } = Colors.Transparent;
	[BindableProperty] public partial Color CardColor { get; set; } = Colors.Transparent;

	public CardView()
	{
		InitializeComponent();

		this.Bind(
			ContentView.ControlTemplateProperty,
			static (CardView ctx) => ctx.CardTemplate,
			source: this,
			convert: (string? templateKey)
				=> !string.IsNullOrEmpty(templateKey)
				&& Resources[templateKey] is ControlTemplate controlTemplate
				? controlTemplate
				: null);
	}
}