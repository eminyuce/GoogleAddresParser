using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleAddressParser.Entities
{
    public class MaritimeDatabaseCom
    {
        public string Id { get; set; }
        public string Company { get; set; }
        public string Activity { get; set; }
        public string Country { get; set; }
        public string Address { get; set; }
        public string Town { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string Website { get; set; }
        public string AOHPhone { get; set; }
        public string GAddress { get; set; }
        public string GCity { get; set; }
        public string GState { get; set; }
        public string GCountry { get; set; }
        public string GZip { get; set; }
        public string GoogleApiXml { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string StreetNumber { get; set; }
        public string Premise { get; set; }
        public string Subpremise { get; set; }

        public bool IsDeliveredAddress { get; set; }
    }
}
