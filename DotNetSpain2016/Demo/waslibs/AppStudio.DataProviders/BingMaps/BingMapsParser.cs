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
            foreach (var resourceSet in searchList?.resourceSets)
            {
                foreach (var resource in resourceSet.resources)
                {
                    BingMapsSchema schema = new BingMapsSchema() { _id = Guid.NewGuid().ToString() };
                    schema.Name = resource.name;
                    var c = resource.point?.coordinates;
                    if (c != null && c.Count >= 2)
                    {
                        schema.Latitude = c[0];
                        schema.Longitude = c[1];
                        yield return schema;
                    }
                }
            }
        }
    }
}
