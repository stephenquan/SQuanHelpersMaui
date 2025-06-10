using System.Globalization;
using CommunityToolkit.Maui.Markup;
using SQuan.Helpers.Maui.Mvvm;
using SQuan.Helpers.Maui.Sample.Resources.Strings;
using RelayCommandAttribute = CommunityToolkit.Mvvm.Input.RelayCommandAttribute;

namespace SQuan.Helpers.Maui.Sample;

public partial class LocalizePage : ContentPage
{
	[ObservableProperty] public partial int Count { get; set; } = 0;

	public List<CultureInfo> SupportedCultures { get; } =
	[
		new CultureInfo("en-US"),
		new CultureInfo("fr-FR"),
		new CultureInfo("zh-CN"),
	];

	[ObservableProperty, NotifyPropertyChangedFor(nameof(CurrentCulture), "Item")]
	public partial int CultureIndex { get; set; } = 0;

	public CultureInfo CurrentCulture => SupportedCultures[CultureIndex];

	public LocalizePage()
	{
		BindingContext = this;
		InitializeComponent();
		CounterBtn.Bind(
			Button.TextProperty,
			binding1: BindingBase.Create(static (LocalizePage ctx) => ctx.CurrentCulture, BindingMode.OneWay),
			binding2: BindingBase.Create(static (LocalizePage ctx) => ctx.Count, BindingMode.OneWay),
			convert: ((CultureInfo? culture, int count) v) => AppStrings.ResourceManager.GetString(nameof(AppStrings.BTN_CLICKED_N_TIMES), v.culture) is string str ? string.Format(str, v.count) : "Counter");
	}

	public string? this[string key]
		=> AppStrings.ResourceManager.GetString(key, CurrentCulture) is string str ? str : key;

	[RelayCommand]
	void IncrementCounter()
	{
		Count++;
		SemanticScreenReader.Announce(CounterBtn.Text);
	}

	[RelayCommand]
	void ChangeCulture()
	{
		CultureIndex = (CultureIndex + 1) % SupportedCultures.Count;
		OnPropertyChanged(nameof(CurrentCulture));
	}
}
