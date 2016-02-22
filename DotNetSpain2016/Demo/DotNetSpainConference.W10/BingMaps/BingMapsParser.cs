using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AppStudio.DataProviders.BingMaps
{
    public class BingMapsParser : IParser<BingMapsSchema>
    {
        public IEnumerable<BingMapsSchema> Parse(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                yield break;
            }
            dynamic searchList = JsonConvert.DeserializeObject(data);
            foreach (var result in searchList?.d?.results)
            {
                BingMapsSchema schema = new BingMapsSchema() { _id = result.EntityID };
                schema.AddressLine = result.AddressLine;
                schema.AdminDistrict = result.AdminDistrict;
                schema.AdminDistrict2 = result.AdminDistrict2;
                schema.DisplayName = result.DisplayName;
                schema.Locality = result.Locality;
                schema.Phone = result.Phone;
                schema.PostalCode = result.PostalCode;                
                schema.Name = result.Name;                
                schema.Latitude = result.Latitude;
                schema.Longitude = result.Longitude;
                schema.EntityTypeID = result.EntityTypeID;
                yield return schema;
            }
        }
    }
}
