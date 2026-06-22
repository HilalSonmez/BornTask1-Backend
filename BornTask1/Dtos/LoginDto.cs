using System.ComponentModel.DataAnnotations;
namespace BornTask1.Dtos
{
    public class LoginDto
    {
        [Required(ErrorMessage = "E-mail alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-mail adresi giriniz.")]
        public string Email { get; set; }


        [Required(ErrorMessage = "Şifre alanı zorunludur.")]
        public string Password { get; set; }
    }
}
