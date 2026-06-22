namespace BornTask1.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public bool IsEmailConfirmed { get; set; } = false; //Başlangıçta false yaptım onaylananınca true ya dönecek.
        public string? EmailConfirmationCode { get; set; } //burdakı soru işareti null olabileceğini gösteriyor. Yani kullanıcıya mail gönderilmediğinde null olabilir.

        public string? PasswordResetCode { get; set; } //null olabılır

    }
}
