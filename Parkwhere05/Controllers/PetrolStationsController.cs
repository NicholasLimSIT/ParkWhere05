using System.Web.Mvc;
using Parkwhere05.DAL;
using Parkwhere05.Models;
using System.Net;
using System.Collections.Generic;

namespace Parkwhere05.Controllers
{
    public class PetrolStationsController : GeneralController<PetrolStation>
    {
        public PetrolStationsController()
        {
            dataGateway = new PetrolStationGateway();
        }


        [HttpGet]
        public ActionResult Index(string addResults)
        {

            ViewBag.List = ((PetrolStationGateway)dataGateway).searchPetrolStation(addResults);
            return View();
        }
        public override ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PetrolStation obj = dataGateway.SelectById(id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            List<string[]> PetrolStationList;
            PetrolStationList = new List<string[]>();
            string[] listString = new string[2];
            listString[0] = obj.latitude.ToString();
            listString[1] = obj.longitude.ToString();
            PetrolStationList.Add(listString);
            ViewBag.Corr = PetrolStationList;
            ViewBag.CurrentCor = HomeController.CurrentCorrList;
            return View();
        }

    }
}
