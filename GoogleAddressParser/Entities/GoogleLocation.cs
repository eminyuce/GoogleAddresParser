using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleAddressParser.Entities
{
    public class GoogleLocation
    {
        public string formatted_address { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string zip { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string website { get; set; }
        public string streetNumber { get; set; }
        public string fax { get; set; }
        public string description { get; set; }
        public string premise  { get; set; }
        public string subpremise { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string country { get; set; }
        public int lid { get; set; }
        public string note { get; set; }
        public Boolean IsActive { get; set; }
        public float Distance { get; set; }
        public string GoogleApiResult { get; set; }
    }
}
