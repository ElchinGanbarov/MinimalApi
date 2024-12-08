using Microsoft.EntityFrameworkCore;
using ProductMinimalApi.Context;
using ProductMinimalApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ProductDbContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("ProductDbContext")));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.MapGet("/products", async (ProductDbContext dbContext) =>
{
    return await dbContext.Products.ToListAsync();
});
app.MapGet("/products/{id}", async (int id, ProductDbContext dbContext) =>
{
    return await dbContext.Products.FindAsync(id);
});

app.MapPost("/products", async (Product product, ProductDbContext dbContext) =>
{
    await dbContext.Products.AddAsync(product);
    await dbContext.SaveChangesAsync();
    return Results.Created<Product>($"/products/{product.Id}", product);
});

app.MapPut("/products/{id}", async (int id, Product incomingProduct, ProductDbContext dbContext) =>
{
    if (await dbContext.Products.FindAsync(id) is Product dbProduct)
    {
        dbProduct.Description = incomingProduct.Description;
        dbProduct.Price = incomingProduct.Price;
        dbProduct.Name = incomingProduct.Name;
        await dbContext.SaveChangesAsync();
        Results.NoContent();
    }
    Results.NotFound();
});

app.MapDelete("/products/{id}", async (int id, ProductDbContext dbContext) =>
{

    if (await dbContext.Products.FindAsync(id) is Product dbProduct)
    {
        dbContext.Remove(dbProduct);
        await dbContext.SaveChangesAsync();
        Results.NoContent();
    }
    Results.NotFound();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
