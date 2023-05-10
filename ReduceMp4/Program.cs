using FFMpegCore;
using FFMpegCore.Enums;

Console.WriteLine("Reducing MP4 files to low-size youtube version");

string mp4InputDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/reducemp4";
string mp4OutputDirectory = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "/reducemp4/output";
Console.WriteLine($"Input directory : {mp4InputDirectory}");
Console.WriteLine($"Output directory: {mp4OutputDirectory}");
if (!Directory.Exists(mp4OutputDirectory))
{
    Directory.CreateDirectory(mp4OutputDirectory);
}

foreach (string mp4File in Directory.GetFiles(mp4InputDirectory).Where(name => name.EndsWith("mp4", StringComparison.OrdinalIgnoreCase)))
{
    var mp4fileWithoutExtension = Path.GetFileNameWithoutExtension(mp4File);
    Console.WriteLine("Convert: " + mp4fileWithoutExtension);
    var outputFile = Path.Combine(Path.GetDirectoryName(mp4File), "output", $"{mp4fileWithoutExtension}_converted.mp4");
    ConvertMp4ToWebVariant(mp4File, outputFile);
}

Console.WriteLine("End of conversion.");

void ConvertMp4ToWebVariant(string inputPath, string outputPath)
{
    FFMpegArguments
        .FromFileInput(inputPath)
        .OutputToFile(outputPath, false, options => options
            .WithVideoCodec(VideoCodec.LibX265)
            .WithConstantRateFactor(21)
            .WithAudioCodec(AudioCodec.Aac)
            .WithVariableBitrate(4)
            .WithVideoFilters(filterOptions => filterOptions
                .Scale(VideoSize.Ld))
            .WithFastStart())
        .ProcessSynchronously();
}
