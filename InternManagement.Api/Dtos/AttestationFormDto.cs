using System;
using System.ComponentModel.DataAnnotations;

namespace InternManagement.Api.Dtos
{
  public class AttestationFormDto
  {
    [Required]
    public int InternId { get; set; }
    [Required]
    public string Code { get; set; }
    [Required]
    public DateTime Date { get; set; }
  }
}