using Common;
using Model.Custom;
using Model.Domain;
using NLog;
using Service;
using System.Linq;
using System.Web.Mvc;

namespace FrontEnd.Controllers
{
    [Authorize]
    public class PolizaController : Controller
    {
        private readonly IPolizaService _polizaService = DependecyFactory.GetInstance<IPolizaService>();

        public ActionResult Index()
        {
            return View(_polizaService.GetAllByUser());
        }


        public ActionResult Crud(int id = 0)
        {
            if (id > 0)
            {
                ViewBag.Poliza = _polizaService.GetAllById(id);
            }

            return View(id == 0  ? new Poliza() : _polizaService.Get(id));
        }

        [HttpPost]
        public ActionResult Crud(Poliza model)
        {
            if (ModelState.IsValid)
            {
                var rh = _polizaService.InsertOrUpdate(model);
                if (rh.Response)
                {
                    return RedirectToAction("index");
                }
            }           

            return View(model);
        }

        public ActionResult Delete(int id)
        {
            _polizaService.Delete(id);
            return RedirectToAction("index");
        }

    }
}