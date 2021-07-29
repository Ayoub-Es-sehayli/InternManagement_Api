using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InternManagement.Api.Dtos;
using InternManagement.Api.Enums;

namespace InternManagement.Api.Models
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Email { get; set; }
        public eUserRole Role { get; set; }
        public string Password { get; set; }
    }
}