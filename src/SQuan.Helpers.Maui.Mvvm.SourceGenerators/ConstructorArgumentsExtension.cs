using System.Collections.Immutable;
using Microsoft.CodeAnalysis;

namespace SQuan.Helpers.Maui.Mvvm.SourceGenerators;

/// <summary>
/// Provides extension methods for working with constructor arguments.
/// </summary>
public static class ConstructorArgumentsExtension
{
	/// <summary>
	/// Retrieves a list of strings from the constructor arguments of a type.
	/// </summary>
	/// <param name="constructorArguments"></param>
	/// <returns></returns>
	public static List<string> ToStringList(this ImmutableArray<TypedConstant> constructorArguments)
	{
		var result = new List<string>();
		foreach (var arg in constructorArguments)
		{
			if (arg.Kind == TypedConstantKind.Primitive && arg.Value is string str)
			{
				result.Add(str);
			}
			if (arg.Kind == TypedConstantKind.Array)
			{
				foreach (var item in arg.Values)
				{
					if (item.Kind == TypedConstantKind.Primitive && item.Value is string strItem)
					{
						result.Add(strItem);
					}
				}
			}
		}
		return result;
	}
}
