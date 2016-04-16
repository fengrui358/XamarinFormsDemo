using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamarinFormsDemo.Models.APIModels
{
    public class BaiduJsonBase
    {
        public int Status { get; set; }

        public string Message { get; set; }
    }

    public class BaiduJsonPlaceSuggestApiModel : BaiduJsonBase
    {
        public List<SuggestModel> Result { get; set; }


        public class SuggestModel
        {
            public string Name { get; set; }

            public LocationModel Location { get; set; }

            public string Uid { get; set; }

            public string City { get; set; }

            public string District { get; set; }

            public string Business { get; set; }

            public string Cityid { get; set; }
        }
    }

    public class BaiduJsonPlaceApiModel : BaiduJsonBase
    {
        public int Total { get; set; }

        public List<SuggestModel> Results { get; set; }


        public class SuggestModel
        {
            public string Name { get; set; }

            public LocationModel Location { get; set; }

            public string Address { get; set; }

            public string Telephone { get; set; }

            public string Uid { get; set; }
        }
    }

    public class LocationModel
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }
}
