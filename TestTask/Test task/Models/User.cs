using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Localization;
using System.ComponentModel.DataAnnotations;
using Test_task;

namespace Test_task.Models
{
    public class User
    {
        public int Id { get; set; }
        
        [StringLength(20, ErrorMessage = "InvalidStringLenght")]
        [Required(ErrorMessage = "FieldIsRequired")]
        public string Name { get; set; }

        [StringLength(20, ErrorMessage = "InvalidStringLenght")]
        [Required(ErrorMessage = "FieldIsRequired")]
        public string Surname { get; set; }

        [StringLength(30, ErrorMessage = "InvalidStringLenght")]
        [EmailAddress(ErrorMessage = "InvalidMail")]
        [Required(ErrorMessage = "FieldIsRequired")]
        public string Mail { get; set; }

        [StringLength(30, ErrorMessage = "InvalidStringLenght")]
        [Required(ErrorMessage = "FieldIsRequired")]
        public string Password { get; set; }

        public string Language { get; set; }

        public bool IsAdmin { get; set; }

        public override bool Equals(object other)
        {
            var toCompareWith = other as User;
            if (toCompareWith == null)
                return false;
            return this.Id == toCompareWith.Id &&
                this.Name == toCompareWith.Name &&
                this.Surname == toCompareWith.Surname &&
                this.Mail == toCompareWith.Mail &&
                this.Password == toCompareWith.Password;
        }
    }
}
