using System.Text.RegularExpressions;
using SceneGate.UI.Formats.Discovery;
using Yarhl.FileFormat;
using Yarhl.FileSystem;

namespace JUS.SceneGatePlugin;

public abstract class RegexPathBasedConverterMatcher<TFormat, TConverter> : IConverterMatcher
    where TFormat : IFormat
    where TConverter : IConverter, new()
{
    private readonly Regex regex;

    protected RegexPathBasedConverterMatcher(string pattern)
    {
        ArgumentException.ThrowIfNullOrEmpty(pattern);
        regex = new Regex(pattern, RegexOptions.Compiled);
    }

    public ConverterMatcherResult Match(Node input, ConverterMatcherContext context)
    {
        if (input.Format is not TFormat) {
            return ConverterMatcherResult.Incompatible();
        }

        bool? compatibleSoftware = SupportedSoftware.IsFromCompatibleSoftware(input, out _);
        bool isInExpectedPath = regex.IsMatch(input.Path);

        MatchingConfidence level = MatchingConfidence.Incompatible;
        if (compatibleSoftware == true && isInExpectedPath) {
            level = MatchingConfidence.Confident;
        } else if (compatibleSoftware is null && isInExpectedPath) {
            level = MatchingConfidence.CouldBe;
        }

        if (level is MatchingConfidence.Incompatible) {
            return ConverterMatcherResult.Incompatible();
        }

        return ConverterMatcherResult.Create(level, new TConverter());
    }
}
