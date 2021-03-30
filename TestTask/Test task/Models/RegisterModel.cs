using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Test_task.Models
{
    public class RegisterModel
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

        //[DataType(DataType.Password)]
        //[Compare("Password", ErrorMessage = "Пароль введен неверно")]
        [StringLength(30, ErrorMessage = "InvalidStringLenght")]
        [Required(ErrorMessage = "FieldIsRequired")]
        public string ConfirmPassword { get; set; }
    }
}
