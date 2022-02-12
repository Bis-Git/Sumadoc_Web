
using Microsoft.AspNetCore.Mvc;
using SustavZaLijecnike;
using System.Dynamic;

namespace OICAR_Web.Controllers
{
    public class ProfilController : Controller
    {
        [HttpGet]
        public IActionResult Profil()
        {
            int userId = getCurrentUserId();

            Pacijent pacijent = Repository.GetPacijentByID(userId);

            return View("Profil",pacijent);
        }

        [HttpPost]
        public IActionResult Profil(string ime, string prezime, string email, string ulicaBroj, string grad, string kontakt)
        {
            int userId = getCurrentUserId();
            Pacijent pacijent = Repository.GetPacijentByID(userId);

            pacijent.Ime = ime;
            pacijent.Prezime = prezime;
            pacijent.Email = email;
            pacijent.Prebivaliste.UlicaBroj = ulicaBroj;
            pacijent.Prebivaliste.Grad = grad;
            pacijent.Telefon = kontakt;

            Repository.UpdatePacijent(pacijent);
           
            return View("Profil",pacijent);
        }

        [HttpGet]
        public IActionResult DeleteProfil()
        {
            int userId = getCurrentUserId();

            Repository.DeletePacijent(userId);

            HttpContext.Session.Remove("userId");

            return RedirectToAction("Login", "Home");
        }

        private int getCurrentUserId()
        {
            string? userId = HttpContext.Session.GetString("userId");
            if (userId == null) return 0;
            return int.Parse(userId);

        }
    }
}
