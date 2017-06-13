using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleAddressParser.Entities
{
    public class UnFormattedAddress
    {
        public string premise { get; set; }
        public string subpremise { get; set; }
        public string streetNumber { get; set; }

        public string Id { get; set; }
        public string Name { get; set; }
        public string AddressSrc { get; set; }
        public string Country { get; set; }
        public string Zip { get; set; }
        public string City { get; set; }
        public string StateRegion { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string AddressLine3 { get; set; }
        public string AddressPostBox { get; set; }
        public string Logo { get; set; }
        public string Url { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Description { get; set; }
        public string Sectors { get; set; }
        public string RegionalMarkets { get; set; }
        public string Registrations { get; set; }
        public string HasMoreOffices { get; set; }
        public string lblDandB { get; set; }
        public string lblFPAL { get; set; }
        public string lblAchilles { get; set; }
        public string GAddress { get; set; }
        public string GCity { get; set; }
        public string GState { get; set; }
        public string GCountry { get; set; }
        public string GZip { get; set; }
        public string GoogleApiXml { get; set; }
        public float Latitude { get;  set; }
        public float Longitude { get;  set; }

        public bool IsGoodAddress { get; set; }
    }

}
