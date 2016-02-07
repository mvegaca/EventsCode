using System;
using AppStudio.DataProviders;

namespace DotNetSpainConference.Sections
{
    /// <summary>
    /// Implementation of the Fotos1Schema class.
    /// </summary>
    public class Fotos1Schema : SchemaBase
    {

        public string Title { get; set; }

        public string Description { get; set; }

        public string Image { get; set; }
    }
}
