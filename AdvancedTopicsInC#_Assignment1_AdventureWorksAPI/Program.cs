using AdvancedTopicsInC__Assignment1_AdventureWorksAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();




//app.MapGet("/Addresses", async (AdventureWorksLt2019Context db) =>
//{
//    return await db.Addresses.ToListAsync();
//});


//app.MapGet("/Addresses/{Id}", (int Id, AdventureWorksLt2019Context db) =>
//{
//    Address address = db.Addresses.FirstOrDefault(a => a.AddressId == Id);



//    return Results.Ok(address);
//});


app.MapGet("/Addresses/{Id}", async (int? Id, AdventureWorksLt2019Context db) =>
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



app.Run();
