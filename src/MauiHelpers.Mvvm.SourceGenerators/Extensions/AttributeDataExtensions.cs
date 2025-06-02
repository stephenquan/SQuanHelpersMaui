
using Microsoft.CodeAnalysis;

namespace MauiHelpers.Mvvm.SourceGenerators;

static class AttributeDataExtensions
{
	public static TypedConstant GetNamedArgument(this AttributeData attribute, string name)
	{
		return attribute.NamedArguments.SingleOrDefault(kvp => kvp.Key == name).Value;
	}

	public static object? GetNamedArgumentValue(this AttributeData attribute, string name, object? defaultValue = null)
	{
		var argument = attribute.GetNamedArgument(name);
		return argument.IsNull
			? defaultValue
			: argument.Value;
	}

	public static TEnum GetNamedArgumentValue<TEnum>(this AttributeData attribute, string name, TEnum defaultValue = default) where TEnum : struct
	{
		var argument = attribute.GetNamedArgument(name);
		if (argument.IsNull || argument.Kind != TypedConstantKind.Enum)
		{
			return defaultValue;
		}

		if (argument.Value is null)
		{
			return defaultValue;
		}

		if (Enum.TryParse<TEnum>(argument.Value.ToString(), true, out var e))
		{
			return (TEnum)e;
		}
		else
		{
			return defaultValue;
		}
	}

	internal static string ConvertXMLDocToString(string xmlDoc, int numTabs = 1)
	{
		if (string.IsNullOrEmpty(xmlDoc))
		{
			return "";
		}
		var lines = xmlDoc.Split(["\r\n", "\n", "\r"], StringSplitOptions.None).ToList();
		lines.RemoveAt(0);
		lines.RemoveAt(lines.Count - 1);
		lines.RemoveAt(lines.Count - 1);
		for (var i = 0; i < lines.Count; i++)
		{
			lines[i] = "\n" + new string(' ', numTabs * 4) + "/// " + lines[i].TrimStart();
		}
		xmlDoc = string.Join("", lines);
		return xmlDoc;
	}
}
