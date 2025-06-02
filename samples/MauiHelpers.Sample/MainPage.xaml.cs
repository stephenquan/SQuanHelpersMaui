using MauiHelpers.Mvvm;

namespace MauiHelpers.Sample;

public partial class MainPage : ContentPage
{
	[BindableProperty]
	public partial int Count { get; set; } = 0;

	public MainPage()
	{
		BindingContext = this;
		InitializeComponent();
		CounterBtn.SetBinding(Button.TextProperty, static (MainPage ctx) => ctx.Count, BindingMode.OneWay, stringFormat: "Clicked {0} times");
	}

	void OnCounterClicked(object? sender, EventArgs e)
	{
		Count++;
		SemanticScreenReader.Announce(CounterBtn.Text);
	}
}
