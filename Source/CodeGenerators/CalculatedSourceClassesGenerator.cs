using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CodeGenerators.Templates;

namespace CodeGenerators;

[Generator]
public class CalculatedSourceClassesGenerator : ISourceGenerator
{
	public void Initialize(GeneratorInitializationContext context)
	{
	}

	public void Execute(GeneratorExecutionContext context)
	{
		string source = BuildFileSource();
		context.AddSource("CalculatedSource.g.cs", SourceText.From(source, System.Text.Encoding.UTF8));
	}

	private static string BuildFileSource()
	{
		var builder = new StringBuilder();
		builder.AppendLine("using System;");
		builder.AppendLine("using System.Collections.Generic;");
		builder.AppendLine("using System.Linq;");
		builder.AppendLine("using System.Text;\r\n"); 
		builder.AppendLine("namespace Morris.Cascades;\r\n");
		for (int i = 1; i <= 16; i++)
			BuildClassSource(builder, i);
		return builder.ToString();
	}

	private static void BuildClassSource(StringBuilder builder, int numberOfInputs)
	{
		IEnumerable<int> range = Enumerable.Range(1, numberOfInputs);
		string genericTypes = string.Join(", ", range.Select(i => $"S{i}"));
		string fields = string.Join("\r\n", range.Select(i => CalculatedSourceClassTemplates.FieldTemplate.Replace("{SourceNumber}", i.ToString())));
		string constructorParameters = string.Join(",\r\n", range.Select(i => CalculatedSourceClassTemplates.ParameterTemplate.Replace("{SourceNumber}", i.ToString())));
		string constructorParameterNames = string.Join(", ", range.Select(i => $"source{i}"));
		string fieldAssignments = string.Join("\r\n", range.Select(i => CalculatedSourceClassTemplates.FieldAssignmentTemplate.Replace("{SourceNumber}", i.ToString())));
		string calculateArguments = string.Join(",\r\n", range.Select(i => CalculatedSourceClassTemplates.CalculateArgumentTemplate.Replace("{SourceNumber}", i.ToString())));
		string result = CalculatedSourceClassTemplates.ClassTemplate
			.Replace("{GenericTypes}", genericTypes)
			.Replace("{Fields}", fields)
			.Replace("{ConstructorParameters}", constructorParameters)
			.Replace("{ConstructorParameterNames}", constructorParameterNames)
			.Replace("{FieldAssignments}", fieldAssignments)
			.Replace("{CalculatedSourceValues}", calculateArguments);
		builder.AppendLine($"{result}\r\n");
	}
}
