using System.Diagnostics.CodeAnalysis;
using JUS.Tool.Texts.Converters;
using Yarhl.IO;

namespace JUS.SceneGatePlugin.Texts;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
public class BinarySuppChrConverterMatcher():
    PathBasedMatcher<IBinary, Binary2SuppChr>("data/bin/chr_s_t.bin");
