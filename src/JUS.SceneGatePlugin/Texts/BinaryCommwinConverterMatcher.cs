using System.Diagnostics.CodeAnalysis;
using JUS.Tool.Texts.Converters;
using Yarhl.IO;

namespace JUS.SceneGatePlugin.Texts;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
public class BinaryCommwinConverterMatcher():
    PathBasedMatcher<IBinary, Binary2Commwin>("data/bin/commwin.bin");
