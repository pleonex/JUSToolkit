using SceneGate.UI.Formats.Discovery;
using Yarhl.FileFormat;
using Yarhl.FileSystem;

namespace JUS.SceneGatePlugin;

public abstract class PathBasedMatcher<TFormat, TConverter> : IConverterMatcher
    where TFormat : IFormat
    where TConverter : IConverter, new()
{
    private readonly string[] assetsPaths;

    protected PathBasedMatcher(params string[] assetsPaths)
    {
        ArgumentNullException.ThrowIfNull(assetsPaths);
        this.assetsPaths = assetsPaths;
    }

    public ConverterMatcherResult Match(Node input, ConverterMatcherContext context)
    {
        if (input.Format is not TFormat) {
            return ConverterMatcherResult.Incompatible();
        }

        bool? compatibleSoftware = SupportedSoftware.IsFromCompatibleSoftware(input, out Node? root);
        bool isInExpectedPath = assetsPaths.Any(x => $"{root?.Path}/{x}" == input.Path);

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
