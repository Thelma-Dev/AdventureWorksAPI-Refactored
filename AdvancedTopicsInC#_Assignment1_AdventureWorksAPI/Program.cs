using AdvancedTopicsInC__Assignment1_AdventureWorksAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AdventureWorksLt2019Context>(options => {
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

app.Run();
