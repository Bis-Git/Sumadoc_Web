
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SustavZaLijecnike;
using System.Dynamic;

namespace OICAR_Web.Controllers
{
    public class PodatciController : Controller
    {

        public IActionResult PovijestBolesti()
        {
            int? userId = getCurrentUserId();
            if (userId == null) return RedirectToAction("Login", "Home");

            IEnumerable<Recept> recepti = Repository.GetRecepti((int)userId);
            IEnumerable<Pregled> pregledi = Repository.GetPregledi((int)userId);

            List<Dijagnoza> userDijagnoze = new List<Dijagnoza>();

            recepti.ToList().ForEach(terapija =>
            {
                userDijagnoze.Add(terapija.Dijagnoza);
            });

            pregledi.ToList().ForEach(pregled =>
            {
                userDijagnoze.Add(pregled.Dijagnoza);
            });

            return View("PovijestBolesti",userDijagnoze);
        }

        public IActionResult PovijestPregleda()
        {
            int? userId = getCurrentUserId();
            if (userId == null) return RedirectToAction("Login", "Home");

            IEnumerable<Pregled> pregledi = Repository.GetPregledi((int)userId);
            return View("PovijestPregleda",pregledi);
        }

        public IActionResult Terapije()
        {
            int? userId = getCurrentUserId();
            if (userId == null) return RedirectToAction("Login", "Home");

            IEnumerable<Recept> recepti = Repository.GetRecepti((int)userId);
           
            return View("Terapije", recepti);
        }

        public IActionResult Termini()
        {
            int? userId = getCurrentUserId();
            if (userId == null) return RedirectToAction("Login", "Home");

            IEnumerable<Termin> termini = Repository.GetTerminiPacijenta((int)userId);

            IEnumerable<Termin> terminiReverse = termini.Reverse();

            return View("Termini", terminiReverse);
        }

        public IActionResult Uputnice()
        {
            int? userId = getCurrentUserId();
            if (userId == null) return RedirectToAction("Login", "Home");

            IEnumerable<Uputnica> uputnice = Repository.GetUputnice((int)userId);
            return View("Uputnice", uputnice);
        }

        public IActionResult Cijepljenja()
        {
            int? userId = getCurrentUserId();
            if (userId == null) return RedirectToAction("Login", "Home");

            IEnumerable<Cijepljenje> cjepljenja = Repository.GetCijepljenja((int)userId);
           
            return View("Cijepljenja", cjepljenja);
        }


        private int? getCurrentUserId()
        {
            string? userId = HttpContext.Session.GetString("userId");
            if (userId == null) userId = "0";

            return int.Parse(userId);
        }

    }
}
