using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MayapadaCRUD.Models;

namespace MayapadaCRUD.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "About page";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact page";

            return View();
        }

        public ActionResult Login()
        {
            ViewBag.Message = "Login page";

            return View();
        }


        public ActionResult Logout()
        {
            Session.Abandon();
            return RedirectToAction("Login");

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(User _user)
        {
            if(ModelState.IsValid)
            {
                using (dbMayapadaEntities oEntities = new dbMayapadaEntities())
                {
                    var result = oEntities.Users.Where(a=> a.Username.Equals(_user.Username)
                                 && a.Password.Equals(_user.Password)).FirstOrDefault();
                    if (result != null)
                    {
                        Session["LogedUserId"] = result.UserId;
                        Session["LogedUsername"] = result.Username.ToString();
                        Session["LogedFirstname"] = result.FirstName.ToString();
                        Session["LogedLastname"] = result.LastName.ToString();
                        return RedirectToAction("Index");
                    }
                }
            }
            return View(_user);
        }

        public ActionResult LoginSuccess()
        {
            if(Session["LogedUserId"]!= null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

    }
}