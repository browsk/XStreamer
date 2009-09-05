using System.IO;
using XStreamer.Protocol.Message;
using Xunit;

namespace XStreamer.Command.Test
{
    public class NullCommandTest
    {
        [Fact]
        public void Test_Command_Writes_Ok_Message()
        {
            var nullMessage = new NullMessage();
            nullMessage.MessageId = 43;

            var command = new NullCommand();

            MemoryStream stream = new MemoryStream();

            command.Execute(nullMessage, stream);

            var sentMessage = new OkMessage();

            sentMessage.Decode(stream.GetBuffer());

            Assert.Equal(nullMessage.MessageId, sentMessage.MessageId);
        }
    }
}
