using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using Newtonsoft.Json;
using Xamarin.Forms;
using XamarinFormsDemo.Const;
using XamarinFormsDemo.Helper;
using XamarinFormsDemo.Hybrid;
using XamarinFormsDemo.Models;
using XamarinFormsDemo.Models.APIModels;

namespace XamarinFormsDemo.Views
{
    public partial class BaiduMapView : ContentPage
    {
        #region 私有方法

        private AbsoluteLayout _absoluteLayout;
        private Entry _searchEntry;

        private HybridWebView _hybirdWeb;
        private Entry _registerEntry;

        #endregion


        #region 构造

        public BaiduMapView()
        {
            InitializeComponent();

            //todo:使用xaml构建该页面
            _absoluteLayout = new AbsoluteLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            _hybirdWeb = new HybridWebView
            {
                Uri = "index.html",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            _absoluteLayout.Children.Add(_hybirdWeb, new Rectangle(0, 0, DeviceInfo.Width, DeviceInfo.Height));

            _searchEntry = new Entry
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = Color.White,
                Placeholder = "搜索"
            };
            _searchEntry.Focused += SearchEntryOnFocused;

            _absoluteLayout.Children.Add(_searchEntry,
                new Rectangle(20, 35, DeviceInfo.Width - 40, AbsoluteLayout.AutoSize));

            Content = _absoluteLayout;

            Messenger.Default.Register<SearchSuggestModel>(this, MessengeToken.SearchCallBack, SearchKeyWordCallBack);
        }

        #endregion


        #region 私有方法

        private async void SearchEntryOnFocused(object sender, FocusEventArgs focusEventArgs)
        {
            var keyWords = ((Entry) sender).Text;

            var navigationPage = IocHelper.GetNavigationPage();
            await navigationPage.PushAsync(new SearchSuggestView(keyWords));
        }

        private async void SearchKeyWordCallBack(SearchSuggestModel searchSuggestModel)
        {
            try
            {
                if (!string.IsNullOrEmpty(searchSuggestModel?.KeyWords))
                {

                    _searchEntry.Text = searchSuggestModel.KeyWords;

                    var location = await SearchPOI(searchSuggestModel);

                    if (location != null)
                    {
                        //todo:定位
                        _hybirdWeb.InvokeJsFunction("addMarker", null, location.Lng, location.Lat);
                    }

                    //弹出门牌号输入
                    _registerEntry = new Entry
                    {
                        HorizontalOptions = LayoutOptions.FillAndExpand,
                        BackgroundColor = Color.White,
                        Placeholder = "楼号/门牌号"
                    };

                    _absoluteLayout.Children.Add(_registerEntry,
                        new Rectangle(20, 35 + 15 + _searchEntry.Height, DeviceInfo.Width - 40, AbsoluteLayout.AutoSize));
                }
                else
                {
                    _searchEntry.Text = string.Empty;

                    _hybirdWeb.InvokeJsFunction("clearMarker", null, null);

                    if (_registerEntry != null)
                    {
                        _registerEntry.IsVisible = false;
                        _absoluteLayout.Children.Remove(_registerEntry);
                        _registerEntry = null;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async Task<LocationModel> SearchPOI(SearchSuggestModel searchSuggestModel)
        {
            if (!string.IsNullOrEmpty(searchSuggestModel?.KeyWords))
            {
                if (searchSuggestModel.ContainLocation)
                {
                    //直接返回坐标
                    return new LocationModel {Lat = searchSuggestModel.Lat, Lng = searchSuggestModel.Lng};
                }
                else
                {
                    var encodekeyWord = StringHelper.UrlEncode(searchSuggestModel.KeyWords);

                    //todo:修改范围
                    var api =
                        $"http://api.map.baidu.com/place/v2/search?query={encodekeyWord}&page_size=1&page_num=0&scope=1&region=131&output=json&ak={AppInfo.BaiduMapAk}";

                    using (var httpClient = new HttpClient())
                    {
                        var response = await httpClient.GetAsync(api);

                        if (response.IsSuccessStatusCode)
                        {
                            var json = await response.Content.ReadAsStringAsync();

                            var objResluts = JsonConvert.DeserializeObject<BaiduJsonPlaceApiModel>(json);

                            //转换此次结果
                            var result = objResluts?.Results?.FirstOrDefault();
                            if (result != null)
                            {
                                return new LocationModel { Lat = result.Location.Lat, Lng = result.Location.Lng };
                            }
                        }
                    }
                }
            }

            return null;
        }

        #endregion

    }
}
