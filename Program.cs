using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FFMpegCore;
using FFMpegCore.Enums;

namespace VideoMinify
{
    class Program
    {
        public static Dictionary<Task,ConversionStatusText> Tasks = new Dictionary<Task, ConversionStatusText>();
        static async Task Main(string[] args)
        {
            
            int counter = Console.CursorTop + 1;
            foreach (var item in args)
            {
                if (item.IsValidVideoPath())
                {
                    Tasks.Add(ConvertVideoAsync(item), new ConversionStatusText(counter,item));
                    counter++;
                }
                else
                {
                    Console.WriteLine($"File {Path.GetFileName(item)} is not a video file... skipping!");
                }
            }
            
            if (Tasks.Count == 0)
            {
                Console.WriteLine("Nothing was converted because no supported file was given!");
                Console.ReadKey();
                return;
            }
            while (!Tasks.Keys.All(x => x.IsCompleted))
            {
                foreach (var (key, value) in Tasks)
                {
                    value.Update(key.IsCompleted);
                }
                await Task.Delay(50);
            }
            Console.WriteLine("All Tasks completed!");
            Console.ReadKey();
            }

        public static async Task ConvertVideoAsync(string path)
        {
            var stopwatch = new Stopwatch();
            var newFileName = Path.GetFileNameWithoutExtension(path);
            newFileName += "_minified.mp4";
            var directory = Path.GetDirectoryName(path);
            var outputPath = directory + Path.DirectorySeparatorChar + newFileName;
            try
            {
                Console.WriteLine($"Starting conversion of file {Path.GetFileName(path)}");
                stopwatch.Start();
                await FFMpegArguments.FromInputFiles(path)
                    .UsingMultithreading(true)
                    .WithVideoCodec(VideoCodec.LibX264)
                    .WithConstantRateFactor(28)
                    .OutputToFile(outputPath)
                    .ProcessAsynchronously();
                stopwatch.Stop();
                Console.WriteLine($"Done with file {Path.GetFileName(path)} in {stopwatch.Elapsed}, output file is {newFileName}");
            }
            catch (Exception e)
            {
                stopwatch.Stop();
                Console.WriteLine($"ERROR: {path} not processed: {e}");
            }
        }
    }
}
