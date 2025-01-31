using NRD.Console.Abstracts;

namespace NRD.Console.Implementations.NES
{
    internal class NesRomFile : IRomFile
    {
        private NesRomHeader _header;
        private byte[] _romData;

        public NesRomHeader Header => _header;

        public NesRomFile()
        {
            _header = new NesRomHeader();
        }

        public void LoadBytes(byte[] bytes)
        {
            _header.LoadBytes(bytes.Take(16).ToArray());
            _romData = bytes.Skip(16).ToArray();
        }
    }
}
