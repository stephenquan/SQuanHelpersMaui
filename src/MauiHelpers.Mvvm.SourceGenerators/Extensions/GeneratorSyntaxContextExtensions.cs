using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace MauiHelpers.Mvvm.SourceGenerators;

static class GeneratorSyntaxContextExtensions
{
	/// <summary>
	/// Get field symbols with a specified attribute name
	/// </summary>
	public static IList<IFieldSymbol> GetFieldSymbolsWithAttribute(this GeneratorSyntaxContext context, string attributeName)
	{
		var symbols = new List<IFieldSymbol>();

		if (context.Node is FieldDeclarationSyntax fieldDeclarationSyntax
			&& fieldDeclarationSyntax.AttributeLists.Count > 0)
		{
			foreach (var variable in fieldDeclarationSyntax.Declaration.Variables)
			{
				if (context.SemanticModel.GetDeclaredSymbol(variable) is IFieldSymbol fieldSymbol
					&& fieldSymbol.GetAttributes().Any(ad => ad?.AttributeClass?.ToDisplayString() == attributeName))
				{
					symbols.Add(fieldSymbol);
				}
			}
		}

		return symbols;
	}

	/// <summary>
	/// Get method declaration symbols with a specified attribute name
	/// </summary>
	public static IList<IMethodSymbol> GetMethodSymbolsWithAttribute(this GeneratorSyntaxContext context, string attributeName)
	{
		var symbols = new List<IMethodSymbol>();

		if (context.Node is MethodDeclarationSyntax methodDeclarationSyntax
			&& methodDeclarationSyntax.AttributeLists.Count > 0)
		{
			if (context.SemanticModel.GetDeclaredSymbol(methodDeclarationSyntax) is IMethodSymbol methodSymbol
				&& methodSymbol.GetAttributes().Any(
					ad => ad is not null
						&& ad.AttributeClass is not null
						&& ad.AttributeClass.ToDisplayString() == attributeName))
			{
				symbols.Add(methodSymbol);
			}
		}

		return symbols;
	}

	/// <summary>
	/// Get partial method declaration symbols with a specified attribute name
	/// </summary>
	public static IList<IMethodSymbol> GetPartialMethodSymbolsWithAttribute(this GeneratorSyntaxContext context, string attributeName)
	{
		var symbols = new List<IMethodSymbol>();

		if (context.Node is MethodDeclarationSyntax methodDeclarationSyntax
			&& methodDeclarationSyntax.AttributeLists.Count > 0)
		{
			if (context.SemanticModel.GetDeclaredSymbol(methodDeclarationSyntax) is IMethodSymbol methodSymbol
				&& methodSymbol.IsPartialDefinition
				&& methodSymbol.PartialImplementationPart is null
				&& methodSymbol.GetAttributes().Any(ad => ad?.AttributeClass?.ToDisplayString() == attributeName))
			{
				symbols.Add(methodSymbol);
			}
		}

		return symbols;
	}
}
