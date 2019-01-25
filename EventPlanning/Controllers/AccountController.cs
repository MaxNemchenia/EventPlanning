using EventPlanning.BLL.DTO;
using EventPlanning.BLL.Infrastructure;
using EventPlanning.BLL.Interfaces;
using EventPlanning.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Net.Mail;

namespace EventPlanning.Controllers
{
    public class AccountController : Controller
    {
        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }


        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }



        public ActionResult Login()
        {
            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginModel model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                UserDTO userDto = new UserDTO { Email = model.Email, Password = model.Password, ConfirmedEmail = UserService.IsEmailConfirmed(model.Email) };
                ClaimsIdentity claim = await UserService.Authenticate(userDto);
                if (claim == null)
                {
                    ModelState.AddModelError("", "Wrong password or user name");
                }
                else
                {
                    if (userDto.ConfirmedEmail == true)
                    {
                        AuthenticationManager.SignOut();
                        AuthenticationManager.SignIn(new AuthenticationProperties
                        {
                            IsPersistent = true
                        }, claim);

                        return RedirectToAction("EventForm", "Event");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Email doesn't confirmed");
                    }
                }
            }
            return View(model);
        }


        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("EventForm", "Event");
        }


        public ActionResult Register()
        {
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterModel model)
        {
            await SetInitialDataAsync();
            if (ModelState.IsValid)
            {
                var userDto = new UserDTO()
                {
                    ConfirmedEmail = false,
                    Email = model.Email,
                    Password = model.Password,
                    Role = "user"
                };

                OperationDetails operationDetails = await UserService.Create(userDto);
                if (operationDetails.Succedeed)
                {
                    MailAddress from = new MailAddress("EventPlanningTask@gmail.com", "Web Registration");
                    MailAddress to = new MailAddress(userDto.Email);
                    MailMessage m = new MailMessage(from, to);
                    m.Subject = "Email confirmation";
                    //
                    m.Body = string.Format("Для завершения регистрации перейдите по ссылке:" +
                                    "<a href=\"{0}\" title=\"Подтвердить регистрацию\">{0}</a>",
                        Url.Action("ConfirmEmail", "Account", new { Token = userDto.Id, Email = userDto.Email }, Request.Url.Scheme));
                    m.IsBodyHtml = true;
                    SmtpClient smtp = new System.Net.Mail.SmtpClient("smtp.gmail.com", 587);
                    smtp.EnableSsl = true;
                    smtp.Credentials = new System.Net.NetworkCredential("EventPlanningTask@gmail.com", "qweqaz12345");
                    smtp.Send(m);
                    return RedirectToAction("Confirm", "Account", new { Email = userDto.Email });
                }
                else
                    ModelState.AddModelError(operationDetails.Property, operationDetails.Message);
            }

            return View(model);
        }


        [AllowAnonymous]
        public ActionResult Confirm(string Email)
        {
            ViewBag.Email = Email;
            return View();
        }


        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string Token, string Email)
        {
            UserDTO user = UserService.FindById(Token);
            if (user != null)
            {
                if (user.Email == Email)
                {
                    user.ConfirmedEmail = true;
                    await UserService.MailConfirmation(user);
                    ClaimsIdentity claim = await UserService.Authenticate(user);
                    AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);

                    return RedirectToAction("EventForm", "Event", new { ConfirmedEmail = user.Email });
                }
                else
                {
                    return RedirectToAction("Confirm", "Account", new { Email = user.Email });
                }
            }
            else
            {
                return RedirectToAction("Confirm", "Account", new { Email = "" });
            }
        }



        private async Task SetInitialDataAsync()
        {
            await UserService.SetInitialData(new UserDTO
            {
                Email = "admin",
                UserName = "admin",
                Password = "123456",
                ConfirmedEmail = true,
                Role = "admin",
            }, new List<string> { "user", "admin" });
        }
    }
}