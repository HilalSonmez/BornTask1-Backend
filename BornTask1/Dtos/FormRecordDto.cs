using System.ComponentModel.DataAnnotations;
namespace BornTask1.Dtos
{
    public class FormRecordDto
    {
        [Required(ErrorMessage = "Metin alanı boş olamaz.")]
        [MaxLength(100, ErrorMessage = "Metin alanı 100 karakterden uzun olamaz.")]
        public string Text1 { get; set; } = string.Empty;

        [Range(50, 100, ErrorMessage = "Sayı alanı 50 ile 100 arasında olmalıdır.")]
        public int Num1 { get; set; }

        public DateTime Date1 { get; set; }
    }
}
