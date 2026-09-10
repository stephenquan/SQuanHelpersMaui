// SoftKey.shared.cs

using System.Diagnostics;

namespace SQuan.Helpers.Maui;

/// <summary>
/// Represents a soft key button that can be used in a soft keyboard interface.
/// </summary>
public partial class SoftKey : Button
{
	/// <summary>
	/// Initializes a new instance of the <see cref="SoftKey"/> class.
	/// </summary>
	public SoftKey()
	{
		Loaded += SoftKey_Loaded;
		Unloaded += SoftKey_Unloaded;
		MinimumHeightRequest = 40;
		MinimumWidthRequest = 100;
		Margin = new Thickness(5);
		BackgroundColor = Colors.Orange;
		BorderWidth = 2;
		BorderColor = Colors.Black;
		this.SetBinding(
			CornerRadiusProperty,
			BindingBase.Create(
				static (VisualElement ctx) => ctx.Height,
				BindingMode.OneWay,
				converter: new FuncToConverter<double, double>(h => h / 2),
				source: this));
		this.SetBinding(
			CommandParameterProperty,
			BindingBase.Create(
				static (SoftKey ctx) => ctx.Text,
				BindingMode.OneWay,
				source: this));
	}

	void SoftKey_Loaded(object? sender, EventArgs e)
	{
		Focused += SoftKey_Focused;
		Clicked += SoftKey_Clicked;
	}

	void SoftKey_Unloaded(object? sender, EventArgs e)
	{
		Focused -= SoftKey_Focused;
		Clicked -= SoftKey_Clicked;
	}

	void SoftKey_Focused(object? sender, FocusEventArgs e)
	{
		Trace.WriteLine($"SoftKey focused: {Text} {BindingContext}");
		if (BindingContext is SoftKeyboard softKeyboard)
		{
			softKeyboard.NotifyFocused(this);
		}
	}

	void SoftKey_Clicked(object? sender, EventArgs e)
	{
		Trace.WriteLine($"SoftKey clicked: {Text} {BindingContext}");
		if (BindingContext is SoftKeyboard softKeyboard
			&& softKeyboard.FocusedElement is InputView inputView
			&& CommandParameter is string keys
			&& !string.IsNullOrEmpty(keys))
		{
			inputView.Dispatcher.Dispatch(() =>
			{
				inputView.Focus();
				inputView.SendKeys(keys);
			});
		}
	}

	/// <summary>
	/// Sets the row and column position of the soft key in a grid layout.
	/// </summary>
	/// <param name="row">The row position in the grid.</param>
	/// <param name="column">The column position in the grid.</param>
	/// <returns>The current <see cref="SoftKey"/> instance.</returns>
	public SoftKey RC(int row, int column)
	{
		Grid.SetRow(this, row);
		Grid.SetColumn(this, column);
		return this;
	}

	/// <summary>
	/// Sets the row, column, row span, and column span of the soft key in a grid layout.
	/// </summary>
	/// <param name="row">The row position in the grid.</param>
	/// <param name="column">The column position in the grid.</param>
	/// <param name="rowSpan">The number of rows the soft key spans.</param>
	/// <param name="columnSpan">The number of columns the soft key spans.</param>
	/// <returns>The current <see cref="SoftKey"/> instance.</returns>
	public SoftKey RC(int row, int column, int rowSpan, int columnSpan)
	{
		Grid.SetRow(this, row);
		Grid.SetColumn(this, column);
		Grid.SetRowSpan(this, rowSpan);
		Grid.SetColumnSpan(this, columnSpan);
		return this;
	}

	/// <summary>
	/// Creates a new instance of the <see cref="SoftKey"/> class with the specified key.
	/// </summary>
	/// <param name="key">The key to be displayed on the soft key.</param>
	/// <param name="row">The row position in the grid.</param>
	/// <param name="column">The column position in the grid.</param>
	/// <param name="rowSpan">The number of rows the soft key spans.</param>
	/// <param name="columnSpan">The number of columns the soft key spans.</param>
	/// <returns>A new instance of the <see cref="SoftKey"/> class with the specified key.</returns>
	public static SoftKey Create(string key, int row = 0, int column = 0, int rowSpan = 1, int columnSpan = 1)
	{
		var softKey = new SoftKey { Text = key, CommandParameter = key };
		Grid.SetRow(softKey, row);
		Grid.SetColumn(softKey, column);
		Grid.SetRowSpan(softKey, rowSpan);
		Grid.SetColumnSpan(softKey, columnSpan);
		return softKey;
	}
}
