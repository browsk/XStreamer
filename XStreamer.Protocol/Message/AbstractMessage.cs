using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace XStreamer.Protocol.Message
{
    public abstract class AbstractMessage :IMessage
    {
        private int _messageId;

        /// <summary>
        /// Gets or sets the message id.
        /// </summary>
        /// <value>The message id.</value>
        public int MessageId
        {
            get
            {
                if (_messageId == 0 || (uint)_messageId == 0xffffffff)
                    throw new InvalidMessageIdException();

                return _messageId;
            }
            set
            {
                if (value == 0 || (uint)value == 0xffffffff)
                    throw new InvalidMessageIdException();

                _messageId = value;
            }
        }

        /// <summary>
        /// Gets the message type.
        /// </summary>
        /// <value>The message type.</value>
        public abstract PacketType Type { get; }

        /// <summary>
        /// Decodes the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        public abstract void Decode(byte[] data);

        /// <summary>
        /// Return the message encoded as bytes
        /// </summary>
        /// <returns>
        /// An array of bytes containing the encoded message
        /// </returns>
        public abstract byte[] AsBytes();

        /// <summary>
        /// Decodes the header.
        /// </summary>
        /// <param name="data">The data.</param>
        protected void DecodeHeader(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException("data");

            if (data.Length < PacketData.HeaderSize)
                throw new InsufficientDataException();

            PacketType type = (PacketType)data[PacketData.TypeOffset];
            if (type != Type)
                throw new InvalidMessageTypeException(Type, type);

            MessageId = BitConverter.ToInt32(data, PacketData.IdOffset);
        }
    }
}
