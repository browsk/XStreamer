namespace XStreamer.Protocol.Message
{
    /// <summary>
    /// Interface for XBMSP messages
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// Gets or sets the message id.
        /// </summary>
        /// <value>The message id.</value>
        int MessageId { get; set; }

        /// <summary>
        /// Gets the message type.
        /// </summary>
        /// <value>The message type.</value>
        PacketType Type { get; }

        /// <summary>
        /// Decodes the specified data.
        /// </summary>
        /// <param name="data">The data.</param>
        void Decode(byte[] data);

        /// <summary>
        /// Return the message encoded as bytes
        /// </summary>
        /// <returns>An array of bytes containing the encoded message</returns>
        byte[] AsBytes();
    }
}
