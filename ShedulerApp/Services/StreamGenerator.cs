namespace ShedulerApp.Services
{
    public class StreamGenerator
    {
        public Stream GenerateStreamFromString(String s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
    }
}
