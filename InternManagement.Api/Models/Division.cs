namespace InternManagement.Api.Models
{
  public class Division
  {
    public int Id { get; set; }
    public string Name { get; set; }

    public Departement Department { get; set; }
  }
}