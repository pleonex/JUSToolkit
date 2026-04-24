using System.Drawing;
using JUS.Tool.Containers.Converters;
using JUS.Tool.Graphics;
using JUS.Tool.Graphics.Converters;
using JUS.Tool.Utils;
using SceneGate.Ekona.Containers.Rom;
using Texim.Colors;
using Texim.Games.Nitro.Backgrounds.ScreenMaps;
using Texim.Images;
using Texim.Images.Standard;
using Texim.Sprites;
using Yarhl.FileFormat;
using Yarhl.FileSystem;
using Yarhl.IO;

namespace JUS.CLI.JUS;

public class SecretCommands
{
    public static void DrawSecret(string gamePath, string outputPath)
    {
        using Node root = NodeFactory.FromFile(gamePath, FileOpenMode.Read)
            .TransformWith(new Binary2NitroRom());

        Node opening = root.Children["data"]?.Children["opening"]?.Children["opening.aar"]!;
        opening = opening.TransformWith(new Binary2Alar3())
            .Children["opening"]!;

        var spaceAtm = opening.Children["space.atm"]!.TransformWith(new Binary2Almt());
        var spaceDig = opening.Children["space.dig"]!
            .TransformWith(new LzssDecompression())
            .TransformWith(new Binary2Dig());
        Dig space = spaceDig.GetFormatAs<Dig>()!;
        RgbImage spaceImage = space
            .ConvertWith(new MapDecompression(new MapDecompressionParams { Map = spaceAtm.GetFormatAs<IScreenMap>()!, OutOfBoundsTileIndex = 0 }))
            .ConvertWith(new IndexedImage2RgbImage(new IndexedImage2RgbImageParams(space) { FirstColorAsTransparent = true }));

        var starAtm = opening.Children["star00.atm"]!.TransformWith(new Binary2Almt());
        Dig starDig = opening.Children["star00.dig"]!
            .TransformWith(new LzssDecompression())
            .TransformWith(new Binary2Dig())
            .GetFormatAs<Dig>()!;
        RgbImage starImage = starDig.ConvertWith(new MapDecompression(new MapDecompressionParams { Map = starAtm.GetFormatAs<IScreenMap>()!, OutOfBoundsTileIndex = 0 }))
            .ConvertWith(new IndexedImage2RgbImage(new IndexedImage2RgbImageParams(starDig) { FirstColorAsTransparent = true }));

        CopySegment(starImage, spaceImage.Pixels);

        new RgbImage2BinaryPng().Convert(spaceImage)
            .Stream.WriteTo("space.png");
    }

    public static void CopySegment(IRgbImage segmentImage, Span<Rgb> output)
    {
        for (int x = 0; x < segmentImage.Width; x++) {
            for (int y = 0; y < segmentImage.Height; y++) {
                int inIdx = (y * segmentImage.Width) + x;
                Rgb pixel = segmentImage.Pixels[inIdx];
                if (pixel.Alpha == 0) {
                    // Do not set transparent pixel, so when copying one segment on top of another
                    // a transparent color doesn't override the color from a bottom layer.
                    continue;
                }

                int outIdx = (y * segmentImage.Width) + x;
                output[outIdx] = pixel;
            }
        }
    }
}
