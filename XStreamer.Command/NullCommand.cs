using System.Diagnostics;
using System.IO;
using XStreamer.Protocol.Message;

namespace XStreamer.Command
{
    public class NullCommand : ICommand<NullMessage>
    {
        public void Execute(NullMessage incomingMessage, Stream stream)
        {
            byte[] data = (new OkMessage(incomingMessage.MessageId)).AsBytes();
            stream.Write(data, 0, data.Length);
        }
    }
}
