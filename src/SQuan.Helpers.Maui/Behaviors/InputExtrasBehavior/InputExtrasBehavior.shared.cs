// InputExtrasBehavior.shared.cs

using System.Text.RegularExpressions;
using CommunityToolkit.Maui;

namespace SQuan.Helpers.Maui;

/// <summary>
/// Represents a behavior that provides additional features for input view controls, such as border thickness and input masking.
/// </summary>
partial class InputExtrasBehavior : Behavior<VisualElement>
{
	/// <summary>
	/// Gets or sets the border thickness of the input view control.
	/// </summary>
	[BindableProperty(PropertyChangedMethodName = nameof(OnBorderThicknessChanged))]
	public partial double BorderThickness { get; set; } = 1.0;

	/// <summary>
	/// Gets or sets the input mask mode for the input view control.
	/// </summary>
	[BindableProperty(PropertyChangedMethodName = nameof(OnInputMaskChanged))]
	public partial InputMode InputMode { get; set; } = InputMode.None;

	/// <summary>
	/// Gets or sets the input pattern for the input view control.
	/// </summary>
	[BindableProperty]
	public partial string InputPattern { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the name of the soft keyboard to be used for the input view control.
	/// </summary>
	[BindableProperty]
	public partial string SoftKeyboardName { get; set; } = string.Empty;

	/// <summary>
	/// Gets or sets the type of soft keyboard to be used for the input view control.
	/// </summary>
	[BindableProperty]
	public partial SoftKeyboardType SoftKeyboardType { get; set; } = SoftKeyboardType.Default;

	[GeneratedRegex("^[-]?\\d*$")]
	internal static partial Regex IntegerRegex();

	[GeneratedRegex("^[-]?\\d*([.,]\\d*)?$")]
	internal static partial Regex DecimalRegex();

	static void OnBorderThicknessChanged(BindableObject bindable, object oldValue, object newValue)
		=> ((InputExtrasBehavior)bindable).UpdateBorderThickness();

	static void OnInputMaskChanged(BindableObject bindable, object oldValue, object newValue)
		=> ((InputExtrasBehavior)bindable).UpdateInputMask();

	void OnFocused(object? sender, FocusEventArgs e)
	{
		if (sender is VisualElement visualElement
			&& SoftKeyboard.TryGetSoftKeyboard(SoftKeyboardName, out var softKeyboard)
			&& softKeyboard is not null)
		{
			softKeyboard.NotifyFocused(visualElement);
		}
	}

	void OnUnfocused(object? sender, FocusEventArgs e)
	{
		if (sender is VisualElement visualElement
			&& SoftKeyboard.TryGetSoftKeyboard(SoftKeyboardName, out var softKeyboard)
			&& softKeyboard is not null)
		{
			softKeyboard.NotifyUnfocused(visualElement);
		}
	}

	partial void UpdateBorderThickness();

	partial void UpdateInputMask();
}
