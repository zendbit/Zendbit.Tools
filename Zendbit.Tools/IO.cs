using System;
using System.IO;
using System.Threading.Tasks;

namespace Zendbit.Tools.IO
{
    /*
    * This class for file operation
    */
    public class FileOp
    {
        // read file as byte async
        async public Task<(bool Success, byte[] Result, string Message)> ReadBytesAsync(string path)
        {
            if (File.Exists(path))
            {
                try
                {
                    var buffStream = new BufferedStream(File.OpenRead(path));
                    var buffer = new byte[buffStream.Length];
                    await buffStream.ReadAsync(buffer, 0, buffer.Length, new System.Threading.CancellationToken());
                    buffStream.Close();
                    return (true, buffer, "OK.");
                }
                catch (Exception ex)
                {
                    return (false, null, ex.Message);
                }
            }

            return (false, null, string.Format("File {0} doesn't exist!.", path));
        }

        // read file async as text
        async public Task<(bool Success, string Result, string Message)> ReadTextAsync(string path)
        {
            var outData = await ReadBytesAsync(path);
            if (outData.Success)
                return (outData.Success, System.Text.Encoding.UTF8.GetString(outData.Result), outData.Message);
            return (outData.Success, string.Empty, outData.Message);
        }

        // read file as bytes
        public (bool Success, byte[] Result, string Message) ReadBytes(string path)
            => ReadBytesAsync(path).GetAwaiter().GetResult();

        // Read text from file
        public (bool Success, string Result, string Message) ReadText(string path)
            => ReadTextAsync(path).GetAwaiter().GetResult();
    }

    public class PathOp
    {
        // create path with delimiter :: will replaced depend on operating system
        // directory separator
        public string Create(string path)
            => path.Replace("::", Path.DirectorySeparatorChar.ToString());
    }
}
