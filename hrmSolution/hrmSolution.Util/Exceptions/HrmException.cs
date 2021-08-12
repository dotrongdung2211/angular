using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace hrmSolution.Util.Exceptions
{
    public class HrmException : Exception
    {
        public HrmException()
        {
        }

        public HrmException(string message) : base(message)
        {
        }

        public HrmException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected HrmException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
