namespace Jivar.Service.Exceptions
{
    public class ExistedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ExistedException"/> class.
        /// </summary>
        public ExistedException() : base("The specified resource already exists.")
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExistedException"/> class with a custom message.
        /// </summary>
        /// <param name="message">The custom message that describes the error.</param>
        public ExistedException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExistedException"/> class with a custom message and an inner exception.
        /// </summary>
        /// <param name="message">The custom message that describes the error.</param>
        /// <param name="innerException">The exception that caused the current exception.</param>
        public ExistedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
