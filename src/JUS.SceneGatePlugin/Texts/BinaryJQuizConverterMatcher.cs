using System.Diagnostics.CodeAnalysis;
using JUS.Tool.Texts.Converters;
using Yarhl.IO;

namespace JUS.SceneGatePlugin.Texts;

[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicConstructors)]
public class BinaryJQuizConverterMatcher():
    PathBasedMatcher<IBinary, Binary2JQuiz>(
        "data/jquiz/jquiz_pack.aar/jquiz/jquiz.bin");
