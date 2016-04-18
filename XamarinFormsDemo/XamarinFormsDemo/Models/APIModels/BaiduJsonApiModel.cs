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

    public class BaiduJsonLocationModel : BaiduJsonBase
    {
        public List<LocationModelXY> Result { get; set; } 
    }

    public class BaiduJsonGetAddressFromPosition
    {
        public int Status { get; set; }

        public BaiduJsonGetAddressFromPositionResult Result { get; set; }
    }

    public class BaiduJsonGetAddressFromPositionResult
    {
        public LocationModel Location { get; set; }

        public string Formatted_Address { get; set; }

        public string Sematic_Description { get; set; }

        public AddressComponent AddressComponent { get; set; }

        public List<Pois> Pois { get; set; }
    }

    public class AddressComponent
    {
        public string Country { get; set; }

        public string Province { get; set; }

        public string City { get; set; }

        public string District { get; set; }

        public string Street { get; set; }

        public string Adcode { get; set; }

        public string Country_Code { get; set; }

        public string Direction { get; set; }

        public string Distance { get; set; }
    }

    public class Pois
    {
        public string Addr { get; set; }

        public string Cp { get; set; }

        public string Direction { get; set; }

        public string Distance { get; set; }

        public string Name { get; set; }

        public string PoiType { get; set; }

        public string Tel { get; set; }

        public string Uid { get; set; }

        public string Zip { get; set; }
    }


    public class LocationModel
    {
        public double Lat { get; set; }
        public double Lng { get; set; }
    }

    public class LocationModelXY
    {
        public double X { get; set; }
        public double Y { get; set; }
    }
}
