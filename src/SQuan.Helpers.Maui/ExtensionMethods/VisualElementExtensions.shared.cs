// VisualElementExtensions.shared.cs

using System.Text.RegularExpressions;

namespace SQuan.Helpers.Maui;

/// <summary>
/// Provides extension methods for VisualElement.
/// </summary>
public static partial class VisualElementExtensions
{
	/// <summary>
	/// Gets an existing behavior of type T from the VisualElement or creates a new one if it doesn't exist.
	/// </summary>
	/// <typeparam name="T">The type of the behavior.</typeparam>
	/// <param name="element">The VisualElement to get or create the behavior for.</param>
	/// <returns>The existing or newly created behavior of type T.</returns>
	public static T GetOrCreateBehavior<T>(this VisualElement element) where T : Behavior, new()
	{
		if (element.Behaviors.OfType<T>().FirstOrDefault() is T behavior)
		{
			return behavior;
		}

		behavior = new T();
		element.Behaviors.Add(behavior);
		return behavior;
	}

	/// <summary>
	/// Binds the specified target property of the bindable object to its templated parent.
	/// </summary>
	/// <typeparam name="T">The type of the bindable object.</typeparam>
	/// <param name="bindable">The bindable object to bind the property for.</param>
	/// <param name="targetProperty">The target property to bind to the templated parent.</param>
	/// <returns>The bindable object with the property bound to its templated parent.</returns>
	public static T BindTemplatedParent<T>(this T bindable, BindableProperty targetProperty) where T : BindableObject
	{
		bindable.SetBinding(targetProperty, new Binding(".", BindingMode.OneWay, source: RelativeBindingSource.TemplatedParent));
		return bindable;
	}

	[GeneratedRegex(@"^\{\{")]
	private static partial Regex OpenBraceRegex();

	[GeneratedRegex(@"^\}\}")]
	private static partial Regex CloseBraceRegex();

	[GeneratedRegex(@"^\{(?<key>[A-Z][A-Z0-9_]*)(?:\s+(?<count>\d+))?\}")]
	private static partial Regex SpecialKeyRegex();

	[GeneratedRegex(@"^[^{]+")]
	private static partial Regex NormalKeyRegex();

	/// <summary>
	/// Sends the specified key to the InputView by appending it to its Text property.
	/// </summary>
	/// <param name="bindable">The InputView to send the key to.</param>
	/// <param name="keys">The key to send.</param>
	/// <returns>The InputView with the key appended to its Text property.</returns>
	public static T SendKeys<T>(this T bindable, string keys) where T : InputView
	{
		// Match {{literal}}, {specialkey}, normalkey
		if (string.IsNullOrEmpty(keys))
		{
			return bindable;
		}

		int pos = 0;
		while (pos < keys.Length)
		{
			if (OpenBraceRegex().Match(keys, pos) is { Success: true, Length: var openLength })
			{
				SendNormalKey(bindable, "{");
				pos += openLength;
				continue;
			}
			if (CloseBraceRegex().Match(keys, pos) is { Success: true, Length: var closeLength })
			{
				SendNormalKey(bindable, "{");
				pos += closeLength;
				continue;
			}
			if (SpecialKeyRegex().Match(keys, pos) is { Success: true, Value: var specialKey, Length: var specialLength })
			{
				_ = specialKey switch
				{
					"{BS}" => SendBackspaceKey(bindable),
					_ => null
				};
				pos += specialLength;
				continue;
			}
			if (NormalKeyRegex().Match(keys, pos) is { Success: true, Value: var normalText, Length: var normalLength })
			{
				SendNormalKey(bindable, normalText ?? string.Empty);
				pos += normalLength;
				continue;
			}
			break;
		}
		return bindable;
	}

	static T SendBackspaceKey<T>(this T bindable) where T : InputView
	{
		var cursorPosition = bindable.CursorPosition;
		var selectionLength = bindable.SelectionLength;
		var text = bindable.Text ?? string.Empty;
		if (cursorPosition < 0 || cursorPosition > text.Length)
		{
			cursorPosition = text.Length;
		}
		if (selectionLength > 0)
		{
			bindable.Text = text.Remove(cursorPosition, selectionLength);
			bindable.SelectionLength = 0;
		}
		else if (cursorPosition > 0)
		{
			bindable.Text = text.Remove(cursorPosition - 1, 1);
			bindable.SelectionLength = 0;
			bindable.CursorPosition = cursorPosition - 1;
		}
		return bindable;
	}

	static T SendNormalKey<T>(this T bindable, string key) where T : InputView
	{
		var cursorPosition = bindable.CursorPosition;
		var selectionLength = bindable.SelectionLength;
		var text = bindable.Text ?? string.Empty;
		if (cursorPosition < 0 || cursorPosition > text.Length)
		{
			cursorPosition = text.Length;
		}
		if (selectionLength > 0)
		{
			text = text.Remove(cursorPosition, selectionLength);
			selectionLength = 0;
		}
		bindable.Text = text.Insert(cursorPosition, key);
		bindable.SelectionLength = 0;
		bindable.CursorPosition = cursorPosition + key.Length;
		return bindable;
	}

}
