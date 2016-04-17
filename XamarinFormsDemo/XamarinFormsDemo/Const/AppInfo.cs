using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using XamarinFormsDemo.Models;

namespace XamarinFormsDemo.Const
{
    public class AppInfo
    {
        public const string BaiduMapAk = "U7hI8UZzzOsQnPG9DQoo7KWXRX2DWFnS";

        public static string ResourceNameSpace { get;private set; }

        static AppInfo()
        {
            var nameSpace = typeof(App).GetTypeInfo().Namespace;
            ResourceNameSpace = $"{nameSpace}.Resources.";
        }
    }

    public class AdministrativeRegionCache
    {
        public static List<AdministrativeRegion> AdministrativeRegionList { get; set; }

        public static async void Init()
        {
            if (AdministrativeRegionList == null || !AdministrativeRegionList.Any())
            {
                await Task.Run(() =>
                {
                    using (var stream = typeof(AdministrativeRegion).GetTypeInfo()
                        .Assembly.GetManifestResourceStream($"{AppInfo.ResourceNameSpace}AdministrativeRegionInfo.json"))
                    {
                        using (var streamReader = new StreamReader(stream))
                        {
                            var json = streamReader.ReadToEnd();

                            AdministrativeRegionList = JsonConvert.DeserializeObject<List<AdministrativeRegion>>(json);
                        }
                    }
                });
            }

        }
    }
}
