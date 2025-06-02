# SQuan.Helpers.Maui.Mvvm
MVVM Helpers for .NET MAUI

## Installation

The C# language version must be set to 'preview' when using [ObservableProperty] or [BindableProperty] on partial properties for the source generators to emit valid code.

This is required because the generated code for [ObservableProperty] on partial properties uses some preview features. Make sure to add <LangVersion>preview</LangVersion> (or above) to your .csproj file.

## Example

You can use [BindableProperty] to reduce the code needed to extend ContentView to create a custom control. The following example shows the `CardTitle` bindable property in the code-behind file to the `CardView` class:

```c#
public partial class CardView : ContentView
{
    [BindableProperty] public partial string CardTitle { get; set; } = string.Empty;
    // ...

    public CardView()
    {
        InitializeComponent();
    }
}
```

## Further information

For more information please visit:

 - Documentation: https://github.com/stephenquan/SQuan.Helpers.Maui/wiki/MVVM
 - GitHub repository: https://github.com/stephenquan/SQuan.Helpers.Maui
