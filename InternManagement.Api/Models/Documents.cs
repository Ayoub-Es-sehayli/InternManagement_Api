using InternManagement.Api.Enums;

namespace InternManagement.Api.Models
{
  public class Documents
  {
    public int Id { get; set; }
    public eDocumentState CV { get; set; }
    public eDocumentState Letter { get; set; }
    public eDocumentState Insurance { get; set; }
    public eDocumentState Convention { get; set; }
    public eDocumentState Report { get; set; }
    public eDocumentState EvaluationForm { get; set; }
  }
}