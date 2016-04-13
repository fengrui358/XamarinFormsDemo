using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamarinFormsDemo.Models.APIModels
{
    public class BaiduJson
    {
        public int Status { get; set; }

        public string Message { get; set; }

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

    public class LocationModel
    {
        public string Lat { get; set; }
        public string Lng { get; set; }
    }
}
