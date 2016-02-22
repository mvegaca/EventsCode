using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace DotNetSpainConference.Converters
{
    public class TechnologyToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value != null)
            {
                string technology = value.ToString().ToLower();
                switch (technology)
                {
                    case "web":
                        return new SolidColorBrush(new Windows.UI.Color() { A = 255, R = 129, G = 167, B = 81 });
                    case "tools/devops":
                        return new SolidColorBrush(new Windows.UI.Color() { A = 255, R = 234, G = 185, B = 58 });
                    case "cross-plat":
                        return new SolidColorBrush(new Windows.UI.Color() { A = 255, R = 104, G = 33, B = 122 });
                    case "apps":
                        return new SolidColorBrush(new Windows.UI.Color() { A = 255, R = 217, G = 84, B = 100 });
                    case "cloud":
                        return new SolidColorBrush(new Windows.UI.Color() { A = 255, R = 68, G = 142, B = 205 });
                    case "data":
                        return new SolidColorBrush(new Windows.UI.Color() { A = 255, R = 249, G = 156, B = 112 });
                    case "fw":
                        return new SolidColorBrush(new Windows.UI.Color() { A = 255, R = 42, G = 141, B = 74 });
                    case "iot":
                        return new SolidColorBrush(new Windows.UI.Color() { A = 255, R = 170, G = 61, B = 166 });
                }
            }
            return new SolidColorBrush(new Windows.UI.Color() { A = 255, R = 251, G = 158, B = 113 });
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
