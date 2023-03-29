using AdvancedTopicsInC__Assignment1_AdventureWorksAPI.Models;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AdventureWorksLt2019Context>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("AdventureWorksDb"));
});
var app = builder.Build();

// Address
app.MapGet("/address", async (AdventureWorksLt2019Context db) =>
{
    return Results.Ok(new
    {
        Addresses = await db.Addresses.ToListAsync()
    });
});
app.MapPost("/address/create", (AdventureWorksLt2019Context db, Address address) =>
{
    db.Add(address);
    db.SaveChanges();
    return Results.Ok();
});
app.MapPut("/address/update/{id}", async (AdventureWorksLt2019Context db, int id, Address address) =>
{
    IEnumerable<Address> addresses = await db.Addresses.ToListAsync();
    Address addressToUpdate = addresses.First(a => a.AddressId == id);
    if (addressToUpdate != null && !addressToUpdate.Equals(address))
    {
        if (address.AddressLine1 != null) { addressToUpdate.AddressLine1 = address.AddressLine1; }
        if (address.AddressLine2 != null) { addressToUpdate.AddressLine2 = address.AddressLine2; }
        if (address.City != null) { addressToUpdate.City = address.City; }
        if (address.CountryRegion != null) { addressToUpdate.CountryRegion = address.CountryRegion; }
        if (address.PostalCode != null) { addressToUpdate.PostalCode = address.PostalCode; }
        if (address.StateProvince != null) { addressToUpdate.StateProvince = address.StateProvince; }
        db.Update(addressToUpdate);
        db.SaveChanges();
        return Results.Ok(addressToUpdate);
    } else
    {
        db.Add(address);
        db.SaveChanges();
        return Results.Ok();
    }
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

app.Run();
