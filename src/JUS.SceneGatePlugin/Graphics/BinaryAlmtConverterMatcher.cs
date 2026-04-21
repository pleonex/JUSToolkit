using System.Diagnostics.CodeAnalysis;
using JUS.Tool.Graphics.Converters;

namespace JUS.SceneGatePlugin.Graphics;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
public class BinaryAlmtConverterMatcher()
    : HeaderBasedBinaryMatcher<Binary2Almt>("ALMT");
