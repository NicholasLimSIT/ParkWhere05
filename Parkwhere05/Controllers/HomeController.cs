using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Parkwhere05.Services;

namespace Parkwhere05.Controllers
{
    public class HomeController : Controller
    {
        WeatherGateway weatherGateway = new WeatherGateway();
        GetMyAreaGateway getMyAreaGateway = new GetMyAreaGateway();
        string currentForecast;
        public static List<string[]> CurrentCorrList;

        public HomeController()
        {
            if (CurrentCorrList != null)
            {
                string lat = "";
                string lng = "";
                foreach (var item in CurrentCorrList)
                {
                    lat = item[0];
                    lng = item[1];
                }
                currentForecast = weatherGateway.GetCurrentWeather(getMyAreaGateway.GetMyAreaName(lat, lng));
                ViewBag.Weather = currentForecast;
            }
            else
            {
                ViewBag.Weather = "Retriving weather in progress...";
            }
        }
        public ActionResult Index()
        {
            
            return View();
        }

        public ActionResult About()
        {
          
            
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public void SaveViewBag(String Lat, String Lng)
        {
            CurrentCorrList = new List<string[]>();
            string[] listString = new string[2];
            listString[0] = Lat;
            listString[1] = Lng;
            CurrentCorrList.Add(listString);
           
        }
    }
}