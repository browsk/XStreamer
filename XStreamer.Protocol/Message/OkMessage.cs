using System;

namespace XStreamer.Protocol.Message
{
    public class OkMessage : IMessage
    {
        public OkMessage(int messageId)
        {
            MessageId = messageId;    
        }

        /// <summary>
        /// Gets or sets the message id.
        /// </summary>
        /// <value>The message id.</value>
        public int MessageId
        {
            get; set;
        }

        /// <summary>
        /// Gets the message type.
        /// </summary>
        /// <value>The message type.</value>
        public PacketType Type
        {
            get { return PacketType.Ok; }
        }

        /// <summary>
        /// Decodes the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        public void Decode(byte[] data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return the message encoded as bytes
        /// </summary>
        /// <returns>
        /// An array of bytes containing the encoded message
        /// </returns>
        public byte[] AsBytes()
        {
            return PacketData.CreateData(PacketType.Ok, MessageId, 0);
        }
    }
}
