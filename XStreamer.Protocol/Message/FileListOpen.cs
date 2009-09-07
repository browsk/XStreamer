namespace XStreamer.Protocol.Message
{
    public class FileListOpen : AbstractMessage
    {
        #region Overrides of AbstractMessage

        /// <summary>
        /// Gets the message type.
        /// </summary>
        /// <value>The message type.</value>
        public override PacketType Type
        {
            get { return PacketType.FileListOpen; }
        }

        /// <summary>
        /// Decodes the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        public override void Decode(byte[] data)
        {
            DecodeHeader(data);
        }

        /// <summary>
        /// Return the message encoded as bytes
        /// </summary>
        /// <returns>
        /// An array of bytes containing the encoded message
        /// </returns>
        public override byte[] AsBytes()
        {
            return PacketData.CreateData(Type, MessageId, 0);
        }

        #endregion
    }
}
