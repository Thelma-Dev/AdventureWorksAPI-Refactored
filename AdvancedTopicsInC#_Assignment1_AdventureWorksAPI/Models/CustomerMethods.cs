using AdvancedTopicsInC__Assignment1_AdventureWorksAPI.Data;
using System.Net;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace AdvancedTopicsInC__Assignment1_AdventureWorksAPI.Models
{
    public class CustomerMethods
    {

        public static IResult GetCustomerDetails(ICustomerRepo customerRepo, ICustomerAddressRepo customerAddressRepo,IAddressRepo addressRepo ,int customerId)
        {

            try
            {
                Customer? customer = customerRepo.GetCustomer(customerId);

                if (customer == null)
                {
                    return Results.NotFound();
                }

                ICollection<CustomerAddress> customerAddresses = customerAddressRepo.GetCustomerAddress(customerId);


                List<Address> Address = new List<Address>();

                foreach (CustomerAddress ca in customerAddresses)
                {
                    Address address = customerRepo.GetAddress(ca.AddressId);
                    Address.Add(address);
                }

               
                var CustomerAddress = addressRepo.GetCustomers().Where(c => c.CustomerId == customerId).Select(x => new
                {
                    CustomerId = x.CustomerId,
                    Title = x.Title,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    CompnayName = x.CompanyName,
                    SalesPerson = x.SalesPerson,
                    EmailAddress = x.EmailAddress,
                    Phone = x.Phone,
                    Rowguid = x.Rowguid,
                    ModifiedDate = x.ModifiedDate,
                    Address = x.CustomerAddresses.Where(ca => ca.CustomerId == customerId).Select(a => new
                    {
                        AddressId = a.Address.AddressId,
                        AddressLine1 = a.Address.AddressLine1,
                        AddressLine2 = a.Address.AddressLine2,
                        City = a.Address.City,
                        StateProvince = a.Address.StateProvince,
                        CountryRegion = a.Address.CountryRegion,
                        PostalCode = a.Address.PostalCode,
                    })

                });

                return Results.Ok(CustomerAddress);



                //var customerAddress = new
                //{
                //    Customer = customer

                //};

                //var options = new JsonSerializerOptions
                //{
                //    ReferenceHandler = ReferenceHandler.Preserve
                //};

                //var serializer = System.Text.Json.JsonSerializer.Serialize(customerAddress, options);

                //return Results.Ok(serializer);

            }
            catch (Exception exe)
            {
                return Results.BadRequest();
            }
        }

        public static IResult UpdateCustomer(ICustomerRepo customerRepo, int id, Customer? customer)
        {
            try
            {
                Customer? customerToUpdate = customerRepo.GetCustomer(id);

                if (customerToUpdate == null && customer != null)
                {
                    Customer newCustomer = new Customer();

                    newCustomer.Rowguid = Guid.NewGuid();
                    newCustomer.ModifiedDate = DateTime.Now;

                    //repo.CreateCustomer(newCustomer);

                    return Results.Created($"/customer?id={customer.CustomerId}", customer);
                }
                else
                {
                    customerToUpdate.NameStyle = customer.NameStyle;
                    customerToUpdate.Title = customer.Title;
                    customerToUpdate.FirstName = customer.FirstName;
                    customerToUpdate.MiddleName = customer.MiddleName;
                    customerToUpdate.LastName = customer.LastName;
                    customerToUpdate.CompanyName = customer.CompanyName;
                    customerToUpdate.Phone = customer.Phone;
                    customerToUpdate.EmailAddress = customer.EmailAddress;
                    customerToUpdate.Rowguid = Guid.NewGuid();
                    customerToUpdate.ModifiedDate = DateTime.Now;

                    customerRepo.UpdateCustomer(customerToUpdate);

                    return Results.Ok(customerToUpdate);
                }
            }
            catch (Exception exe)
            {
                return Results.BadRequest();
            }
        }

        public static IResult AddCustomerToAddress(ICustomerRepo customerRepo, ICustomerAddressRepo customerAddressRepo, int customerId,int addressId)
        {
            try
            {
                Customer? customer = customerRepo.GetCustomer(customerId);

                Address? address = customerRepo.GetAddress(addressId);

                if (customer == null)
                {
                    return Results.BadRequest("Customer Not Found");
                }

                if (customer != null && address != null) // and address is not null
                {
                    CustomerAddress ca = new CustomerAddress();

                    ca.CustomerId = customer.CustomerId;
                    ca.AddressId = address.AddressId;
                    ca.AddressType = "Main Office";
                    ca.Rowguid = Guid.NewGuid();
                    ca.ModifiedDate = DateTime.Now;

                    if (!customerAddressRepo.AllCustomerAddress().Any(ca => ca.CustomerId == customer.CustomerId && ca.AddressId == address.AddressId))
                    {
                        customerRepo.AddCustomerToAddress(ca);


                        return Results.Ok($"{customer.FirstName} is added to {address.AddressLine1}");
                    }
                    else
                    {
                        return Results.BadRequest("Customer already added to address");
                    }
                }
                else
                {
                    return Results.BadRequest("Address not found");
                }

            }
            catch (Exception exe)
            {
                return Results.BadRequest();
            }
        }
    }
}
