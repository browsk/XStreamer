using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using XStreamer.Protocol.Message;

namespace XStreamer.Protocol
{
    public class MessageFactory
    {
        public static IMessage CreateFromData(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            if (data.Length < PacketData.HeaderSize)
                throw new ArgumentException("Not enough data", "data");

            IMessage message = null;

            PacketType type = (PacketType)data[PacketData.TypeOffset];

            switch(type)
            {
                case PacketType.Null:
                    message = new NullMessage();
                    break;
            }

            if (message != null)
                message.Decode(data);
            throw new SystemException();
        }

    }
}
