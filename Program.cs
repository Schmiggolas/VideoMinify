using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FFMpegCore;
using FFMpegCore.Enums;
using Newtonsoft.Json;

namespace VideoMinify
{
    class Program
    {
        public static Dictionary<Task, ConversionStatusText> Tasks = new Dictionary<Task, ConversionStatusText>();

        private static Config config;

        private static CancellationTokenSource _cancellationTokenSource;

        static async Task Main(string[] args)
        {
            config = ReadConfig();
            _cancellationTokenSource = new CancellationTokenSource();

            AppDomain.CurrentDomain.ProcessExit +=
                (_, _) => _cancellationTokenSource.Cancel();

            int counter = Console.CursorTop + 1;
            foreach (var item in args)
            {
                if (item.IsValidVideoPath())
                {
                    Tasks.Add(ConvertVideoAsync(item), new ConversionStatusText(counter, item));
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
            Console.WriteLine("Press any key or close this window to exit!");
            Console.ReadKey();
        }

        private static Config ReadConfig()
        {
            var path = AppContext.BaseDirectory + "config.json";
            if (File.Exists(path))
            {
                Console.WriteLine("config.json found!");
                Config? conf = null;
                try
                {
                    conf = JsonConvert.DeserializeObject<Config>(File.ReadAllText(path));
                    if (conf != null)
                    {
                        Console.WriteLine("Config loaded!");
                        return conf;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error while parsing config! \r\n" + e.Message);
                    Console.WriteLine("Falling back to defaults!");
                    return new Config();
                }
            }

            Console.WriteLine("No config found. Falling back to defaults!");
            return new Config();
        }


        private static async Task ConvertVideoAsync(string path)
        {
            var stopwatch = new Stopwatch();
            var newFileName = Path.GetFileNameWithoutExtension(path);
            newFileName += config.Suffix + ".mp4";
            var directory = Path.GetDirectoryName(path);
            var outputPath = directory + Path.DirectorySeparatorChar + newFileName;
            try
            {
                stopwatch.Start();
                await FFMpegArguments.FromFileInput(path)
                    .OutputToFile(outputPath, config.OverwriteExisting, options => options
                        .UsingMultithreading(config.UseMultithreading)
                        .WithVideoCodec(VideoCodec.LibX264)
                        .WithConstantRateFactor(config.ConstantRateFactor))
                    .CancellableThrough(_cancellationTokenSource.Token)
                    .ProcessAsynchronously();
                stopwatch.Stop();
                Console.WriteLine(
                    $"Done with file {Path.GetFileName(path)} in {stopwatch.Elapsed}, output file is {newFileName}");
            }
            catch (Exception e)
            {
                stopwatch.Stop();
                Console.WriteLine($"ERROR: {path} not processed: {e}");
            }
        }
    }
}