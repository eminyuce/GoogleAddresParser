using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleAddressParser.Entities
{
    public class UnformattedAddressShiporacleCom
    {
        public string Id { get; set; }
        public string Company { get; set; }
        public string Country { get; set; }
        public string Phone { get; set; }
        public string Services { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Zip { get; set; }
        public string GAddress { get; set; }
        public string GCity { get; set; }
        public string GState { get; set; }
        public string GCountry { get; set; }
        public string GZip { get; set; }
        public string GoogleApiXml { get; set; }
        public float Latitude { get; set; }
        public float Longitude { get; set; }
        public string streetNumber { get; set; }
        public string premise { get; set; }
        public string subpremise { get; set; }
    }
}
