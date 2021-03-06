using System.Collections.Generic;

namespace InternManagement.Api.Models
{
  public class Department
  {
    public int Id { get; set; }
    public string Name { get; set; }

    public IList<Division> Divisions { get; set; }
    public int LocationId { get; set; }
    public Location Location { get; set; }
  }
}