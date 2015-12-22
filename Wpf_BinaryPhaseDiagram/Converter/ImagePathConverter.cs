using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media.Imaging;

namespace Wpf_BinaryPhaseDiagram.Converter
{
    public class ImagePathConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string imageName = value.ToString();
            string imagePath = System.IO.Path.Combine(Environment.CurrentDirectory, "Images", imageName);
            if (File.Exists(imagePath))
            {
                return new BitmapImage(new Uri(imagePath));
            }
            else
            {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
