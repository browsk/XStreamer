using System;

namespace XStreamer.Protocol.Message
{
    /// <summary>
    /// Abstract base class for exceptions thrown when decoding a message
    /// </summary>
    public abstract class DecodeException : SystemException
    {
        protected DecodeException(string reason) : base(reason)
        {}
    }
}
