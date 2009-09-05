using XStreamer.Protocol.Message;
using Xunit;

namespace XStreamer.Command.Test
{
    public class CommandFactoryTest
    {
        [Fact]
        public void Test_Command_Factory_Can_Find_Command_For_NullMessage()
        {
            var command = CommandFactory.CreateCommand<NullMessage>();

            Assert.NotNull(command);
            Assert.IsType<NullCommand>(command);
        }
    }
}
