using DotNetSensorApp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetSensorApp.Model
{
    public class MWeather
    {
        #region Properties
        public sys sys { get; set; }
        public List<weather> weather { get; set; }
        public main main { get; set; }
        public wind wind { get; set; }
        public string name { get; set; }
        #endregion
    }

    public class sys
    {
        public string country { get; set; }
        public double sunrise { get; set; }
        public string sunriseString { get { return UnitConverter.UnixTimeStampToDateTime(sunrise).ToString("hh:mm:ss"); } }
        public double sunset { get; set; }
        public string sunsetString { get { return UnitConverter.UnixTimeStampToDateTime(sunset).ToString("hh:mm:ss"); } }
    }
    public class weather
    {
        public string main { get; set; }
        public string description { get; set; }
        public string icon { get; set; }
        public string image { get { return string.Format("http://openweathermap.org/img/w/{0}.png", this.icon); } }
        public string localimage { get { return string.Format("/Assets/WeatherIcons/{0}.png", this.icon); } }
    }
    public class main
    {
        public double temp { get; set; }
        public double tempCelsius { get { return temp - 273.15; } }
        public double pressure { get; set; }
        public double humidity { get; set; }
        public double temp_min { get; set; }
        public double temp_minCelsius { get { return temp_min - 273.15; } }
        public double temp_max { get; set; }
        public double temp_maxCelsius { get { return temp_max - 273.15; } }
    }
    public class wind
    {
        public double speed { get; set; }
        public double speedKph { get { return speed * 3.6; } }
        public double deg { get; set; }
        public double var_beg { get; set; }
        public double var_end { get; set; }
    }
}
