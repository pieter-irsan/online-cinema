using OnlineCinema.Models;
using System;
using System.Web.Mvc;

namespace OnlineCinema.Controllers
{
    [HandleError]
    public class PurchaseController : Controller
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
                PurchaseDbHandler dbHandler = new PurchaseDbHandler();
                ModelState.Clear();
                ViewBag.Revenue = dbHandler.Revenue();
                return View(dbHandler.GetPurchase());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Details(string username, string title, int price, DateTime time)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    PurchaseModel purchase = new PurchaseModel();
                    purchase.Username = username;
                    purchase.Title = title;
                    purchase.Price = Convert.ToInt32(price);
                    purchase.Time = time;
                    PurchaseDbHandler dbHandler = new PurchaseDbHandler();
                    if (dbHandler.AddPurchase(purchase))
                    {
                        TempData["Success"] = "Purchase successful";
                        ModelState.Clear();
                    }
                    else
                    {
                        TempData["Alert"] = "You have already purchased this movie";
                    }
                }
                return RedirectToAction("Index", "Home");
            }
            catch
            {
                TempData["Alert"] = "Purchase failed";
                return View();
            }
        }

        public ActionResult Details(int? id)
        {
            try
            {
                if (Session["User"] != null)
                {
                    MovieDbHandler dbHandler = new MovieDbHandler();
                    return View(dbHandler.GetMovie().Find(movie => movie.Id == id));
                }
                else
                {
                    TempData["Alert"] = "You have to login first";
                    return RedirectToAction("Login", "User");
                }
            }
            catch
            {
                ModelState.Clear();
                TempData["Alert"] = "Something went wrong";
                return RedirectToAction("Index");
            }
        }

        public ActionResult Search(string param)
        {
            try
            {
                PurchaseDbHandler dbHandler = new PurchaseDbHandler();
                ModelState.Clear();
                ViewBag.Revenue = dbHandler.Revenue();
                return View(dbHandler.SearchPurchase(param));
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