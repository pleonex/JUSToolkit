using System.Diagnostics.CodeAnalysis;
using JUS.Tool.Graphics.Converters;

namespace JUS.SceneGatePlugin.Graphics;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
public class BinaryDstx4ConverterMatcher()
    : HeaderBasedBinaryMatcher<BinaryDtx4ToSpriteImage>("DSTX", 1, 4);
