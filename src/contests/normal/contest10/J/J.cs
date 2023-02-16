using System.IO;

internal class BinaryFileReader
{
    private readonly string _path;
    public BinaryFileReader(string path)
    {
        _path = path;
    }

    private short GetInt16(BinaryReader read)
    {
        short sum = 0;
        while (read.BaseStream.Position != read.BaseStream.Length)
        {
            sum += read.ReadInt16();
        }

        return sum;
    }

    private int GetInt32(BinaryReader read)
    {
        int sum = 0;
        while (read.BaseStream.Position != read.BaseStream.Length)
        {
            sum += read.ReadInt32();
        }

        return sum;
    }
    
    public int GetDifference()
    {
        using var retard = new FileStream(_path, FileMode.Open, FileAccess.Read);
        using var sr = new BinaryReader(retard);
        var result = GetInt16(sr);
        retard.Seek(0, SeekOrigin.Begin);

        var result32 = GetInt32(sr);

        return result32 - result;
    }
}