using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESNLib.Tools
{
    /// <summary>
    /// Adapter to write to a generic stream
    /// </summary>
    public class StreamLoggerAdaptater<T>
    {
        internal T? stream;

        public void SetStream(T Stream)
        {
            this.stream = Stream;
        }

        public StreamLoggerAdaptater()
        {
            stream = default;
        }

        public StreamLoggerAdaptater(T stream)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

            this.stream = stream;
        }

        public void WriteData(string data)
        {
            if (stream == null)
                throw new ArgumentNullException(nameof(stream));

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
