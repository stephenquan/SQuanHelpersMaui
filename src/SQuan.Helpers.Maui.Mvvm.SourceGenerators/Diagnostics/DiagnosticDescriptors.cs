using Microsoft.CodeAnalysis;

namespace SQuan.Helpers.Maui.Mvvm.SourceGenerators.Diagnostics;

/// <summary>
/// A container for all <see cref="DiagnosticDescriptor"/> instances for errors reported by analyzers in this project.
/// </summary>
public static class DiagnosticDescriptors
{
	/// <inheritdoc/>
	public static readonly DiagnosticDescriptor CSharpLanguageVersionIsNotPreviewForObservableProperty = new DiagnosticDescriptor(
		id: "SQMVVMTK0001",
		title: "C# language version is not 'preview'",
		messageFormat: """Using [ObservableProperty] on partial properties requires the C# language version to be set to 'preview', as support for the 'field' keyword is needed by the source generators to emit valid code (add <LangVersion>preview</LangVersion> to your .csproj/.props file)""",
		category: typeof(ObservablePropertyGenerator).FullName,
		defaultSeverity: DiagnosticSeverity.Error,
		isEnabledByDefault: true,
		description: "The C# language version must be set to 'preview' when using [ObservableProperty] on partial properties for the source generators to emit valid code (the <LangVersion>preview</LangVersion> option must be set in the .csproj/.props file).",
		helpLinkUri: "https://aka.ms/mvvmtoolkit/errors/mvvmtk0041");

	/// <inheritdoc/>
	public static readonly DiagnosticDescriptor CSharpLanguageVersionIsNotPreviewForBindableProperty = new DiagnosticDescriptor(
		id: "SQMVVMTK0002",
		title: "C# language version is not 'preview'",
		messageFormat: """Using [BindableProperty] on partial properties requires the C# language version to be set to 'preview', as support for the 'field' keyword is needed by the source generators to emit valid code (add <LangVersion>preview</LangVersion> to your .csproj/.props file)""",
		category: typeof(BindablePropertyGenerator).FullName,
		defaultSeverity: DiagnosticSeverity.Error,
		isEnabledByDefault: true,
		description: "The C# language version must be set to 'preview' when using [BindableProperty] on partial properties for the source generators to emit valid code (the <LangVersion>preview</LangVersion> option must be set in the .csproj/.props file).",
		helpLinkUri: "https://aka.ms/mvvmtoolkit/errors/mvvmtk0041");

}

