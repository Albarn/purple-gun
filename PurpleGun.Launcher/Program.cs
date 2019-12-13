using System;
using System.IO;
using System.IO.MemoryMappedFiles;

namespace PurpleGun.Launcher
{
    class Program
    {
        private const int FileCapacity = 16 * 1024 * 1024;

        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("usage: <destination path> <map name>");
                return;
            }

            using (var map = MemoryMappedFile.CreateFromFile(args[0], FileMode.OpenOrCreate, args[1], FileCapacity))
            {
                Console.WriteLine("File map created. Press Enter to finish.");
                Console.ReadLine();
            }
        }
    }
}
