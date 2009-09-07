using System;

namespace XStreamer.FileSystem.Exception
{
    public class PathHasNoShareException : SystemException
    {
        public PathHasNoShareException(string path) 
            : base(string.Format(
            @"The specified path '{0}' does not contain a valid share. 
            It's probably the root path.", path))
        {}
    }
}
