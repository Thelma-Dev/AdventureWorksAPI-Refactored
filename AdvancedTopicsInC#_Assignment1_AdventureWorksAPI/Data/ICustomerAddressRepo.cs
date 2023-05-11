using AdvancedTopicsInC__Assignment1_AdventureWorksAPI.Models;

namespace AdvancedTopicsInC__Assignment1_AdventureWorksAPI.Data
{
    public interface ICustomerAddressRepo
    {
        public ICollection<CustomerAddress> GetCustomerAddress(int customerid);

        public ICollection<CustomerAddress> AllCustomerAddress();
    }
}
