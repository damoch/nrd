using NRD.Console.Abstracts;

namespace NRD.Console.Implementations.NES
{
    internal class NesImageConverter : IImageConverter
    {
        private int _rows = 8;
        private int _collumns = 8;

        private static readonly int[] _colorPalette = new int[]
{
    0x000000,  // 000000
    0xfcfcfc,  // fcfcfc
    0xf8f8f8,  // f8f8f8
    0xbcbcbc,  // bcbcbc
    0x7c7c7c,  // 7c7c7c
    0xa4e4fc,  // a4e4fc
    0x3cbcfc,  // 3cbcfc
    0x0078f8,  // 0078f8
    0x0000fc,  // 0000fc
    0xb8b8f8,  // b8b8f8
    0x6888fc,  // 6888fc
    0x0058f8,  // 0058f8
    0x0000bc,  // 0000bc
    0xd8b8f8,  // d8b8f8
    0x9878f8,  // 9878f8
    0x6844fc,  // 6844fc
    0x4428bc,  // 4428bc
    0xf8b8f8,  // f8b8f8
    0xf878f8,  // f878f8
    0xd800cc,  // d800cc
    0x940084,  // 940084
    0xf8a4c0,  // f8a4c0
    0xf85898,  // f85898
    0xe40058,  // e40058
    0xa80020,  // a80020
    0xf0d0b0,  // f0d0b0
    0xf87858,  // f87858
    0xf83800,  // f83800
    0xa81000,  // a81000
    0xfce0a8,  // fce0a8
    0xfca044,  // fca044
    0xe45c10,  // e45c10
    0x881400,  // 881400
    0xf8d878,  // f8d878
    0xf8b800,  // f8b800
    0xac7c00,  // ac7c00
    0x503000,  // 503000
    0xd8f878,  // d8f878
    0xb8f818,  // b8f818
    0x00b800,  // 00b800
    0x007800,  // 007800
    0xb8f8b8,  // b8f8b8
    0x58d854,  // 58d854
    0x00a800,  // 00a800
    0x006800,  // 006800
    0xb8f8d8,  // b8f8d8
    0x58f898,  // 58f898
    0x00a844,  // 00a844
    0x005800,  // 005800
    0x00fcfc,  // 00fcfc
    0x00e8d8,  // 00e8d8
    0x008888,  // 008888
    0x004058,  // 004058
    0xf8d8f8,  // f8d8f8
    0x787878   // 787878
};

        public int[][] ConvertToBitmap(byte[] data)
        {
            var bitmap = new int[_rows][];
            int nonZeros = 0;
            for (var i = 0; i < _rows; i++)
            {
                bitmap[i] = new int[_collumns];

                // Ensure plane2 is correctly accessed (usually after _rows, depending on the tile structure)
                var plane1 = data[i];
                var plane2 = data[i + _rows];  // Adjusted index for second plane

                for (var j = 0; j < _collumns; j++)
                {
                    var mask = 1 << (7 - j); // Masking from left to right (7 - j)

                    // Combine the bits from both planes (plane1 and plane2)
                    var color = (plane1 & mask) >> (7 - j) | ((plane2 & mask) >> (6 - j)); // Shift bits accordingly

                    // Ensure valid index for color lookup (0 to 3 for 2-bit color)
                    if (color < 0 || color >= _colorPalette.Length)
                    {
                        // Handle invalid color (optional, for safety)
                        color = 0;
                    }
                    if(color != 0)
                    {
                        nonZeros++;
                    }
                    bitmap[i][j] = _colorPalette[color];
                }
            }

            return bitmap;
        }
    }
}
