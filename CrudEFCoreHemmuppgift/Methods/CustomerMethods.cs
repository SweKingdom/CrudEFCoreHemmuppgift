using CrudEFCoreHemmuppgift.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudEFCoreHemmuppgift.Methods;

public static class CustomerMethods
{
    // Customer List funktion
    public static async Task ListCustomersAsync()
    {
        using var db = new ShopContext();
        var customers = await db.Customers
            .AsNoTracking()
            .OrderBy(c => c.CustomerId)
            .ToListAsync();
        Console.WriteLine("Id | Name | Email | City");
        foreach (var customer in customers)
        {
            Console.WriteLine($"{customer.CustomerId} | {customer.Name} | {customer.Email} | {customer.City}");
        }
    }
    
    // Add Customer
    public static async Task AddCustomerAsync()
    {
        var db = new ShopContext();
        Console.Write("Enter Customer Name: ");
        var name = Console.ReadLine()?.Trim() ?? string.Empty;
        
        if (string.IsNullOrEmpty(name) || name.Length > 100)
        {
            Console.WriteLine("Name is required.");
            return;
        }
        
        Console.Write("Enter Customer Email: ");
        var email = Console.ReadLine() ?? string.Empty;
        
        if (string.IsNullOrEmpty(email) || name.Length > 250)
        {
            Console.WriteLine("Email is required.");
            return;
        }
        
        Console.WriteLine("City:");
        var city = Console.ReadLine() ?? string.Empty;
        if (city.Length > 250)
        {
            Console.WriteLine("City name can't be longer than 250 characters.");
            return;
        }

        await db.Customers.AddAsync(new Customer
        {
            Name = name,
            Email = email,
            City = city
        });

        try
        {
            await db.SaveChangesAsync();
            Console.WriteLine("Customer added.");
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine("Db Error! " + ex.GetBaseException().Message);
        }
    }
    
    // Edit customer <id>
    public static async Task EditCustomerAsync(int CustomerId)
    {
        using var db = new ShopContext();
        
        var customer = await db.Customers.FirstOrDefaultAsync(c => c.CustomerId == CustomerId);
        if (customer == null)
        {
            Console.WriteLine("Customer not found.");
            return;
        }
        
        Console.WriteLine("Current Customer Name: " + customer.Name);
        var name = Console.ReadLine()?.Trim() ?? string.Empty;
        if (!string.IsNullOrEmpty(name))
        {
            customer.Name = name;
        }
        
        Console.WriteLine("Current Customer Name: " + customer.Email);
        var email = Console.ReadLine()?.Trim() ?? string.Empty;
        if (!string.IsNullOrEmpty(email))
        {
            customer.Email = email;
        }
        
        Console.WriteLine("Current Customer Name: " + customer.City);
        var city = Console.ReadLine()?.Trim() ?? string.Empty;
        if (!string.IsNullOrEmpty(city))
        {
            customer.City = city;
        }

        try
        {
            await db.SaveChangesAsync();
            Console.WriteLine("Customer edited");
        }
        catch (DbUpdateException exception)
        {
            Console.WriteLine(exception.Message);
        }
        
    }
    // Deletecustomer <id> - cascade
    public static async Task DeleteCustomerAsync(int CustomerId)
    {
        using var db = new ShopContext();
        
        var customer = await db.Customers.FirstOrDefaultAsync(c => c.CustomerId == CustomerId);
        if (customer == null)
        {
            Console.WriteLine("Customer not found.");
            return;
        }
        db.Customers.Remove(customer);

        try
        {
            await db.SaveChangesAsync();
            Console.WriteLine("Customer deleted.");
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}

















