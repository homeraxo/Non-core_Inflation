using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Non_core_Inflation.Models;

namespace Non_core_Inflation.Controllers
{
    //[Authorize(Roles = "Administrador")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Method to Test with Angular
        /// </summary>
        /// <param name="UserMessage"></param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult SendMessage(string UserMessage)
        {
            try
            {
                string messagereceived = "El mensaje lo recibió el servidor: " + UserMessage.Trim();

                Response.StatusCode = 400;
                return Json(messagereceived, JsonRequestBehavior.AllowGet);
            }
            catch (System.Exception ex)
            {
                Response.StatusCode = 400;
                return Json("Error: " + ex.Message.ToString(), JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Connect wit the Web API Inflation
        /// </summary>
        /// <returns></returns>
        private DataSerie[] ReadSerie()
        {
            try
            {                
                string url = "http://localhost:64163/Api/Serie";
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Accept = "application/json";

                HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(DataSerie[]));
                object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
                DataSerie[] jsonResponse = objResponse as DataSerie[];
                return jsonResponse;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return null;
        }

        /// <summary>
        /// Get information of the serie
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public JsonResult GetSerie()
        {
            try
            {
                //Response response = ReadSerie();
                DataSerie[] response = ReadSerie();

                Response.StatusCode = 400;
                return Json(response, JsonRequestBehavior.AllowGet);
            }
            catch (System.Exception ex)
            {
                Response.StatusCode = 400;
                return Json("Error: " + ex.Message.ToString(), JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Method to authentication
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        //[HttpPost]
        public ActionResult Login(LoginViewModel login)
        {
            if (ModelState.IsValid)
            {
                var userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();
                var authManager = HttpContext.GetOwinContext().Authentication;

                AppUser user = userManager.Find(login.UserName, login.Password);
                if (user != null)
                {
                    var ident = userManager.CreateIdentity(user,
                        DefaultAuthenticationTypes.ApplicationCookie);
                    //use the instance that has been created. 
                    authManager.SignIn(
                        new AuthenticationProperties { IsPersistent = false }, ident);
                    return Redirect(login.ReturnUrl ?? Url.Action("Index", "Home"));
                }
            }

            ModelState.AddModelError("", "Invalid username or password");

            return View(login);
        }

        public ActionResult CreateRole(string roleName)
        {
            var roleManager = HttpContext.GetOwinContext().GetUserManager<RoleManager<AppRole>>();

            if (!roleManager.RoleExists(roleName))
                roleManager.Create(new AppRole(roleName));
            // rest of code

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}