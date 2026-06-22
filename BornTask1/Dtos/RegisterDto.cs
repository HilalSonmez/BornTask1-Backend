using System.ComponentModel.DataAnnotations;
namespace BornTask1.Dtos

{
    public class RegisterDto
    {
        [Required(ErrorMessage = "E-mail alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-mail adresi giriniz.")]
        public string Email { get; set; } = string.Empty;

        [Required(ErrorMessage = "Şifre alanı zorunludur.")]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
        public string Password { get; set; }= string.Empty;
    }
}
