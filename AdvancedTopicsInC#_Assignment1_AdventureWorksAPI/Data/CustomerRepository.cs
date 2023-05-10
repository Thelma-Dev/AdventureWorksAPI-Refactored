using AdvancedTopicsInC__Assignment1_AdventureWorksAPI.Models;

namespace AdvancedTopicsInC__Assignment1_AdventureWorksAPI.Data
{
    public class CustomerRepository : ICustomerRepo
    {
        private AdventureWorksLt2019Context _context;

        public CustomerRepository(AdventureWorksLt2019Context context)
        {
            _context = context;
        }

        public Customer GetCustomer(int id)
        {
            return _context.Customers.Find(id);
        }

        public Address GetAddress(int id)
        {
            return _context.Addresses.Find(id);
        }

        public void UpdateCustomer(Customer customer)
        {           
            _context.Customers.Update(customer);
            _context.SaveChanges();
        }

        public void AddCustomerToAddress(CustomerAddress customerAddress)
        {
           _context.CustomerAddresses.Add(customerAddress);
            _context.SaveChanges();
        }
    }
}
