using JUS.Tool.Texts.Converters;
using Yarhl.IO;

namespace JUS.SceneGatePlugin.Texts;

public class BinaryAbilityConverterMatcher():
    PathBasedMatcher<IBinary, Binary2Ability>("data/bin/ability_t.bin");
