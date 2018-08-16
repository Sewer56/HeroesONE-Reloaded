# HeroesONE Reloaded

![Example Image](https://i.imgur.com/5CqbwQe.png)

HeroesONE Reloaded is a fully featured, from the ground up rewrite of the original [HeroesONE](https://github.com/sonicretro/HeroesONE) archiving utility used for the extraction and creation of .ONE files in both Sonic Heroes (2003/2004) and Shadow The Hedgehog (2005). 

To be precise; HeroesONE Reloaded consists specifically of a C# library that allows for parsing, saving + modification of Heroes and Shadow ONE formats and additionally an easy to use front-end GUI that makes use of said library allowing for those actions to be easily performed by and end user. 

Originally written due to a small frustration of both the lack of performance in the original HeroesONE and the lack of documentation of the .ONE archive formats. The new, from scarch implementation makes use of my own custom SEGA PRS Compressor/Decompressor; favourably faster than the FraGag port of fuzziqer's PRS implementation used in the original. 

# Usage

### End Users:
Simply download a pre-compiled copy of the utility from the Releases tab and use the executable provided.

### Developers:
The most useful classes/structs within this project include the following:

- `Archive` Provides you with a generic archive structure from which you can either load an archive into or build a Shadow 0.60/0.50 archive or a Sonic Heroes .ONE Archive. Probably everything you need.
- `ONEArchiveTester` which allows for guessing of the ONE Archive Type (Sonic Heroes archive or Shadow The Hedgehog Version 0.50/0,60 archive)
- `RWVersion` which provides an easy to use abstraction over a 4 byte RenderWare version signature allowing for the extraction and modification of the individual parts of the packed integer containing the RW Version.
- `Prs` provides an easy to use abstraction with additional adaptive search buffer support over my C# frontend for my D compression library.
- `ONEArchive` which both describes a Sonic Heroes ONE archive and is a parser of said archives as well as providing means of converting self to `Archive`
- `ONEShadowArchive` which is the same as `ONEArchive` but for Shadow The Hedgehog.

Most other classes either contain utilities or the individual structures (substructures) contained inside both Heroes and Shadow .ONE file structures.

# Notable Improvements

This list is non-exhaustive, i.e. it does not include all of the changes; just the few more important or useful ones.

### Faster PRS Compressor & Decompressor
HeroesONE Reloaded makes use of my own, from the ground up reversed and implemented solution for the compression and decompression of SEGA's PRS compression format written in D [dlang-prs](https://github.com/sewer56lol/dlang-prs).

This allows for an approximate 268.7% relative compression speed in X86 mode (in which the original HeroesONE was compiled) and 151.8% in X64 mode at the mere expense of approximately 101.7% relative compressed size.

### PRS Caching

The original HeroesONE utility that made use of the older PRS compressor has used a relatively *naive* approach of storing uncompressed files in memory, only compressing all of the files at the time of saving.

Said approach did not take into the account the relative computational complexity of compression versus decompression, whereby the decompression speed for a certain PRS stream/file is of magnitudes greater than compression.

HeroesONE Reloaded instead takes this from the other side, instead storing compressed files in memory, minimizing the amount of computationally expensive compressions; now only limited to occur every time when the user adds or replaces a file rather than <file count> times on each save operation.

The direct result? Instant saving of files without noticeable impact on the speed of extracting files, wasting significantly less time.

### Adaptive Search Buffer Size for Compression

One of the various improvements brought by dlang-prs available for use are the option of setting a custom search buffer size for data compression; allowing for the dynamic adjustment and balancing between compression ratio and speed.

By default, in order to even extract more speed and provide a feeling of instant file addition/replacement (where compression is performed); HeroesONE Reloaded will automatically pick a search buffer size depending on the size of data about to be compressed.

In other words, to ensure snappiness - HeroesONE Reloaded will automatically opt to use a smaller window, resulting in much lesser compression time but at the expense of the size of the final compressed file.

That said, this option may be disabled and a custom search buffer size specified.

Extra Resource: [dlang-prs benchmarks](https://github.com/sewer56lol/dlang-prs)
Manual search buffer size adjustment: ![Example](https://i.imgur.com/y4D9LyY.png)

### Windows Explorer File Associations

![Image](https://i.imgur.com/rl3IVMp.png)

Not very difficult to explain this one. HeroesONE Reloaded will automatically set for you a file type association such that you may simply double click any .ONE file from your explorer window to make it open in HeroesONE Reloaded.

### RenderWare Version Parsing and Manipulation

![RW Version Tag Changing](https://i.imgur.com/UvXxa5Q.png)

HeroesONE Reloaded is capable of modifying the RenderWare version that is written to the .ONE archive metadata both on an archive and per-file level as originally set in the original .ONE files and ignored by the original HeroesONE.

This will directly allow you to save your ONE Archives wih any arbitrary RenderWare version written in the metadata; allowing for your ONE Archives and contained within files to not be ignored by earlier versions of the game such as the E3 prototype.

### Shadow The Hedgehog Dummy Entry Checking

There were the very, very odd and very, very rare files (such as vehicleresource.one) that the original HeroesONE could not open simply because the late/last file entries of the file metadata section have been dummied out in the .ONE archives. Well, this has been fixed.
