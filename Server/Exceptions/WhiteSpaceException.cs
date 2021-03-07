using System;

namespace Service.Server.Exceptions
{
    /// <summary>
    /// Exception thrown when a string is unexpectedly null, empty, or whitespace.
    /// </summary>
    public sealed class WhiteSpaceException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WhiteSpaceException" /> class.
        /// </summary>
        /// <param name="objectName">Name of the object that was unexpected null, empty or whitespace.</param>
        public WhiteSpaceException(string objectName)
            : base($"\"{objectName}\" cannot be null, empty or whitespace.")
        {
        }
    }
}
