using MovieBookingSystem.Core.IServices;
using MovieBookingSystem.Core.Models;
using MovieBookingSystem.Domain.IRepositories;

namespace MovieBookingSystem.Domain.Services;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _repo;

    public CustomerService(ICustomerRepository repo)
    {
        _repo = repo;
    }

    public List<Customer> GetCustomers()
    {
        return _repo.GetCustomers();
    }

    public Customer GetCustomerById(int id)
    {
        if (id > 0)
        {
            return _repo.GetCustomerById(id);
        }
        return null;
    }

    public Customer CreateCustomer(Customer customer)
    {
        return _repo.CreateCustomer(customer);
    }

    public Customer UpdateCustomer(Customer customer)
    {
        return _repo.UpdateCustomer(customer);
    }

    public void DeleteCustomer(int id)
    {
        if (id > 0)
        {
            _repo.DeleteCustomer(id);
        }
    }
}