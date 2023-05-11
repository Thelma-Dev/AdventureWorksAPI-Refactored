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



// Address

app.MapGet("/Address/", AddressMethods.Read);

app.MapPost("/address/create", AddressMethods.CreateAddress);

app.MapPut("/address/update", AddressMethods.UpdateAddress);

app.MapDelete("/Address/Delete", AddressMethods.RemoveAddress);

app.MapGet("/Address/Details", AddressMethods.GetAddressDetails);



// Customer

app.MapGet("/Customers/{Id?}", CustomerMethods.Read);

app.MapPost("/customer/create", CustomerMethods.CreateCustomer);

app.MapDelete("/customer/delete", CustomerMethods.RemoveCustomer); 

app.MapPut("/customer/update", CustomerMethods.UpdateCustomer);

app.MapGet("/customer/details", CustomerMethods.GetCustomerDetails);

app.MapPost("/customer/addtoaddress", CustomerMethods.AddCustomerToAddress);



// Product

app.MapGet("/Product/{Id?}", ProductMethods.Read);

app.MapPost("/product/create", ProductMethods.CreateProduct);

app.MapPut("/product/update/{id}", ProductMethods.UpdateProduct);

app.MapDelete("/product/Delete/{id}", ProductMethods.RemoveProduct);



// SalesOrderHeader

app.MapGet("/SalesOrderHeader/{Id?}", SalesOrderHeaderMethods.Read);

app.MapPost("/salesorder/create", SalesOrderHeaderMethods.CreateSalesOrder);

app.MapPut("/salesorder/update/", SalesOrderHeaderMethods.UpdateSalesOrder);

app.MapDelete("/salesOrderHeader/Delete/{id}", SalesOrderHeaderMethods.RemoveSalesOrder);


app.Run();
