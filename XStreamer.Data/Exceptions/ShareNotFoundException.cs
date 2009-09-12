namespace XStreamer.Data.Exceptions
{
    /// <summary>
    /// Exception thrown when the specified share is not found/does not exist
    /// </summary>
    public class ShareNotFoundException : DataException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShareNotFoundException"/> class.
        /// </summary>
        /// <param name="shareName">Name of the share.</param>
        public ShareNotFoundException(string shareName)
            : base(string.Format("The share with the specified name `{0}` does not exist", shareName))
        {
        }
    }
}