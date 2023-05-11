using AdvancedTopicsInC__Assignment1_AdventureWorksAPI.Models;

namespace AdvancedTopicsInC__Assignment1_AdventureWorksAPI.Data
{
    public class CustomerRepository : ICustomerRepository
    { 
    
        private AdventureWorksLt2019Context _context;

        public CustomerRepository(AdventureWorksLt2019Context context)
        {
            _context = context;
        }

        public void CreateCustomer(Customer customer)
        {
                
                _context.Add(customer);
                _context.SaveChanges();
        }

        public void RemoveCustomer(Customer customer)
        {
            _context.Customers.Remove(customer);
            _context.SaveChanges();
        }

        public HashSet<Customer> GetAllCustomers()
        {
           return _context.Customers.ToHashSet();
        }

        public Customer GetCustomerById(int? id)
        {
            return _context.Customers.Find(id);
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
