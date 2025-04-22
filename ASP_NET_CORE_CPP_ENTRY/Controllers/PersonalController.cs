using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Pruebas.Cliente.Interface;

namespace Pruebas.Cliente.Controllers
{
    public class PersonalController : Controller
    {
        private readonly IPersonal? _personal;

        public PersonalController(IPersonal personal)
        {
            _personal = personal;
        }

        public IActionResult PersonalIndex()
        {
            var lstPersonal = _personal.ObtenerPersonal(string.Empty);

            return View(lstPersonal);
        }

        public IActionResult PersonalIndexJson()
        {
            var lstPersonal = _personal.ObtenerPersonal(string.Empty);

            return View(lstPersonal);
        }
        //
        [HttpGet]
        public JsonResult ObtenerPersonalJson()
        {
            //
            string status = "OK";
            List<Models.Personal> _lstPersonal = new List<Models.Personal>();
            //
            try
            {
                //
                _lstPersonal = _personal.ObtenerPersonal(string.Empty);
            }
            catch (Exception ex)
            {
                //
                status = String.Format("PERSONAL | ERROR | {0}-{1}", ex.Message, ex.StackTrace);
                //
                throw;
            }

            //
            var json = Json(_lstPersonal);

            //json.MaxJsonLength = int.MaxValue;
            //
            return json;
        }
        //
        // GET: HomeController1/Delete/5
        [HttpGet]
        public JsonResult Delete(int id)
        {
            //  
            string status = "OK";
            //
            try
            {
                //
                status = _personal.BorrarPersonal(id).ToString();
            }
            catch (Exception ex)
            {
                //
                status = String.Format("PERSONAL | ERROR | {0}-{1}", ex.Message, ex.StackTrace);
                //
                throw;
            }
            //
            var json = Json(status);
            //
            return json;
        }
        //
        [HttpGet]

        public JsonResult DeleteJson(int id)
        {
            //  
            string status = "OK";
            //
            try
            {
                //
                status = _personal.BorrarPersonal(id).ToString();
            }
            catch (Exception ex)
            {
                //
                status = String.Format("PERSONAL | ERROR | {0}-{1}", ex.Message, ex.StackTrace);
                //
                throw;
            }

            var lstPersonal = _personal.ObtenerPersonal(string.Empty);

            var json = Json(lstPersonal);

            return json;
        }



        //
        /*
            // GET: HomeController1
            public ActionResult Index()
            {
                return View();
            }

            // GET: HomeController1/Details/5
            public ActionResult Details(int id)
            {
                return View();
            }

            // GET: HomeController1/Create
            public ActionResult Create()
            {
                return View();
            }

            // POST: HomeController1/Create
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Create(IFormCollection collection)
            {
                try
                {
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }

            // GET: HomeController1/Edit/5
            public ActionResult Edit(int id)
            {
                return View();
            }

            // POST: HomeController1/Edit/5
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Edit(int id, IFormCollection collection)
            {
                try
                {
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }



            // POST: HomeController1/Delete/5
            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult Delete(int id, IFormCollection collection)
            {
                try
                {
                    return RedirectToAction(nameof(Index));
                }
                catch
                {
                    return View();
                }
            }
          */
    }
}
