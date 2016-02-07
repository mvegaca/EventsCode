using System;
using AppStudio.DataProviders;

namespace DotNetSpainConference.Sections
{
    /// <summary>
    /// Implementation of the Ponentes1Schema class.
    /// </summary>
    public class Ponentes1Schema : SchemaBase
    {

        public string Name { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }

        public string Url { get; set; }

        public string Twitter { get; set; }
    }
}
