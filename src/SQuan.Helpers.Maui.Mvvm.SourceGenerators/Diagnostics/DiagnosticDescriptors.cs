using Microsoft.CodeAnalysis;

namespace SQuan.Helpers.Maui.Mvvm.SourceGenerators.Diagnostics;

/// <summary>
/// A container for all <see cref="DiagnosticDescriptor"/> instances for errors reported by analyzers in this project.
/// </summary>
public static class DiagnosticDescriptors
{
	/// <inheritdoc/>
	public static readonly DiagnosticDescriptor CSharpLanguageVersionIsNotPreviewForBindableProperty = new DiagnosticDescriptor(
		id: "SQMVVMTK0041",
		title: "C# language version is not 'preview'",
		messageFormat: """Using [BindableProperty][ObservableProperty] on partial properties requires the C# language version to be set to 'preview', as support for the 'field' keyword is needed by the source generators to emit valid code (add <LangVersion>preview</LangVersion> to your .csproj/.props file)""",
		category: typeof(ObservablePropertyGenerator).FullName,
		defaultSeverity: DiagnosticSeverity.Error,
		isEnabledByDefault: true,
		description: "The C# language version must be set to 'preview' when using [ObservableProperty] on partial properties for the source generators to emit valid code (the <LangVersion>preview</LangVersion> option must be set in the .csproj/.props file).",
		helpLinkUri: "https://aka.ms/mvvmtoolkit/errors/mvvmtk0041");
}
