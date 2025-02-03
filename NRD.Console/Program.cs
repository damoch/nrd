namespace NRD.Console
{
    using NRD.Console.Implementations.NES;
    using System;
    using System.Drawing;
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
            Console.WriteLine($"TV System: {romFile.Header.TVSystem}");
            Console.WriteLine($"PRG ROM Size: {romFile.Header.PGPRomSize} x 16 KiB = {romFile.Header.PGPRomSize * 16} KiB");

            if (romFile.Header.UsesChrRam)
            {
                Console.WriteLine("This cart uses CHR RAM");
            }
            else
            {
                Console.WriteLine($"CHR ROM Size: {romFile.Header.CHRRomSize} x 8 KiB = {romFile.Header.CHRRomSize * 8} KiB");
            }

            var imageConverter = new NesImageConverter();

            for (int i = 0; i < romFile.ImagesCount; i++)
            {
                var imageData = romFile.GetImageData(i);
                var bitmap = imageConverter.ConvertToBitmap(imageData);
                using (var bitmapImage = new Bitmap(8, 8))
                {
                    for (var y = 0; y < 8; y++)
                    {
                        for (var x = 0; x < 8; x++)
                        {
                            var color = Color.FromArgb(bitmap[x][y]);
                            color = Color.FromArgb(color.R, color.G, color.B);
                            bitmapImage.SetPixel(x, y, color);
                        }
                    }
                    bitmapImage.Save($"sprite_{i}.png");
                }


            }

            Console.ReadKey();
        }
    }
}
