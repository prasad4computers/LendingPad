using System.Collections.Generic;
using BusinessEntities;
using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Users
{
    public class UserModel
    {
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Email format is invalid.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "User type is required.")]
        public UserTypes Type { get; set; }
        [Required(ErrorMessage = "Annual Salary is required.")]
        [Range(0, double.MaxValue, ErrorMessage = "Annual salary must be non-negative.")]
        public decimal? AnnualSalary { get; set; }
        public IEnumerable<string> Tags { get; set; }
    }
}