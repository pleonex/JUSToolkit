using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.Versioning;
using JUS.Tool.Graphics;
using JUS.Tool.Graphics.Converters;
using JUS.Tool.Utils;
using SceneGate.Ekona.Containers.Rom;
using Texim.Games.Nitro.Backgrounds.ScreenMaps;
using Texim.Images;
using Texim.Images.Standard;
using Yarhl.FileFormat;
using Yarhl.FileSystem;
using Yarhl.IO;

namespace JUS.CLI.JUS;

[SupportedOSPlatform("windows")]
public class SuperSecretCommands
{
    public static void DrawSecret(string gamePath, string outputPath)
    {
        using Node root = NodeFactory.FromFile(gamePath, FileOpenMode.Read)
            .TransformWith(new Binary2NitroRom())
            .Children["data"]
            ?.Children["Data"]!;

        Node opening = root.Children["opening"]!;

        var spaceAtm = opening.Children["jump_bg.atm"]!.TransformWith(new Binary2Almt());
        var spaceDig = opening.Children["jump_bg.dig"]!
            .TransformWith(new LzssDecompression())
            .TransformWith(new Binary2Dig());
        Dig space = spaceDig.GetFormatAs<Dig>()!;
        RgbImage spaceImage = space
            .ConvertWith(new MapDecompression(new MapDecompressionParams { Map = spaceAtm.GetFormatAs<IScreenMap>()!, OutOfBoundsTileIndex = 0 }))
            .ConvertWith(new IndexedImage2RgbImage(new IndexedImage2RgbImageParams(space) { FirstColorAsTransparent = true }));

        var pngImage = new RgbImage2BinaryPng().Convert(spaceImage);
        pngImage.Stream.WriteTo(Path.Combine(outputPath, "jump_bg.png"));

        pngImage.Stream.Position = 0;
        var drawingImage = System.Drawing.Image.FromStream(pngImage.Stream);
        var drawer = System.Drawing.Graphics.FromImage(drawingImage);

        Stream secretData = root.Children["title"]?.Children["pattern.bin"]?.Stream!;
        var secretReader = new DataReader(secretData);
        uint pointCount = secretReader.ReadUInt32();
        secretReader.Stream.Position = 0x14;

        Color[] colors = [
            Color.FromArgb(0x4D, 0x9D, 0xE0),
            Color.FromArgb(0xE1,0x55, 0x54),
            Color.FromArgb(0xE1, 0xBC, 0x29),
            Color.FromArgb(0x3B, 0xB2, 0x73),
            Color.FromArgb(0x77, 0x68, 0xAE),
            Color.FromArgb(0x8E, 0x83, 0x58),
            Color.FromArgb(0x26, 0x46, 0x53),
        ];

        int colorIdx = 0;
        Pen pen = new Pen(colors[colorIdx++]);
        Point lastPoint = new(secretReader.ReadUInt16(), secretReader.ReadUInt16());
        for (int i = 1; i < pointCount; i++) {
            Point nextPoint = new(secretReader.ReadUInt16(), secretReader.ReadUInt16());
            if (nextPoint.X == 0xFFFF) {
                lastPoint = new(secretReader.ReadUInt16(), secretReader.ReadUInt16());
                i++;
                pen = new Pen(colors[colorIdx++]);
                continue;
            }

            drawer.DrawLine(pen, lastPoint, nextPoint);

            lastPoint = nextPoint;
        }

        drawer.Flush(FlushIntention.Sync);
        drawer.Save();
        drawingImage.Save(Path.Combine(outputPath, "jump.png"));
    }
}
