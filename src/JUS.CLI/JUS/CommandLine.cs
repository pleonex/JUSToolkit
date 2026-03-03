// Copyright (c) 2022 Priverop

// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:

// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.

// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
// SOFTWARE.
using System.CommandLine;

namespace JUSToolkit.CLI.JUS
{
    /// <summary>
    /// Command-line interface for Jump Ultimate Stars! game.
    /// </summary>
    public static class CommandLine
    {
        /// <summary>
        /// Create the CLI command for the game.
        /// </summary>
        /// <returns>The CLI command.</returns>
        public static Command CreateCommand()
        {
            return new Command("jus", "Jump Ultimate Stars! game") {
                CreateContainerCommand(),
                CreateGraphicCommand(),
                CreateTextCommand(),
                CreateBatchCommand(),
                CreateRomCommand(),
            };
        }

        private static Command CreateGraphicCommand()
        {
            var exportKomasContainer = new Option<string>("--container", "the input koma.aar container");
            var exportKomasKoma = new Option<string>("--koma", "the koma.bin file");
            var exportKomasKshape = new Option<string>("--kshape", "the kshape.bin file");
            var exportKomasOutput = new Option<string>("--output", "the output folder");
            var exportKomas = new Command("export-komas", "Export komas") {
                exportKomasContainer,
                exportKomasKoma,
                exportKomasKshape,
                exportKomasOutput,
            };
            exportKomas.SetAction(ctx => {
                DtxCommands.ExportKomas(
                    ctx.GetValue(exportKomasContainer),
                    ctx.GetValue(exportKomasKoma),
                    ctx.GetValue(exportKomasKshape),
                    ctx.GetValue(exportKomasOutput));
            });

            var importKomasPng = new Option<string>("--png", "the png to import");
            var importKomasDtx = new Option<string>("--dtx", "the original file.dtx");
            var importKomasKoma = new Option<string>("--koma", "the koma.bin file");
            var importKomasKshape = new Option<string>("--kshape", "the kshape.bin file");
            var importKomasOutput = new Option<string>("--output", "the output folder");
            var importDtx4 = new Command("import-komas", "Import komas") {
                importKomasPng,
                importKomasDtx,
                importKomasKoma,
                importKomasKshape,
                importKomasOutput,
            };
            importDtx4.SetAction(ctx => {
                DtxCommands.ImportKoma(
                    ctx.GetValue(importKomasPng),
                    ctx.GetValue(importKomasDtx),
                    ctx.GetValue(importKomasKoma),
                    ctx.GetValue(importKomasKshape),
                    ctx.GetValue(importKomasOutput));
            });

            var exportDtx4Dtx = new Option<string>("--dtx", "the input file.dtx");
            var exportDtx4Koma = new Option<string>("--koma", "the koma.bin file");
            var exportDtx4Kshape = new Option<string>("--kshape", "the kshape.bin file");
            var exportDtx4Output = new Option<string>("--output", "the output folder");
            var exportDtx4 = new Command("export-dtx4", "Export dtx") {
                exportDtx4Dtx,
                exportDtx4Koma,
                exportDtx4Kshape,
                exportDtx4Output,
            };
            exportDtx4.SetAction(ctx => {
                DtxCommands.ExportDtx4(
                    ctx.GetValue(exportDtx4Dtx),
                    ctx.GetValue(exportDtx4Koma),
                    ctx.GetValue(exportDtx4Kshape),
                    ctx.GetValue(exportDtx4Output));
            });

            var exportDtx3Dtx = new Option<string>("--dtx", "the input file.dtx");
            var exportDtx3Output = new Option<string>("--output", "the output folder");
            var exportDtx3 = new Command("export-dtx3", "Export dtx") {
                exportDtx3Dtx,
                exportDtx3Output,
            };
            exportDtx3.SetAction(ctx => {
                DtxCommands.ExportDtx3(
                    ctx.GetValue(exportDtx3Dtx),
                    ctx.GetValue(exportDtx3Output));
            });

            var exportDtx3TxDtx = new Option<string>("--dtx", "the input file.dtx");
            var exportDtx3TxOutput = new Option<string>("--output", "the output folder");
            var exportDtx3TxImage = new Command("export-dtx3tx-image", "Export dtx3 image") {
                exportDtx3TxDtx,
                exportDtx3TxOutput,
            };
            exportDtx3TxImage.SetAction(ctx => {
                DtxCommands.ExportDtx3TxImage(
                    ctx.GetValue(exportDtx3TxDtx),
                    ctx.GetValue(exportDtx3TxOutput));
            });

            var importDtx3Input = new Option<string>("--input", "the folder with the .pngs to import");
            var importDtx3Dtx = new Option<string>("--dtx", "the input file.dtx");
            var importDtx3Output = new Option<string>("--output", "the output folder");
            var importDtx3 = new Command("import-dtx3", "Import dtx3") {
                importDtx3Input,
                importDtx3Dtx,
                importDtx3Output,
            };
            importDtx3.SetAction(ctx => {
                DtxCommands.ImportDtx3(
                    ctx.GetValue(importDtx3Input),
                    ctx.GetValue(importDtx3Dtx),
                    ctx.GetValue(importDtx3Output));
            });

            var importDtx3TxInput = new Option<string>("--input", "the folder with the .pngs to import");
            var importDtx3TxDtx = new Option<string>("--dtx", "the input file.dtx");
            var importDtx3TxYaml = new Option<string>("--yaml", "segments metadata");
            var importDtx3TxOutput = new Option<string>("--output", "the output folder");
            var importDtx3Tx = new Command("import-dtx3tx", "Import dtx3") {
                importDtx3TxInput,
                importDtx3TxDtx,
                importDtx3TxYaml,
                importDtx3TxOutput,
            };
            importDtx3Tx.SetAction(ctx => {
                DtxCommands.ImportDtx3Tx(
                    ctx.GetValue(importDtx3TxInput),
                    ctx.GetValue(importDtx3TxDtx),
                    ctx.GetValue(importDtx3TxYaml),
                    ctx.GetValue(importDtx3TxOutput));
            });

            var exportDigDig = new Option<string>("--dig", "the input file.dig");
            var exportDigAtm = new Option<string>("--atm", "the input map.atm file");
            var exportDigOutput = new Option<string>("--output", "the output folder");
            var exportDsigAlmt = new Command("export-dig", "Export dsig+almt") {
                exportDigDig,
                exportDigAtm,
                exportDigOutput,
            };
            exportDsigAlmt.SetAction(ctx => {
                DigCommands.ExportDig(
                    ctx.GetValue(exportDigDig),
                    ctx.GetValue(exportDigAtm),
                    ctx.GetValue(exportDigOutput));
            });

            var importDigInput = new Option<string>("--input", "the png to import");
            var importDigInsertTransparent = new Option<bool>("--insertTransparent", "insert a transparent tile at the start of the .dig");
            var importDigDig = new Option<string>("--dig", "the original file.dig");
            var importDigAtm = new Option<string>("--atm", "the original file.atm");
            var importDigOutput = new Option<string>("--output", "the output folder");
            var importDig = new Command("import-dig", "Import dsig+almt") {
                importDigInput,
                importDigInsertTransparent,
                importDigDig,
                importDigAtm,
                importDigOutput,
            };
            importDig.SetAction(ctx => {
                DigCommands.ImportDig(
                    ctx.GetValue(importDigInput),
                    ctx.GetValue(importDigInsertTransparent),
                    ctx.GetValue(importDigDig),
                    ctx.GetValue(importDigAtm),
                    ctx.GetValue(importDigOutput));
            });

            var mergeDigInput = new Option<string[]>("--input", "the png to import") { Arity = ArgumentArity.OneOrMore };
            var mergeDigInsertTransparent = new Option<bool>("--insertTransparent", "insert a transparent tile at the start of the .dig");
            var mergeDigDig = new Option<string>("--dig", "the original file.dig");
            var mergeDigAtm = new Option<string[]>("--atm", "the original file.atm") { Arity = ArgumentArity.OneOrMore };
            var mergeDigOutput = new Option<string>("--output", "the output folder");
            var mergeDig = new Command("merge-dig", "Import Import dsig with multiple almt") {
                mergeDigInput,
                mergeDigInsertTransparent,
                mergeDigDig,
                mergeDigAtm,
                mergeDigOutput,
            };
            mergeDig.SetAction(ctx => {
                DigCommands.MergeDig(
                    ctx.GetValue(mergeDigInput),
                    ctx.GetValue(mergeDigInsertTransparent),
                    ctx.GetValue(mergeDigDig),
                    ctx.GetValue(mergeDigAtm),
                    ctx.GetValue(mergeDigOutput));
            });

            var exportYamlDtx3Dtx = new Option<string>("--dtx", "the input file.dtx");
            var exportYamlDtx3Output = new Option<string>("--output", "the output folder");
            var exportYamlDtx3 = new Command("export-yaml-dtx3", "Export the sprite metadata") {
                exportYamlDtx3Dtx,
                exportYamlDtx3Output,
            };
            exportYamlDtx3.SetAction(ctx => {
                DtxCommands.ExportYamlDtx3(
                    ctx.GetValue(exportYamlDtx3Dtx),
                    ctx.GetValue(exportYamlDtx3Output));
            });

            return new Command("graphics", "Import/Export graphic files") {
                exportKomas,
                exportDtx3,
                exportDtx3TxImage,
                exportYamlDtx3,
                exportDtx4,
                importDtx3,
                importDtx3Tx,
                importDtx4,
                exportDsigAlmt,
                importDig,
                mergeDig,
            };
        }

        private static Command CreateContainerCommand()
        {
            var exportContainer = new Option<string>("--container", "the input alar container");
            var exportOutput = new Option<string>("--output", "the output folder");
            var export = new Command("export", "Export alar container, any version") {
                exportContainer,
                exportOutput,
            };
            export.SetAction(ctx => {
                ContainerCommands.Export(
                    ctx.GetValue(exportContainer),
                    ctx.GetValue(exportOutput));
            });

            var exportAlar3Container = new Option<string>("--container", "the input alar3 container");
            var exportAlar3Output = new Option<string>("--output", "the output folder");
            var exportAlar3 = new Command("export-alar3", "Export alar3") {
                exportAlar3Container,
                exportAlar3Output,
            };
            exportAlar3.SetAction(ctx => {
                ContainerCommands.ExportAlar3(
                    ctx.GetValue(exportAlar3Container),
                    ctx.GetValue(exportAlar3Output));
            });

            var exportAlar2Container = new Option<string>("--container", "the input alar2 container");
            var exportAlar2Output = new Option<string>("--output", "the output folder");
            var exportAlar2 = new Command("export-alar2", "Export alar2") {
                exportAlar2Container,
                exportAlar2Output,
            };
            exportAlar2.SetAction(ctx => {
                ContainerCommands.ExportAlar2(
                    ctx.GetValue(exportAlar2Container),
                    ctx.GetValue(exportAlar2Output));
            });

            var importContainer = new Option<string>("--container", "the input alar container");
            var importInput = new Option<string>("--input", "the input directory to insert");
            var importOutput = new Option<string>("--output", "the output folder");
            var import = new Command("import", "import alar") {
                importContainer,
                importInput,
                importOutput,
            };
            import.SetAction(ctx => {
                ContainerCommands.Import(
                    ctx.GetValue(importContainer),
                    ctx.GetValue(importInput),
                    ctx.GetValue(importOutput));
            });

            var importAlar2Container = new Option<string>("--container", "the input alar2 container");
            var importAlar2Input = new Option<string>("--input", "the input directory to insert");
            var importAlar2Output = new Option<string>("--output", "the output folder");
            var importAlar2 = new Command("import-alar2", "import alar2") {
                importAlar2Container,
                importAlar2Input,
                importAlar2Output,
            };
            importAlar2.SetAction(ctx => {
                ContainerCommands.ImportAlar2(
                    ctx.GetValue(importAlar2Container),
                    ctx.GetValue(importAlar2Input),
                    ctx.GetValue(importAlar2Output));
            });

            var importAlar3Container = new Option<string>("--container", "the input alar3 container");
            var importAlar3Input = new Option<string>("--input", "the input directory to insert");
            var importAlar3Output = new Option<string>("--output", "the output folder");
            var importAlar3 = new Command("import-alar3", "import alar3") {
                importAlar3Container,
                importAlar3Input,
                importAlar3Output,
            };
            importAlar3.SetAction(ctx => {
                ContainerCommands.ImportAlar3(
                    ctx.GetValue(importAlar3Container),
                    ctx.GetValue(importAlar3Input),
                    ctx.GetValue(importAlar3Output));
            });

            return new Command("containers", "Unpack/Repack container files") {
                export,
                exportAlar3,
                exportAlar2,
                import,
                importAlar2,
                importAlar3,
            };
        }

        private static Command CreateBatchCommand()
        {
            var importPng2Alar3Container = new Option<string>("--container", "the original alar3 container");
            var importPng2Alar3Input = new Option<string>("--input", "the input directory to insert");
            var importPng2Alar3Output = new Option<string>("--output", "the output folder");
            var importPng2Alar3 = new Command("import-png-alar3", "Batch import PNG to alar3") {
                importPng2Alar3Container,
                importPng2Alar3Input,
                importPng2Alar3Output,
            };
            importPng2Alar3.SetAction(ctx => {
                BatchCommands.ImportPng2Alar3(
                    ctx.GetValue(importPng2Alar3Container),
                    ctx.GetValue(importPng2Alar3Input),
                    ctx.GetValue(importPng2Alar3Output));
            });

            var exportAlarDig2PngContainer = new Option<string>("--container", "the alar container");
            var exportAlarDig2PngOutput = new Option<string>("--output", "the output folder");
            var exportAlarDig2Png = new Command("export-alar-dig-png", "Batch export PNGs from alar digs files") {
                exportAlarDig2PngContainer,
                exportAlarDig2PngOutput,
            };
            exportAlarDig2Png.SetAction(ctx => {
                BatchCommands.ExportAlarDig2Png(
                    ctx.GetValue(exportAlarDig2PngContainer),
                    ctx.GetValue(exportAlarDig2PngOutput));
            });

            var exportAlarDtx2PngContainer = new Option<string>("--container", "the alar container");
            var exportAlarDtx2PngOutput = new Option<string>("--output", "the output folder");
            var exportAlarDtx2PngGetImage = new Option<bool>("--getImage", "only export the image");
            var exportAlarDtx2Png = new Command("export-alar-dtx-png", "Batch export PNGs from alar dtxs files") {
                exportAlarDtx2PngContainer,
                exportAlarDtx2PngOutput,
                exportAlarDtx2PngGetImage,
            };
            exportAlarDtx2Png.SetAction(ctx => {
                BatchCommands.ExportAlarDtx2Png(
                    ctx.GetValue(exportAlarDtx2PngContainer),
                    ctx.GetValue(exportAlarDtx2PngOutput),
                    ctx.GetValue(exportAlarDtx2PngGetImage));
            });

            var uncompressEverythingInput = new Option<string>("--input", "the input directory with all the files");
            var uncompressEverything = new Command("uncompress-everything", "Batch uncompress every file") {
                uncompressEverythingInput,
            };
            uncompressEverything.SetAction(ctx => {
                BatchCommands.UncompressFiles(
                    ctx.GetValue(uncompressEverythingInput));
            });

            return new Command("batch", "Batch import/export PNG to/from Alar") {
                importPng2Alar3,
                exportAlarDig2Png,
                exportAlarDtx2Png,
                uncompressEverything,
            };
        }

        private static Command CreateRomCommand()
        {
            var importGame = new Option<string>("--game", "the path of the rom");
            var importInput = new Option<string>("--input", "the input directory to insert");
            var importOutput = new Option<string>("--output", "the output folder");
            var import = new Command("import", "Import files to the Rom") {
                importGame,
                importInput,
                importOutput,
            };
            import.SetAction(ctx => {
                RomCommands.Import(
                    ctx.GetValue(importGame),
                    ctx.GetValue(importInput),
                    ctx.GetValue(importOutput));
            });

            var importFontGame = new Option<string>("--game", "the path of the rom");
            var importFontFont = new Option<string>("--font", "the font to insert");
            var importFontOutput = new Option<string>("--output", "the output folder");
            var importFont = new Command("import-font", "Import the translated font to the Rom") {
                importFontGame,
                importFontFont,
                importFontOutput,
            };
            importFont.SetAction(ctx => {
                RomCommands.ImportFont(
                    ctx.GetValue(importFontGame),
                    ctx.GetValue(importFontFont),
                    ctx.GetValue(importFontOutput));
            });

            return new Command("game", "Import files to the Game") {
                import,
                importFont,
            };
        }

        private static Command CreateTextCommand()
        {
            var exportBin = new Option<string>("--bin", "the input .bin file");
            var exportOutput = new Option<string>("--output", "the output folder");
            var export = new Command("export", "Export .bin file to a .po file") {
                exportBin,
                exportOutput,
            };
            export.SetAction(ctx => {
                TextExportCommands.Export(
                    ctx.GetValue(exportBin),
                    ctx.GetValue(exportOutput));
            });

            var exportJQuizBin = new Option<string>("--bin", "the input jquiz.bin file");
            var exportJQuizOutput = new Option<string>("--output", "the output folder");
            var exportJQuiz = new Command("export-jquiz", "Export JQuiz .bin file to multiple .po files") {
                exportJQuizBin,
                exportJQuizOutput,
            };
            exportJQuiz.SetAction(ctx => {
                TextExportCommands.JQuizExport(
                    ctx.GetValue(exportJQuizBin),
                    ctx.GetValue(exportJQuizOutput));
            });

            var deckExportDirectory = new Option<string>("--directory", "the directory with the .bin files");
            var deckExportPdeck = new Option<bool>("--pdeck", "true for pdeck files");
            var deckExportOutput = new Option<string>("--output", "the output folder");
            var deckExport = new Command("export-deck", "Export deck/pdeck .bin files from a folder, to a single .po file") {
                deckExportDirectory,
                deckExportPdeck,
                deckExportOutput,
            };
            deckExport.SetAction(ctx => {
                TextExportCommands.DeckExport(
                    ctx.GetValue(deckExportDirectory),
                    ctx.GetValue(deckExportPdeck),
                    ctx.GetValue(deckExportOutput));
            });

            var batchExportDirectory = new Option<string>("--directory", "the input directory with the .bin files");
            var batchExportOutput = new Option<string>("--output", "the output folder");
            var batchExport = new Command("batch-export", "Batch export .bin files from a folder, to .po files") {
                batchExportDirectory,
                batchExportOutput,
            };
            batchExport.SetAction(ctx => {
                TextExportCommands.BatchExport(
                    ctx.GetValue(batchExportDirectory),
                    ctx.GetValue(batchExportOutput));
            });

            var importPo = new Option<string>("--po", "the input .po file");
            var importOutput = new Option<string>("--output", "the output folder");
            var import = new Command("import", "Import a .po file into a .bin") {
                importPo,
                importOutput,
            };
            import.SetAction(ctx => {
                TextImportCommands.Import(
                    ctx.GetValue(importPo),
                    ctx.GetValue(importOutput));
            });

            var deckImportPo = new Option<string>("--po", "the po file");
            var deckImportPdeck = new Option<bool>("--pdeck", "true for pdeck files");
            var deckImportOutput = new Option<string>("--output", "the output folder");
            var deckImport = new Command("import-deck", "Import a single po file, to multiple .bin files") {
                deckImportPo,
                deckImportPdeck,
                deckImportOutput,
            };
            deckImport.SetAction(ctx => {
                TextImportCommands.DeckImport(
                    ctx.GetValue(deckImportPo),
                    ctx.GetValue(deckImportPdeck),
                    ctx.GetValue(deckImportOutput));
            });

            var batchImportDirectory = new Option<string>("--directory", "the input directory with the .bin files");
            var batchImportOutput = new Option<string>("--output", "the output folder");
            var batchImport = new Command("batch-import", "Import .bin files from a folder, to a .po file") {
                batchImportDirectory,
                batchImportOutput,
            };
            batchImport.SetAction(ctx => {
                TextImportCommands.BatchImport(
                    ctx.GetValue(batchImportDirectory),
                    ctx.GetValue(batchImportOutput));
            });

            var importJQuizContainer = new Option<string>("--container", "the container of the .po files");
            var importJQuizOutput = new Option<string>("--output", "the output folder");
            var importJQuiz = new Command("import-jquiz", "Import the jquiz Po folder into a .bin") {
                importJQuizContainer,
                importJQuizOutput,
            };
            importJQuiz.SetAction(ctx => {
                TextImportCommands.ImportJQuiz(
                    ctx.GetValue(importJQuizContainer),
                    ctx.GetValue(importJQuizOutput));
            });

            return new Command("texts", "Export or import bin files to Po") {
                export,
                deckExport,
                exportJQuiz,
                batchExport,
                import,
                deckImport,
                batchImport,
                importJQuiz,
            };
        }
    }
}
