using System;

namespace WeddingPlanner.Models
{
  // the createdAt and updatedAt will be repeatedly used, so place it in a abstract class
  public abstract class DataModel
  {
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;
  }
}