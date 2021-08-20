using System;

namespace InternManagement.Api.Dtos
{
  public class CancellationFormDto
  {
    public int InternId { get; set; }
    public string Code { get; set; }
    public DateTime Date { get; set; }
  }
}