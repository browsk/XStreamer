using System.IO;
using XStreamer.Protocol.Message;

namespace XStreamer.Command
{
    public interface ICommand<TForMessage>
    {
        /// <summary>
        /// Executes the implemented command
        /// </summary>
        /// <param name="incomingMessage">The incoming message.</param>
        /// <param name="stream">The stream to be used for writing the response.</param>
        void Execute(TForMessage incomingMessage, Stream stream);
    }
}
