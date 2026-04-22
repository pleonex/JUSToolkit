using System.Diagnostics.CodeAnalysis;
using JUS.Tool.Texts.Converters;
using Yarhl.IO;

namespace JUS.SceneGatePlugin.Texts;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
public class BinaryTutorialConverterMatcher():
    PathBasedMatcher<IBinary, Binary2Tutorial>(
        "data/deckmake/tutorial.bin",
        "data/battle/tutorial0.bin",
        "data/battle/tutorial1.bin",
        "data/battle/tutorial2.bin",
        "data/battle/tutorial3.bin",
        "data/battle/tutorial4.bin",
        "data/battle/tutorial5.bin");
