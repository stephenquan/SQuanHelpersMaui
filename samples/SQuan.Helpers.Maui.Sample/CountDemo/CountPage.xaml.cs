using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Mvvm.Input;
using ObservablePropertyAttribute = SQuan.Helpers.Maui.Mvvm.ObservablePropertyAttribute;

namespace SQuan.Helpers.Maui.Sample;

public partial class CountPage : ContentPage
{
	[ObservableProperty] public partial int Count { get; set; } = 0;

	public CountPage()
	{
		BindingContext = this;
		InitializeComponent();
		CounterBtn.Bind(
			Button.TextProperty,
			static (CountPage ctx) => ctx.Count,
			stringFormat: "Clicked {0} times");
	}

	[RelayCommand]
	void IncrementCounter()
	{
		Count++;
		SemanticScreenReader.Announce(CounterBtn.Text);
	}
}
