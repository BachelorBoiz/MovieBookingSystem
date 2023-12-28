namespace MovieBookingSystem.Infrastructure;

public interface IDbInitializer
{
    void Initialize(MovieBookingContext context);
}