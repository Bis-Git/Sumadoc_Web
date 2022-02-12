
using Microsoft.AspNetCore.Mvc;
using OICAR_Web.Models;
using SustavZaLijecnike;
using SustavZaLijecnike.DAL;
using System.Diagnostics;

namespace OICAR_Web.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {
        }

        public IActionResult Login()
        {
            return View("Login");
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (username == null)  username = "";
            if (password == null) password = "";

            Kredencijal kredencijal = Repository.GetKredencijali(username);
            if(kredencijal != null && HashSalt.IsEqual(password, kredencijal.Hash, kredencijal.Salt))
            {
                var pacijent = Repository.GetPacijent(username, password);
                if (pacijent != null)
                {
                    string userId = pacijent.IDPacijent.ToString();
                    HttpContext.Session.SetString("userId", userId);

                    return RedirectToAction("Profil", "Profil", null);
                }
                else
                {
                    return View("Login");
                }
            }
            return View("Login");
        }

        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("userId");
            return RedirectToAction("Login");

        }

    }
}