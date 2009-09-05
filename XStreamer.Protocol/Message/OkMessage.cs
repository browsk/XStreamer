using System;

namespace XStreamer.Protocol.Message
{
    class OkMessage : IMessage
    {
        private int _messageId;

        private const int Length = 9;

        public OkMessage(int messageId)
        {
            _messageId = messageId;    
        }

        public int MessageId
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        public PacketType Type
        {
            get { throw new NotImplementedException(); }
        }

        public void Decode(byte[] data)
        {
            throw new NotImplementedException();
        }

        public byte[] AsBytes()
        {
            return PacketData.CreateData(PacketType.Ok, _messageId, 0);
        }
    }
}
