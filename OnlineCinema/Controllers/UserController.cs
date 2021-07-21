using OnlineCinema.Models;
using System.Web.Mvc;

namespace OnlineCinema.Controllers
{
    [HandleError]
    public class UserController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(UserModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserDbHandler dbHandler = new UserDbHandler();
                    if (dbHandler.LoginUser(user))
                    {
                        Session["User"] = user.Username.ToString();
                        ModelState.Clear();
                        TempData["Success"] = "Successfully logged in";
                        return RedirectToAction("Index", "Home");
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
                TempData["Alert"] = "Log in failed";
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session["User"] = null;
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(UserModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    UserDbHandler dbHandler = new UserDbHandler();
                    if (dbHandler.RegisterUser(user))
                        ViewBag.Success = "Successfully registered";
                    else
                        ViewBag.Taken = "Username is already taken";
                }
                return View();
            }
            catch
            {
                ViewBag.Alert = "Registration failed";
                return View();
            }
        }

        public ActionResult Movie()
        {
            if (Session["User"] == null)
            {
                TempData["Alert"] = "You have to log in first";
                return RedirectToAction("Login");
            }
            else
            {
                string username = Session["User"].ToString();
                MovieDbHandler dbHandler = new MovieDbHandler();
                ModelState.Clear();
                return View(dbHandler.UserMovie(username));
            }
        }
    }
}