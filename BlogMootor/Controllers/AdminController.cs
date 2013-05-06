using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogMootor.Models;
using System.Web.Security;

namespace Blog.Controllers
{
    public class AdminController : Controller
    {
        private BlogModel model = new BlogModel();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Administraator model)
        {
            if (ModelState.IsValid)
            {

                if (model.KasutajaNimi.Equals("Admin") && model.Parool.Equals("Admin"))
                {
                    FormsAuthentication.SetAuthCookie(model.KasutajaNimi, false);
                    return RedirectToAction("Controlpanel","Admin");
                }
                {
                    ModelState.AddModelError("", "Vale kasutajanimi või parool!");
                }
            }
            return View();
        }
        public ActionResult Controlpanel(int? id)
        {
            Postitused postitus = getpost(id);
            return View(postitus);
        }
        
        public ActionResult update(int? id, string pealkiri, string sisu, DateTime kuupaev)
        {
            Postitused postitus = getpost(id);
            postitus.Pealkiri = pealkiri;
            postitus.Sisu = sisu;
            postitus.Kuupaev = kuupaev;

            if (!id.HasValue)
            {
                model.Postitused.Add(postitus);
            }
            model.SaveChanges();

            return RedirectToAction("Details","Postitused",new { id = postitus.ID });

        }
        public ActionResult Lisa(int? id)
        {
            Postitused postitus = getpost(id);
            return View(postitus);
        }
        private Postitused getpost(int? id)
        {
            return id.HasValue ? model.Postitused.Where(x => x.ID == id).First() : new Postitused() { ID = -1 };
        }
        public ActionResult Signout() {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index","Postitused");
        }
        
    }
}
