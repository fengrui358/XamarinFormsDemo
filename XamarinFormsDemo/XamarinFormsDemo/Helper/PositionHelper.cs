using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Xamarin.Forms;
using XamarinFormsDemo.Const;
using XamarinFormsDemo.Models.APIModels;

namespace XamarinFormsDemo.Helper
{
    public class PositionHelper
    {
        public static string Address { get; set; }

        public static string City { get; set; }

        //static PositionHelper()
        //{
        //    if (Application.Current.Properties.ContainsKey(nameof(Address)))
        //    {
        //        Address = Application.Current.Properties[nameof(Address)].ToString();
        //    }

        //    if (Application.Current.Properties.ContainsKey(nameof(City)))
        //    {
        //        City = Application.Current.Properties[nameof(City)].ToString();
        //    }
        //}

        public async static Task<Position> GetNativePosition()
        {
            try
            {
                var locator = CrossGeolocator.Current;
                locator.DesiredAccuracy = 50;

                var position = await locator.GetPositionAsync(timeoutMilliseconds: 10*1000);

                return position;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Unable to get location, may need to increase timeout: " + ex);
            }

            return null;
        }

        public async static Task<LocationModel> GetBaiduPosition()
        {
            try
            {
                var nativePosition = await GetNativePosition();

                var api =
                    $"http://api.map.baidu.com/geoconv/v1/?coords={nativePosition.Longitude},{nativePosition.Latitude}&from=1&to=5&ak={AppInfo.BaiduMapAk}";

                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(new Uri(api));

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();

                        var objResluts = JsonConvert.DeserializeObject<BaiduJsonLocationModel>(json);

                        if (objResluts?.Status == 0 && objResluts.Result != null)
                        {
                            var result = objResluts.Result.FirstOrDefault();

                            return new LocationModel {Lng = result.X, Lat = result.Y};
                        }
                        else
                        {
                            return new LocationModel {Lat = nativePosition.Latitude, Lng = nativePosition.Longitude};
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }

            return null;
        } 
    }
}
