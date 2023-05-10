using AdvancedTopicsInC__Assignment1_AdventureWorksAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace AdvancedTopicsInC__Assignment1_AdventureWorksAPI.Data
{
    public class AddressRepo : IAddressRepo
    {
        private AdventureWorksLt2019Context _context;

        public AddressRepo(AdventureWorksLt2019Context context)
        {
            _context = context;
        }
        public Address GetAddressById(int? id)
        {
            return _context.Addresses.Find(id);
        }
        public HashSet<Address> GetAddress()
        {
            return _context.Addresses.ToHashSet();
        }
        public void CreateAddress(Address address)
        {
            _context.Add(address);
            _context.SaveChanges();
        }

        public void UpdateAddress(int id)
        {
            Address address = GetAddressById(id);
            _context.Update(address);
            _context.SaveChanges();
        }

        public void DeleteAddress(int id)
        {
            Address address = GetAddressById(id);
            _context.Remove(address);
            _context.SaveChanges();
        }

        private ICollection<Customer> GetCustomers()
        {

            return _context.Customers.ToList();
        }

        public ICollection<CustomerAddress> GetCustomerInAddress(int addressId)
        {
            Address address = GetAddressById(addressId);

            ICollection<Customer> customer = GetCustomers();

            return _context.CustomerAddresses.Where(ca => ca.AddressId == addressId && ca.CustomerId == 1).ToList();
        }

    }
}
