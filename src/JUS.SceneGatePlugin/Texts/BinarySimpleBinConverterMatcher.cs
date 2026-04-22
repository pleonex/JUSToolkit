using System.Diagnostics.CodeAnalysis;
using JUS.Tool.Texts.Converters;
using Yarhl.IO;

namespace JUS.SceneGatePlugin.Texts;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
public class BinarySimpleBinConverterMatcher():
    PathBasedMatcher<IBinary, Binary2SimpleBin>(
        "data/bin/clearlst.bin",
        "data/bin/infoname.bin",
        "data/bin/title.bin");
