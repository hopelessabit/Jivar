namespace Jivar.Service.Exceptions
{
    public class BadRequestException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BadRequestException"/> class.
        /// </summary>
        public BadRequestException() : base("Bad request.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BadRequestException"/> class with a custom message.
        /// </summary>
        /// <param name="message">The custom message that describes the error.</param>
        public BadRequestException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BadRequestException"/> class with a custom message and an inner exception.
        /// </summary>
        /// <param name="message">The custom message that describes the error.</param>
        /// <param name="innerException">The exception that caused the current exception.</param>
        public BadRequestException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
