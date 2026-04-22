using System.Diagnostics.CodeAnalysis;
using JUS.Tool.Texts.Converters;
using Yarhl.IO;

namespace JUS.SceneGatePlugin.Texts;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
public class BinaryJGalaxyComplexConverterMatcher():
    PathBasedMatcher<IBinary, Binary2JGalaxyComplex>(
        "data/jgalaxy/jgalaxy.aar/jgalaxy/jgalaxy.bin");
