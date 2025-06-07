using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;
using SQuan.Helpers.Maui.Mvvm.SourceGenerators.Extensions;
using static SQuan.Helpers.Maui.Mvvm.SourceGenerators.Diagnostics.DiagnosticDescriptors;

namespace SQuan.Helpers.Maui.Mvvm.SourceGenerators;

/// <summary>
/// A diagnostic analyzer that generates errors when a property using <c>[BindableProperty]</c> on a partial property is in a project with the C# language version not set to preview.
/// </summary>
[DiagnosticAnalyzer(LanguageNames.CSharp)]
public sealed class BindablePropertyRequiresCSharpLanguageVersionPreviewAnalyzer : DiagnosticAnalyzer
{
	/// <inheritdoc/>
	public override ImmutableArray<DiagnosticDescriptor> SupportedDiagnostics { get; } = [CSharpLanguageVersionIsNotPreviewForBindableProperty];

	/// <inheritdoc/>
	public override void Initialize(AnalysisContext context)
	{
		context.ConfigureGeneratedCodeAnalysis(GeneratedCodeAnalysisFlags.None);
		context.EnableConcurrentExecution();

		context.RegisterCompilationStartAction(static context =>
		{
			// If the language version is set to preview, we'll never emit diagnostics
			if (((CSharpCompilation)(context.Compilation)).LanguageVersion == LanguageVersion.Preview)
			{
				return;
			}

			// Get the symbol for [BindableProperty]
			INamedTypeSymbol? foundPropertySymbol = null;
			if (context.Compilation.GetTypeByMetadataName("SQuan.Helpers.Maui.Mvvm.BindablePropertyAttribute") is INamedTypeSymbol _bindablePropertySymbol
				&& _bindablePropertySymbol is not null)
			{
				foundPropertySymbol = _bindablePropertySymbol;
			}
			if (context.Compilation.GetTypeByMetadataName("SQuan.Helpers.Maui.Mvvm.ObservedPropertyAttribute") is INamedTypeSymbol _observedPropertySymbol
				&& _observedPropertySymbol is not null)
			{
				foundPropertySymbol = _observedPropertySymbol;
			}
			if (foundPropertySymbol is null)
			{
				// If we don't have either of the attributes, we can skip the analysis
				return;
			}

			context.RegisterSymbolAction(context =>
			{
				// We only want to target partial property definitions (also include non-partial ones for diagnostics)
				if (context.Symbol is not IPropertySymbol { PartialDefinitionPart: null } partialProperty)
				{
					return;
				}

#if TODO
				// Make sure to skip the warning if the property is not actually partial
				if (partialProperty.DeclaringSyntaxReferences is [var syntaxReference])
				{
					// Make sure we can find the syntax node, and that it's a property declaration
					if (syntaxReference.GetSyntax(context.CancellationToken) is PropertyDeclarationSyntax propertyDeclarationSyntax)
					{
						// If the property is not partial, ignore it, as we'll already have a warning from the other analyzer here
						if (!propertyDeclarationSyntax.Modifiers.Any(SyntaxKind.PartialKeyword))
						{
							return;
						}
					}
				}
#endif

				if (context.Symbol.TryGetAttributeWithType(foundPropertySymbol, out AttributeData? bindablePropertyAttribute))
				{
					context.ReportDiagnostic(Diagnostic.Create(
						CSharpLanguageVersionIsNotPreviewForBindableProperty,
						bindablePropertyAttribute?.GetLocation(),
						context.Symbol));
				}
			}, SymbolKind.Property);
		});
	}
}

