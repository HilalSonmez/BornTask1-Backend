namespace BornTask1.Models
{
    public class FormRecord
    {
        public int Id { get; set; }
        public string Text1 { get; set; } = string.Empty;
        public int Num1 { get; set; }
        public DateTime Date1 { get; set; }

        public int UserId { get; set; } // Form dolduran kullanıcının Id si  
        public User User { get; set; } = null!; // FormRecord ile User arasında ilişki var 

    }
}
