namespace MovieBookingSystem.Core.Exceptions;

public class MovieUnavailableException : Exception
{
    public MovieUnavailableException() { }

    public MovieUnavailableException(string message) : base(message) { }

    public MovieUnavailableException(string message, Exception innerException) : base(message, innerException) { }
}