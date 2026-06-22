using System.ComponentModel.DataAnnotations;
namespace BornTask1.Dtos
{
    public class ResetPasswordDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty; //kullanıcının hangi hesabın şifresini değiştireceğini söyleyecek

        [Required]
        public string Code { get; set; } = string.Empty;

        [Required]
        [MinLength(6, ErrorMessage = "Şifre en az 6 karakter olmalıdır.")]
        public string NewPassword { get; set; } = string.Empty;
    }
}
