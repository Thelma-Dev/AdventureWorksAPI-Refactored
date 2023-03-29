using AdvancedTopicsInC__Assignment1_AdventureWorksAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AdventureWorksLt2019Context>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("AdventureWorksDb"));
});
var app = builder.Build();

// Address
app.MapGet("/Addresses/{Id?}", async (int? Id, AdventureWorksLt2019Context db) =>
{
    if (Id != null)
    {
        Address address = await db.Addresses.FindAsync(Id);
        if (address == null)
        {
            return Results.NotFound();
        }
        return Results.Ok(address);
    }
    else
    {
        List<Address> addresses = await db.Addresses.ToListAsync();
        return Results.Ok(addresses);
    }
});
app.MapPost("/address/create", (AdventureWorksLt2019Context db, Address address) =>
{
    db.Add(address);
    db.SaveChanges();
    return Results.Ok();
});

// Customer
app.MapGet("/Customers/{Id?}", async (int? Id, AdventureWorksLt2019Context db) =>
{
    if (Id != null)
    {
        Customer customer = await db.Customers.FindAsync(Id);
        if (customer == null)
        {
            return Results.NotFound();
        }
        return Results.Ok(customer);
    }
    else
    {
        List<Customer> customers = await db.Customers.ToListAsync();
        return Results.Ok(customers);
    }
});
app.MapPost("/customer/create", (AdventureWorksLt2019Context db, Customer customer) =>
{
    db.Add(customer);
    db.SaveChanges();
    return Results.Ok();
});

// Product
app.MapGet("/Products/{Id?}", async (int? Id, AdventureWorksLt2019Context db) =>
{
    if (Id != null)
    {
        Product product = await db.Products.FindAsync(Id);
        if (product == null)
        {
            return Results.NotFound();
        }
        return Results.Ok(product);
    }
    else
    {
        List<Product> products = await db.Products.ToListAsync();
        return Results.Ok(products);
    }
});
app.MapPost("/product/create", (AdventureWorksLt2019Context db, Product product) =>
{
    db.Add(product);
    db.SaveChanges();
    return Results.Ok();
});

// SalesOrderHeader
app.MapGet("/SalesOrderHeaders/{Id?}", async (int? Id, AdventureWorksLt2019Context db) =>
{
    if (Id != null)
    {
        SalesOrderHeader salesOrderHeader = await db.SalesOrderHeaders.FindAsync(Id);
        if (salesOrderHeader == null)
        {
            return Results.NotFound();
        }
        return Results.Ok(salesOrderHeader);
    }
    else
    {
        List<SalesOrderHeader> salesOrderHeaders = await db.SalesOrderHeaders.ToListAsync();
        return Results.Ok(salesOrderHeaders);
    }
});
app.MapPost("/salesorder/create", (AdventureWorksLt2019Context db, SalesOrderHeader salesOrder) =>
{
    db.Add(salesOrder);
    db.SaveChanges();
    return Results.Ok();
});

app.Run();
