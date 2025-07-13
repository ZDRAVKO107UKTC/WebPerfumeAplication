using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;

namespace WebCosmeticApp.Models;
public class Cosmetic
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; }

    public string Brand { get; set; }

    public string Category { get; set; }

    [Required]
    public string Picture { get; set; } // Това ще съдържа линк към снимката

    public string Description { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }
}
