namespace MauiHelpers.Mvvm;

/// <summary>
/// Indicates that a property is bindable, allowing it to be used in data binding scenarios.
/// </summary>
[AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
public class BindablePropertyAttribute : Attribute
{
}
