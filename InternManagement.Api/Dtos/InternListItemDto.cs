using InternManagement.Api.Enums;

namespace InternManagement.Api.Dtos
{
  public class InternListItemDto
  {
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Division { get; set; }
    public eInternState State { get; set; }
  }
}