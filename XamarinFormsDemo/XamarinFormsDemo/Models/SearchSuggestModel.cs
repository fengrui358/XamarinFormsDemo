using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamarinFormsDemo.Models
{
    public class SearchSuggestModel
    {
        public string KeyWords { get; set; }

        public double Lat { get; set; }
        public double Lng { get; set; }

        public bool ContainLocation => Lat > 0 && Lng > 0;

        public SearchSuggestModel(string keyWords = null, double lat = 0, double lng = 0)
        {
            KeyWords = keyWords;
            Lat = lat;
            Lng = lng;
        }
    }
}
