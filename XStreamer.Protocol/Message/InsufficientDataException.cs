namespace XStreamer.Protocol.Message
{
    public class InsufficientDataException : DecodeException
    {
        public InsufficientDataException() : base("There was insufficent data in the message to decode it.")
        {
        }
    }
}
