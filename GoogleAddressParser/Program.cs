using GoogleAddressParser.Entities;
using GoogleAddressParser.Helpers;
using GoogleAddressParser.Repositories;
using NLog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GoogleAddressParser
{
    class Program
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            //string ip = "";
            ////SaveUnformattedAddressShiporacleComs();
            //string address = "118 E 25th Fl 2 New York NY 10010";
            //var geoLocation = LocationParser.GetParseLocation(address, ip, 8080);
            //string googleXmlApi = geoLocation.GoogleApiResult;
            // 118 E 25th Fl 2 New York NY 10010
            //Console.WriteLine(googleXmlApi);

            //Console.ReadKey();
            var p = UnFormattedAddressRepository.GetUnFormattedAddresss();
            foreach (var item in p)
            {
                var geoLocation = LocationParser.ParseGoogleApiXmlResult(item.GoogleApiXml,false);
                if (!String.IsNullOrEmpty(geoLocation.streetNumber) || !String.IsNullOrEmpty(geoLocation.premise))
                {
                    // Console.WriteLine(geoLocation.formatted_address);
                    item.IsGoodAddress = true;
                    item.streetNumber = geoLocation.streetNumber;
                    item.premise = geoLocation.premise;
                    item.subpremise = geoLocation.subpremise;
                }
                else
                {
                    item.IsGoodAddress = false;
                }

                UnFormattedAddressRepository.SaveOrUpdateUnFormattedAddress(item);
            }

        }

        private static void SaveUnformattedAddressShiporacleComs()
        {
            string ip = "";
            //UnFormattedAddressRepository.ParseLocationAddressWithoutProxy(1000,0, ip, 8080);
            var items = UnformattedAddressShiporacleComRepository.GetUnformattedAddressShiporacleComs();
            var dt = ReadExcelFile("Test", @"C:\Users\Yuce\Desktop\samples2\ShiporacleCom-2017-06-12-All.xlsx");
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DataRow dr = dt.Rows[i];
                // do something with dr
                try
                {
                    var item = new UnformattedAddressShiporacleCom();

                    if (items.Any(t => t.Id.Equals(dr["Id"].ToStr())))
                    {
                        continue;
                    }

                    item.Id = dr["Id"].ToStr();
                    item.Company = dr["Company"].ToStr();
                    item.Country = dr["Country"].ToStr();
                    item.Phone = dr["Phone"].ToStr();
                    item.Services = dr["Services"].ToStr();
                    item.City = dr["City"].ToStr();
                    item.Address = dr["Address"].ToStr();
                    item.Zip = dr["Zip"].ToStr();

                    try
                    {
                        //string address = String.Format("{0} {1} {2} {3}", item.Address, item.City, item.Zip, item.Country);
                        //var geoLocation = LocationParser.GetParseLocation(address, ip, 8080);
                        //Thread.Sleep(500);
                        //item.GCity = geoLocation.city.ToStr();
                        //item.GCountry = geoLocation.country.ToStr();
                        //item.GState = geoLocation.state.ToStr();
                        //item.GZip = geoLocation.zip.ToStr();
                        //item.GAddress = geoLocation.address.ToStr();
                        //item.Latitude = geoLocation.latitude.ToFloat();
                        //item.Longitude = geoLocation.longitude.ToFloat();
                        //item.GoogleApiXml = geoLocation.GoogleApiResult.ToStr();

                    }
                    catch (Exception)
                    {


                    }


                    UnformattedAddressShiporacleComRepository.SaveOrUpdateUnformattedAddressShiporacleCom(item);
                    Console.WriteLine(item.Id);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                }
            }
            // DataTableHelper.ToPrintConsole(dt);
        }

        private static DataTable ReadExcelFile(string sheetName, string path)
        {

            using (OleDbConnection conn = new OleDbConnection())
            {
                DataTable dt = new DataTable();
                string Import_FileName = path;
                string fileExtension = Path.GetExtension(Import_FileName);
                if (fileExtension == ".xls")
                    conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Import_FileName + ";" + "Extended Properties='Excel 8.0;HDR=YES;'";
                if (fileExtension == ".xlsx")
                    conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Import_FileName + ";" + "Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                using (OleDbCommand comm = new OleDbCommand())
                {
                    comm.CommandText = "Select * from [" + sheetName + "$]";

                    comm.Connection = conn;

                    using (OleDbDataAdapter da = new OleDbDataAdapter())
                    {
                        da.SelectCommand = comm;
                        da.Fill(dt);
                        return dt;
                    }

                }
            }
        }
    }
}
