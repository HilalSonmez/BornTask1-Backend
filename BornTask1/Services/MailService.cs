using Microsoft.Extensions.Configuration;
using System.Net;//ağ üzerinden veri alışverişi için gerekli kütüphane
using System.Net.Mail;//mail gönderme işleri için gerekli kütüphane
namespace BornTask1.Services
{
    public class MailService
    {
        private readonly IConfiguration _configuration; //appsettings.json dosyasındaki ayarlara erişmek için yazdım

        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        //Mail gönderme işlemlerini aşağıda yaptım
        public void SendEmail(string toEmail, string subject, string body)// mail kıme gıdecek konu gövde
        {
            var smtpClient = new SmtpClient(); //gmaille konusacak nesne ürettik

            smtpClient.Host = _configuration["SmtpSettings:Host"]; //hangı sunucuya bağlanacak onu appsettings.json dosyasından aldık
            smtpClient.Port = Convert.ToInt32(_configuration["SmtpSettings:Port"]);//_configuration["SmtpSettings:Port"] string olarak geldi onu int e cevırdık 587 yi getircek gmailin mail gönderme servisinin portu dinlediği kapı yani

            smtpClient.EnableSsl = true; //Ssl şifreli baglantı

            //NetworkCredential sınıfı, bir ağ kaynağına erişim sağlamak için kullanılan kullanıcı adı ve parola bilgilerini temsil eder.
            smtpClient.Credentials = new NetworkCredential(
                _configuration["SmtpSettings:SenderEmail"],
                _configuration["SmtpSettings:SenderPassword"]);
            // mail mesajı için nesne olusturdum
            var mailMessage = new MailMessage();

            mailMessage.From = new MailAddress(
            _configuration["SmtpSettings:SenderEmail"],

            _configuration["SmtpSettings:SenderName"]);

            mailMessage.To.Add(toEmail);

            mailMessage.Subject = subject;

            mailMessage.Body = body;

            smtpClient.Send(mailMessage); //postaya verme işlemi
        }
    }
}

