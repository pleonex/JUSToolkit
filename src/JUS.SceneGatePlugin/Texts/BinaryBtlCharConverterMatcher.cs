using System.Diagnostics.CodeAnalysis;
using JUS.Tool.Texts.Converters;
using Yarhl.IO;

namespace JUS.SceneGatePlugin.Texts;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
public class BinaryBtlCharConverterMatcher():
    PathBasedMatcher<IBinary, Binary2BtlChr>("data/bin/chr_b_t.bin");
