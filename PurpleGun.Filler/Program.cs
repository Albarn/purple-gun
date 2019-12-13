using System;
using System.Configuration;
using System.IO;
using System.Text;

namespace PurpleGun.Filler
{
    class Program
    {
        private static readonly int FileCapacity;
        private static readonly int MaxValue;
        private static readonly int LineSize;

        static Program()
        {
            FileCapacity = int.Parse(ConfigurationManager.AppSettings[nameof(FileCapacity)]);
            MaxValue = int.Parse(ConfigurationManager.AppSettings[nameof(MaxValue)]);
            LineSize = int.Parse(ConfigurationManager.AppSettings[nameof(LineSize)]);
        }

        static void Main(string[] args)
        {
            if (args.Length < 1)
            {
                Console.WriteLine("usage: <destination path>");
                return;
            }

            var path = args[0];

            var f = new Random();
            var line = GetBytes(f.Next(MaxValue));
            using (var writer = File.Create(path))
            {
                while (writer.Position < FileCapacity)
                {
                    writer.Write(line, 0, line.Length);
                    line = GetBytes(f.Next(MaxValue));
                }
            }
        }

        private static byte[] GetBytes(int num)
        {
            num %= MaxValue;
            return Encoding.ASCII.GetBytes(num.ToString().PadLeft(LineSize) + '\n');
        }
    }
}
