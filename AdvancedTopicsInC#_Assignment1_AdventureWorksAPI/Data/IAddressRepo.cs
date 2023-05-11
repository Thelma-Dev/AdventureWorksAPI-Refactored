using AdvancedTopicsInC__Assignment1_AdventureWorksAPI.Models;

namespace AdvancedTopicsInC__Assignment1_AdventureWorksAPI.Data
{
    public interface IAddressRepo
    {

        public void CreateAddress(Address address);

        public void UpdateAddress(int id);

        public void DeleteAddress(int id);

        public Address GetAddressById(int id);

        public HashSet<Address> GetAddress();

        public HashSet<Customer> GetCustomers();

    }
}
