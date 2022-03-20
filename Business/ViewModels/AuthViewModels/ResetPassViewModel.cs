using System.ComponentModel.DataAnnotations;

namespace Business.ViewModels.AuthViewModels
{
    public class ResetPassViewModel
    {
        public string Email { get; set; }
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [DataType(DataType.Password), Compare(nameof(NewPassword))]
        public string ConfirmPassword { get; set; }
        public string Token { get; set; }
    }
}
