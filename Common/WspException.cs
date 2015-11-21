namespace Common
{
    using System;
    using System.Runtime.Serialization;

    public class WspException : Exception
    {
        private const int DefaultStatusCode = 500;

        public WspException()
            : this(null, null, DefaultStatusCode)
        {
        }

        protected WspException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }

        public WspException(string theMessage, Exception theInnerException)
            : this(theMessage, theInnerException, DefaultStatusCode)
        {
        }

        public WspException(string theMessage, int theStatusCode)
            : this(theMessage, null, theStatusCode)
        {
        }

        public WspException(string theMessage, Exception theInnerException, int theStatusCode)
            : base(theMessage, theInnerException)
        {
            this.StatusCode = theStatusCode;
            this.exceptionId = Guid.NewGuid();
        }

        public WspException(string theMessage)
            : this(theMessage, null, DefaultStatusCode)
        {
        }

        public int StatusCode { get; set; }

        private Guid exceptionId;

        public Guid ExceptionId
        {
            get
            {
                return this.exceptionId;
            }
        }
    }
}
