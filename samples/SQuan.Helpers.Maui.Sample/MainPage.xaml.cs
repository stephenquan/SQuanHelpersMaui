using CommunityToolkit.Mvvm.Input;

namespace SQuan.Helpers.Maui.Sample;

public partial class MainPage : ContentPage
{
	public MainPage()
	{
		BindingContext = this;
		InitializeComponent();
	}

	[RelayCommand]
	async Task Navigate(string route)
	{
		await Shell.Current.GoToAsync(route);
	}
}
