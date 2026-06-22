using BornTask1.Data; //DbContextimi burda kullanmak için ekledim
using BornTask1.Dtos;
using BornTask1.Models;
using BornTask1.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BornTask1.Controllers //Login Register Forgot Password işlemlerini burda yapacağım
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDbContext _context; // veritabanına erişmek için yazdım

        private readonly IConfiguration _configuration; // appsettings.json dosyasındaki ayarlara erişmek için yazdım
        private readonly MailService _mailService; //mail gönderme işlemleri için yazdım nesne olusturdum
        public AuthController(ApplicationDbContext context, IConfiguration configuration, MailService mailService)
        {
            _context = context; //_context veritabanı alanıydı context dışardan gelen nesneydi onu alanın içine yerleştirdim (Kendime Not)
            _configuration = configuration; //uygulama ayarlarına erişmek için configuration nesnesini alanın içine yerleştirdim (Kendime Not)

            _mailService = mailService;
        }


        [HttpGet]
        [HttpPost("register")]
        public IActionResult Register(RegisterDto dto)
        {
            /* bu kısmı dtolarda otomatık kontrol ettırdık if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
             {
                 return BadRequest("Email ve şifre girmek zorunludur");
             }*/
            var existingUser = _context.Users.FirstOrDefault(x => x.Email == dto.Email);//swaggerdan gelen maille tablodakı maili karsılastırdım

            if (existingUser != null)
            {
                return BadRequest("Bu E-mail adresi zaten kayıtlı");
            }

            
            User user = new User();

            user.Email = dto.Email;//dto dakı emaili usera aktardım

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            user.IsEmailConfirmed = false;

            user.EmailConfirmationCode = new Random().Next(100000, 999999).ToString();

            _context.Users.Add(user); //Users tablosuna user nesnesını eklemek ıstıyorum

            _context.SaveChanges();// bu veritabanına kaydedıyor. Bunun nedenı toplu olarak kaydetmekmiş devamlı baglantı kurup kaydetmıyormus !!!!

            _mailService.SendEmail(
             user.Email, //maili göndermek istediğim kullanıcı maili
            "Email Doğrulama", //mail konusu
            $"Doğrulama kodunuz: {user.EmailConfirmationCode}"); //mail içeriği doğrulama kodunu gönderdim

            return Ok(new
            {
                Message = "Kullanıcı başarıyla eklendi. Doğrulama kodu e-mail adresinize gönderildi."
            });


        }
        [HttpPost("login")]

        public IActionResult Login(LoginDto dto)
        {

            /* if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
             {
                 return BadRequest("E-mail adresi ve şifre zorunludur");
             }*/
            var user = _context.Users.FirstOrDefault(x => x.Email == dto.Email);

            if (user == null)
            {
                return BadRequest("Bu E-mail adresi ile kayıtlı bir kullanıcı bulunamadı");
            }

            if (!BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash)) //verify girilen şifreyi hashlenmiş şifre ile karşılaştırıyor
            {
                return BadRequest("Şifre hatalı");
            }
            if (!user.IsEmailConfirmed) //kullanıcı emailini doğrulamadıysa
            {
                return BadRequest("E-mail doğrulaması yapılmadan giriş yapılamaz");
            }

            var token = GenerateJwtToken(user);//gelecek token degerını token degıskenıne atadım

            //burayaa geri dönucem
            return Ok(new
            {
                Message = "Başarıyla giriş yapıldı",
                Token = token,
                UserId = user.Id,
                Email = user.Email

            });

        }
        [HttpPost("confirm-email")]
        public IActionResult ConfirmEmail(ConfirmEmailDto dto)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == dto.Email);
            if (user == null)
            {
                return BadRequest("Bu E-mail adresi ile kayıtlı bir kullanıcı bulunamadı");
            }
            if (user.EmailConfirmationCode != dto.Code) //swaggerdan gelen kod ile tablodaki kodu karsılastırdım
            {
                return BadRequest("E-mail doğrulama kodu hatalı");
            }
            user.IsEmailConfirmed = true;

            user.EmailConfirmationCode = null; // doğrulama kodunu sıfırlıyorum çünkü artık kullanmaya gerek yok

            _context.SaveChanges();
            return Ok(new
            {
                Message = "Doğrulama başarılı"
            });
        }

        [HttpPost("forgot-password")]

        public IActionResult ForgotPassword(ForgotPasswordDto dto)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == dto.Email); //ilk eşleşen kullanıcıyı getır yoksa null döndür.Veritabanındakı mail ile gelen maili karsılastır
            if (user == null)
            {
                return BadRequest("Bu e-mail adresi ile kayıtlı kullanıcı bulunamadı.");
            }

            user.PasswordResetCode = new Random().Next(100000, 999999).ToString();

            _context.SaveChanges();

            _mailService.SendEmail(user.Email, "Şifre Sıfırlama", $"Şifre sıfırlama kodunuz:{user.PasswordResetCode}");


            return Ok(new
            {
                Message = "Şifre sıfırlama kodu e-mail adresinize gönderildi"
            });

        }

        [HttpPost("reset-password")]

        public IActionResult ResetPassword(ResetPasswordDto dto)
        {
            var user = _context.Users.FirstOrDefault(x => x.Email == dto.Email);
            if (user == null)
            {
                return BadRequest("Bu e-mail adresi ile kayıtlı kullanıcı bulunamadı.");
            }

            if (user.PasswordResetCode != dto.Code)
            {
                return BadRequest(" Girdiğiniz şifre sıfırlama kodu yanlış");
            }

            
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.NewPassword);// userın passsword hashıne yenı passwordun hashını atadım yerdegısrırı

            user.PasswordResetCode = null; //artık reset kodu sılınsın işi bitti

            _context.SaveChanges();

            return Ok(new
            {
                Message = "Şifre başarıyla güncellendi"
            });
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new[] //token içine koymak istediğim bilgileri claim olarak tanımladım
    {
        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),// kullanıcı id bilgisini koy
        new Claim(ClaimTypes.Email, user.Email)// kullanıcı email bilgisini koy
    };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["Jwt:Key"])); //appsettings.json dosyasındaki Jwt anahtarı

            var credentials = new SigningCredentials( //tokenı imzalarken ,şu anahtarı kullan ve  şu algoritmayı kullan demek
                key, //gizli anahtar
                SecurityAlgorithms.HmacSha256);//imzalama algoritması

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],//tokenı kim oluşturdu
                audience: _configuration["Jwt:Audience"],// token kimin için oluşturuldu
                claims: claims, //token içine koymak istediğim bilgiler
                expires: DateTime.Now.AddMinutes(
                    Convert.ToDouble(_configuration["Jwt:ExpireMinutes"])),//tokenın geçerlilik süresi
                signingCredentials: credentials);//Token hangi anahtarla ve hangi algoritmayla imzalandı

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        /*
        [Authorize]
        [HttpGet("profile")] deneme amaclı yazıldı
        public IActionResult Profile()
        {
            return Ok("Bu endpoint'e sadece giriş yapan kullanıcı erişebilir");
        }
        */
    }
}
