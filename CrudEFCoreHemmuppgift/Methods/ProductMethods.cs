using CrudEFCoreHemmuppgift.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudEFCoreHemmuppgift.Methods;

public static class ProductMethods
{
    public static async Task ListProductsAsync()
    {
        using var db  = new ShopContext();
    
        var rows = await db.Products
            .AsNoTracking()
            .OrderBy(p => p.ProductName)
            .ToListAsync();
    
        Console.WriteLine("Id | Name | Pris");
        foreach (var row in rows)
        {
            Console.WriteLine($"{row.ProductId} | {row.ProductName} | {row.Pris}");    
        }
    }
    
    
}