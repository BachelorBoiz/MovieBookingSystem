namespace MovieBookingSystem.Core.Exceptions;

public class InvalidDateSpanException : Exception
{
    public InvalidDateSpanException() { }

    public InvalidDateSpanException(string message) : base(message) { }

    public InvalidDateSpanException(string message, Exception innerException) : base(message, innerException) { }
}