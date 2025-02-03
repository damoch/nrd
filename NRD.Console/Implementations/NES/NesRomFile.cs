using NRD.Console.Abstracts;

namespace NRD.Console.Implementations.NES
{
    internal class NesRomFile : IRomFile
    {
        private NesRomHeader _header;
        private byte[] _romData;
        private int _chrRomStart => 16 + _header.PGPRomSize;
        public int ImagesCount => _header.CHRRomSize * 8 * 1024 / _imageBlockSize;
        private int _imageBlockSize => 16;

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

        public byte[] GetImageData(int offset)
        {
            var start = _chrRomStart + offset * _imageBlockSize;
            return _romData.Skip(start).Take(_imageBlockSize).ToArray();
        }
    }
}
