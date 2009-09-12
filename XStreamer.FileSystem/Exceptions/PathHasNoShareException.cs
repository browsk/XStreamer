using System;

namespace XStreamer.FileSystem.Exceptions
{
    public class PathHasNoShareException : Exception
    {
        public PathHasNoShareException(string path) 
            : base(string.Format(
                       @"The specified path '{0}' does not contain a valid share. 
            It's probably the root path.", path))
        {}
    }
}