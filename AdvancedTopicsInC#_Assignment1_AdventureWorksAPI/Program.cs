using AdvancedTopicsInC__Assignment1_AdventureWorksAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Drawing;
using NuGet.Versioning;
using AdvancedTopicsInC__Assignment1_AdventureWorksAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AdventureWorksLt2019Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("AdventureWorksDb"));
});


builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IAddressRepo, AddressRepo>();
builder.Services.AddScoped<ICustomerAddressRepo, CustomerAddressRepo>();


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
    });

var app = builder.Build();


app.MapGet("/Address/", AddressMethods.Read);

app.MapPost("/address/create", AddressMethods.CreateAddress);

app.MapPut("/address/update", AddressMethods.UpdateAddress);

app.MapDelete("/Address/Delete", AddressMethods.RemoveAddress); 


// Customer
app.MapGet("/Customers/{Id?}", CustomerMethods.Read);

app.MapPost("/customer/create", CustomerMethods.CreateCustomer);

app.MapDelete("/customer/delete", CustomerMethods.RemoveCustomer); 

app.MapPut("/customer/update", CustomerMethods.UpdateCustomer);

app.MapGet("/customer/details", CustomerMethods.GetCustomerDetails);

app.MapPost("/customer/addtoaddress", CustomerMethods.AddCustomerToAddress);


// Product
app.MapGet("/Product/{Id?}", async (int? Id, AdventureWorksLt2019Context db) =>
{
    if (Id != null)
    {
        Product? product = await db.Products.FindAsync(Id);
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
        db.Update(productToUpdate);
        db.SaveChanges();
        return Results.Ok(productToUpdate);
    } else
    {
        db.Add(product);
        await db.SaveChangesAsync();
        return Results.Ok(productToUpdate);
    }
});
app.MapDelete("/product/Delete/{id}", async (AdventureWorksLt2019Context db, int id) =>
{
    var product = await db.Products.FindAsync(id);
    if (product == null)
    {
        return Results.NotFound();
    }
    db.Products.Remove(product);
    await db.SaveChangesAsync();
    return Results.NoContent();
});

// SalesOrderHeader
app.MapGet("/SalesOrderHeader/{Id?}", async (int? Id, AdventureWorksLt2019Context db) =>
{
    if (Id != null)
    {
        SalesOrderHeader? salesOrderHeader = await db.SalesOrderHeaders.FindAsync(Id);
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
        db.Update(salesOrderToUpdate);
        await db.SaveChangesAsync();
        return Results.Ok(salesOrderToUpdate);
    } else
    {
        db.Add(salesOrder);
        await db.SaveChangesAsync();
        return Results.Ok(salesOrder);
    }
});
app.MapDelete("/salesOrderHeader/Delete/{id}", async (AdventureWorksLt2019Context db, int id) =>
{
    var salesOrder = await db.SalesOrderHeaders.FindAsync(id);
    if (salesOrder == null)
    {
        return Results.NotFound();
    }
    db.SalesOrderHeaders.Remove(salesOrder);
    await db.SaveChangesAsync();
    return Results.NoContent();
});



app.MapGet("/Address/Details", (int AddressId, IAddressRepo repo) =>
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
});



app.Run();
