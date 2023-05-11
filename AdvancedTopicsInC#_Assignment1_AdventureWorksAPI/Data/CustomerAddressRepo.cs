using AdvancedTopicsInC__Assignment1_AdventureWorksAPI.Models;

namespace AdvancedTopicsInC__Assignment1_AdventureWorksAPI.Data
{
    public class CustomerAddressRepo : ICustomerAddressRepo
    {
        private AdventureWorksLt2019Context _context;

        public CustomerAddressRepo(AdventureWorksLt2019Context context)
        {
            _context = context;
        }

        public ICollection<CustomerAddress> GetCustomerAddress(int customerid)
        {
            Customer customer = GetCustomer(customerid);

            return _context.CustomerAddresses.Where(ca => ca.CustomerId == customer.CustomerId).ToList();
        }

        public ICollection<CustomerAddress> AllCustomerAddress()
        {
            return _context.CustomerAddresses.ToList();
        }

        private Customer GetCustomer(int id)
        {
            return _context.Customers.Find(id);
        }
    }
}
