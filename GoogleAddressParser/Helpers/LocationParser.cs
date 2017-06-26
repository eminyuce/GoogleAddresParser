using GoogleAddressParser.Entities;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GoogleAddressParser.Helpers
{
    public class LocationParser
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();

        public static GoogleLocation GetParseLocation(string address, String ip, int port, Boolean isLatitudeOnly = false)
        {
            address = Regex.Replace(address, @"\r\n?|\n", ", ");
            address = address.Replace("\n", String.Empty);
            address = address.Replace("\r", String.Empty);
            address = address.Replace("\t", String.Empty);
            XDocument xdoc = GoogleGeoApiHelper.GetAddressResponseFromApi(address, ip, port);
            _logger.Debug(String.Format("Address={0} IP={1} PORT={2} API={3}", address, ip, port, xdoc.ToStr()));
            GoogleLocation loc = ParseGoogleApiXmlResult(xdoc, isLatitudeOnly);


            return loc;
        }
        public static GoogleLocation ParseGoogleApiXmlResult(String xdoc, Boolean isLatitudeOnly = false)
        {
            if (string.IsNullOrEmpty(xdoc))
            {
                return new GoogleLocation();
            }
            return ParseGoogleApiXmlResult(XDocument.Parse(xdoc), isLatitudeOnly);
        }
        public static GoogleLocation ParseGoogleApiXmlResult(XDocument xdoc, Boolean isLatitudeOnly = false)
        {
            GoogleLocation loc = new GoogleLocation();
            if (xdoc == null)
            {
                return loc;
            }
            loc.GoogleApiResult = xdoc.ToString();
            var xElement1 = xdoc.Element("GeocodeResponse");
            if (xElement1 != null)
            {
                var result = xElement1.Element("result");
                if (result == null)
                    return loc;
                var element1 = result.Element("geometry");
                if (element1 != null)
                {
                    var locationElement = element1.Element("location");
                    if (locationElement != null)
                    {
                        var xElement = locationElement.Element("lat");
                        var element = locationElement.Element("lng");
                        if (element != null)
                        {
                            var lng = element.Value.Trim().ToStr();
                            loc.longitude = lng.ToFloat();
                        }

                        if (xElement != null)
                        {
                            var lat = xElement.Value.Trim().ToStr();
                            loc.latitude = lat.ToFloat();
                        }
                    }
                }
                if (isLatitudeOnly)
                {
                    return loc;
                }
                var formatted_address = result.Element("formatted_address");
                if (formatted_address != null)
                {
                    loc.formatted_address = formatted_address.Value.Trim().ToStr();
                }

                var addressComponent = result.Elements("address_component");
                String streetNumber = "";
                foreach (var xElement in addressComponent)
                {
                    var xElement2 = xElement.Element("type");
                    if (xElement2 != null)
                    {
                        var type = xElement2.Value.Trim().ToStr();
                        if (type.ToLower().Equals("country"))
                        {
                            var element = xElement.Element("long_name");
                            if (element != null) loc.country = element.Value.Trim().ToStr();
                        }
                        else if (type.ToLower().Equals("locality"))
                        {
                            var element = xElement.Element("long_name");
                            if (element != null) loc.city = element.Value.Trim().ToStr();
                        }
                        else if (type.ToLower().Equals("postal_code"))
                        {
                            var element = xElement.Element("long_name");
                            if (element != null) loc.zip = element.Value.Trim().ToStr();
                        }
                        else if (type.ToLower().Equals("street_number"))
                        {
                            var element = xElement.Element("long_name");
                            if (element != null) loc.streetNumber = element.Value.Trim().ToStr();
                        }
                        else if (type.ToLower().Equals("premise"))
                        {
                            var element = xElement.Element("long_name");
                            if (element != null) loc.premise = element.Value.Trim().ToStr();
                        }
                        else if (type.ToLower().Equals("subpremise"))
                        {
                            var element = xElement.Element("long_name");
                            if (element != null) loc.subpremise = element.Value.Trim().ToStr();
                        }
                        else if (type.ToLower().Equals("administrative_area_level_1"))
                        {
                            var element = xElement.Element("short_name");
                            if (element != null) loc.state = element.Value.Trim().ToStr();
                        }
                        else if (type.ToLower().Equals("route"))
                        {
                            var element = xElement.Element("long_name");
                            if (element != null)
                                loc.address = streetNumber + " " + element.Value.Trim().ToStr();
                        }
                        else if (type.ToLower().Equals("street_number"))
                        {
                            var element = xElement.Element("long_name");
                            if (element != null)
                                streetNumber = element.Value.Trim().ToStr();
                        }
                    }
                }
            }
            return loc;
        }
    }
}
