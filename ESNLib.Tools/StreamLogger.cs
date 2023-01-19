using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESNLib.Tools
{
    /// <summary>
    /// Adapter for stream logging. <b>Do not use this class directly.</b> Use <see cref="LoggerStream{T}"/> instead
    /// </summary>
    /// <typeparam name="T">Stream, StreamWriter or TextWriter child</typeparam>
    public class StreamLogger<T>
    {
        /// <summary>
        /// The stream where the logs will be written
        /// </summary>
        public T StreamOutput { get; set; }

        /// <summary>
        /// Create an instance of the <see cref="StreamLogger{T}"/>
        /// </summary>
        public StreamLogger()
        {

        }

        /// <summary>
        /// Create an instance of the <see cref="StreamLogger{T}"/> with a specified stream
        /// </summary>
        public StreamLogger(T stream)
        {
            this.StreamOutput = stream;
        }

        /// <summary>
        /// Indicate if the specified type is supported by the class
        /// </summary>
        public static bool IsTypeSupported(Type type)
        {
            return type.IsSubclassOf(typeof(Stream))
                || type.IsSubclassOf(typeof(TextWriter))
                || type.IsSubclassOf(typeof(StreamWriter));
        }

        /// <summary>
        /// Write data to the stream
        /// </summary>
        /// <param name="data"></param>
        /// <exception cref="ArgumentNullException">StreamOutput must not be null</exception>
        /// <exception cref="InvalidOperationException">Stream type not supported. Use 'Stream', 'StreamWriter' or 'TextWriter'</exception>
        public void WriteData(string data)
        {
            if (StreamOutput == null)
                throw new ArgumentNullException(nameof(StreamOutput));

            Type streamType = StreamOutput.GetType();

            // Note : Newline is already in the data string
            if (streamType.IsSubclassOf(typeof(Stream)))
            {
                byte[] bytes = Encoding.Default.GetBytes(data);
                (StreamOutput as Stream).Write(bytes, 0, bytes.Length);
            }
            else if (streamType.IsSubclassOf(typeof(StreamWriter)))
            {
                (StreamOutput as StreamWriter).Write(data);
            }
            else if (streamType.IsSubclassOf(typeof(TextWriter)))
            {
                (StreamOutput as TextWriter).Write(data);
            }
            else
            {
                throw new InvalidOperationException("Unsupported stream type : " + streamType);
            }
        }
    }
}
