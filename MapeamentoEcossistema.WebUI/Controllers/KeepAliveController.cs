using System.Web.Mvc;

namespace MapeamentoEcossistema.WebUI.Controllers
{
    public class KeepAliveController : Controller
    {
        public JsonResult Index()
        {
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}
