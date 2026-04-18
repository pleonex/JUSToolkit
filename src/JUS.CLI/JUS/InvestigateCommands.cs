using System.Text;
using SceneGate.Ekona.Containers.Rom;
using Yarhl.FileSystem;
using Yarhl.IO;

namespace JUS.CLI.JUS;

public static class InvestigateCommands
{
    public static void PrintAlarFiles(FileInfo game, bool recursive)
    {
        using Node rom = NodeFactory.FromFile(game.FullName, FileOpenMode.Read)
            .TransformWith(new Binary2NitroRom());

        foreach (Node node in Navigator.IterateNodes(rom))
        {
            if (node.IsContainer || !node.Name.EndsWith(".aar"))
            {
                continue;
            }

            PrintAlarInfo(node);
        }

        var sorted = alarNames
            .OrderBy(x => x.Item1.Length)
            .Reverse();
        bool g = sorted.All(x => x.Item3 == x.Item4);
        foreach (var x in sorted)
        {
            string equal = x.Item3 == x.Item4 ? "v" : "x";
            Console.WriteLine($"{equal} {x.Item1} (v{x.Item2}-{x.Item5:X2}): {x.Item3:X4} vs {x.Item4:X4}");
        }

        Console.WriteLine(g);

        return;

        static void PrintAlarInfo(Node alar)
        {
            var reader = new DataReader(alar.Stream!);
            string format = reader.ReadString(4);
            if (format != "ALAR")
            {
                return; // compressed probably
            }

            int version = reader.ReadByte();
            int flags = reader.ReadByte();
            int fileCount = reader.ReadUInt16();
            _ = reader.ReadUInt32(); // only on some v2

            // no v1
            // v2 only with flags 0x01 and 0x03
            // v3 only with flags 0x45 and 0x05
            if (version == 3)
            {
                PrintAlarV3(reader, flags, fileCount);
            }
            else if (version == 2)
            {
                PrintAlarV2(reader, flags, fileCount);
            }
        }

        static void PrintAlarV2(DataReader reader, int flags, int fileCount)
        {
            for (int i = 0; i < fileCount; i++)
            {
                reader.Stream.Position = 0x10 + (i * 16);
                uint id = reader.ReadUInt32();
                uint offset = reader.ReadUInt32();
                uint length = reader.ReadUInt32();
                uint fileFlags = reader.ReadUInt32();
                string name = string.Empty;
                if (fileFlags >> 31 != 0)
                {
                    // has name and checksum
                    reader.Stream.Position = offset - 0x22;
                    name = reader.ReadString(0x20).Replace("\0", "");
                    ushort checksum = reader.ReadUInt16();

                    uint actual = Checksum1(name);
                    //alarNames.Add((name, 2, checksum, actual, flags));
                    if (checksum != actual)
                    {
                        Console.WriteLine($"{i} {name} {checksum:X4} vs {actual:X4}");
                    }
                }

                uint unkFlags = (fileFlags >> 24) & 0x7F;
                uint fileType = id >> 24;

                // AMT and AOD 0x40, DIG 0x01
                if (unkFlags != 0x00)
                {
                    Console.WriteLine($"{name}: {unkFlags:X2}");
                }

                // Only may different to 1 for AMT (41) and AOD (43) file types
                if ((fileFlags & 0x00FFFFFF) != 1)
                {
                    //Console.WriteLine($"{name}: {fileFlags:X8} {fileType:X2}");
                }
            }
        }

        static void PrintAlarV3(DataReader reader, int flags, int fileCount)
        {
            for (int i = 0; i < fileCount; i++)
            {
                reader.Stream.Position = 0x12 + (i * 2);
                ushort offset = reader.ReadUInt16();

                reader.Stream.Position = offset + 0x0C;
                uint fileFlags = reader.ReadUInt32();
                if (fileFlags >> 24 != 0x80)
                {
                    Console.WriteLine($"{i}: {fileFlags:X8}");
                    return;
                }

                ushort checksum = reader.ReadUInt16();
                string name = reader.ReadString();
                ushort actual;
                if ((flags & 0x40) != 0)
                {
                    actual = Checksum2(name);
                }
                else
                {
                    actual = Checksum1(name);
                }

                if (actual != checksum)
                {
                    Console.WriteLine($"{flags:X} {i}: {name} {checksum:X} != {actual:X}");
                }

                if ((fileFlags & 0x00FFFFFF) != 1)
                {
                    Console.WriteLine($"{i}: {name} -> {fileFlags:X8}");
                }

                // none, that's why
                if (name.EndsWith(".amt"))
                {
                    Console.WriteLine($"{name} -> {fileFlags:X8}");
                }
            }
        }
    }

    private static List<(string, int, uint, uint, int)> alarNames = [];

    private static ushort Checksum1(string name)
    {
        int extensionIdx = name.IndexOf('.');
        int endPos = extensionIdx == -1 ? name.Length : extensionIdx;
        string target = name[..endPos];
        byte[] data = Encoding.ASCII.GetBytes(target);

        uint checksum = 0;
        foreach (byte ch in data) {
            uint tmp = (checksum << 1) ^ ch;
            checksum = (tmp & 0xFFFF) ^ (tmp >> 16);
        }

        return (ushort)checksum;
    }

    private static ushort Checksum2(string name)
    {
        int extensionIdx = name.IndexOf('.');
        int endPos = extensionIdx == -1 ? name.Length : extensionIdx;
        endPos = Math.Min(endPos, 0x20);
        string target = name[..endPos];
        byte[] data = Encoding.ASCII.GetBytes(target);

        int checksum = 0;
        foreach (byte ch in data) {
            checksum ^= (ushort)(checksum << 1) | ch;
        }

        return (ushort)checksum;
    }
}
