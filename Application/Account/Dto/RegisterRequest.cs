using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Application.Account.Dto
{
    public class RegisterRequest
    {
        [Required(ErrorMessage = "{0} وارد نشده است")]
        [StringLength(100)]
        [DisplayName("نام کاربری")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} وارد نشده است")]
        [StringLength(500)]
        [DisplayName("کلمه عبور")]
        public string Password { get; set; }

        [Required(ErrorMessage = "{0} وارد نشده است")]
        [StringLength(100)]
        [DisplayName("نام و نام خانوادگی")]
        public string FullName { get; set; }

        [DisplayName("سن")]
        public int Age { get; set; }

        [Required(ErrorMessage = "{0} وارد نشده است")]
        [EmailAddress]
        [DisplayName("ایمیل")]
        public string Email { get; set; }
    }
}
