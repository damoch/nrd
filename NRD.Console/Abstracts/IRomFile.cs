namespace NRD.Console.Abstracts
{
    internal interface IRomFile
    {
        //IRomHeader Header { get; }
        void LoadBytes(byte[] bytes);
    }
}
