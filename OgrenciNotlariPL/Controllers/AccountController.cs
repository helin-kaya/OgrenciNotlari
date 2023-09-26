using Microsoft.AspNetCore.Identity;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using OgrenciNotlariBL.EmailSenderProcess;
using OgrenciNotlariEL.IdentityModels;
using OgrenciNotlariEL.ViewModels;
using OgrenciNotlariUL;
using OgrenciNotlariBL.InterfaceofManagers;
using OgrenciNotlariPL.Models;
using Microsoft.AspNetCore.Authorization;

namespace OgrenciNotlariPL.Controllers
{
    public class AccountController:Controller
    {
        public readonly SignInManager<AppUser> _signInManager;
        public readonly UserManager<AppUser> _userManager;
        public readonly IEmailManager _emailManager;
        public readonly IUserForgotPasswordTokensManager _userForgotPasswordTokensManager;
        public readonly IUserForgotPasswordsHistoricalManager _userForgotPasswordsHistoricalManager;
        private readonly IPasswordHasher<AppUser> _passwordHasher;

        public AccountController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager, IEmailManager emailManager, IUserForgotPasswordTokensManager userForgotPasswordTokensManager, IUserForgotPasswordsHistoricalManager userForgotPasswordsHistoricalManager, IPasswordHasher<AppUser> passwordHasher)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _emailManager = emailManager;
            _userForgotPasswordTokensManager = userForgotPasswordTokensManager;
            _userForgotPasswordsHistoricalManager = userForgotPasswordsHistoricalManager;
            _passwordHasher = passwordHasher;
        }

        [HttpGet]
        public IActionResult Register()
        {
            //string wwwPath = _environment.WebRootPath;
            //string contentPath = _environment.ContentRootPath;
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                //Aynı usernameden sistemde var mı? betul_aksan
                var sameUserName = _userManager.FindByNameAsync(model.Username).Result;
                if (sameUserName != null)
                {
                    ModelState.AddModelError("", "Bu kullanıcı ismi zaten sistemde kayıtlıdır!");
                    return View(model);
                }

                //program.csde ayarını yaptık --->>>>>opt.User.RequireUniqueEmail = true;

                ////Aynı emailden sistemde var mı?
                //var sameEmail = _userManager.FindByEmailAsync(model.Email).Result;
                //if (sameEmail != null)
                //{
                //    ModelState.AddModelError("", "Bu email zaten sistemde kayıtlıdır!");
                //    return View(model);
                //}


                //Artık bu sisteme üye olabilir

                AppUser user = new AppUser()
                {
                    Name = model.Name,
                    Surname = model.Surname,
                    Email = model.Email,
                    UserName = model.Username,
                    BirthDate = model.BirthDate,
                    LessonId = model.LessonId,
                    EmailConfirmed = false// Eğer zamanımız kalırsa bunu false yapıp aktivasyon işlemi ekleyeceğiz
                };

                var result = _userManager.CreateAsync(user, model.Password).Result;
                if (!result.Succeeded)
                {
                    ModelState.AddModelError("", "Yeni kişi kayıt edilemedi! Tekrar deneyiniz!");

                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                    return View(model);
                }
                // user kayıt edildi.

                //Biz historical tablo oluşturduk ama bir de araştırdık ki identity paketinde
                //bu olay zaten düşünülmüş.... :) Bu durumda histori tablosunu kullanmayabilirsiniz
                //
                UserForgotPasswordsHistoricalDTO u = new UserForgotPasswordsHistoricalDTO
                {
                    InsertedDate = DateTime.Now,
                    IsDeleted = false,
                    UserId = user.Id,
                    Password = user.PasswordHash
                };

                _userForgotPasswordsHistoricalManager.Add(u);
                //Usera rol atamasi yapilacaktir
                var roleResult = _userManager.AddToRoleAsync(user, AllRoles.WAITINGFORACTIVATION.ToString()).Result;

                var token = _userManager.GenerateEmailConfirmationTokenAsync(user).Result;
                var encToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(token));
                if (roleResult.Succeeded)
                {
                    var url = Url.Action("Activation", "Account", new { u = user.Id, t = encToken },
                        protocol: Request.Scheme);

                    _emailManager.SendEmail(new EmailMessageModel()
                    {
                        Subject = "Addressbook Aktivasyon İşlemi",
                        Body = $"<b>Merhaba {user.Name} {user.Surname},</b><br/>" +
                       $"Sisteme kaydınız başarılıdır! <br/>" +
                       $"Sisteme giriş yapmak için aktivasyonunuz gerçekleştirmek üzere <a href='{url}'>buraya</a> tıklayınız.",
                        To = user.Email
                    });

                    TempData["RegisterSuccessMsg"] = "Kayıt işlemi başarıdır!";

                    return RedirectToAction("Login", "Account", new { email = model.Email });

                }
                else
                {
                    TempData["RegisterFailMsg"] = "Kullanıcı kaydınız başarılıdır! Fakat rol ataması yapılamadığı için sistem yöneticisine başvurunuz!";
                    //log

                    return RedirectToAction("Login", "Account", new { email = model.Email });

                }



            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Beklenmedik bir sorun oldu!");
                //ex loglanacak
                return View(model);
            }
        }
        [HttpGet]
        public IActionResult Login(string? email)
        {


            return View("Login", email);
        }

        [HttpPost]
        public IActionResult Login(string email, string password)
        {
            try
            {
                if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
                {
                    ModelState.AddModelError("", "Lütfen gerekli alanları dolduurnuz!");
                    return View("Login", email);
                }
                var user = _userManager.FindByEmailAsync(email).Result;
                if (user == null)
                {
                    ModelState.AddModelError("", "Lütfen sisteme üye olduğunuza emin olunuz!");
                    return View("Login", email);
                }

                //Eğer aktivasyonunu yapmamış ise giriş yapamasın!
                if (_userManager.IsInRoleAsync(user, AllRoles.WAITINGFORACTIVATION.ToString()).Result)
                {
                    ModelState.AddModelError("", "DİKKAT! Sisteme giriş yapabilmeniz için emailinize gelen aktivasyon linkine tıklayınız! Aktivasyon işleminden sonra tekrar giriş yapmayı deneyiniz!");
                    return View("Login", email);

                }

                if (_userManager.IsInRoleAsync(user, AllRoles.PASSIVE.ToString()).Result)
                {
                    ModelState.AddModelError("", "DİKKAT! Sisteme giriş yapamazsınız. Çünkü kaydınızı sildirmişsiniz! Sistem yöneticisiyle görüşün!");
                    return View("Login", email);

                }
                if (_userManager.IsInRoleAsync(user, AllRoles.ADMIN.ToString()).Result)
                {
                    var signResult = _signInManager.PasswordSignInAsync(user, password, false, false).Result;

                    if (!signResult.Succeeded)
                    {
                        ModelState.AddModelError("", "Lütfen eposta veya şifrenizi doğru yazsfdasgn");
                        return View("Login", email);
                    }

                    return RedirectToAction("Index", "Admin");
                }
                else if (_userManager.IsInRoleAsync(user, AllRoles.MEMBER.ToString()).Result)
                {
                    var signResult = _signInManager.PasswordSignInAsync(user, password, false, false).Result;

                    if (!signResult.Succeeded)
                    {
                        ModelState.AddModelError("", "Lütfen eposta veya şifrenizi doğru yazsfdasgn");
                        return View("Login", email);
                    }

                    //Role göre sayfalara gidebilir.


                    return RedirectToAction("Indexim", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "DİKKAT! Sisteme giriş yapamazsınız. Çünkü rol atamanız yapılmamıştır. s. y.g.");
                    return View("Login", email);
                }


            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "Beklenmedik bir sorun oldu!");
                //ex loglanacak
                return View("Login", email);
            }
        }
        [HttpGet]
        [Authorize]
        public IActionResult Logout()
        {
            _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        [HttpGet]
        public IActionResult Activation(string u, string t)
        {
            try
            {
                var user = _userManager.FindByIdAsync(u).Result;
                if (user == null)
                {
                    TempData["ActivationFailMsg"] = "Aktivasyon işleminiz kullanıcı bulunamadığı için gerçekleşemedi!";
                    //ex loglanacak
                    return RedirectToAction("Login");
                }

                //user null değil
                if (user.EmailConfirmed)
                {
                    TempData["ActivationSuccessMsg"] = "Email aktivasyonunuz zaten gerçekleşmiştir! Sisteme giriş yapabilirsiniz!";
                    //ex loglanacak
                    return RedirectToAction("Login");
                }

                var token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(t));

                var confirmResult = _userManager.ConfirmEmailAsync(user, token).Result;

                if (!confirmResult.Succeeded)
                {
                    TempData["ActivationFailMsg"] = "Aktivasyon işleminiz gerçekleşmedi! Sistem yöneticisiyle görüşünüz!";
                    //ex loglanacak
                    return RedirectToAction("Login");

                }

                // aktivasyonu olduğuna göre ROLUNU değiştirelim
                // her birine .Result eklenip sonuçları if ile kontrol edilmelidir
                var deleteRole = _userManager.RemoveFromRoleAsync(user, AllRoles.WAITINGFORACTIVATION.ToString()).Result;

                var addRoleResult = _userManager.AddToRoleAsync(user, AllRoles.MEMBER.ToString()).Result;

                TempData["ActivationSuccessMsg"] = "Email aktivasyonunuz başarılı bir şekilde gerçekleşmiştir! Sisteme giriş yapabilirsiniz!";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                TempData["ActivationFailMsg"] = "Aktivasyon işleminde Beklenmedik bir sorun oluştu!";
                //ex loglanacak
                return RedirectToAction("Login");
            }
        }
    }
}
