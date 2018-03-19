using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Imagination.MySpring.Exception
{
    public class SpringException:System.Exception
    {
        public SpringException()
        {
        }

        public SpringException(string message) : base(message)
        {
        }

        public SpringException(string message, System.Exception innerException) : base(message, innerException)
        {
        }

        protected SpringException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
