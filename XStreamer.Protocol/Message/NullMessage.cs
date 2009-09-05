using System;

namespace XStreamer.Protocol.Message
{
    public class NullMessage : IMessage
    {
        private const int MessageLength = 9;

        /// <summary>
        /// Gets or sets the message id.
        /// </summary>
        /// <value>The message id.</value>
        public int MessageId { get; set; }

        public PacketType Type
        {
            get { return PacketType.Null; }
        }

        /// <summary>
        /// Decodes the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        public void Decode(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            if (data.Length < PacketData.HeaderSize)
                throw new InsufficientDataException();

            PacketType type = (PacketType) data[PacketData.TypeOffset];
            if (type != Type)
                throw new InvalidMessageTypeException(Type, type);

            MessageId = BitConverter.ToInt32(data, PacketData.IdOffset);
        }

        /// <summary>
        /// Return the message encoded as bytes
        /// </summary>
        /// <returns>
        /// An array of bytes containing the encoded message
        /// </returns>
        public byte[] AsBytes()
        {
            return PacketData.CreateData(PacketType.Null, MessageId, 0);
        }
    }
}
