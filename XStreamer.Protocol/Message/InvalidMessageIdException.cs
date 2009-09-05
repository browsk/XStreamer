using System;

namespace XStreamer.Protocol.Message
{
    public class InvalidMessageIdException : SystemException
    {
        public InvalidMessageIdException() : base("Message id can't be 0 or 0xffffffff")
        {
        }
    }
}
