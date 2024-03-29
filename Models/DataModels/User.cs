using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner.Models
{
  public class User
  {
    [Key]
    public int UserId { get; set; }
    [Required]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "First name must be longer than 3 characters!")]
    public string FirstName { get; set; }

    [Required]
    [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name must be longer than 2 characters!")]
    public string LastName { get; set; }

    [EmailAddress]
    [Required]
    public string Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    // [RegularExpression(@"^((?=.*[a-z])(?=.*[A-Z])(?=.*\d)).+$", ErrorMessage = "Password must contain atleast 1 uppercase, 1 lowercase and 1 number!")]
    [StringLength(255, MinimumLength = 8, ErrorMessage = "Password must at least 8 characters long!")]
    // [MinLength(8, ErrorMessage="Password must be 8 characters or longer!")]
    public string Password { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    [NotMapped]
    [Compare("Password")]
    [DataType(DataType.Password)]
    public string Confirm { get; set; }

    //nav properties go here
    public List<Association> UserWeddings { get; set; }
  }
}