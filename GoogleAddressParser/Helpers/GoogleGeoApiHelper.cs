using GoogleAddressParser.Entities;
using GoogleAddressParser.Repositories;
using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GoogleAddressParser.Helpers
{
    public class GoogleGeoApiHelper
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        public static XDocument GetAddressResponseFromApi(string address, String proxyIp = "201.47.57.178", int port = 3128)
        {
            //https://developers.google.com/maps/documentation/geocoding/start
            //https://maps.googleapis.com/maps/api/geocode/json?address=1600+Amphitheatre+Parkway,+Mountain+View,+CA&key=
            string key = "";
            var requestUri = string.Format("https://maps.googleapis.com/maps/api/geocode/xml?address={0}&key={1}",
                                           Uri.EscapeDataString(address), key);

            // var requestUri = string.Format("http://maps.googleapis.com/maps/api/xml?address={0}&sensor=false",Uri.EscapeDataString(address));

            // var request = WebRequest.Create(requestUri);
            string requestString = "";
            string responseString = HttpHelper.GetWebRequest(requestUri, requestString, "GET", System.Text.Encoding.UTF8);

            if (!String.IsNullOrEmpty(proxyIp))
            {
                WebProxy proxyObject = new WebProxy(proxyIp, port);
                //// Disable proxy use when the host is local.
                //proxyObject.BypassProxyOnLocal = true;
                //// HTTP requests use this proxy information.
                GlobalProxySelection.Select = proxyObject;
               // request.Proxy = proxyObject;
            }
            //  var response = request.GetResponse();
            // var xdoc = XDocument.Load(response.GetResponseStream());
            var xdoc = XDocument.Load(new StringReader(responseString));
            if (xdoc == null)
                throw new Exception("Xml is not found");
            var result = xdoc.Element("GeocodeResponse").Element("status");
            if (result.Value.Equals("OVER_QUERY_LIMIT"))
            {
                throw new Exception("OVER_QUERY_LIMIT");
            }

            return xdoc;
        }

        public static Boolean ProxyTest(string url, String proxyIp)
        {
            Boolean isResult = false;
            long len = 0;
            proxyIp = proxyIp.Trim();
            String message = "Done";
            try
            {

                var requestUri = string.Format("http://maps.googleapis.com/maps/api/geocode/xml?address={0}&sensor=false",
                               Uri.EscapeDataString("1740 10 St Sw,	Calgary,	AB,	T2T 3E8,	Canada"));
                var request = WebRequest.Create(requestUri);
                WebProxy proxyObject = new WebProxy(proxyIp, 8080);

                //// Disable proxy use when the host is local.
                //proxyObject.BypassProxyOnLocal = true;

                //// HTTP requests use this proxy information.
                GlobalProxySelection.Select = proxyObject;

                request.Proxy = proxyObject;
                var response = request.GetResponse();

                len = response.ContentLength;

                var xdoc = XDocument.Load(response.GetResponseStream());
                isResult = true;
                var result = xdoc.Element("GeocodeResponse").Element("status");
                if (result.Value.Equals("OVER_QUERY_LIMIT"))
                {
                    isResult = false;
                }
                // convert stream to string
                //var reader = new StreamReader(stream);
                //string text = reader.ReadToEnd();
                //message = text;
            }
            catch (Exception e)
            {
                message = e.Message;

            }
            //Console.WriteLine(String.Format("url={0} proxyIp={1} ContentLength={2} message={3}", url, proxyIp, len, message));

            return isResult;
        }


        public static Dictionary<string, int> GetProxies2()
        {
            var proxies = new Dictionary<string, int>();
            proxies["201.221.131.76"] = 8080;
            proxies["190.184.144.86"] = 8080;
            proxies["190.199.230.154 "] = 8080;
            proxies["186.232.160.54  "] = 8080;
            proxies["190.151.10.226  "] = 8080;
            proxies["201.221.131.76  "] = 8080;
            proxies["190.199.230.154  "] = 8080;
            proxies["177.107.97.246  "] = 8080;
            proxies["190.184.144.86  "] = 8080;
            proxies["190.0.16.58  "] = 8080;
            proxies["190.0.16.58  "] = 8080;
            proxies["190.151.10.226 "] = 8080;
            proxies["190.184.144.86  "] = 8080;
            proxies["190.0.16.58  "] = 8080;
            proxies["186.232.160.54 "] = 8080;
            proxies["189.17.66.162 "] = 8080;


            proxies["190.151.10.226 "] = 8080;
            proxies["186.232.160.54 "] = 8080;



            proxies["190.0.16.58 "] = 8080;
            proxies["190.184.144.86 "] = 8080;
            proxies["190.203.169.157 "] = 8080;
            proxies["177.107.97.246 "] = 8080;
            proxies["190.207.117.87 "] = 8080;


            proxies["186.92.72.247 "] = 8080;
            proxies["190.207.117.87 "] = 8080;

            proxies["	190.207.117.87"] = 8080;
            proxies["	186.93.135.97 "] = 9064;
            proxies["	190.202.252.224	"] = 8080;
            proxies["	201.242.53.95	"] = 8080;
            proxies["	186.92.72.247 "] = 8080;



            proxies["	177.192.178.171	"] = 8080;
            proxies["	186.227.53.30	"] = 8080;
            proxies["	186.232.160.54	"] = 8080;
            proxies["	186.94.69.133	"] = 8080;
            proxies["	187.105.87.182	"] = 8080;
            proxies["	187.109.201.17	"] = 8080;
            proxies["	190.153.113.22	"] = 8080;
            proxies["	190.203.253.125	"] = 8080;
            proxies["	190.205.156.233	"] = 8080;
            proxies["	190.39.123.171	"] = 8080;
            proxies["	190.73.60.172	"] = 8080;
            proxies["	200.68.9.92	"] = 8080;
            proxies["	201.76.172.110	"] = 8080;

            proxies["	201.210.64.184	"] = 8080;
            proxies["	187.18.158.9	"] = 8080;
            proxies["	187.109.201.17	"] = 8080;
            proxies["	177.192.178.171	"] = 8080;
            proxies["	190.73.132.214	"] = 8080;
            proxies["	200.188.217.190	"] = 8080;
            proxies["	186.232.160.54	"] = 8080;
            proxies["	177.136.224.17	"] = 8080;
            proxies["	201.76.172.110	"] = 8080;
            proxies["	190.205.152.172	"] = 8080;
            proxies["	190.73.60.172	"] = 8080;
            proxies["	190.198.81.26	"] = 8080;
            proxies["	201.243.151.249	"] = 8080;
            proxies["	187.105.87.182	"] = 8080;
            proxies["	186.94.69.133	"] = 8080;
            proxies["	186.95.55.165	"] = 8080;
            proxies["	190.151.10.226	"] = 8080;
            proxies["	190.73.140.94	"] = 8080;
            proxies["	186.227.53.30	"] = 8080;
            proxies["	186.88.104.22	"] = 8080;
            proxies["	201.211.139.194	"] = 8080;
            proxies["	190.36.126.50	"] = 8080;
            proxies["	190.39.123.171	"] = 8080;
            proxies["	190.198.5.121	"] = 8080;
            proxies["	190.207.235.159	"] = 8080;
            proxies["	186.93.105.189	"] = 8080;
            proxies["	190.153.113.22	"] = 8080;
            proxies["	186.95.212.60	"] = 8080;
            proxies["	186.92.178.238	"] = 8080;
            proxies["	190.203.253.125	"] = 8080;
            proxies["	200.68.9.92	"] = 8080;
            proxies["	190.36.139.167	"] = 8080;
            proxies["	186.94.86.1	"] = 8080;
            proxies["	190.203.237.215	"] = 8080;
            proxies["	186.92.101.44	"] = 8080;
            proxies["	190.207.136.35	"] = 8080;
            proxies["	190.205.156.233	"] = 8080;
            proxies["	190.73.130.138	"] = 8080;
            proxies["	186.92.217.241	"] = 8080;
            proxies["	190.198.113.243	"] = 8080;
            proxies["	186.95.40.14	"] = 8080;
            proxies["	190.204.17.114	"] = 8080;
            proxies["	190.205.248.139	"] = 8080;
            proxies["	186.95.232.59	"] = 8080;
            proxies["	177.107.97.246	"] = 8080;
            proxies["	190.73.249.66	"] = 8080;
            proxies["	190.39.117.112	"] = 8080;
            proxies["	201.248.124.65	"] = 8080;
            proxies["	190.78.129.93	"] = 8080;
            proxies["	200.93.90.166	"] = 8080;
            proxies["	200.84.44.65	"] = 8080;
            proxies["	201.75.7.21	"] = 8080;
            proxies["	190.198.217.139	"] = 8080;
            proxies["	186.91.38.184	"] = 8080;
            proxies["	190.207.4.174	"] = 8080;
            proxies["	177.23.178.70	"] = 8080;
            proxies["	190.203.169.157	"] = 8080;
            proxies["	177.220.201.4	"] = 8080;
            proxies["	186.89.233.53	"] = 8080;
            proxies["	190.79.26.86	"] = 8080;
            proxies["	201.210.80.40	"] = 8080;
            proxies["	190.198.29.202	"] = 8080;
            proxies["	190.38.88.86	"] = 8080;
            proxies["	186.88.97.168	"] = 8080;
            proxies["	186.88.177.243	"] = 8080;
            proxies["	190.198.82.242	"] = 8080;
            proxies["	186.232.26.10	"] = 8080;
            proxies["	190.206.243.234	"] = 8080;
            proxies["	190.37.222.16	"] = 8080;
            proxies["	189.17.66.162	"] = 8080;
            proxies["	186.90.126.122	"] = 8080;
            proxies["	190.79.152.123	"] = 8080;
            proxies["	186.232.83.47	"] = 8080;
            proxies["	190.73.130.22	"] = 8080;
            proxies["	190.36.7.40	"] = 8080;
            proxies["	186.89.31.193	"] = 8080;
            proxies["	190.73.117.164	"] = 8080;
            proxies["	200.84.163.65	"] = 8080;
            proxies["	190.36.0.252	"] = 8080;
            proxies["	190.94.217.129	"] = 8080;
            proxies["	177.87.104.119	"] = 8080;


            return proxies;
        }

        public static Dictionary<string, int> GetProxies()
        {
            var proxies = new Dictionary<string, int>();
            proxies["	190.204.17.114	"] = 8080;
            proxies["	177.107.97.246	"] = 8080;
            proxies["	201.248.124.65	"] = 8080;
            proxies["	190.78.129.93	"] = 8080;
            proxies["	200.93.90.166	"] = 8080;
            proxies["	201.75.7.21	"] = 8080;
            proxies["	190.198.217.139	"] = 8080;
            proxies["	190.39.123.171	"] = 8080;
            proxies["	177.23.178.70	"] = 8080;
            proxies["	190.203.169.157	"] = 8080;
            proxies["	186.89.233.53	"] = 8080;
            proxies["	190.38.88.86	"] = 8080;
            proxies["	186.88.97.168	"] = 8080;
            proxies["	186.88.177.243	"] = 8080;
            proxies["	190.198.82.242	"] = 8080;
            proxies["	189.17.66.162	"] = 8080;
            proxies["	190.79.152.123	"] = 8080;
            proxies["	186.232.83.47	"] = 8080;
            proxies["	190.73.117.164	"] = 8080;
            proxies["	190.36.0.252	"] = 8080;
            proxies["	190.94.217.129	"] = 8080;
            proxies["	177.87.104.119	"] = 8080;
            proxies["	186.237.23.160	"] = 8080;
            proxies["	186.14.156.50	"] = 8080;
            proxies["	201.211.196.23	"] = 8080;
            proxies["	190.198.149.205	"] = 8080;
            proxies["	186.88.128.251	"] = 8080;
            proxies["	190.199.213.61	"] = 8080;
            proxies["	177.129.88.39	"] = 8080;
            proxies["	186.88.172.25	"] = 8080;
            proxies["	177.124.101.108	"] = 8080;
            proxies["	187.78.35.194	"] = 8080;
            proxies["	190.39.151.49	"] = 8080;
            proxies["	190.37.191.159	"] = 8080;
            proxies["	201.209.94.3	"] = 8080;
            proxies["	201.243.102.12	"] = 8080;
            proxies["	190.75.39.84	"] = 8080;
            proxies["	190.206.182.40	"] = 8080;
            proxies["	190.78.183.253	"] = 8080;
            proxies["	190.202.245.112	"] = 8080;
            proxies["	201.209.177.80	"] = 8080;
            proxies["	190.201.45.89	"] = 8080;
            proxies["	190.206.230.43	"] = 8080;
            proxies["	190.199.230.154	"] = 8080;
            proxies["	190.72.148.237	"] = 8080;
            proxies["	190.198.161.151	"] = 8080;
            proxies["	201.211.242.115	"] = 8080;
            proxies["	186.70.19.88	"] = 8080;
            // proxies["	189.254.245.197	"] = 8080;
            proxies["	207.248.42.139	"] = 8080;
            proxies["	190.221.29.210	"] = 8080;
            proxies["	190.221.29.213	"] = 8080;
            proxies["	201.221.131.62	"] = 8080;
            proxies["	190.27.246.2	"] = 8080;
            proxies["	190.184.144.86	"] = 8080;
            proxies["	201.221.133.94	"] = 8080;
            proxies["	186.103.143.214	"] = 8080;
            proxies["	190.90.193.18	"] = 8080;
            proxies["	190.0.16.58	"] = 8080;
            proxies["	201.221.131.76	"] = 8080;
            proxies["	190.184.144.6	"] = 8080;
            proxies["	190.79.26.86	"] = 8080;
            proxies["	201.210.80.40	"] = 8080;
            proxies["	190.198.29.202	"] = 8080;
            proxies["	190.206.243.234	"] = 8080;
            proxies["	190.37.222.16	"] = 8080;
            proxies["	186.90.126.122	"] = 8080;
            proxies["	190.73.130.22	"] = 8080;
            proxies["	190.36.7.40	"] = 8080;
            proxies["	186.89.31.193	"] = 8080;
            proxies["	200.84.163.65	"] = 8080;
            proxies["	186.93.141.111	"] = 8080;
            proxies["	190.73.98.96	"] = 8080;
            proxies["	190.207.190.75	"] = 8080;
            proxies["	190.204.155.17	"] = 8080;
            proxies["	190.77.182.191	"] = 8080;
            proxies["	186.93.216.203	"] = 8080;
            proxies["	190.207.239.19	"] = 8080;
            proxies["	186.88.102.35	"] = 8080;
            proxies["	186.93.102.155	"] = 8080;
            proxies["177.99.197.1"] = 8080;
            proxies["187.72.175.250"] = 8080;
            proxies["186.251.144.9"] = 8080;
            proxies["177.220.201.4"] = 8080;
            proxies["201.70.183.154"] = 8080;
            proxies["	190.221.29.210	"] = 8080;
            proxies["	190.221.29.213	"] = 8080;
            proxies["	177.220.201.4	"] = 8080;
            proxies["	186.227.208.59	"] = 8080;
            proxies["	187.84.187.8	"] = 8080;
            proxies["	177.99.173.138	"] = 8080;
            proxies["	200.196.234.30	"] = 8080;
            proxies["	200.192.215.138	"] = 8080;
            proxies["	190.77.182.191	"] = 8080;
            proxies["	186.89.31.193	"] = 8080;
            proxies["	190.37.222.16	"] = 8080;
            proxies["	200.84.163.65	"] = 8080;
            proxies["	186.93.102.155	"] = 8080;
            proxies["	190.36.7.40	"] = 8080;
            proxies["	190.73.130.22	"] = 8080;
            proxies["	190.73.98.96	"] = 8080;
            proxies["	190.207.190.75	"] = 8080;
            proxies["	190.206.243.234	"] = 8080;
            proxies["	201.242.71.111	"] = 8080;
            proxies["	190.204.155.17	"] = 8080;
            proxies["	186.90.126.122	"] = 8080;
            proxies["	186.93.216.203	"] = 8080;
            proxies["	186.88.51.30	"] = 8080;
            proxies["	190.207.239.19	"] = 8080;
            proxies["	186.88.102.35	"] = 8080;
            proxies["	190.73.153.202	"] = 8080;
            proxies["	186.93.141.111	"] = 8080;
            proxies["	186.232.160.54	"] = 8080;
            proxies["	186.232.26.10	"] = 8080;
            proxies["190.201.109.173"] = 8080;
            proxies["200.84.163.65"] = 8080;
            proxies["186.89.31.193"] = 8080;
            proxies["177.220.201.4"] = 8080;
            proxies["186.232.160.54"] = 8080;
            proxies["177.99.173.138"] = 8080;
            proxies["186.237.23.160"] = 8080;
            proxies["125.214.187.26"] = 8080;
            proxies["222.222.251.129"] = 8080;
            proxies["190.73.153.202"] = 8080;
            proxies["201.243.102.12"] = 8080;
            proxies["200.188.217.190"] = 8080;
            proxies["201.211.143.29"] = 8080;
            proxies["188.191.80.8"] = 8080;
            proxies["186.91.221.87"] = 8080;
            proxies["186.95.66.162"] = 8080;
            proxies["190.0.16.58"] = 8080;
            proxies["189.208.57.239"] = 8080;
            proxies["177.87.107.1"] = 8080;
            proxies["177.220.201.4"] = 8080;
            proxies["186.88.51.30"] = 8080;
            proxies["187.61.117.11"] = 8080;
            proxies["177.12.224.54"] = 8080;
            proxies["200.153.105.1"] = 8080;
            proxies["110.78.164.61"] = 3128;
            proxies["201.70.183.130"] = 3128;
            proxies["200.196.234.30"] = 8080;
            proxies["190.207.239.19"] = 8080;
            proxies["190.184.144.6"] = 8080;
            proxies["190.85.49.218"] = 80;
            proxies["125.214.187.26"] = 8080;
            proxies["121.96.18.196"] = 8080;
            proxies["190.207.227.214"] = 8080;
            proxies["177.67.130.226"] = 8080;
            proxies["177.21.200.1"] = 8080;
            proxies["177.87.104.116"] = 8080;
            proxies["190.90.193.18"] = 8080;
            proxies["190.36.163.218"] = 8080;
            return proxies;
        }

       


        private static List<String> nonGoodIps()
        {
            var list = new List<String>();
            list.Add("	177.107.97.246	");
            list.Add("	177.136.224.17	");
            list.Add("	177.87.104.119	");
            list.Add("	186.232.26.10	");
            list.Add("	186.88.104.22	");
            list.Add("	186.88.177.243	");
            list.Add("	186.88.97.168	");
            list.Add("	186.89.233.53	");
            list.Add("	186.89.31.193	");
            list.Add("	186.90.126.122	");
            list.Add("	186.91.38.184	");
            list.Add("	186.92.101.44	");
            list.Add("	186.92.178.238	");
            list.Add("	186.92.217.241	");
            list.Add("	186.93.105.189	");
            list.Add("	186.94.86.1	");
            list.Add("	186.95.212.60	");
            list.Add("	186.95.232.59	");
            list.Add("	186.95.40.14	");
            list.Add("	186.95.55.165	");
            list.Add("	187.105.87.182	");
            list.Add("	189.17.66.162	");
            list.Add("	190.153.113.22	");
            list.Add("	190.198.113.243	");
            list.Add("	190.198.217.139	");
            list.Add("	190.198.29.202	");
            list.Add("	190.198.5.121	");
            list.Add("	190.198.81.26	");
            list.Add("	190.198.82.242	");
            list.Add("	190.203.169.157	");
            list.Add("	190.203.237.215	");
            list.Add("	190.203.253.125	");
            list.Add("	190.204.17.114	");
            list.Add("	190.205.152.172	");
            list.Add("	190.205.156.233	");
            list.Add("	190.205.248.139	");
            list.Add("	190.206.243.234	");
            list.Add("	190.207.136.35	");
            list.Add("	190.207.235.159	");
            list.Add("	190.207.4.174	");
            list.Add("	190.36.0.252	");
            list.Add("	190.36.126.50	");
            list.Add("	190.36.139.167	");
            list.Add("	190.36.7.40	");
            list.Add("	190.37.222.16	");
            list.Add("	190.38.88.86	");
            list.Add("	190.39.117.112	");
            list.Add("	190.39.123.171	");
            list.Add("	190.73.117.164	");
            list.Add("	190.73.130.138	");
            list.Add("	190.73.130.22	");
            list.Add("	190.73.132.214	");
            list.Add("	190.73.140.94	");
            list.Add("	190.73.249.66	");
            list.Add("	190.78.129.93	");
            list.Add("	190.79.152.123	");
            list.Add("	190.79.26.86	");
            list.Add("	190.94.217.129	");
            list.Add("	200.188.217.190	");
            list.Add("	200.68.9.92	");
            list.Add("	200.84.163.65	");
            list.Add("	200.84.44.65	");
            list.Add("	200.93.90.166	");
            list.Add("	201.210.64.184	");
            list.Add("	201.211.139.194	");
            list.Add("	201.243.151.249	");
            list.Add("	201.248.124.65	");
            list.Add("	201.76.172.110	");

            return list;
        }

    }
}
