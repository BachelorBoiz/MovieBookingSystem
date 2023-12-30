using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieBookingSystem.Core.Exceptions
{
    public class CustomerHasOverdueBookingException : Exception
    {
        public CustomerHasOverdueBookingException() { }

        public CustomerHasOverdueBookingException(string message) : base(message) { }

        public CustomerHasOverdueBookingException(string message, Exception innerException) : base(message, innerException) { }
    }
}
