// SoftKeyboard.shared.cs

using CommunityToolkit.Maui;

namespace SQuan.Helpers.Maui;

/// <summary>
/// Represents a soft keyboard that can be displayed on the screen for text input.
/// </summary>
public partial class SoftKeyboard : ContentView
{
	static Dictionary<string, SoftKeyboard> keyboards = new Dictionary<string, SoftKeyboard>();

	/// <summary>
	/// Gets or sets the name of the soft keyboard. This name is used to register and retrieve the soft keyboard instance.
	/// </summary>
	[BindableProperty(PropertyChangedMethodName = nameof(OnSoftKeyboardNameChanged))]
	public partial string SoftKeyboardName { get; set; } = string.Empty;

	/// <summary>
	/// Gets the visual element that is currently focused and receiving input from the soft keyboard.
	/// </summary>
	public VisualElement? FocusedElement { get; private set; }

	static void OnSoftKeyboardNameChanged(BindableObject bindable, object oldValue, object newValue)
	{
		if (bindable is not SoftKeyboard softKeyboard)
		{
			return;
		}

		if (softKeyboard.Handler is null)
		{
			return;
		}

		if (bindable is SoftKeyboard keyboard && newValue is string newName)
		{
			if (!string.IsNullOrEmpty(newName))
			{
				RegisterSoftKeyboard(newName, keyboard);
			}
			else if (!string.IsNullOrEmpty(keyboard.SoftKeyboardName))
			{
				keyboards.Remove(keyboard.SoftKeyboardName);
			}
		}
	}

	IDispatcherTimer hideTimer;
	VisualElement? hideElement;

	/// <summary>
	/// Initializes a new instance of the <see cref="SoftKeyboard"/>.
	/// </summary>
	public SoftKeyboard()
	{
		hideTimer = Dispatcher.CreateTimer();
		hideTimer.Interval = TimeSpan.FromMilliseconds(250);
		hideTimer.IsRepeating = false;

		Loaded += SoftKeyboard_Loaded;
		Unloaded += SoftKeyboard_Unloaded;

		ControlTemplate = new ControlTemplate(() =>
		{
			var grid = new Grid
			{
				RowDefinitions = new RowDefinitionCollection
				{
					new RowDefinition { Height = GridLength.Auto },
					new RowDefinition { Height = GridLength.Auto },
					new RowDefinition { Height = GridLength.Auto },
					new RowDefinition { Height = GridLength.Auto },
				},
				ColumnDefinitions = new ColumnDefinitionCollection
				{
					new ColumnDefinition { Width = GridLength.Auto },
					new ColumnDefinition { Width = GridLength.Auto },
					new ColumnDefinition { Width = GridLength.Auto },
					new ColumnDefinition { Width = GridLength.Auto },
				},
				Children =
				{
					SoftKey.Create("1"),
					SoftKey.Create("2", 0, 1),
					SoftKey.Create("3", 0, 2),
					SoftKey.Create("{BS}", 0, 3),
					SoftKey.Create("4", 1, 0),
					SoftKey.Create("5", 1, 1),
					SoftKey.Create("6", 1, 2),
					SoftKey.Create("-", 1, 3),
					SoftKey.Create("7", 2, 0),
					SoftKey.Create("8", 2, 1),
					SoftKey.Create("9", 2, 2),
					SoftKey.Create(".", 2, 3),
					SoftKey.Create("0", 3, 0, 1, 2),
					SoftKey.Create(",", 3, 2),
					SoftKey.Create("{ENTER}", 3, 3),
				}
			}
			.BindTemplatedParent(BindingContextProperty);
			return grid;
		});
	}

	void SoftKeyboard_Loaded(object? sender, EventArgs e)
	{
		RegisterSoftKeyboard(SoftKeyboardName, this);
		hideTimer.Tick -= HideTimer_Tick;
		hideTimer.Tick += HideTimer_Tick;
	}

	void SoftKeyboard_Unloaded(object? sender, EventArgs e)
	{
		UnregisterSoftKeyboard(SoftKeyboardName);
		if (hideTimer.IsRunning)
		{
			hideTimer.Stop();
		}
		hideElement = null;
		hideTimer.Tick -= HideTimer_Tick;
	}

	static void RegisterSoftKeyboard(string softKeyboardName, SoftKeyboard softKeyboard)
	{
		if (keyboards.ContainsKey(softKeyboardName))
		{
			keyboards[softKeyboardName] = softKeyboard;
		}
		else
		{
			keyboards.Add(softKeyboardName, softKeyboard);
		}
	}

	static void UnregisterSoftKeyboard(string softKeyboardName)
	{
		if (keyboards.ContainsKey(softKeyboardName))
		{
			keyboards.Remove(softKeyboardName);
		}
	}

	void HideTimer_Tick(object? sender, EventArgs e)
	{
		if (Handler is not null
			&& FocusedElement is not null
			&& FocusedElement == hideElement)
		{
			IsVisible = false;
			FocusedElement = null;
			hideElement = null;
		}
	}

	/// <summary>
	/// Tries to get the soft keyboard instance associated with the specified name.
	/// </summary>
	/// <param name="softKeyboardName">The name of the soft keyboard.</param>
	/// <param name="softKeyboard">When this method returns, contains the soft keyboard instance associated with the specified name, if found; otherwise, null.</param>
	/// <returns>true if the soft keyboard instance was found; otherwise, false.</returns>
	public static bool TryGetSoftKeyboard(string softKeyboardName, out SoftKeyboard? softKeyboard)
	{
		return keyboards.TryGetValue(softKeyboardName, out softKeyboard);
	}

	/// <summary>
	/// Notifies the soft keyboard that the specified visual element has received focus, allowing it to receive input from the soft keyboard.
	/// </summary>
	/// <param name="visualElement">The visual element that has received focus.</param>
	public void NotifyFocused(VisualElement visualElement)
	{
		if (hideTimer.IsRunning)
		{
			hideTimer.Stop();
			hideElement = null;
		}

		if (visualElement is SoftKey)
		{
			return;
		}

		IsVisible = true;
		FocusedElement = visualElement;
	}

	/// <summary>
	/// Notifies the soft keyboard that the specified visual element has lost focus, allowing it to stop receiving input from the soft keyboard.
	/// </summary>
	/// <param name="visualElement">The visual element that has lost focus.</param>
	public void NotifyUnfocused(VisualElement visualElement)
	{
		if (IsVisible
			&& FocusedElement == visualElement)
		{
			hideElement = visualElement;
			hideTimer.Stop();
			hideTimer.Start();
		}
	}
}
