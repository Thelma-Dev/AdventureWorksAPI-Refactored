using AdvancedTopicsInC__Assignment1_AdventureWorksAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Drawing;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AdventureWorksLt2019Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AdventureWorksDb"));
});
var app = builder.Build();

// Address
app.MapGet("/address", (AdventureWorksLt2019Context db) =>
{
    return Results.Ok(new
    {
        Addresses = db.Addresses.ToList()
    });
});
app.MapPost("/address/create", (AdventureWorksLt2019Context db, Address address) =>
{
    db.Add(address);
    db.SaveChanges();
    return Results.Ok();
});

// Customer
app.MapGet("/customer", (AdventureWorksLt2019Context db) =>
{
    return Results.Ok(new
    {
        Customers = db.Customers.ToList()
    });
});
app.MapPost("/customer/create", (AdventureWorksLt2019Context db, Customer customer) =>
{
    db.Add(customer);
    db.SaveChanges();
    return Results.Ok();
});

// Product
app.MapGet("/product", (AdventureWorksLt2019Context db) =>
{
    return Results.Ok(new
    {
        Products = db.Products.ToList()
    });
});
app.MapPost("/product/create", (AdventureWorksLt2019Context db, Product product) =>
{
    db.Add(product);
    db.SaveChanges();
    return Results.Ok();
});

// SalesOrderHeader
app.MapGet("/salesorder", (AdventureWorksLt2019Context db) =>
{
    return Results.Ok(new
    {
        SalesOrderHeaders = db.SalesOrderHeaders.ToList()
    });
});
app.MapPost("/salesorder/create", (AdventureWorksLt2019Context db, SalesOrderHeader salesOrder) =>
{
    db.Add(salesOrder);
    db.SaveChanges();
    return Results.Ok();
});

//---------------------------------------------------------------------------------------------------


app.MapGet("/Customer/Details/{CustomerId}", (int CustomerId, AdventureWorksLt2019Context db) =>

{

    Customer customer = db.Customers.Include(a => a.CustomerAddresses)

    .ThenInclude(b => b.Address)

    .FirstOrDefault(c => c.CustomerId == CustomerId);

    if (customer == null)

    {
        return Results.BadRequest("Customer does not exist.");
    }
    var address = customer.CustomerAddresses.Select(a => a.Address);

    var customerAddress = new

    {

        Customer = customer,

        Address = address

    };

    var options = new JsonSerializerOptions
    {
        ReferenceHandler = ReferenceHandler.Preserve
    };

    var serializer = System.Text.Json.JsonSerializer.Serialize(customerAddress, options);

    return Results.Ok(serializer);

});

app.MapGet("/Address/Details/{AddressId}", (int AddressId, AdventureWorksLt2019Context db) =>
{

    Address address = db.Addresses.Include(a => a.CustomerAddresses)
    .ThenInclude(b => b.Customer)

    .FirstOrDefault(c => c.AddressId == AddressId);


    if (address == null)
    {
        return Results.BadRequest("Address does not exist.");
    }

    var customer = address.CustomerAddresses.Select(a => a.Customer);

    var customerAddress = new

    {
        Address = address,
        Customer = customer
    };

    var options = new JsonSerializerOptions
    {
        ReferenceHandler = ReferenceHandler.Preserve
    };

    var serializer = System.Text.Json.JsonSerializer.Serialize(customerAddress, options);

    return Results.Ok(serializer);
});




app.Run();
