using JUS.Tool.Texts.Converters;
using Yarhl.IO;

namespace JUS.SceneGatePlugin.Texts;

public class BinaryInfoDeckDeckConverterMatcher()
    : RegexPathBasedConverterMatcher<IBinary, Binary2InfoDeckDeck>(@"data/bin/InfoDeck.aar/bin/deck/\w{2}.bin");
