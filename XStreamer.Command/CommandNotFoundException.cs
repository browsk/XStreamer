using System;

namespace XStreamer.Command
{
    public class CommandNotFoundException : SystemException
    {
        public CommandNotFoundException(Type messageType)
            : base(string.Format("Unable to find command for message of type {0}", messageType.Name))
        {}
    }
}
