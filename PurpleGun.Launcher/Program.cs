using System;
using System.Configuration;
using System.IO;
using System.IO.MemoryMappedFiles;

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

            using (var map = MemoryMappedFile.CreateFromFile(args[0], FileMode.OpenOrCreate, args[1], FileCapacity))
            {
                Console.Write("File map created. Press Enter to finish.");
                Console.ReadLine();
            }
        }
    }
}
