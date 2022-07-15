using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Dto.UserDto
{
    public class LoginRequest
    {
        [Required(ErrorMessage = "{0} وارد نشده است")]
        [StringLength(100)]
        [DisplayName("نام کاربری")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} وارد نشده است")]
        [StringLength(500)]
        [DisplayName("کلمه عبور")]
        public string Password { get; set; }
    }
}
