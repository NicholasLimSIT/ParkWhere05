
using Parkwhere05.DAL;
using Parkwhere05.Services;
using System;
using System.Net;
using System.Web.Mvc;


namespace Parkwhere05.Controllers
{
    public class GeneralController<T> : Controller where T : class
    {

        WeatherGateway weatherGateway = new WeatherGateway();
        GetMyAreaGateway getMyAddressGateway = new GetMyAreaGateway();
        public String currentForecast;

        internal IDataGateway<T> dataGateway;
        internal GeneralController()
        {
            if (HomeController.CurrentCorrList != null)
            {
                string lat = "";
                string lng = "";
                foreach (var item in HomeController.CurrentCorrList)
                {
                    lat = item[0];
                    lng = item[1];
                }
                currentForecast = weatherGateway.GetCurrentWeather(getMyAddressGateway.GetMyAreaName(lat, lng));
                ViewBag.Weather = currentForecast;
            }
            else
            {
                ViewBag.Weather = "Error retriving weather forecast. Please try again.";
            }
        }
    
        public ActionResult currentlat(float coordinates)
        {
            ViewData.Model = coordinates;
            return View();
        }


        // GET: General
        virtual public ActionResult Index(int? id)
        {
            return View(dataGateway.SelectAll());
        }
      
       virtual public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            T obj = dataGateway.SelectById(id);
            if (obj == null)
            {
                return HttpNotFound();
            }
            
            return View(obj);
        }

    }
}