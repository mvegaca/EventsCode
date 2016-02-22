using System;
using AppStudio.DataProviders;

namespace DotNetSpainConference.Sections
{
    /// <summary>
    /// Implementation of the Agenda1Schema class.
    /// </summary>
    public class Agenda1Schema : SchemaBase
    {

        public string Title { get; set; }

        public string Description { get; set; }

        public string Url { get; set; }

        public string Image { get; set; }

        public string Speaker { get; set; }

        public string Technology { get; set; }

        public DateTime? Time { get; set; }
    }
}
