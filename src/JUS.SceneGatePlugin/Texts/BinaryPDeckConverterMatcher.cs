using JUS.Tool.Texts.Converters;
using Yarhl.IO;

namespace JUS.SceneGatePlugin.Texts;

public class BinaryPDeckConverterMatcher()
    : RegexPathBasedConverterMatcher<IBinary, Binary2PDeck>(
        @"data/deck/Deck\.aar/deck/\w+/p\d{3}.bin");
