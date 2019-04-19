using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models
{
  public class Registration
  {
    [Key]
    public int UserId { get; set; }
    [Required]
    public string FirstName { get; set; }

    [Required]
    public string LastName { get; set; }

    [Required]
    public string Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    [MinLength(8, ErrorMessage="Password must be 8 characters or longer!")]
    public string Password { get; set; }

    [NotMapped]
    [Compare("Password")]
    [DataType(DataType.Password)]
    public string Confirm { get; set; }
  }
}