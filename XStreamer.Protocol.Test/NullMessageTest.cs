using System;
using XStreamer.Protocol.Message;
using Xunit;

namespace XStreamer.Protocol.Test
{
    public class NullMessageTest
    {
        [Fact]
        public void Test_Can_Decode_Valid_Packet()
        {
            int id = 25;
            byte[] data = new byte[] { 5, 0, 0, 0, (byte)PacketType.Null, (byte)id, 0, 0, 0 };

            NullMessage message = new NullMessage();
            message.Decode(data);

            Assert.Equal(id, message.MessageId);
            Assert.Equal(PacketType.Null, message.Type);
        }

        [Fact]
        public void Test_Decode_With_Short_Packet_Throws_Exception()
        {
            int id = 25;
            byte[] data = new byte[] { 5, 0, 0, 0, (byte)PacketType.Null, (byte)id, 0, 0 };

            NullMessage message = new NullMessage();

            Assert.Throws<InsufficientDataException>(() => message.Decode(data));
        }

        [Fact]
        public void Test_Decode_With_Incorrect_Packet_Type_Throws_Exception()
        {
            int id = 212;
            byte[] data = new byte[] { 5, 0, 0, 0, (byte)PacketType.Ok, (byte)id, 0, 0, 0 };

            NullMessage message = new NullMessage();

            Assert.Throws<InvalidMessageTypeException>(() => message.Decode(data));
        }

        [Fact]
        public void Test_Decode_With_Null_data_Throws_Exception()
        {
            NullMessage message = new NullMessage();

            Assert.Throws<ArgumentNullException>(() => message.Decode(null));
        }
    }
}
