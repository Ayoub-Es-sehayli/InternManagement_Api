using System;

namespace InternManagement.Api.Dtos
{
    public class AttestationDto
    {
        public string Template { get; set; }
        public int CurrentYear { get; set; } = DateTime.Now.Year;
        public string FullName { get; set; }
        public string Location { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }


    }
}