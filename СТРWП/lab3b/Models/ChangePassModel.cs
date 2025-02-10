using System.ComponentModel.DataAnnotations;

namespace lab3b.Models
{
    public class ChangePassModel
    {
        [Required][DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required][DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmNewPassword { get; set; }
    }
}

