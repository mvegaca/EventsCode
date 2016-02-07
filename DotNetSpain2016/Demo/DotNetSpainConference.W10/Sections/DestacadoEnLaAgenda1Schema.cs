using System;
using AppStudio.DataProviders;

namespace DotNetSpainConference.Sections
{
    /// <summary>
    /// Implementation of the DestacadoEnLaAgenda1Schema class.
    /// </summary>
    public class DestacadoEnLaAgenda1Schema : SchemaBase
    {

        public string Title { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public string Image { get; set; }

        public string Speaker { get; set; }
    }
}
