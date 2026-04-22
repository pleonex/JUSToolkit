using JUS.Tool.Texts.Converters;
using Yarhl.IO;

namespace JUS.SceneGatePlugin.Texts;

public class BinaryDeckConverterMatcher()
    : RegexPathBasedConverterMatcher<IBinary, Binary2Deck>(
        @"data/deck/Deck\.aar/deck/\w+/\d{3}.bin");
