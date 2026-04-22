using System.Diagnostics.CodeAnalysis;
using JUS.Tool.Graphics.Converters;

namespace JUS.SceneGatePlugin.Graphics;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
public class BinaryDstx3ConverterMatcher()
    : HeaderBasedBinaryMatcher<BinaryToDtx3>("DSTX", 1, 3);
