using MovieBookingSystem.Core.Models;
using MovieBookingSystem.Domain.IRepositories;

namespace MovieBookingSystem.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly MovieBookingContext _context;

    public CustomerRepository(MovieBookingContext context)
    {
        _context = context;
    }
    public List<Customer> GetCustomers()
    {
        return _context.Customers.ToList();
    }

    public Customer GetCustomerById(int id)
    {
        return _context.Customers.Find(id) ?? throw new InvalidOperationException();
    }

    public Customer CreateCustomer(Customer customer)
    {
        _context.Customers.Add(customer);
        return customer;
    }

    public Customer UpdateCustomer(Customer customer)
    {
        _context.Customers.Update(customer);
        return customer;
    }

    public void DeleteCustomer(int id)
    {
        var customer = _context.Customers.Find(id);
        if (customer != null) _context.Customers.Remove(customer);
    }
}