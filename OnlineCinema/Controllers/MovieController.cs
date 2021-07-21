using OnlineCinema.Models;
using System;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace OnlineCinema.Controllers
{
    [HandleError]
    public class MovieController : Controller
    {
        public ActionResult Index()
        {
            if (Session["Admin"] == null)
            {
                TempData["Alert"] = "You have to log in first";
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                MovieDbHandler dbHandler = new MovieDbHandler();
                ModelState.Clear();
                return View(dbHandler.GetMovie());
            }
        }

        public ActionResult Add()
        {
            if (Session["Admin"] == null)
            {
                TempData["Alert"] = "You have to log in first";
                return RedirectToAction("Login", "Admin");
            }
            else
                return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(MovieModel movie)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (movie.Trailer == null)
                    {
                        movie.Trailer = "";
                    }
                    if (movie.Poster != null)
                    {
                        string extension = Path.GetExtension(movie.Poster.FileName);
                        var supportedTypes = new[] { "jpg", "jpeg" };

                        if (!supportedTypes.Contains(extension.Substring(1)))
                        {
                            ViewBag.Alert = "Please upload JPG or JPEG files only";
                            return View();
                        }
                        else
                        {
                            string fileName = movie.Title.ToLower().Replace(" ", "-") + "-poster" + extension;
                            fileName = new string
                                ((from c in fileName where char.IsLetterOrDigit(c) || c == '-' || c == '.' select c).ToArray());

                            movie.PosterPath = "~/Content/Images/" + fileName;
                            fileName = Path.Combine(Server.MapPath("~/Content/Images/"), fileName);
                            movie.Poster.SaveAs(fileName);
                        }
                    }
                    else
                        movie.PosterPath = movie.PosterPath;

                    MovieDbHandler dbHandler = new MovieDbHandler();
                    if (dbHandler.AddMovie(movie))
                    {
                        ViewBag.Success = "Movie added successfully";
                        ModelState.Clear();
                    }
                }
                return View();
            }
            catch (Exception e)
            {
                ViewBag.Alert = "Something went wrong: " + e;
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            if (Session["Admin"] == null)
            {
                TempData["Alert"] = "You have to log in first";
                return RedirectToAction("Login", "Admin");
            }
            else
            {
                MovieDbHandler dbHandler = new MovieDbHandler();
                return View(dbHandler.GetMovie().Find(movie => movie.Id == id));
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MovieModel movie)
        {
            try
            {
                if (movie.Trailer == null)
                {
                    movie.Trailer = movie.Trailer;
                }
                if (movie.Poster != null)
                {
                    string extension = Path.GetExtension(movie.Poster.FileName);
                    var supportedTypes = new[] { "jpg", "jpeg" };

                    if (!supportedTypes.Contains(extension.Substring(1)))
                    {
                        ViewBag.Alert = "Please upload JPG or JPEG files only";
                        return View();
                    }
                    else
                    {
                        string fileName = movie.Title.ToLower().Replace(" ", "-") + "-poster" + extension;
                        fileName = new string
                            ((from c in fileName where char.IsLetterOrDigit(c) || c == '-' || c == '.' select c).ToArray());

                        movie.PosterPath = "~/Content/Images/" + fileName;
                        fileName = Path.Combine(Server.MapPath("~/Content/Images/"), fileName);
                        movie.Poster.SaveAs(fileName);
                    }
                }
                else
                    movie.PosterPath = movie.PosterPath;

                MovieDbHandler dbHandler = new MovieDbHandler();
                if (dbHandler.EditMovie(movie))
                {
                    ViewBag.Success = "Successfully updated movie";
                    ModelState.Clear();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Alert = "Failed to edit movie";
                    return View();
                }
            }
            catch (Exception e)
            {
                ViewBag.Alert = "Something went wrong: " + e;
                return View();
            }
        }

        public ActionResult Delete(int id, string posterPath)
        {
            try
            {
                posterPath = Request.MapPath(posterPath);
                if (System.IO.File.Exists(posterPath))
                {
                    System.IO.File.Delete(posterPath);
                }

                MovieDbHandler dbHandler = new MovieDbHandler();
                if (dbHandler.DeleteMovie(id))
                {
                    ViewBag.Success = "Student deleted successfully";
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
                ViewBag.Alert = "Search operation failed";
                return View("Index");
            }
        }
    }
}