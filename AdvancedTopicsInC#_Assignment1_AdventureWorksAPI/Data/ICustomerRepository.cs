using AdvancedTopicsInC__Assignment1_AdventureWorksAPI.Models;

namespace AdvancedTopicsInC__Assignment1_AdventureWorksAPI.Data
{
    public interface ICustomerRepository
    {
        public void CreateCustomer(Customer customer);

        public HashSet<Customer> GetAllCustomers();

        public void RemoveCustomer(Customer customer);

        public Customer GetCustomerById(int? id);

        
    }
}
