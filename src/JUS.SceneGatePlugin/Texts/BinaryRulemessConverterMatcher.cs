using System.Diagnostics.CodeAnalysis;
using JUS.Tool.Texts.Converters;
using Yarhl.IO;

namespace JUS.SceneGatePlugin.Texts;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
public class BinaryRulemessConverterMatcher():
    PathBasedMatcher<IBinary, Binary2Rulemess>("data/bin/rulemess.bin");
