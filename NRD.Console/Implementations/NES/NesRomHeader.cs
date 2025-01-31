using NRD.Console.Abstracts;

namespace NRD.Console.Implementations.NES
{
    internal class NesRomHeader : IRomHeader
    {
        private byte[] _bytes;

        public int PGPRomSize => _bytes[4];
        public int PGRamSize => _bytes[8];
        public int CHRRomSize => _bytes[5];
        public bool UsesChrRam => _bytes[5] == 0;
        public bool IsInesFormat => _bytes[0] == 'N' && _bytes[1] == 'E' && _bytes[2] == 'S' && _bytes[3] == 0x1A;
        public bool IsNes20Format => IsInesFormat && (_bytes[7] & 0x0C) == 0x08;
        public string TVSystem => (_bytes[9] & 0b0000001) == 0 ? "PAL" : "NTSC";//Flag 9 bit 0 hex 0x01

        public void LoadBytes(byte[] bytes)
        {
            _bytes = bytes;
        }
    }
}
