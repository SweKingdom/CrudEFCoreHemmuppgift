using System.ComponentModel.DataAnnotations;

namespace CrudEFCoreHemmuppgift.Models;

public class Product
{
    public int ProductId { get; set; }
    
    [Required]
    public decimal Pris {get ; set;}

    [Required, StringLength(150)]
    public string ProductName { get; set; } = null!;
    
    [StringLength(250)]
    public string? Description {get; set;}
}