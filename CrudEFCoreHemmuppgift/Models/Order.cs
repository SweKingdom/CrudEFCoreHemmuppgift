using System.ComponentModel.DataAnnotations;

namespace CrudEFCoreHemmuppgift.Models;

public class Order
{
    // PK
    public int OrderId { get; set; }
    
    // FK
    public int CustomerId { get; set; }
    
    public DateTime OrderDate { get; set; }

    [Required,  StringLength(50)]
    public string Status { get; set; } = null!;
    
    [Required]
    public decimal TotalAmount { get; set; }
    
    public Customer? Customer { get; set; }

    public List<OrderRow> OrderRows { get; set; } = new();
}