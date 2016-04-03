using System.Collections.Generic;
using System.Linq;
using Parkwhere05.Models;

namespace Parkwhere05.DAL
{
    public class PetrolStationGateway : DataGateway<PetrolStation>
    {
        IEnumerable<PetrolStation> PetrolStations;
        public List<string[]> GetAllPetrolStation()
        {

            List<string[]> PetrolStationList;

            PetrolStations = SelectAll();
            PetrolStationList = new List<string[]>();

            foreach (var item in PetrolStations)
            {
                string[] listString = new string[4];
                listString[0] = item.petrolStationName;
                listString[1] = item.latitude.ToString();
                listString[2] = item.longitude.ToString();
                listString[3] = item.Id.ToString();
                PetrolStationList.Add(listString);
            }

            return PetrolStationList;
        }

        public List<string[]> searchPetrolStation(string address)
        {
            if (address == null)
            {
                List<string[]> PetrolStationList;
                PetrolStations = data.SqlQuery("SELECT * From dbo.PetrolStation ").ToList(); 
                PetrolStationList = new List<string[]>();

                foreach (var item in PetrolStations)
                {
                    string[] listString = new string[4];
                    listString[0] = item.petrolStationName;
                    listString[1] = item.latitude.ToString();
                    listString[2] = item.longitude.ToString();
                    listString[3] = item.Id.ToString();
                    PetrolStationList.Add(listString);
                }

                return PetrolStationList;
            }
            else {
                List<string[]> PetrolStationList;

                string str = address.Substring(0, 8);
                PetrolStations = data.SqlQuery("SELECT * From dbo.PetrolStation WHERE address LIKE '%" + str + "%'").ToList();
                PetrolStationList = new List<string[]>();

                foreach (var item in PetrolStations)
                {
                    string[] listString = new string[4];
                    listString[0] = item.petrolStationName;
                    listString[1] = item.latitude.ToString();
                    listString[2] = item.longitude.ToString();
                    listString[3] = item.Id.ToString();
                    PetrolStationList.Add(listString);
                }

                return PetrolStationList;
            }
        }
    }
}