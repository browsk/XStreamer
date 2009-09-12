namespace XStreamer.Data.Exceptions
{
    /// <summary>
    /// Exception thrown when an attempt is made to add a new share or update
    /// an existing share with a name of a pre-existing share
    /// </summary>
    public class ShareExistsException : DataException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShareExistsException"/> class.
        /// </summary>
        /// <param name="shareName">Name of the share that already exists.</param>
        public ShareExistsException(string shareName) 
            : base(string.Format("The share `{0}` already exists", shareName))
        {}
    }
}