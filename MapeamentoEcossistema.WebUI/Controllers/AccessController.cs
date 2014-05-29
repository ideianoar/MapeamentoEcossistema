using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MapeamentoEcossistema.WebUI.Models;

namespace MapeamentoEcossistema.WebUI.Controllers
{
    public class AccessController : Controller
    {
        // Fields.
        private readonly MapeamentoEntities _context;

        // Constructors.
        public AccessController(MapeamentoEntities context)
        {
            _context = context;
        }

        // Actions.
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string accessToken)
        {
            var user = _context.Users.FirstOrDefault(u => u.AccessToken == accessToken);

            if (user != null)
            {
                FormsAuthentication.SetAuthCookie(user.Id.ToString(), false);
                return RedirectToAction("Select");
            }
            else
            {
                ViewBag.Error = true;
                return View();
            }
        }
        [Authorize]
        public ActionResult Select()
        {
            ViewBag.Id = new SelectList(_context.Questionnaires.OrderBy(q => q.Name), "Alias", "Name");
            return View();
        }
        [Authorize, HttpPost]
        public ActionResult Select(string id)
        {
            return RedirectToAction("Index", "Questionnaires", new { id = id });
        }
    }
}
