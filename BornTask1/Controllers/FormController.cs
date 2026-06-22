using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BornTask1.Data;
using Microsoft.AspNetCore.Authorization;
using BornTask1.Dtos;
using BornTask1.Models;
using System.Security.Claims;

namespace BornTask1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FormController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FormController(ApplicationDbContext context)
        {
            _context = context;
        }


        [Authorize] //token kontrolu ıcın
        [HttpPost("save-form")]       
            
        
        public IActionResult SaveForm(FormRecordDto dto) // form kaydetme işi
        {
           
            
            if (dto.Date1 <= DateTime.Today)
            {
                return BadRequest("Tarih alanı bugünden sonraki bir tarih olmalıdır.");
            }        
            var userId=User.FindFirst(ClaimTypes.NameIdentifier)?.Value; // Burdaki User modeldekı user deği Controller ıcındekı user asp.net in olustrudugu gırıs yapmıs kullanıcı bılgısı .Kullanıcı id'sini alıyoruz
            if(userId == null)
            {
                return Unauthorized("Kullanıcı kimliği bulunamadı.");
            }
            FormRecord formRecord = new FormRecord(); 
            formRecord.Text1 = dto.Text1;
            formRecord.Num1 = dto.Num1;
            formRecord.Date1 = dto.Date1;
            formRecord.UserId=int.Parse(userId); // kullanıcı id'sini form kaydına ekliyoruz gelen userId string olduğu için int'e çeviriyoruz
            _context.FormRecords.Add(formRecord); //formu veritabanına ekledik 
            _context.SaveChanges();


            return Ok(new
            {
                Message = "Form başarıyla kaydedildi."
            });
        }

       
       

    }
}
