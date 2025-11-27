using CrudEFCoreHemmuppgift.Methods;
using CrudEFCoreHemmuppgift.Models;
using Microsoft.EntityFrameworkCore;




Console.WriteLine("DB: " + Path.Combine(AppContext.BaseDirectory, "shop.db"));
using (var db = new ShopContext())
{
    await db.Database.MigrateAsync();
    if (!db.Products.Any())
    {
        db.Products.AddRange(
            new Product {Pris = 10, ProductName = "Apple"},
            new Product {Pris = 20, ProductName = "Orange"},
            new Product {Pris = 30, ProductName = "Banana"},
            new Product {Pris = 40, ProductName = "Milk"},
            new Product {Pris = 50, ProductName = "Musli"},
            new Product {Pris = 60, ProductName = "Water"}
        );
        await db.SaveChangesAsync();
        Console.WriteLine("Seeded DB");
    }
}
    
    
    
    
while (true)
{
    Console.WriteLine("\nCommands: Customers | +customer | EditCustomer <Id> | DeleteCustomer <Id>");
    Console.WriteLine("Commands: Orders | OrderDetail <id> | AddOrder");
    Console.WriteLine("Commands: ListProducts | OrderCustomerSearch <id> | obs <status> ");
    Console.WriteLine(">");
    var line = Console.ReadLine()?.Trim() ?? string.Empty;
    // Hoppa över tomma rader
    if (string.IsNullOrEmpty(line))
    {
        continue;
    }

    if (line.Equals("exit", StringComparison.OrdinalIgnoreCase))
    {
        break; // Avsluta programmet, hoppa ur loopen
    }
    
    var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
    var cmd = parts[0].ToLowerInvariant();

    switch (cmd)
    {
        case "customers":
            await CustomerMethods.ListCustomersAsync();
            break;

        case "+customer":
            await CustomerMethods.AddCustomerAsync();
            break;
        case "editcustomer":
            await CustomerMethods.ListCustomersAsync();
            if (parts.Length < 2 || !int.TryParse(parts[1], out var id))
            {
                Console.WriteLine("Usage: Edit <id>");
                break;
            }
            
            await CustomerMethods.EditCustomerAsync(id);
            break;
        case "deletecustomer":
            if (parts.Length < 2 || !int.TryParse(parts[1], out var idD))
            {
                Console.WriteLine("Usage: Delete <id>");
                break;
            }
            await CustomerMethods.DeleteCustomerAsync(idD);
            break;
        case "orders":
            Console.WriteLine("Please write the page");
            var pageLoan = int.Parse((Console.ReadLine()));
            Console.WriteLine("Please write the page size");
            var pageSizeLoan = int.Parse((Console.ReadLine()));
            await OrderMethods.ListOrdersAsync(pageLoan, pageSizeLoan);
            break;
        case "orderdetail":
            if (parts.Length < 2 || !int.TryParse(parts[1], out var orderId))
            {
                Console.WriteLine("Usage: OrderDetail <id>");
                break;
            }
            await OrderMethods.OrderDetailsAsync(orderId);
            break;
        case "addorder":
            await OrderMethods.AddOrder();
            break;
        case "listproducts":
            await ProductMethods.ListProductsAsync();
            break;
        case "ordercustomersearch":
            if (parts.Length < 2 || !int.TryParse(parts[1], out var oId))
            {
                Console.WriteLine("Usage: OrderCustomerSearch <id>");
                break;
            }
            await OrderMethods.OrderByCustomerAsync(oId);
            break;
        case "obs":
        {
            if (parts.Length < 2)
            {
                Console.WriteLine("Usage: obs <status>");
                break;
            }

            if (!Enum.TryParse<OrderStatus>(parts[1], true, out var status))
            {
                Console.WriteLine("Invalid status. Valid options: Pending - 0, Paid - 1, Shipped - 2");
                break;
            }

            await OrderMethods.OrderByStatusAsync(status);
            break;
        }
        case "exit":
            return;

        default:
            Console.WriteLine("Unknown command.");
            break;
    }
}