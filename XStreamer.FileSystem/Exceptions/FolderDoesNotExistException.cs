using System;

namespace XStreamer.FileSystem.Exceptions
{
    public class FolderDoesNotExistException : Exception
    {
        public FolderDoesNotExistException(string folder, string currentDirectory)
            : base(string.Format(@"The specified folder '{0}' does not exist in 
            virtual folder '{1}'", folder, currentDirectory))
        {}
    }
}