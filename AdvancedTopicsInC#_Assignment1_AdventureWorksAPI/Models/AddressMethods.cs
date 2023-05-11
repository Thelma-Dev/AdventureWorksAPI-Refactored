using AdvancedTopicsInC__Assignment1_AdventureWorksAPI.Data;
using System.Net;

namespace AdvancedTopicsInC__Assignment1_AdventureWorksAPI.Models
{
    public class AddressMethods
    {
        public static IResult CreateAddress(IAddressRepo repo, Address address)
        {
            try
            {
                address.Rowguid = Guid.NewGuid();
                address.ModifiedDate = DateTime.Now;
                repo.CreateAddress(address);
                return Results.Created($"/Address?id={address.AddressId}", address);
            }
            catch (Exception ex)
            {
                return Results.NotFound();
            }

        }

        public static IResult Read(IAddressRepo repo, int id)
        {
            HashSet<Address> addresses = new HashSet<Address>();

            if (id == null)
            {
                addresses = repo.GetAddress();
                return Results.Ok(addresses);

            }
            else
            {
                Address address = repo.GetAddressById(id);

                if (address == null)
                {
                    return Results.NotFound();
                }
                else
                {
                    return Results.Ok(address);
                }
            }

        }

        public static IResult RemoveAddress(IAddressRepo repo, int id)
        {
            Address address = repo.GetAddressById(id);

            if (address == null)
            {
                return Results.NotFound();
            }
            else
            {
                return Results.Ok($" Address with Id {address.AddressId} is removed successfully.");
            }

        }

        public static IResult UpdateAddress(IAddressRepo repo, int id, Address? address)
        {
            try
            {
                Address selectedAddress = repo.GetAddressById(id);

                if (selectedAddress == null && address != null)
                {
                    address.Rowguid = Guid.NewGuid();
                    address.ModifiedDate = DateTime.Now;
                    repo.CreateAddress(address);
                    return Results.Ok(repo.GetAddressById(address.AddressId));
                }
                else
                {
                    selectedAddress.AddressLine1 = address.AddressLine1;
                    selectedAddress.AddressLine2 = address.AddressLine2;
                    selectedAddress.City = address.City;
                    selectedAddress.StateProvince = address.StateProvince;
                    selectedAddress.CountryRegion = address.CountryRegion;
                    selectedAddress.PostalCode = address.PostalCode;
                    selectedAddress.ModifiedDate = DateTime.Now;
                    repo.UpdateAddress(selectedAddress.AddressId);
                    return Results.Ok(repo.GetAddressById(selectedAddress.AddressId));
                }
            }
            catch (Exception exe)
            {
                return Results.BadRequest();
            }
        }

        
        public static IResult GetAddressDetails(int AddressId, IAddressRepo repo)
        {
            //return repo.GetCustomerInAddress(AddressId);

            //Address? address = db.Addresses.Include(a => a.CustomerAddresses)
            //.ThenInclude(b => b.Customer)

            //.FirstOrDefault(c => c.AddressId == AddressId);


            //if (address == null)
            //{
            //    return Results.BadRequest("Address does not exist.");
            //}

            //var customer = address.CustomerAddresses.Select(a => a.Customer);

            //var customerAddress = new

            //{
            //    Address = address,
            //    Customer = customer
            //};

            //var options = new JsonSerializerOptions
            //{
            //    ReferenceHandler = ReferenceHandler.Preserve
            //};

            //var serializer = System.Text.Json.JsonSerializer.Serialize(customerAddress, options);

            //return Results.Ok(serializer);

            return Results.Ok();
        }

    }
}
