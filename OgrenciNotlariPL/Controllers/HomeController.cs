using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OgrenciNotlariBL.EmailSenderProcess;
using OgrenciNotlariBL.ImplementationofManagers;
using OgrenciNotlariBL.InterfaceofManagers;
using OgrenciNotlariEL.Entities;
using OgrenciNotlariEL.ViewModels;
using OgrenciNotlariPL.Models;

namespace OgrenciNotlariPL.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public readonly IStudentManager _studentManager;
        

        public HomeController(ILogger<HomeController> logger, IStudentManager studentManager)
        {
            _logger = logger;
            _studentManager = studentManager;
        }

        //public HomeController(ILogger<HomeController> logger, StudentManager studentManager)
        //{
        //    _logger = logger;
        //    _studentManager = studentManager;
        //}

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Indexim()
        {
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
        [Authorize]

        public IActionResult Students()
        {
            try
            {
                var model = _studentManager.GetAll().Data;
                return View(model);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Beklenmedik sorun oluştu!");
                //_logger.LogError(ex, "HATA: Home/Students");
                return View();
            }

        }
        [Authorize]
        public IActionResult AddStudent()
        {
            return View();
        
        }

        [Authorize]
        [HttpPost]
        public IActionResult AddStudent(StudentDTO model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }
                if(_studentManager.GetAll(x=>x.StudentNumber == model.StudentNumber).Data.Count > 0)
                {
                    ModelState.AddModelError("", $"{model.Name} isimli öğrenci zaten eklidir!");
                    return View(model);
                }
                if (_studentManager.Add(model).IsSuccess)
                {
                    TempData["StudentAddSuccessMsg"] = $"{model.Name} Öğrenci Eklendi! ";

                    //#region MailGonderelim
                    //EmailMessageModel m = new EmailMessageModel()
                    //{
                    //    To = user.Email,
                    //    Subject = $"Human Group Telefon Rehberi - Rehbere Yeni Kişi Eklendi!",
                    //    Body = $"<p>Merhaba {user.Name} {user.Surname}, </p> <br/> <p> Rehberinize {model.PhoneGroupNameSurname} adlı kişiyi eklediniz. </p>"
                    //};

                    //_emailSender.SendEmail(m);

                    //#endregion

                    return RedirectToAction("Students", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Yeni öğrenci eklenemedi! Tekrar deneyiniz!");
                    return View(model);
                }
            }
            catch
            {
                ModelState.AddModelError("", "Beklenmedik bir hata oluştu!");
                return View(model);
            }
        

        }
    }
}