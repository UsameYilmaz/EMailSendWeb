using EMailSendWeb.DTO;
using EMailSendWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace EMailSendWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly EmailDbContext _context;

        public HomeController(ILogger<HomeController> logger,EmailDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
       
        //private JsonResult UploadFile(IFormFile file)
        //{
            
        //        var fileName = Path.GetFileName(file.FileName);
        //        var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\txt", fileName);
        //        using (var fileStream = new FileStream(filePath, FileMode.Create))
        //        {
        //             file.CopyToAsync(fileStream);
        //        }
        //        string[] lines = System.IO.File.ReadAllLines(filePath);
        //        return Json(lines.ToList());
           
        //}

        [HttpPost]
        public ActionResult UploadFile(IFormFile file)
        {
            var fileName = Path.GetFileName(file.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"wwwroot\txt", fileName);//file alındı
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyToAsync(fileStream);
            }
            string[] lines = System.IO.File.ReadAllLines(filePath);//dosya ierisin lines atandı.
            return Json(lines);//veri json  olarak gonderildi.
        }
        [HttpPost]
        public ActionResult Index(EmailDTO model, IFormFile file)
        {
            for (int i = 0; i < model.EPosta.Count; i++)
            {
                MailMessage mailim = new MailMessage();
                mailim.To.Add(model.EPosta[i]);
                mailim.From = new MailAddress("usameyilmz@gmail.com");
                mailim.Subject = "Bize Ulaşın Sayfasından Mesajınız Var. " + model.Baslik;
                mailim.Body = "Sayın yetkili, " + model.AdSoyad + " kişisinden gelen mesajın içeriği aşağıdaki gibidir. https://www.picussecurity.com/ <br>" + model.Icerik;
                mailim.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);
                smtp.Credentials = new NetworkCredential("usame.kasibeyaz@gmail.com", "Usame334q");
                smtp.EnableSsl = true;
                smtp.UseDefaultCredentials = false;

                try
                {
                    smtp.Send(mailim);
                    TempData["Message"] = "Mesajınız iletilmiştir. En kısa zamanda size geri dönüş sağlanacaktır.";

                    var emailDTO = new Email{
                        Baslik = model.Baslik,
                        Icerik = model.Icerik,
                        AdSoyad = model.AdSoyad,
                        EPosta = model.EPosta[i],
                        SendDate = DateTime.Now
                    };
                    _context.Emails.Add(emailDTO);
                    _context.SaveChanges();//veritabanına gönderme
                }
                catch (Exception ex)
                {
                    TempData["Message"] = "Mesaj gönderilemedi.Hata nedeni:" + ex.Message;
                }
            }
            return View();

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
