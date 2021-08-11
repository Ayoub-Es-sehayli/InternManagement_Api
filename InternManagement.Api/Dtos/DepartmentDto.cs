using System.Collections.Generic;

namespace InternManagement.Api.Dtos
{
  public class DepartmentDto
  {
    public int Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<DivisionDto> Divisions { get; set; }
  }
}