using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner.Models
{
  public class Login
  {
    [Required]
    public string LoginEmail { get; set; }

    [Required]
    [DataType(DataType.Password)]
    public string LoginPassword { get; set; }
  }
}