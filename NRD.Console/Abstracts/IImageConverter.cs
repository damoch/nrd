namespace NRD.Console.Abstracts
{
    internal interface IImageConverter
    {
        int[][] ConvertToBitmap(byte[] data);
    }
}
