using System;

namespace InternManagement.Api.Models
{
  public class Decision
  {
    public int Id { get; set; }
    public int InternId { get; set; }
    public string Code { get; set; }
    public DateTime Date { get; set; }
  }
}