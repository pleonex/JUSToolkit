using System.Diagnostics.CodeAnalysis;
using JUS.Tool.Containers.Converters;

namespace JUS.SceneGatePlugin.Containers;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
public class BinaryAlar2ConverterMatcher()
    : HeaderBasedBinaryMatcher<Binary2Alar2>("ALAR", 2);

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
public class BinaryAlar3ConverterMatcher()
    : HeaderBasedBinaryMatcher<Binary2Alar3>("ALAR", 3);
