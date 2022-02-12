using Microsoft.AspNetCore.Mvc;
using SustavZaLijecnike;
using System.Dynamic;

namespace OICAR_Web.Controllers
{
    public class NarudzbeController : Controller
    {
        [HttpGet]
        public IActionResult Termin(DateTime? date)
        {
            if (date == null)
            {
                date = DateTime.Now.Date;
            }


            int? userId = getCurrentUserId();
            if (userId == null) return RedirectToAction("Login", "Home");

            Pacijent pacijent = Repository.GetPacijentByID((int)userId);

            IEnumerable<Termin> termini = Repository.GetTerminiLijecnika(pacijent.Lijecnik.IDZaposlenik);
            IEnumerable<string> predefiniraniTermini = Repository.GetPredefiniraniTermini();

            var filteredTermini = termini.Where(x => x.Datum == date);
            List<string> slobodniTermini = predefiniraniTermini.ToList();
            foreach (var item in filteredTermini)
            {
                slobodniTermini.RemoveAll(x => x.Contains(item.Vrijeme));
            }

            dynamic data = new ExpandoObject();

            data.pacijent = pacijent;
            data.date = date;
            data.predefiniraniTermini = slobodniTermini;

            return View(data);
        }

        [HttpPost]
        public IActionResult Termin(DateTime date, string napomena, string vrijeme)
        {
            int? userId = getCurrentUserId();
            if (userId == null) return RedirectToAction("Login", "Home");
            Pacijent pacijent = Repository.GetPacijentByID((int)userId);

            Termin termin = new Termin(pacijent, pacijent.Lijecnik, date, vrijeme, napomena);
            Repository.AddTermin(termin);

            return RedirectToAction("Uspijeh", new {back= "termin"});
        }

        [HttpGet]
        public IActionResult Recept()
        {
            int? userId = getCurrentUserId();
            if (userId == null) return RedirectToAction("Login", "Home");

            IEnumerable<Lijek> lijekovi = Repository.GetLijekovi();
            IEnumerable<Dijagnoza> dijagnoze = Repository.GetDijagnoze();

            dynamic data = new ExpandoObject();
            data.lijekovi = lijekovi;
            data.dijagnoze = dijagnoze;

            return View(data);
        }

        [HttpPost]
        public IActionResult Recept(int dijagnozaId, int lijekId, string napomena, bool ponavljajuc)
        {
            int? userId = getCurrentUserId();
            if (userId == null) return RedirectToAction("Login", "Home");

            Pacijent pacijent = Repository.GetPacijentByID((int)userId);
            Lijek lijek = Repository.GetLijek(lijekId);
            Dijagnoza dijagnoza = Repository.GetDijagnoza(dijagnozaId);

            Recept recept = new Recept(pacijent, dijagnoza, lijek, DateTime.Now, napomena, ponavljajuc, false);

            Repository.AddRecept(recept);

            return RedirectToAction("Uspijeh", new { back = "recept"});
        }

        [HttpGet]
        public IActionResult Uspijeh(string back)
        {
            return View("Uspijeh", back);
        }


        private int? getCurrentUserId()
        {
            string? userId = HttpContext.Session.GetString("userId");
            if (userId == null) userId = "0";
            return int.Parse(userId);
        }
    }
}
