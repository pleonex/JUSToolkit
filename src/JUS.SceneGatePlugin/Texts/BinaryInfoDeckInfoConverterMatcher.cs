using JUS.Tool.Texts.Converters;
using Yarhl.IO;

namespace JUS.SceneGatePlugin.Texts;

public class BinaryInfoDeckInfoConverterMatcher()
    : RegexPathBasedConverterMatcher<IBinary, Binary2InfoDeckInfo>(@"data/bin/InfoDeck.aar/bin/info/(\w{2}|jump).bin");
