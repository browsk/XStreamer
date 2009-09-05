using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XStreamer.Protocol.Message;
using Xunit;

namespace XStreamer.Protocol.Test
{
    public class OkMessageTest
    {
        [Fact]
        public void Test_Constructor()
        {
            int id = 3819;
            OkMessage message = new OkMessage(id);

            Assert.Equal(id, message.MessageId);
            Assert.Equal(PacketType.Ok, message.Type);
        }

        [Fact]
        public void Test_AsBytes()
        {
            int id = 3819;
            OkMessage message = new OkMessage(id);

            byte[] data = message.AsBytes();

            Assert.Equal(5, BitConverter.ToInt32(data, 0));
            Assert.Equal(id, BitConverter.ToInt32(data, PacketData.IdOffset));
            Assert.Equal(PacketType.Ok, (PacketType)data[PacketData.TypeOffset]);
        }
    }
}
