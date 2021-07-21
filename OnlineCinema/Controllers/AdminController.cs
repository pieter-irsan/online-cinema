using OnlineCinema.Models;
using System.Web.Mvc;

namespace OnlineCinema.Controllers
{
    [HandleError]
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            if (Session["Admin"] == null)
            {
                TempData["Alert"] = "You have to log in first";
                return RedirectToAction("Login");
            }
            else
                return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(AdminModel admin)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserDbHandler dbHandler = new UserDbHandler();
                    if (dbHandler.LoginAdmin(admin))
                    {
                        Session["Admin"] = admin.Username.ToString();
                        ModelState.Clear();
                        ViewBag.Success = "Successfully logged in";
                        return View("Index");
                    }
                    else
                    {
                        ViewBag.Message = "Invalid Username or Password";
                    }
                }
                return View();
            }
            catch
            {
                ViewBag.Alert = "Login failed";
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session["Admin"] = null;
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}