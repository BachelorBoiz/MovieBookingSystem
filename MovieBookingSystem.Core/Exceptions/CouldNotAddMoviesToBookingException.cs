namespace MovieBookingSystem.Core.Exceptions;

public class CouldNotAddMoviesToBookingException : Exception
{
    public CouldNotAddMoviesToBookingException() { }

    public CouldNotAddMoviesToBookingException(string message) : base(message) { }

    public CouldNotAddMoviesToBookingException(string message, Exception innerException) : base(message, innerException) { }
}