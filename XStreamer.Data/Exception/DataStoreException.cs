namespace XStreamer.Data.Exception
{
    public class DataStoreException : DataException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataStoreException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="innerException">The inner exception.</param>
        public DataStoreException(string message, System.Exception innerException)
            : base(message, innerException)
        {
        }

        public DataStoreException(string message) : base(message) {}
    }
}
