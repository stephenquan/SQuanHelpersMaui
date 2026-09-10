// SoftKeyboardType.shared.cs

namespace SQuan.Helpers.Maui;

/// <summary>
/// Defines the type of soft keyboard to display for text input.
/// </summary>
public enum SoftKeyboardType
{
	/// <summary>
	/// Show the default soft keyboard for the platform.
	/// </summary>
	Default = 0,

	/// <summary>
	/// Show a soft keyboard optimized for entering chat messages.
	/// </summary>
	Chat,

	/// <summary>
	/// Show a soft keyboard optimized for entering dates.
	/// </summary>
	Date,

	/// <summary>
	/// Show a soft keyboard optimized for entering email addresses.
	/// </summary>
	Email,

	/// <summary>
	/// Show a soft keyboard optimized for entering numbers.
	/// </summary>
	Numeric,

	/// <summary>
	/// Show a soft keyboard optimized for entering passwords.
	/// </summary>
	Password,

	/// <summary>
	/// Show a soft keyboard optimized for entering phone numbers.
	/// </summary>
	Plain,

	/// <summary>
	/// Show a soft keyboard optimized for entering telephone numbers.
	/// </summary>
	Telephone,

	/// <summary>
	/// Show a soft keyboard optimized for entering text.
	/// </summary>
	Text,

	/// <summary>
	/// Show a soft keyboard optimized for entering time values.
	/// </summary>
	Time,

	/// <summary>
	/// Show a soft keyboard optimized for entering URLs.
	/// </summary>
	Url,

	/// <summary>
	/// Don't show any soft keyboard.
	/// </summary>
	None,
}
