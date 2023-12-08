// See https://aka.ms/new-console-template for more information

using System.CommandLine;
using System.CommandLine.NamingConventionBinder;
using System.Diagnostics;
using System.Runtime.InteropServices;
using HeroesONE_R.Structures;
using HeroesONE_R.Structures.Common;
using HeroesONE_R.Structures.Substructures;
using Spectre.Console;

// Extract Command
var extractCommand = new Command("extract", "Extract files from an archive.")
{
    new Option<string>("--source", "Source archive to extract from.") { IsRequired = true },
    new Option<string>("--target", "Target location to extract to.") { IsRequired = true }
};

extractCommand.Handler = CommandHandler.Create<string, string>(Extract);

// Repack Command
var repackCommand = new Command("repack", "Re-packs files, replacing an existing archive.")
{
    new Option<string>("--source", "[Required] Source folder to pack files from.") { IsRequired = true },
    new Option<string>("--target", "[Required] Location of existing archive file.") { IsRequired = true },
};

repackCommand.Handler = CommandHandler.Create<string, string>(Repack);

// Batch Extract
var batchExtractCommand = new Command("batch-extract", "Batch extract files from multiple archives.")
{
    new Option<string>("--file", "Text file with source and target paths for extraction. Source and target should be separated by '|'") { IsRequired = true }
};

// Batch Repack
batchExtractCommand.Handler = CommandHandler.Create<string>(BatchExtract);

var batchRepackCommand = new Command("batch-repack", "Batch repack files into multiple archives.")
{
    new Option<string>("--file", "Text file with source and target paths for repacking. Source and target should be separated by '|'") { IsRequired = true }
};

batchRepackCommand.Handler = CommandHandler.Create<string>(BatchRepack);

// Root command
var rootCommand = new RootCommand
{
    extractCommand,
    repackCommand,
    batchExtractCommand,
    batchRepackCommand
};

// Parse the incoming args and invoke the handler 
rootCommand.Invoke(args);

void BatchExtract(string file)
{
    var lines = File.ReadAllLines(file);

    AnsiConsole.Progress()
        .Columns(new ProgressBarColumn(), new PercentageColumn())
        .Start(ctx =>
        {
            foreach (var line in lines)
            {
                var parts = line.Split('|', 2);
                if (parts.Length != 2) 
                    continue;

                var source = parts[0];
                var target = parts[1];
                var task = ctx.AddTask($"[green]Extracting {source}[/]", new ProgressTaskSettings { MaxValue = 100 });
                
                ExtractImpl(source, target, new Progress<double>(p => task.Value = p * 100));
            }
        });
}

void BatchRepack(string file)
{
    var lines = File.ReadAllLines(file);

    AnsiConsole.Progress()
        .Columns(new ProgressBarColumn(), new PercentageColumn())
        .Start(ctx =>
        {
            foreach (var line in lines)
            {
                var parts = line.Split('|', 2);
                if (parts.Length != 2) 
                    continue;

                var source = parts[0];
                var target = parts[1];
                var task = ctx.AddTask($"[green]Repacking {source}[/]", new ProgressTaskSettings { MaxValue = 100 });

                RepackImpl(source, target, new Progress<double>(p => task.Value = p * 100));
            }
        });
}

void Extract(string source, string target)
{
    Console.WriteLine($"Extracting {source} to {target}.");
    var unpackingTimeTaken = Stopwatch.StartNew();

    AnsiConsole.Progress()
        .Columns(new ProgressBarColumn(), new PercentageColumn())
        .Start(ctx =>
        {
            var packTask = ctx.AddTask("[green]Unpacking Files[/]");
            var progress = new Progress<double>(p => packTask.Value = p * 100);
            ExtractImpl(source, target, progress);
        });

    Console.WriteLine("Unpacked in {0}ms", unpackingTimeTaken.ElapsedMilliseconds);
}

void ExtractImpl(string source, string target, IProgress<double> progress)
{
    var archive = Archive.FromONEFile(File.ReadAllBytes(source));
    Directory.CreateDirectory(target);

    var totalFiles = archive.Files.Count;
    var extractedFiles = 0;

    foreach (var file in archive.Files)
    {
        file.WriteToFile(Path.Combine(target, file.Name));
        Interlocked.Increment(ref extractedFiles);
        progress.Report((double)extractedFiles / totalFiles);
    }
}

void Repack(string source, string target)
{
    Console.WriteLine($"Repacking {source} to {target}.");
    var packingTimeTaken = Stopwatch.StartNew();

    AnsiConsole.Progress()
        .Columns(new ProgressBarColumn(), new PercentageColumn())
        .Start(ctx =>
        {
            var packTask = ctx.AddTask("[green]Packing Files[/]");
            var progress = new Progress<double>(p => packTask.Value = p * 100);
            RepackImpl(source, target, progress);
        });

    var ms = packingTimeTaken.ElapsedMilliseconds;
    Console.WriteLine("Packed in {0}ms", ms);
    Console.WriteLine("Size {0} Bytes", new FileInfo(target).Length);
}

void RepackImpl(string source, string target, IProgress<double> progress)
{
    ONEArchiveType origArchiveType;
    using (var origFs = new FileStream(target, FileMode.Open))
    {
        var header = GC.AllocateUninitializedArray<byte>(16);
        origFs.ReadExactly(header);
        origArchiveType = ONEArchiveTester.GetArchiveType(ref header);
    }

    var newArchive = new Archive(CommonRWVersions.HeroesPreE3)
    {
        OriginalArchiveType = origArchiveType
    };
    var files = Directory.GetFiles(source);
    var totalFiles = files.Length;
    var processedFiles = 0;

    foreach (var file in files)
    {
        var archiveFile = new ArchiveFile(file);
        lock (newArchive)
            newArchive.Files.Add(archiveFile);

        Interlocked.Increment(ref processedFiles);
        progress.Report((double)processedFiles / totalFiles);
    }

    var data = CollectionsMarshal.AsSpan(newArchive.BuildArchiveWithOriginalType());
    using var fs = new FileStream(target, FileMode.Create);
    fs.Write(data);
}