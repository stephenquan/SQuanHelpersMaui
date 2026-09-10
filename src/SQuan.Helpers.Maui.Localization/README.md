# SQuan.Helpers.Maui.Localization

Simple, runtime-aware localization for .NET MAUI. Use your existing resource system with:

- a XAML `Localize` markup extension;
- a fluent C# `Localize` extension method; and
- a single `LocalizationManager` that refreshes localized bindings when the culture changes.

## Install

Install the NuGet package:

```bash
dotnet add package SQuan.Helpers.Maui.Localization
```

## Configure

Register your string provider once in `MauiProgram.cs`. For a generated resource class named `AppStrings`, the `ResourceManager.GetString` method is enough:

```c#
using SQuan.Helpers.Maui.Localization;

public static MauiApp CreateMauiApp()
{
    MauiAppBuilder builder = MauiApp.CreateBuilder();

    builder
        .UseMauiApp<App>();

    LocalizationManager.Current.LocalizationProvider = AppStrings.ResourceManager.GetString;

    return builder.Build();
}
```

The provider receives a resource key and the current UI culture, so it can be replaced with any lookup strategy.

## Localize in XAML

Add the namespace to a page or view, then use resource keys directly with the `Localize` bindings:

```xml
<ContentPage
    xmlns="http://schemas.microsoft.com/dotnet/2021/maui"
    xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
    xmlns:i18n="clr-namespace:SQuan.Helpers.Maui.Localization;assembly=SQuan.Helpers.Maui.Localization"
    Title="{i18n:Localize TITLE_HOME}">
    <ScrollView>
        <VerticalStackLayout>
           <Label Text="{i18n:Localize LABEL_HELLO_WORLD}" />
           <Label Text="{i18n:Localize LABEL_WELCOME}" />
           <Button x:Name="CounterBtn" Text="{i18n:Localize BUTTON_CLICK_ME}" />
        </VerticalStackLayout>
    </ScrollView>
</ContentPage>
```

## Localize in C#

You can do culture-aware bindings in C# using the same resource key:

```c#
using SQuan.Helpers.Maui.Localization;

CounterBtn.Localize(Button.TextProperty, "BUTTON_CLICK_ME");
```

Formatting arguments are also supported:

```c#
CounterBtn.Localize(Label.TextProperty, "BUTTON_CLICKED_N_TIMES", count);
```

For type safety, a function provider is also supported:

```c#
CounterBtn.Localize(Label.TextProperty, _ => AppStrings.BUTTON_CLICKED_N_TIMES, count);
```


## Change culture at runtime

Set `CurrentUICulture` for translated strings and `CurrentCulture` for formatting such as dates, numbers, and currency. Existing localized bindings update automatically.

```c#
using System.Globalization;
using SQuan.Helpers.Maui.Localization;

CultureInfo culture = new("de-DE");
LocalizationManager.Current.CurrentUICulture = culture;
LocalizationManager.Current.CurrentCulture = culture;
```

## Further information

For more information please visit:

 - Documentation: https://stephenquan.github.io/SQuan.Helpers/Maui.Localization/
 - GitHub repository: https://github.com/stephenquan/SQuan.Helpers
