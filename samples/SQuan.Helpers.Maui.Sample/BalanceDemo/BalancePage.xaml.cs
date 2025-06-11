using System.Globalization;
using SQuan.Helpers.Maui.Mvvm;
using SQuan.Helpers.Maui.Sample.Resources.Strings;
using RelayCommandAttribute = CommunityToolkit.Mvvm.Input.RelayCommandAttribute;

namespace SQuan.Helpers.Maui.Sample;

public partial class BalancePage : ContentPage
{
	public List<CultureInfo> SupportedCultures { get; } =
	[
		new CultureInfo("en-US"),
		new CultureInfo("en-GB"),
		new CultureInfo("fr-FR"),
		new CultureInfo("zh-CN"),
	];

	[ObservableProperty, NotifyPropertyChangedFor(nameof(CurrentCulture), "Item")]
	public partial int CultureIndex { get; set; } = 0;

	public CultureInfo CurrentCulture => SupportedCultures[CultureIndex];

	public string this[string key]
		=> AppStrings.ResourceManager.GetString(key, CurrentCulture) is string str ? str : key;

	public BalancePage()
	{
		BindingContext = this;
		InitializeComponent();
	}

	[RelayCommand]
	void ChangeCulture()
	{
		CultureIndex = (CultureIndex + 1) % SupportedCultures.Count;
	}
}
