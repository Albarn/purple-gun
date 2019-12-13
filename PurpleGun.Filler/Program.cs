using System;
using System.IO;
using System.Text;

namespace PurpleGun.Filler
{
    class Program
    {
        private const int MaxValue = 100;

        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("usage: <path> <size>");
                return;
            }

            var path = args[0];
            int.TryParse(args[1], out var size);

            var f = new Random();
            var line = GetBytes(f.Next(MaxValue));
            using (var writer = File.Create(path))
            {
                while (writer.Position < size)
                {
                    writer.Write(line, 0, line.Length);
                    line = GetBytes(f.Next(MaxValue));
                }
            }
        }

        private static byte[] GetBytes(int num)
        {
            return Encoding.ASCII.GetBytes(num.ToString().PadLeft(3) + '\n');
        }
    }
}
