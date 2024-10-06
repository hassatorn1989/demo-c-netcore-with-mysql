using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DemoProductApp.Models;

[Table("product")]
public class Product
{
    public int Id { get; set; }

    [Column("product_name")]
    [Required(ErrorMessage = "Product name is required")]
    public string ProductName { get; set; }

    [Column("product_price")]
    [Required(ErrorMessage = "Product price is required")]
    public decimal ProductPrice { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [Column("updated_at")]
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
}
