using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternManagement.Api.Models
{
    public class Preferences
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int nInterns { get; set; }
        public int nAttendanceDays { get; set; }
    }
}