using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Mvvm.Input;
using ObservablePropertyAttribute = SQuan.Helpers.Maui.Mvvm.ObservablePropertyAttribute;

namespace SQuan.Helpers.Maui.Sample;

public partial class MainPage : ContentPage
{
	[ObservableProperty] public partial int Count { get; set; } = 0;

	public ObservableCollection<CardInfo> Cards { get; } =
	[
		new CardInfo
		{
			CardTitle = "Slavko Vlasic",
			CardDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Nulla elit dolor, convallis non interdum.",
		},
		new CardInfo
		{
			CardTitle = "Carolina Pena",
			CardDescription = "Phasellus eu convallis mi. In tempus augue eu dignissim fermentum. Morbi ut lacus vitae eros lacinia.",
		},
		new CardInfo
		{
			CardTitle = "Wade Banks",
			CardDescription = "Aliquam sagittis, odio lacinia fermentum cictum, mi erat sccelerisque erat; quis alliquet arou.",
		},
		new CardInfo
		{
			CardTitle = "Colette Quint",
			CardDescription = "In pellenteqsuq odio ecet augue elemntum lobortis. Sed augue masso, rhoocus eu nisi vitae, egestas.",
		}
	];

	[ObservableProperty] public partial CardInfo? SelectedCard { get; set; } = null;

	partial void OnSelectedCardChanged(CardInfo? oldValue, CardInfo? newValue)
	{
		if (oldValue is not null)
		{
			oldValue.CardTemplate = "CardViewCompressed";
		}
		if (newValue is not null)
		{
			newValue.CardTemplate = "CardViewDefault";
		}
	}

	public MainPage()
	{
		BindingContext = this;
		InitializeComponent();
		CounterBtn.Bind(
			Button.TextProperty,
			static (MainPage ctx) => ctx.Count,
			stringFormat: "Clicked {0} times");
	}

	[RelayCommand]
	void IncrementCounter()
	{
		Count++;
		SemanticScreenReader.Announce(CounterBtn.Text);
	}
}
