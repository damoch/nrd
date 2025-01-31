namespace NRD.Console
{
    using NRD.Console.Implementations.NES;
    using System;
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(args[0]);

            var romFile = new NesRomFile();
            using(var fileStream = System.IO.File.OpenRead(args[0]))
            {
                var bytes = new byte[fileStream.Length];
                fileStream.Read(bytes, 0, bytes.Length);
                romFile.LoadBytes(bytes);
            }

            Console.WriteLine($"Is iNES Format: {romFile.Header.IsInesFormat}");
            Console.WriteLine($"Is NES 2.0 Format: {romFile.Header.IsNes20Format}");

            Console.WriteLine($"PRG ROM Size: {romFile.Header.PGPRomSize} x 16 KiB = {romFile.Header.PGPRomSize * 16}");

            if (romFile.Header.UsesChrRam)
            {
                Console.WriteLine("This cart uses CHR RAM");
            }
            else
            {
                Console.WriteLine($"CHR ROM Size: {romFile.Header.CHRRomSize} x 8 KiB = {romFile.Header.CHRRomSize * 8}");
            }

            Console.ReadKey();
        }
    }
}
