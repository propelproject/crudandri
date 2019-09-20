using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MayapadaCRUD.Models;

namespace MayapadaCRUD.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            ViewBag.Message = "List of users";
            using (dbMayapadaEntities dc = new dbMayapadaEntities())
            {
                return View(dc.Users.OrderBy(a=> a.UserId).ToList());
            }   
        }

        public ActionResult CreateUser()
        {
            return View();
        }

        public ActionResult EditUser(int id)
        {
            using (dbMayapadaEntities dc = new dbMayapadaEntities())
            {
                return View(dc.Users.Where(a => a.UserId == id).FirstOrDefault());
            }

        }
        
        public ActionResult DeleteUser(int id)
        {
            using (dbMayapadaEntities dc = new dbMayapadaEntities())
            {
                return View(dc.Users.Where(a=> a.UserId == id).FirstOrDefault());
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser(User _user)
        {
            try
            {
                using (dbMayapadaEntities dc = new dbMayapadaEntities())
                {
                    dc.Users.Add(_user);
                    dc.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult EditUser(int id, User _user)
        {
            try
            {
                using (dbMayapadaEntities dc = new dbMayapadaEntities())
                {
                    dc.Entry(_user).State = EntityState.Modified;
                    dc.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch 
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult DeleteUser(int id, FormCollection collection)
        {
            try
            {
                using (dbMayapadaEntities dc = new dbMayapadaEntities())
                {
                    var result = dc.Users.Where(a => a.UserId == id).FirstOrDefault();

                    User user  = dc.Users.Where(a => a.UserId == id).FirstOrDefault();
                    dc.Users.Remove(user);
                    dc.SaveChanges();

                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
          
        }
    }
}