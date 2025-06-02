using Microsoft.CodeAnalysis;

namespace MauiHelpers.Mvvm.SourceGenerators;

/// <summary>
/// A container for all <see cref="DiagnosticDescriptor"/> instances for errors reported by analyzers in this project.
/// </summary>
static class DiagnosticDescriptors
{
	const string prefix = "MVVM";

	#region NotifyProperty
	public static readonly DiagnosticDescriptor InvalidNotifyPropertyNameError = new DiagnosticDescriptor(
		id: prefix + "0001",
		title: $"Invalid property name",
		messageFormat: $"Cannot apply [NotifyProperty] to {{0}}, invalid property name will be generated",
		category: typeof(BindablePropertyGenerator).FullName,
		defaultSeverity: DiagnosticSeverity.Error,
		isEnabledByDefault: true);
	#endregion

	#region BindableProperty
	public static readonly DiagnosticDescriptor InvalidBindablePropertyNameError = new DiagnosticDescriptor(
		id: prefix + "1001",
		title: $"Invalid property name",
		messageFormat: $"Cannot apply [BindableProperty] to {{0}}, invalid property name will be generated",
		category: typeof(BindablePropertyGenerator).FullName,
		defaultSeverity: DiagnosticSeverity.Error,
		isEnabledByDefault: true);

	public static readonly DiagnosticDescriptor CreateDefaultValueReturnsVoidError = new DiagnosticDescriptor(
		id: prefix + "1002",
		title: $"Invalid default create default value method definition",
		messageFormat: $"Cannot apply [BindableProperty] to {{0}}, create default value method must return a type",
		category: typeof(BindablePropertyGenerator).FullName,
		defaultSeverity: DiagnosticSeverity.Error,
		isEnabledByDefault: true);

	public static readonly DiagnosticDescriptor CreateDefaultValueHasParametersError = new DiagnosticDescriptor(
		id: prefix + "1003",
		title: $"Invalid default create default value method definition",
		messageFormat: $"Cannot apply [BindableProperty] to {{0}}, create default value accepts parameters",
		category: typeof(BindablePropertyGenerator).FullName,
		defaultSeverity: DiagnosticSeverity.Error,
		isEnabledByDefault: true);

	public static readonly DiagnosticDescriptor BindingPropertyChangedMethodError = new DiagnosticDescriptor(
		id: prefix + "1004",
		title: $"Invalid default binding property changed method definition",
		messageFormat: $"Cannot apply [BindableProperty] to {{0}}, signature must be  or void On<propertyName>Changing(<propertyType> oldValue, <propertyType> newValue)",
		category: typeof(BindablePropertyGenerator).FullName,
		defaultSeverity: DiagnosticSeverity.Error,
		isEnabledByDefault: true);
	#endregion
}
