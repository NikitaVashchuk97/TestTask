using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Localization;
using System.ComponentModel.DataAnnotations;
using Test_task;
//using Test_task.Resources;

namespace Test_task.Models
{
    public class User
    {
        private IStringLocalizer<Resources.ErrorRes> sharedResources;
        public User()
        {

        }
        public int Id { get; set; }

        [StringLength(20, ErrorMessageResourceName = "InvalidStringLenght"/*, ErrorMessageResourceType = typeof(ErrorRes)*/)]
        [Required(ErrorMessageResourceName = "FieldIsRequired"/*, ErrorMessageResourceType = typeof(ErrorRes)*/)]
        public string Name { get; set; }

        [StringLength(20, ErrorMessageResourceName = "InvalidStringLenght"/*, ErrorMessageResourceType = typeof(ErrorRes)*/)]
        [Required(ErrorMessageResourceName = "FieldIsRequired"/*, ErrorMessageResourceType = typeof(ErrorRes)*/)]
        public string Surname { get; set; }

        [StringLength(30, ErrorMessageResourceName = "InvalidStringLenght"/*,/* ErrorMessageResourceType = typeof(ErrorRes)*/)]
        [EmailAddress(ErrorMessageResourceName = "InvalidMail"/*, ErrorMessageResourceType = typeof(ErrorRes)*/)]
        [Required(ErrorMessageResourceName = "FieldIsRequired"/*, ErrorMessageResourceType = typeof(ErrorRes)*/)]
        public string Mail { get; set; }

        [StringLength(30, ErrorMessageResourceName = "InvalidStringLenght"/*, ErrorMessageResourceType = typeof(ErrorRes)*/)]
        [Required(ErrorMessageResourceName = "FieldIsRequired"/*, ErrorMessageResourceType = typeof(ErrorRes)*/)]
        public string Password { get; set; }

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
