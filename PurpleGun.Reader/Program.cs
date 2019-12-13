using System;
using System.IO;
using System.IO.MemoryMappedFiles;
using System.Text;

namespace PurpleGun.Reader
{
    class Program
    {
        private const int FileCapacity = 16 * 1024 * 1024;
        private const int MaxValue = 100;

        static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                Console.WriteLine("usage: <map name> <source path>");
                return;
            }

            using (var mmf = MemoryMappedFile.CreateOrOpen(args[0], FileCapacity))
            using (var accessor = mmf.CreateViewAccessor(0, FileCapacity))
            using (var reader = new StreamReader(File.Open(args[1], FileMode.Open)))
            {
                var f = new Random();
                var line = GetBytes(0);
                var step = sizeof(byte) * line.Length;
                for (long i = 0; i < reader.BaseStream.Length; i += step)
                {
                    var num = int.Parse(reader.ReadLine());
                    accessor.ReadArray(i, line, 0, line.Length);
                    var currentNum = GetInteger(line);
                    line = GetBytes(num);

                    //write minimum
                    if (currentNum > num)
                    {
                        accessor.WriteArray(i, line, 0, line.Length);
                    }
                }
            }
        }

        private static byte[] GetBytes(int num)
        {
            return Encoding.ASCII.GetBytes(num.ToString().PadLeft(3) + '\n');
        }

        private static int GetInteger(byte[] line)
        {
            try
            {
                return int.Parse(Encoding.ASCII.GetString(line));
            }
            catch
            {
                return MaxValue;
            }
        }
    }
}
