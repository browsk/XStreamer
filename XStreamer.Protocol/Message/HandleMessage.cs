using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XStreamer.Protocol.Message
{
    public class HandleMessage : AbstractMessage
    {
        #region Overrides of AbstractMessage

        /// <summary>
        /// Gets the message type.
        /// </summary>
        /// <value>The message type.</value>
        public override PacketType Type
        {
            get { return PacketType.Handle; }
        }

        /// <summary>
        /// Decodes the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        public override void Decode(byte[] data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Return the message encoded as bytes
        /// </summary>
        /// <returns>
        /// An array of bytes containing the encoded message
        /// </returns>
        public override byte[] AsBytes()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
