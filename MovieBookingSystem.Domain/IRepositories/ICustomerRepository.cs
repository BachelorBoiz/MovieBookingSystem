using MovieBookingSystem.Core.Models;

namespace MovieBookingSystem.Domain.IRepositories;

public interface ICustomerRepository
{
    public List<Customer> GetCustomers();
    public Customer GetCustomerById(int id);
    public Customer CreateCustomer(Customer customer);
    public Customer UpdateCustomer(Customer customer);
    public void DeleteCustomer(int id);
}