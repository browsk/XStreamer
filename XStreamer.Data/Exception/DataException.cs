using System;

namespace XStreamer.Data.Exception
{
    /// <summary>
    /// Abstract exception used as base for all exceptions thrown externally
    /// from the <see cref="XStreamer.Data"/> assembly.
    /// </summary>
    public abstract class DataException : SystemException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        protected DataException(string message) : base(message)
        {
        }
    }
}
