using System;
using System.ComponentModel.DataAnnotations;

namespace InternManagement.Api.Dtos
{
  public class LatestInternDto
  {
    public int Id { get; set; }
    public string FullName { get; set; }

    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }
  }
}