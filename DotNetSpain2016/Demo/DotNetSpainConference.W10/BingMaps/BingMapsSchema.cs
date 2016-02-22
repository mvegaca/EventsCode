using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppStudio.DataProviders.BingMaps
{
    public class BingMapsSchema : SchemaBase
    {
        public enum POIEntityType
        {
            Unknown = 0,
            Restaurant = 5800,
            Cinema = 7832,
            ParkingLot = 7520,
            Hotel = 7011,
            SpecialtyStore = 9567,
            IndustrialZone = 9991,
            GasolineStation = 5540,
            ATM = 3578,
            Bank = 6000,
            Shopping = 6512,
            GroceryStore = 5400
        };

        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string AddressLine { get; set; }
        public string Locality { get; set; }
        public string AdminDistrict { get; set; }
        public string AdminDistrict2 { get; set; }
        public string PostalCode { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Phone { get; set; }
        public POIEntityType EntityTypeID { get; set; }        
    }
}
