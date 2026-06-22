using System.ComponentModel.DataAnnotations;
namespace BornTask1.Dtos
{
    public class ForgotPasswordDto
    {
        [Required(ErrorMessage = "E-mail alanı zorunludur.")]
        [EmailAddress(ErrorMessage = "Geçerli bir e-mail adresi giriniz.")] //girilenen değerin e-mail formatında olup olmadığını kontrol eder
        public string Email { get; set; } = string.Empty; //kullanıcının e-mail adresini alacak
    }

}
