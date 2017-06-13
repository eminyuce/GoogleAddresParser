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
    public class UnformattedAddressShiporacleComRepository
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        public static List<UnformattedAddressShiporacleCom> GetUnformattedAddressShiporacleComs()
        {
            return DBDirectory.GetUnformattedAddressShiporacleComs();
        }
        public static int SaveOrUpdateUnformattedAddressShiporacleCom(UnformattedAddressShiporacleCom item)
        {
            return DBDirectory.SaveOrUpdateUnformattedAddressShiporacleCom(item);
        }
        public static UnformattedAddressShiporacleCom GetUnformattedAddressShiporacleCom(int id)
        {
            return DBDirectory.GetUnformattedAddressShiporacleCom(id);
        }
        public static void DeleteUnformattedAddressShiporacleCom(int id)
        {
            DBDirectory.DeleteUnformattedAddressShiporacleCom(id);
        }
        public static List<UnformattedAddressShiporacleCom> GetAddresses(int top, int skip)
        {
            return GetUnformattedAddressShiporacleComs().Skip(skip).Take(top).ToList();
        }
        public static void ParseLocationAddress()
        {


            Console.WriteLine(String.Format("The program began to extract address"));
            Dictionary<string, int> proxies = GoogleGeoApiHelper.GetProxies2();

            int page = 0;
            List<String> ips = new List<string>();
            foreach (KeyValuePair<string, int> keyValuePair in proxies)
            {
                String ip = keyValuePair.Key.Trim();
                int port = keyValuePair.Value;

                var b = GoogleGeoApiHelper.ProxyTest("https://www.yahoo.com/", ip);
                Console.WriteLine(String.Format("ProxyTest={0}  IP={1}".ToUpper(), b, ip));
                _logger.Info(String.Format("ProxyTest={0}  IP={1}".ToUpper(), b, ip));
                if (b && !ips.Contains(ip))
                {
                    int top = 750;
                    int skip = page * top;
                    page++;
                    ips.Add(ip);
                    Console.WriteLine("Tested IP " + ip);
                    Task.Factory.StartNew(() => ExtractLocations(top, skip, ip, port));
                }


            }

            _logger.Info("Location geo parsing is over.");
            Console.WriteLine("Location geo parsing is over.");
        }
        public static void ExtractLocations(int top, int skip, string ip, int port)
        {
            try
            {

                var locations = GetAddresses(top, skip);
                _logger.Info("IP=" + ip + " port=" + port + " Number of records =" + locations.Count() + " top=" + top + " skip=" + skip);
                Console.WriteLine("IP=" + ip + " port=" + port + " Number of records =" + locations.Count() + " top=" + top + " skip=" + skip);
                if (!locations.Any())
                {
                    return;
                }


                int i = 1;
                foreach (var nwmLocation in locations)
                {
                    try
                    {
                        String address =   String.Format("{0} {1} {2} {3}", 
                            nwmLocation.Address,
                            nwmLocation.City, 
                            nwmLocation.Zip, 
                            nwmLocation.Country);
                        string nwmLocationId = nwmLocation.Id;
                        address = address.Trim();
                        if (String.IsNullOrEmpty(address))
                            continue;

                        var geoLocation = LocationParser.GetParseLocation(address, ip, port);
                        Thread.Sleep(500);
                        _logger.Info(String.Format("{0} {1}", ip, geoLocation.city));
                        nwmLocation.GCity = geoLocation.city.ToStr();
                        nwmLocation.GCountry = geoLocation.country.ToStr();
                        nwmLocation.GState = geoLocation.state.ToStr();
                        nwmLocation.GZip = geoLocation.zip.ToStr();
                        nwmLocation.GAddress = geoLocation.address.ToStr();
                        nwmLocation.Latitude = geoLocation.latitude.ToFloat();
                        nwmLocation.Longitude = geoLocation.longitude.ToFloat();
                        nwmLocation.GoogleApiXml = geoLocation.GoogleApiResult.ToStr();
                        nwmLocation.streetNumber = geoLocation.streetNumber;
                        nwmLocation.premise = geoLocation.premise;
                        nwmLocation.subpremise = geoLocation.subpremise;
                        SaveOrUpdateUnformattedAddressShiporacleCom(nwmLocation);


                        Console.WriteLine("{0} {1} {2} {3}", nwmLocation.Id, geoLocation.country,
                                 geoLocation.latitude, geoLocation.longitude);


                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message.ToUpper());
                        _logger.Info("Message=" + e.Message.ToUpper());
                        if (e.Message.Equals("OVER_QUERY_LIMIT"))
                            break;
                        if (e.Message.Equals("Unable to connect to the remote server"))
                        {
                            break;
                        }
                        if (e.Message.ToUpper().Contains("Forbidden".ToUpper()))
                            break;


                    }
                }


                _logger.Info("Location geo parsing is done.Ip=" + ip + " Port=" + port);
                Console.WriteLine("Location geo parsing is done.Ip=" + ip + " Port=" + port);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }
        }
    }

}
