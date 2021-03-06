using Newtonsoft.Json;
using PointOfSale.Helper;
using PointOfSale.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace PointOfSale.Controllers
{
    public class HomeController : Controller
    {

        [AuthorizationFilter]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AccessDenied()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public JsonResult CheckLogin(string username, string password)
        {
            POS_TutorialEntities db = new POS_TutorialEntities();
            string md5StringPassword = AppHelper.GetMd5Hash(password);
            //var dataItem = db.Users.Where(x => x.Username == username && x.Password == md5StringPassword).SingleOrDefault();
            var dataItem = db.Users.Where(x => x.Username == username && x.Password == password).SingleOrDefault();

            bool isLogged = true;
            if (dataItem != null)
            {
                //var mdl = System.Web.HttpContext.Current.User.Identity.Name;
                Session["Username"] = dataItem.Username;
                Session["Role"] = dataItem.Role;
                isLogged = true;
            }
            else
            {
                isLogged = false;
            }
            return Json(isLogged, JsonRequestBehavior.AllowGet);
        }


        public ActionResult TestPage()
        {
            return View();
        }

        public ActionResult TestPage2()
        {
            return View();
        }

        // user start
        [AuthorizationFilter]
        public ActionResult UserCreate()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveUser(User user)
        {
            POS_TutorialEntities db = new POS_TutorialEntities();
            bool isSuccess = true;

            if (user.UserId > 0)
            {
                db.Entry(user).State = EntityState.Modified;
            }
            else
            {
                user.Status = 1;
                user.Password = AppHelper.GetMd5Hash(user.Password);
                db.Users.Add(user);
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                isSuccess = false;
            }

            return Json(isSuccess, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllUser()
        {
            POS_TutorialEntities db = new POS_TutorialEntities();
            var dataList = db.Users.Where(x => x.Status == 1).ToList();
            return Json(dataList, JsonRequestBehavior.AllowGet);
        }

        // user end

        // category start
        [AuthorizationFilter]
        public ActionResult Category()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveCategory(Category cat)
        {
            POS_TutorialEntities db = new POS_TutorialEntities();
            bool isSuccess = true;
            if (cat.CategoryId > 0)
            {
                db.Entry(cat).State = EntityState.Modified;
            }
            else
            {
                cat.Status = 1;
                db.Categories.Add(cat);
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                isSuccess = false;
            }
            return Json(isSuccess, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllCategory()
        {
            POS_TutorialEntities db = new POS_TutorialEntities();
            var dataList = db.Categories.Where(x => x.Status == 1).ToList();
            return Json(dataList, JsonRequestBehavior.AllowGet);

            //string json = JsonConvert.SerializeObject(dataList, Formatting.None);
            //return Json(json, JsonRequestBehavior.AllowGet);


            
        }

        // category end


        // product start
        public ActionResult Product()
        {
            return View();
        }

        [HttpPost]
        public JsonResult SaveProduct(Product product)
        {
            POS_TutorialEntities db = new POS_TutorialEntities();
            bool isSuccess = true;
            if (product.ProductId > 0)
            {
                db.Entry(product).State = EntityState.Modified;
            }
            else
            {
                product.Status = 1;
                product.Name = "aaa";
                db.Products.Add(product);
            }
            try
            {
                db.SaveChanges();
            }
            catch (Exception)
            {
                isSuccess = false;
            }

            return Json(isSuccess, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetAllProduct()
        {
            POS_TutorialEntities db = new POS_TutorialEntities();
            var dataList = db.Products.Where(x => x.Status == 1).ToList();
            return Json(dataList, JsonRequestBehavior.AllowGet);
        }

        // product end


        public ActionResult Logout()
        {
            Session["Username"] = null;
            Session["Role"] = null;
            return RedirectToAction("Login");
        }




    }
}