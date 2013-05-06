using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BlogMootor.Models;

namespace Blog.Controllers
{
    public class PostitusedController : Controller
    {
        private BlogModel model = new BlogModel();
        private const int PostitusedLehel = 4;

        public ActionResult Index(int? id)
        {
            int leheNumber = id ?? 0;
            IEnumerable<Postitused> posts =
                (from post in model.Postitused
                 where post.Kuupaev < DateTime.Now
                 orderby post.Kuupaev descending
                 select post).Skip(leheNumber * PostitusedLehel).Take(PostitusedLehel + 1);
            ViewBag.IsPreviousLinkVisible = leheNumber > 0;
            ViewBag.IsNextLinkVisible = posts.Count() > PostitusedLehel;
            ViewBag.LeheNumber = leheNumber;
            return View(posts.Take(PostitusedLehel));
        }
        public ActionResult Details(int id)
        {
            Postitused post = getpost(id);
            return View(post);
        }
        private Postitused getpost(int? id)
        {
            return id.HasValue ? model.Postitused.Where(x => x.ID == id).First() : new Postitused() { ID = -1 };
        }
    }

}
