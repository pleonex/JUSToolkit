using System.Diagnostics.CodeAnalysis;
using JUS.Tool.Graphics.Converters;
using Yarhl.IO;

namespace JUS.SceneGatePlugin.Graphics;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
public class BinaryKomaConverterMatcher()
    : PathBasedMatcher<IBinary, Binary2Koma>("data/bin/koma.bin");
