using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using Parkwhere05.DAL;
using Parkwhere05.Models;

namespace Parkwhere05.Controllers
{
    public class CarparksController : GeneralController<Carpark>
    {
        //To store top five bookmarked carparks
        IEnumerable<Carpark> TopFiveCarparks;

        public CarparksController() {
            dataGateway = new CarparkGateway();
            //TopFiveCarparks = ((CarparkGateway)dataGateway).GetTopFiveBookmarks();
        }

        [HttpPost]
        public ActionResult Index(string value1, string value2)
        {
            if (Session["searchResult"] == null)
            {
                ViewBag.List = ((CarparkGateway)dataGateway).FilterByType(value1, value2);
                return View();
            }
            else {
                string searchResult = Session["searchResult"].ToString();
                ViewBag.List = ((CarparkGateway)dataGateway).FilterAddressByType(searchResult, value1, value2);
                return View();
            }
        }

        [HttpGet]
        public ActionResult Index(string addResults)
        {
            Session["searchResult"] = addResults;
            ViewBag.List = ((CarparkGateway)dataGateway).searchCarpark(addResults);
            //ViewBag.List = ((CarparkGateway)dataGateway).GetAllCarparks();
            return View();
        }
        public override ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Carpark obj = dataGateway.SelectById(id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            List<string[]> CarparkList;
            CarparkList = new List<string[]>();
            string[] listString = new string[2];
            listString[0] = obj.x_coord.ToString();
            listString[1] = obj.y_coord.ToString();
            CarparkList.Add(listString);
            ViewBag.Corr = CarparkList;
            ViewBag.CurrentCor = HomeController.CurrentCorrList;

            Carpark carpark = dataGateway.SelectById(id);
            return View(carpark);
        }

    }

}
