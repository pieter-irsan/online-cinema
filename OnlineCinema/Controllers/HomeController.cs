using OnlineCinema.Models;
using System.Web.Mvc;

namespace OnlineCinema.Controllers
{
    [HandleError]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            MovieDbHandler dbHandler = new MovieDbHandler();
            ModelState.Clear();
            return View(dbHandler.GetMovie());
        }

        public ActionResult Movie(int id)
        {
            MovieDbHandler dbHandler = new MovieDbHandler();
            return View(dbHandler.GetMovie().Find(movie => movie.Id == id));
        }

        public ActionResult Search(string param)
        {
            try
            {
                MovieDbHandler dbHandler = new MovieDbHandler();
                ModelState.Clear();
                return View(dbHandler.SearchMovie(param));
            }
            catch
            {
                ModelState.Clear();
                TempData["Alert"] = "Search operation failed";
                return RedirectToAction("Index");
            }
        }
    }
}