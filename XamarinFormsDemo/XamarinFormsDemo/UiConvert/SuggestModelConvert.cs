using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinFormsDemo.Models.APIModels;

namespace XamarinFormsDemo.UiConvert
{
    public class SuggestModelToAddressConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var suggest = value as BaiduJsonPlaceSuggestApiModel.SuggestModel;
            if (suggest != null)
            {
                return $"{suggest.City}{suggest.District}";
            }

            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
