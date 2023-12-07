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

// Root command
var rootCommand = new RootCommand
{
    extractCommand,
    repackCommand,
};

// Parse the incoming args and invoke the handler 
rootCommand.Invoke(args);

void Extract(string source, string target)
{
    Console.WriteLine($"Extracting {source} to {target}.");
    var initializeTimeTaken = Stopwatch.StartNew();
    var archive = Archive.FromONEFile(File.ReadAllBytes(source));
    Console.WriteLine("Initialized in {0}ms", initializeTimeTaken.ElapsedMilliseconds);

    Directory.CreateDirectory(target);
    
    var totalFiles = archive.Files.Count;
    var extractedFiles = 0;
    var unpackingTimeTaken = Stopwatch.StartNew();
    AnsiConsole.Progress()
        .Columns(new ProgressBarColumn(), new PercentageColumn())
        .Start(ctx => 
        {
            var packTask = ctx.AddTask("[green]Unpacking Files[/]", new ProgressTaskSettings { MaxValue = totalFiles });
            Parallel.ForEach(archive.Files, file =>
            {
                file.WriteToFile(Path.Combine(target, file.Name));
                Interlocked.Increment(ref extractedFiles);
                packTask.Increment(1);
            });
        });
    
    Console.WriteLine("Unpacked in {0}ms", unpackingTimeTaken.ElapsedMilliseconds);
}

void Repack(string source, string target)
{
    Console.WriteLine($"Repacking {source} to {target}.");
    Prs.ADAPTIVE_SEARCH_BUFFER = false;
    
    var initializeTimeTaken = Stopwatch.StartNew();
    ONEArchiveType origArchiveType;
    using (var origFs = new FileStream(target, FileMode.Open))
    {
        var header = GC.AllocateUninitializedArray<byte>(16);
        origFs.ReadExactly(header);
        origArchiveType = ONEArchiveTester.GetArchiveType(ref header);
    }
    
    Console.WriteLine("Initialized in {0}ms", initializeTimeTaken.ElapsedMilliseconds);

    // Games (at least Heroes) don't care about RW version as long as it's lower or equal to one built with game.
    var newArchive = new Archive(CommonRWVersions.HeroesPreE3)
    {
        OriginalArchiveType = origArchiveType
    };
    var files = Directory.GetFiles(source);
    var totalFiles = files.Length;
    var processedFiles = 0;

    // Progress Reporting.
    var packingTimeTaken = Stopwatch.StartNew();
    AnsiConsole.Progress()
        .Columns(new ProgressBarColumn(), new PercentageColumn())
        .Start(ctx => 
        {
            var packTask = ctx.AddTask("[green]Packing Files[/]", new ProgressTaskSettings
            {
                MaxValue = totalFiles
            });

            Parallel.ForEach(files, file =>
            {
                var archiveFile = new ArchiveFile(file); // Make sure this is thread-safe
                lock (newArchive)
                    newArchive.Files.Add(archiveFile);

                Interlocked.Increment(ref processedFiles);
                packTask.Increment(1);
            });
        });

    var data = CollectionsMarshal.AsSpan(newArchive.BuildArchiveWithOriginalType());
    using var fs = new FileStream(target, FileMode.Create);
    fs.Write(data);

    var ms = packingTimeTaken.ElapsedMilliseconds;
    Console.WriteLine("Packed in {0}ms", ms);
    Console.WriteLine("Size {0} Bytes", data.Length);
}