using System;
using System.Configuration;
using System.IO;
using System.IO.MemoryMappedFiles;
using WhiteFang.Diagnostics;

namespace PurpleGun.Launcher
{
    class Program
    {
        private static readonly int FileCapacity;

        static Program()
        {
            FileCapacity = int.Parse(ConfigurationManager.AppSettings[nameof(FileCapacity)]);
        }

        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("usage: <destination path> <map name>");
                return;
            }
            var beginInfo = new PerfomanceSnapshot();
            var endInfo = new PerfomanceSnapshot();
            beginInfo.Update();
            Console.WriteLine("started:\n" + beginInfo);
            using (var map = MemoryMappedFile.CreateFromFile(args[0], FileMode.Open, args[1], FileCapacity))
            {
                Console.Write("File map created. Press Enter to finish.");
                Console.ReadLine();
            }
            endInfo.Update();
            Console.WriteLine("finished:\n" + endInfo);
            Console.WriteLine("difference:\n" + endInfo.Difference(beginInfo));
            Console.ReadLine();
        }
    }
}
