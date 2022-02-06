using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESNLib.Tools
{
    public class StreamLogger<T>
    {
        private T stream;

        public StreamLogger(T stream)
        {
            this.stream = stream;
        }

        public void WriteData(string data)
        {
            Type streamType = stream.GetType();

            if (streamType.IsSubclassOf(typeof(Stream)))
            {
                byte[] bytes = Encoding.Default.GetBytes(data + Environment.NewLine);
                (stream as Stream).Write(bytes, 0, bytes.Length);
            }
            else if (streamType.IsSubclassOf(typeof(StreamWriter)))
            {
                (stream as StreamWriter).WriteLine(data);
            }
            else if (streamType.IsSubclassOf(typeof(TextWriter)))
            {
                (stream as TextWriter).WriteLine(data);
            }
            else
            {
                throw new ArgumentException("Unsupported stream type : " + streamType);
            }
        }
    }
}
