using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Parkwhere05.Services
{
    public class WeatherGateway : IWeatherGateway
    {
        public string GetCurrentWeather(string location)
        {
            string weatherForecast = "";
            string dataset = "nowcast";
            string keyref = "781CF461BB6606AD4AF8F309C0CCE994AC81FD9664F88220";         // Developer Key to use API
            
            // URL Connection to NEA Dataset API
            String url = "http://www.nea.gov.sg/api/WebAPI?dataset=" + dataset + "&keyref=" + keyref;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            try
            {
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);

                string content;
                using (readStream)
                {
                    content = readStream.ReadLine();
                }

                string[] tokens = content.Split(new[] { '<' + "area name=" + '"', '"' + " forecast=" + '"', '"' + " icon=" + '"' }, StringSplitOptions.None);
                
                string myArea = location.ToString();
                int i = 0;
                int length = tokens.Count();
                Boolean isEnd = false;
                while (!isEnd)
                {
                    if (!tokens[i].Equals(myArea.ToUpper()))
                    {
                        i++;
                        if (i == length)
                        {
                            isEnd = true;
                        }
                    }

                    else
                        isEnd = true;
                }

                if (i == length)
                    weatherForecast = "Error accessing current location forecast.";
                else
                    weatherForecast = "My current location: " + tokens[i] + " | Forecast: " + tokens[i + 1];
            }
            catch (WebException we)
            {
                //Run code if failed to retrieve weather forecast
                weatherForecast = "My current location: " + location + ". Error accessing NEA API. Forecast cannot be retrived.";
            }

            return weatherForecast;
        }
    }
}