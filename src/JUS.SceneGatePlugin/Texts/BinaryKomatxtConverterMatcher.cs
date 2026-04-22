using System.Diagnostics.CodeAnalysis;
using JUS.Tool.Texts.Converters;
using Yarhl.IO;

namespace JUS.SceneGatePlugin.Texts;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
public class BinaryKomatxtConverterMatcher():
    PathBasedMatcher<IBinary, Binary2Komatxt>("data/bin/komatxt.bin");
