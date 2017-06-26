using GoogleAddressParser.DB;
using GoogleAddressParser.Entities;
using GoogleAddressParser.Helpers;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoogleAddressParser.Repositories
{
    public class MaritimeDatabaseComRepository
    {
        public static List<MaritimeDatabaseCom> GetMaritimeDatabaseComs()
        {
            return DBDirectory.GetMaritimeDatabaseComs();
        }
        public static int SaveOrUpdateMaritimeDatabaseCom(MaritimeDatabaseCom item)
        {
            return DBDirectory.SaveOrUpdateMaritimeDatabaseCom(item);
        }
        public static MaritimeDatabaseCom GetMaritimeDatabaseCom(int id)
        {
            return DBDirectory.GetMaritimeDatabaseCom(id);
        }
        public static void DeleteMaritimeDatabaseCom(int id)
        {
            DBDirectory.DeleteMaritimeDatabaseCom(id);
        }
        public static void ParseAddress()
        {
            var items = GetMaritimeDatabaseComs();
            string ip = "";
            foreach (var item in items)
            {
                try
                {
                   

                    var geoLocation = LocationParser.GetParseLocation(item.Address, ip, 8080);

                    item.GCity = geoLocation.city.ToStr();
                    item.GCountry = geoLocation.country.ToStr();
                    item.GState = geoLocation.state.ToStr();
                    item.GZip = geoLocation.zip.ToStr();
                    item.GAddress = geoLocation.address.ToStr();
                    item.Latitude = geoLocation.latitude.ToFloat();
                    item.Longitude = geoLocation.longitude.ToFloat();
                    item.GoogleApiXml = geoLocation.GoogleApiResult.ToStr();
                    item.StreetNumber = geoLocation.streetNumber.ToStr();
                    item.Premise = geoLocation.premise.ToStr();
                    item.Subpremise = geoLocation.subpremise.ToStr();
                    
                    SaveOrUpdateMaritimeDatabaseCom(item);
                    Console.WriteLine(item.Id);

                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                }
       
            }
           

        }
    }

}
