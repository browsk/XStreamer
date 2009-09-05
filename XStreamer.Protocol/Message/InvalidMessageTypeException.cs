using System;

namespace XStreamer.Protocol.Message
{
    /// <summary>
    /// Exception thrown when decoding a message and the packet type
    /// does not match that which is expected
    /// </summary>
    public class InvalidMessageTypeException : DecodeException
    {
        public InvalidMessageTypeException(PacketType expectedType, PacketType actualType)
            : base(
            string.Format(
                "The message type {0} does not match the expected type {1}", 
                    Enum.GetName(typeof(PacketType), expectedType), 
                    Enum.GetName(typeof(PacketType), actualType)))
        {
        }
    }
}
