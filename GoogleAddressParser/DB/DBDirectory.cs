using GoogleAddressParser.Entities;
using GoogleAddressParser.Helpers;
using PlaceHoldersEngine.Domain.DB;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleAddressParser.DB
{
    public class DBDirectory
    {
        public static string ConnectionStringKey = "TestEYConnectionStringKey";
        public static int SaveOrUpdateUnFormattedAddress(UnFormattedAddress item)
        {
            string connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringKey].ConnectionString;
            String commandText = @"SaveOrUpdateUnFormattedAddress";
            var parameterList = new List<SqlParameter>();
            var commandType = CommandType.StoredProcedure;
            parameterList.Add(DatabaseUtility.GetSqlParameter("Id", item.Id.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("Name", item.Name.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("AddressSrc", item.AddressSrc.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("Country", item.Country.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("Zip", item.Zip.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("City", item.City.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("StateRegion", item.StateRegion.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("AddressLine1", item.AddressLine1.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("AddressLine2", item.AddressLine2.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("AddressLine3", item.AddressLine3.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("AddressPostBox", item.AddressPostBox.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("Logo", item.Logo.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("Url", item.Url.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("Email", item.Email.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("Phone", item.Phone.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("Fax", item.Fax.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("Description", item.Description.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("Sectors", item.Sectors.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("RegionalMarkets", item.RegionalMarkets.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("Registrations", item.Registrations.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("HasMoreOffices", item.HasMoreOffices.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("lblDandB", item.lblDandB.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("lblFPAL", item.lblFPAL.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("lblAchilles", item.lblAchilles.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("GAddress", item.GAddress.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("GCity", item.GCity.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("GState", item.GState.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("GCountry", item.GCountry.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("GZip", item.GZip.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("GoogleApiXml", item.GoogleApiXml.ToStr(), SqlDbType.NVarChar));

            parameterList.Add(DatabaseUtility.GetSqlParameter("Latitude", item.Latitude, SqlDbType.Float));
            parameterList.Add(DatabaseUtility.GetSqlParameter("Longitude", item.Longitude, SqlDbType.Float));
            parameterList.Add(DatabaseUtility.GetSqlParameter("IsGoodAddress", item.IsGoodAddress, SqlDbType.Bit));

            parameterList.Add(DatabaseUtility.GetSqlParameter("streetNumber", item.streetNumber.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("premise", item.premise.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("subpremise", item.subpremise.ToStr(), SqlDbType.NVarChar));


            int id = DatabaseUtility.ExecuteScalar(new SqlConnection(connectionString), commandText, commandType, parameterList.ToArray()).ToInt();
            return id;
        }

        public static UnFormattedAddress GetUnFormattedAddress(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringKey].ConnectionString;
            String commandText = @"SELECT * FROM UnFormattedAddress WHERE id=@id";
            var parameterList = new List<SqlParameter>();
            var commandType = CommandType.Text;
            parameterList.Add(DatabaseUtility.GetSqlParameter("id", id, SqlDbType.Int));
            DataSet dataSet = DatabaseUtility.ExecuteDataSet(new SqlConnection(connectionString), commandText, commandType, parameterList.ToArray());
            if (dataSet.Tables.Count > 0)
            {
                using (DataTable dt = dataSet.Tables[0])
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        var e = GetUnFormattedAddressFromDataRow(dr);
                        return e;
                    }
                }
            }
            return null;
        }

        public static List<UnFormattedAddress> GetUnFormattedAddresss()
        {
            var list = new List<UnFormattedAddress>();
            String commandText = @"SELECT * FROM UnFormattedAddress ORDER BY Id DESC";
            var parameterList = new List<SqlParameter>();
            string connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringKey].ConnectionString;
            var commandType = CommandType.Text;
            DataSet dataSet = DatabaseUtility.ExecuteDataSet(new SqlConnection(connectionString), commandText, commandType, parameterList.ToArray());
            if (dataSet.Tables.Count > 0)
            {
                using (DataTable dt = dataSet.Tables[0])
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        var e = GetUnFormattedAddressFromDataRow(dr);
                        list.Add(e);
                    }
                }
            }
            return list;
        }
        public static void DeleteUnFormattedAddress(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringKey].ConnectionString;
            String commandText = @"DELETE FROM UnFormattedAddress WHERE Id=@Id";
            var parameterList = new List<SqlParameter>();
            var commandType = CommandType.Text;
            parameterList.Add(DatabaseUtility.GetSqlParameter("Id", id, SqlDbType.Int));
            DatabaseUtility.ExecuteNonQuery(new SqlConnection(connectionString), commandText, commandType, parameterList.ToArray());
        }

        private static UnFormattedAddress GetUnFormattedAddressFromDataRow(DataRow dr)
        {
            var item = new UnFormattedAddress();

            item.Id = dr["Id"].ToStr();
            item.Name = dr["Name"].ToStr();
            item.AddressSrc = dr["AddressSrc"].ToStr();
            item.Country = dr["Country"].ToStr();
            item.Zip = dr["Zip"].ToStr();
            item.City = dr["City"].ToStr();
            item.StateRegion = dr["StateRegion"].ToStr();
            item.AddressLine1 = dr["AddressLine1"].ToStr();
            item.AddressLine2 = dr["AddressLine2"].ToStr();
            item.AddressLine3 = dr["AddressLine3"].ToStr();
            item.AddressPostBox = dr["AddressPostBox"].ToStr();
            item.Logo = dr["Logo"].ToStr();
            item.Url = dr["Url"].ToStr();
            item.Email = dr["Email"].ToStr();
            item.Phone = dr["Phone"].ToStr();
            item.Fax = dr["Fax"].ToStr();
            item.Description = dr["Description"].ToStr();
            item.Sectors = dr["Sectors"].ToStr();
            item.RegionalMarkets = dr["RegionalMarkets"].ToStr();
            item.Registrations = dr["Registrations"].ToStr();
            item.HasMoreOffices = dr["HasMoreOffices"].ToStr();
            item.lblDandB = dr["lblDandB"].ToStr();
            item.lblFPAL = dr["lblFPAL"].ToStr();
            item.lblAchilles = dr["lblAchilles"].ToStr();
            item.GAddress = dr["GAddress"].ToStr();
            item.GCity = dr["GCity"].ToStr();
            item.GState = dr["GState"].ToStr();
            item.GCountry = dr["GCountry"].ToStr();
            item.GZip = dr["GZip"].ToStr();
            item.GoogleApiXml = dr["GoogleApiXml"].ToStr();
            return item;
        }

      
        public static int SaveOrUpdateUnformattedAddressShiporacleCom(UnformattedAddressShiporacleCom item)
        {
            string connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringKey].ConnectionString;
            String commandText = @"SaveOrUpdateUnformattedAddressShiporacleCom";
            var parameterList = new List<SqlParameter>();
            var commandType = CommandType.StoredProcedure;
            parameterList.Add(DatabaseUtility.GetSqlParameter("Id", item.Id.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("Company", item.Company.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("Country", item.Country.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("Phone", item.Phone.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("Services", item.Services.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("City", item.City.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("Address", item.Address.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("Zip", item.Zip.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("GAddress", item.GAddress.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("GCity", item.GCity.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("GState", item.GState.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("GCountry", item.GCountry.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("GZip", item.GZip.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("GoogleApiXml", item.GoogleApiXml.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("Latitude", item.Latitude, SqlDbType.Float));
            parameterList.Add(DatabaseUtility.GetSqlParameter("Longitude", item.Longitude, SqlDbType.Float));
            parameterList.Add(DatabaseUtility.GetSqlParameter("streetNumber", item.streetNumber.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("premise", item.premise.ToStr(), SqlDbType.NVarChar));
            parameterList.Add(DatabaseUtility.GetSqlParameter("subpremise", item.subpremise.ToStr(), SqlDbType.NVarChar));
            int id = DatabaseUtility.ExecuteScalar(new SqlConnection(connectionString), commandText, commandType, parameterList.ToArray()).ToInt();
            return id;
        }

        public static UnformattedAddressShiporacleCom GetUnformattedAddressShiporacleCom(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringKey].ConnectionString;
            String commandText = @"SELECT * FROM UnformattedAddressShiporacleCom WHERE id=@id";
            var parameterList = new List<SqlParameter>();
            var commandType = CommandType.Text;
            parameterList.Add(DatabaseUtility.GetSqlParameter("id", id, SqlDbType.Int));
            DataSet dataSet = DatabaseUtility.ExecuteDataSet(new SqlConnection(connectionString), commandText, commandType, parameterList.ToArray());
            if (dataSet.Tables.Count > 0)
            {
                using (DataTable dt = dataSet.Tables[0])
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        var e = GetUnformattedAddressShiporacleComFromDataRow(dr);
                        return e;
                    }
                }
            }
            return null;
        }

        public static List<UnformattedAddressShiporacleCom> GetUnformattedAddressShiporacleComs()
        {
            var list = new List<UnformattedAddressShiporacleCom>();
            String commandText = @"SELECT * FROM UnformattedAddressShiporacleCom ORDER BY Id DESC";
            var parameterList = new List<SqlParameter>();
            string connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringKey].ConnectionString;
            var commandType = CommandType.Text;
            DataSet dataSet = DatabaseUtility.ExecuteDataSet(new SqlConnection(connectionString), commandText, commandType, parameterList.ToArray());
            if (dataSet.Tables.Count > 0)
            {
                using (DataTable dt = dataSet.Tables[0])
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        var e = GetUnformattedAddressShiporacleComFromDataRow(dr);
                        list.Add(e);
                    }
                }
            }
            return list;
        }
        public static void DeleteUnformattedAddressShiporacleCom(int id)
        {
            string connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringKey].ConnectionString;
            String commandText = @"DELETE FROM UnformattedAddressShiporacleCom WHERE Id=@Id";
            var parameterList = new List<SqlParameter>();
            var commandType = CommandType.Text;
            parameterList.Add(DatabaseUtility.GetSqlParameter("Id", id, SqlDbType.Int));
            DatabaseUtility.ExecuteNonQuery(new SqlConnection(connectionString), commandText, commandType, parameterList.ToArray());
        }

        private static UnformattedAddressShiporacleCom GetUnformattedAddressShiporacleComFromDataRow(DataRow dr)
        {
            var item = new UnformattedAddressShiporacleCom();

            item.Id = dr["Id"].ToStr();
            item.Company = dr["Company"].ToStr();
            item.Country = dr["Country"].ToStr();
            item.Phone = dr["Phone"].ToStr();
            item.Services = dr["Services"].ToStr();
            item.City = dr["City"].ToStr();
            item.Address = dr["Address"].ToStr();
            item.Zip = dr["Zip"].ToStr();
            item.GAddress = dr["GAddress"].ToStr();
            item.GCity = dr["GCity"].ToStr();
            item.GState = dr["GState"].ToStr();
            item.GCountry = dr["GCountry"].ToStr();
            item.GZip = dr["GZip"].ToStr();
            item.GoogleApiXml = dr["GoogleApiXml"].ToStr();
            item.Latitude = dr["Latitude"].ToFloat();
            item.Longitude = dr["Longitude"].ToFloat();
            item.streetNumber = dr["streetNumber"].ToStr();
            item.premise = dr["premise"].ToStr();
            item.subpremise = dr["subpremise"].ToStr();
            return item;
        }



    }
}
