using System.Diagnostics.CodeAnalysis;
using JUS.Tool.Texts.Converters;
using Yarhl.IO;

namespace JUS.SceneGatePlugin.Texts;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
public class BinaryJGalaxySimpleConverterMatcher():
    PathBasedMatcher<IBinary, Binary2JGalaxySimple>(
        "data/jgalaxy/jgalaxy.aar/jgalaxy/battle.bin",
        "data/jgalaxy/jgalaxy.aar/jgalaxy/mission.bin");
