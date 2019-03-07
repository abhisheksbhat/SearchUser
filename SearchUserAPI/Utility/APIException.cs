#region Namespaces
using System;
using System.Runtime.Serialization;
#endregion

namespace SearchUserAPI.Utility
{
    /// <summary>
    /// API Exception class
    /// </summary>
    public class APIException : ApplicationException
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public APIException()
        {
        }

        /// <summary>
        /// Constructor with message
        /// </summary>
        /// <param name="message"></param>
        public APIException(string message) : base(message)
        {
        }

        /// <summary>
        /// Constructor with message and inner exception
        /// </summary>
        /// <param name="message"></param>
        public APIException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
