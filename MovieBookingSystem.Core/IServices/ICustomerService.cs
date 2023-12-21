using MovieBookingSystem.Core.Models;

namespace MovieBookingSystem.Core.IServices;

public interface ICustomerService
{
    public List<Customer> GetCustomers();
    public Customer GetCustomerById(int id);
    public Customer CreateCustomer(Customer customer);
    public Customer UpdateCustomer(Customer customer);
    public void DeleteCustomer(int id);
}