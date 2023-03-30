using AdvancedTopicsInC__Assignment1_AdventureWorksAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Drawing;
using NuGet.Versioning;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AdventureWorksLt2019Context>(options =>
{
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
        if (address.ModifiedDate != null) { addressToUpdate.ModifiedDate = address.ModifiedDate; }
        db.Update(addressToUpdate);
        db.SaveChanges();
        return Results.Ok(addressToUpdate);
    } else
    {
        db.Add(address);
        db.SaveChanges();
        return Results.Ok(addressToUpdate);
    }
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
app.MapPut("/customer/update/{id}", async(AdventureWorksLt2019Context db, int id, Customer customer) =>
{
    IEnumerable<Customer> customers = await db.Customers.ToListAsync();
    Customer customerToUpdate = customers.First(c => c.CustomerId == id);
    if (customerToUpdate != null && !customerToUpdate.Equals(customer))
    {
        if (customerToUpdate.Title != customer.Title) { customerToUpdate.Title = customer.Title; }
        if (customerToUpdate.FirstName != customer.FirstName) { customerToUpdate.FirstName = customer.FirstName; }
        if (customerToUpdate.LastName != customer.LastName) { customerToUpdate.LastName = customer.LastName; }
        if (customerToUpdate.MiddleName != customer.MiddleName) { customerToUpdate.MiddleName = customer.MiddleName; }
        if (customerToUpdate.Suffix != customer.Suffix) { customerToUpdate.Suffix = customer.Suffix; }
        if (customerToUpdate.CompanyName != customer.CompanyName) { customerToUpdate.CompanyName = customer.CompanyName; }
        if (customerToUpdate.SalesPerson != customer.SalesPerson) { customerToUpdate.SalesPerson = customer.SalesPerson; }
        if (customerToUpdate.EmailAddress != customer.EmailAddress) { customerToUpdate.EmailAddress = customer.EmailAddress; }
        if (customerToUpdate.Phone != customer.Phone) { customerToUpdate.Phone = customer.Phone; }
        if (customerToUpdate.ModifiedDate != customer.ModifiedDate) { customerToUpdate.ModifiedDate = customer.ModifiedDate; }
        db.Update(customerToUpdate);
        db.SaveChangesAsync();
        return Results.Ok(customerToUpdate);
    }else
    {
        db.Add(customerToUpdate);
        db.SaveChangesAsync();
        return Results.Ok(customerToUpdate);
    }
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
app.MapPut("/product/update/{id}", async (AdventureWorksLt2019Context db, int id, Product product) =>
{
    IEnumerable<Product> products = await db.Products.ToListAsync();
    Product productToUpdate = products.First(p => p.ProductId == id);
    if (productToUpdate != null && !productToUpdate.Equals(product))
    {
        if (productToUpdate.Name != product.Name) { productToUpdate.Name = product.Name;}
        if (productToUpdate.ProductNumber != product.ProductNumber) { productToUpdate.ProductNumber = product.ProductNumber; }
        if (productToUpdate.Color != product.Color) { productToUpdate.Color = product.Color; }
        if (productToUpdate.StandardCost != product.StandardCost) { productToUpdate.StandardCost = product.StandardCost; }
        if (productToUpdate.ListPrice != product.ListPrice) { productToUpdate.ListPrice = product.ListPrice; }
        if (productToUpdate.Size != product.Size) { productToUpdate.Size = product.Size; }
        if (productToUpdate.Weight != product.Weight) { productToUpdate.Weight = product.Weight;}
        if (productToUpdate.SellStartDate != product.SellStartDate) { productToUpdate.SellStartDate = product.SellStartDate; }
        if (productToUpdate.SellEndDate != product.SellEndDate) { productToUpdate.SellEndDate = product.SellEndDate; }
        return Results.Ok(productToUpdate);
    } else
    {
        db.Add(productToUpdate);
        db.SaveChangesAsync();
        return Results.Ok(productToUpdate);
    }
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
app.MapPut("/salesorder/update/{id}", async (AdventureWorksLt2019Context db, int id, SalesOrderHeader salesOrder) =>
{
    IEnumerable<SalesOrderHeader> salesOrders = await db.SalesOrderHeaders.ToListAsync();
    SalesOrderHeader salesOrderToUpdate = salesOrders.First(s => s.SalesOrderId == id);
    if (salesOrderToUpdate != null && !salesOrderToUpdate.Equals(salesOrder))
    {
        if (salesOrderToUpdate.OrderDate != salesOrder.OrderDate) { salesOrderToUpdate.OrderDate = salesOrder.OrderDate; }
        if (salesOrderToUpdate.DueDate != salesOrder.DueDate) { salesOrderToUpdate.DueDate = salesOrder.DueDate; }
        if (salesOrderToUpdate.ShipDate != salesOrder.ShipDate) { salesOrderToUpdate.ShipDate = salesOrder.ShipDate; }
        if (salesOrderToUpdate.Status != salesOrder.Status) { salesOrderToUpdate.Status = salesOrder.Status; }
        if (salesOrderToUpdate.OnlineOrderFlag != salesOrder.OnlineOrderFlag) { salesOrderToUpdate.OnlineOrderFlag = salesOrder.OnlineOrderFlag; }
        if (salesOrderToUpdate.SalesOrderNumber != salesOrder.SalesOrderNumber) { salesOrderToUpdate.SalesOrderNumber = salesOrder.SalesOrderNumber; }
        if (salesOrderToUpdate.PurchaseOrderNumber != salesOrder.PurchaseOrderNumber) { salesOrderToUpdate.PurchaseOrderNumber = salesOrder.PurchaseOrderNumber; }
        if (salesOrderToUpdate.ShipToAddressId != salesOrder.ShipToAddressId) { salesOrderToUpdate.ShipToAddressId = salesOrder.ShipToAddressId; }
        if (salesOrderToUpdate.BillToAddress != salesOrder.BillToAddress) { salesOrderToUpdate.BillToAddress = salesOrder.BillToAddress; }
        if (salesOrderToUpdate.ShipMethod != salesOrder.ShipMethod) { salesOrderToUpdate.ShipMethod = salesOrder.ShipMethod; }
        if (salesOrderToUpdate.CreditCardApprovalCode != salesOrder.CreditCardApprovalCode) { salesOrderToUpdate.CreditCardApprovalCode = salesOrder.CreditCardApprovalCode; }
        if (salesOrderToUpdate.SubTotal != salesOrder.SubTotal) { salesOrderToUpdate.SubTotal = salesOrder.SubTotal; }
        if (salesOrderToUpdate.TaxAmt != salesOrder.TaxAmt) { salesOrderToUpdate.TaxAmt = salesOrder.TaxAmt; }
        if (salesOrderToUpdate.Freight != salesOrder.Freight) { salesOrderToUpdate.Freight = salesOrder.Freight; }
        if (salesOrderToUpdate.TotalDue != salesOrder.TotalDue) { salesOrderToUpdate.TotalDue = salesOrder.TotalDue; }
        if (salesOrderToUpdate.Comment != salesOrder.Comment) { salesOrderToUpdate.Comment = salesOrder.Comment; }

    }
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
