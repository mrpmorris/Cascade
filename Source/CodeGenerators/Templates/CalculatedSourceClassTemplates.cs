namespace CodeGenerators.Templates;

internal static class CalculatedSourceClassTemplates
{
	public const string ClassTemplate = @"
public class CalculatedSource<{GenericTypes}, TResult> : CalculatedSourceBase<TResult>
{
{Fields}
	private readonly Func<{GenericTypes}, TResult> Calculate = null!;

	public CalculatedSource(
{ConstructorParameters},
		Func<{GenericTypes}, TResult> calculate)
		: base({ConstructorParameterNames})
	{
		Calculate = calculate ?? throw new ArgumentNullException(nameof(calculate));
{FieldAssignments}
	}

	protected override TResult GetCalulatedValue() =>
		Calculate(
{CalculatedSourceValues});
}";

	public const string FieldTemplate = "\tprivate readonly ISource<S{SourceNumber}> Source{SourceNumber} = null!;";

	public const string ParameterTemplate = "\t\tISource<S{SourceNumber}> source{SourceNumber}";

	public const string FieldAssignmentTemplate = "\t\tSource{SourceNumber} = source{SourceNumber} ?? throw new ArgumentNullException(nameof(source{SourceNumber}));";

	public const string CalculateArgumentTemplate = "\t\t\tSource{SourceNumber}.Value";
}
