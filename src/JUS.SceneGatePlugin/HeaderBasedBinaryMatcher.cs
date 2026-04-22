using SceneGate.UI.Formats.Discovery;
using Yarhl.FileSystem;
using Yarhl.IO;

namespace JUS.SceneGatePlugin;

public abstract class HeaderBasedBinaryMatcher<T> : IConverterMatcher
    where T : new()
{
    private readonly string expectedFormatId;
    private readonly byte? expectedVersion;
    private readonly byte? expectedFlags;

    protected HeaderBasedBinaryMatcher(string formatId)
    {
        ArgumentNullException.ThrowIfNull(formatId);
        expectedFormatId = formatId;
    }

    protected HeaderBasedBinaryMatcher(string formatId, byte version)
    {
        ArgumentNullException.ThrowIfNull(formatId);
        expectedFormatId = formatId;
        expectedVersion = version;
    }

    protected HeaderBasedBinaryMatcher(string formatId, byte version, byte flags)
    {
        ArgumentNullException.ThrowIfNull(formatId);
        expectedFormatId = formatId;
        expectedVersion = version;
        expectedFlags = flags;
    }

    public ConverterMatcherResult Match(Node input, ConverterMatcherContext context)
    {
        if (input.Format is not IBinary) {
            return ConverterMatcherResult.Incompatible();
        }

        MatchingConfidence level = MatchingConfidence.CannotDetermine;
        if (context.Header.Length >= 4) {
            string formatId = context.Header.ReadAsciiString(0, 4);
            if (formatId != expectedFormatId) {
                return ConverterMatcherResult.Incompatible();
            }

            if (expectedVersion.HasValue && context.Header.Length >= 5) {
                byte version = context.Header.Span[4];
                if (version != expectedVersion.Value) {
                    return ConverterMatcherResult.Incompatible();
                }
            }

            if (expectedFlags.HasValue && context.Header.Length >= 6) {
                byte flags = context.Header.Span[5];
                if (flags != expectedFlags.Value) {
                    return ConverterMatcherResult.Incompatible();
                }
            }

            level = MatchingConfidence.CouldBe;
        }

        bool? fromCompatibleSoftware = SupportedSoftware.IsFromCompatibleSoftware(input, out _);
        if (fromCompatibleSoftware == false) {
            return ConverterMatcherResult.Incompatible();
        }

        if (fromCompatibleSoftware == true && level >= MatchingConfidence.CouldBe) {
            level = MatchingConfidence.Confident;
        }

        return ConverterMatcherResult.Create(level, new T());
    }
}
