using System.ComponentModel.DataAnnotations;

namespace OrixNetCoreApp.ModelDto
{
    public class LoginDto
    {
      
        [Required]
        [EmailAddress(ErrorMessage = "รูปแบบอีเมลล์ไม่ถูกต้อง")]
        public string Email { get; set; } = null!;
        [Required]
        [StringLength(100, ErrorMessage = "รหัสผ่านอย่างน้อย {2} ขึ้นไป และไม่เกิน{1} ตัวอักษร", MinimumLength = 3)]
        public string Password { get; set; } = null!;
    }
}
