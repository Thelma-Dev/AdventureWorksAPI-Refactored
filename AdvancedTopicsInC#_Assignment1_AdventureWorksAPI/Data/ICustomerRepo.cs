using AdvancedTopicsInC__Assignment1_AdventureWorksAPI.Models;

namespace AdvancedTopicsInC__Assignment1_AdventureWorksAPI.Data
{
    public interface ICustomerRepo
    {
        public Customer GetCustomer(int id);

        public void UpdateCustomer(Customer customer);

        public void AddCustomerToAddress(CustomerAddress customerAddress);

        public Address GetAddress(int id);
    }
}
