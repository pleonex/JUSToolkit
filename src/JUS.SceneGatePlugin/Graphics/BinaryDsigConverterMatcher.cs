using System.Diagnostics.CodeAnalysis;
using JUS.Tool.Graphics.Converters;

namespace JUS.SceneGatePlugin.Graphics;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
public class BinaryDsigConverterMatcher()
    : HeaderBasedBinaryMatcher<Binary2Dig>("DSIG");
