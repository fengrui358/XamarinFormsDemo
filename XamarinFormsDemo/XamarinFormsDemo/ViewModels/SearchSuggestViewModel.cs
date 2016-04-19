using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Newtonsoft.Json;
using Xamarin.Forms;
using XamarinFormsDemo.Const;
using XamarinFormsDemo.Helper;
using XamarinFormsDemo.Models;
using XamarinFormsDemo.Models.APIModels;
using XamarinFormsDemo.Views;

namespace XamarinFormsDemo.ViewModels
{
    public class SearchSuggestViewModel : ViewModelBase
    {
        #region 字段

        private Guid _searchSequenceKey;

        private string _searchKeyWords;
        private List<BaiduJsonPlaceSuggestApiModel.SuggestModel> _suggestResults;

        private BaiduJsonPlaceSuggestApiModel.SuggestModel _selectedItem;

        #endregion


        #region 属性

        public string SearchKeyWords
        {
            get { return _searchKeyWords; }
            set
            {
                if (_searchKeyWords != value)
                {
                    _searchKeyWords = value;
                    RaisePropertyChanged();

                    SelectedItem = null;
                }
            }
        }

        public List<BaiduJsonPlaceSuggestApiModel.SuggestModel> SuggestResults
        {
            get { return _suggestResults; }
            set { Set(() => SuggestResults, ref _suggestResults, value); }
        }

        public BaiduJsonPlaceSuggestApiModel.SuggestModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    //选中
                    _selectedItem = value;

                    if (_selectedItem != null)
                    {
                        _searchKeyWords = _selectedItem.Name;
                        RaisePropertyChanged(() => SearchKeyWords);
                        SearchCommandHandler();
                    }
                }
            }
        }

        #endregion


        #region 命令

        public RelayCommand SearchCommand { get; private set; }

        #endregion


        #region 构造

        public SearchSuggestViewModel(string keyWords, LocationModel position = null)
        {
            SuggestResults = new List<BaiduJsonPlaceSuggestApiModel.SuggestModel>();
            SearchKeyWords = keyWords;
            TextChangedHandler(keyWords);

            SearchCommand = new RelayCommand(SearchCommandHandler);
        }

        #endregion


        #region 公共方法

        public void TextChangedHandler(string newKeyWord)
        {
            if (string.IsNullOrEmpty(newKeyWord))
            {
                SuggestResults = new List<BaiduJsonPlaceSuggestApiModel.SuggestModel>();
                return;
            }

            _searchSequenceKey = Guid.NewGuid();

            GetSuggestResult(newKeyWord, _searchSequenceKey);
        }

        #endregion


        #region 私有方法

        /// <summary>
        /// 查询并更新models
        /// </summary>
        /// <param name="newKeyWord"></param>
        /// <param name="searchSequenceKey"></param>
        private async void GetSuggestResult(string newKeyWord, Guid searchSequenceKey)
        {
            try
            {
                var encodekeyWord = StringHelper.UrlEncode(newKeyWord);

                var city = string.Empty;
                if (!string.IsNullOrEmpty(PositionHelper.City))
                {
                    city = StringHelper.UrlEncode(PositionHelper.City);
                }
                else
                {
                    city = "131";
                }

                Debug.WriteLine("定位城市" + PositionHelper.City);

                var api =
                    $"http://api.map.baidu.com/place/v2/suggestion?query={encodekeyWord}&region={city}&output=json&ak={AppInfo.BaiduMapAk}";

                using (var httpClient = new HttpClient())
                {
                    var response = await httpClient.GetAsync(api);

                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();

                        var objResluts = JsonConvert.DeserializeObject<BaiduJsonPlaceSuggestApiModel>(json);

                        if (objResluts?.Result != null)
                        {
                            if (searchSequenceKey == _searchSequenceKey)
                            {
                                //转换此次结果
                                SuggestResults = objResluts.Result;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        private async void SearchCommandHandler()
        {
            var result = new SearchSuggestModel();

            if (SelectedItem != null)
            {
                result.KeyWords = SelectedItem.Name;

                if (SelectedItem.Location != null)
                {
                    result.Lat = SelectedItem.Location.Lat;
                    result.Lng = SelectedItem.Location.Lng;
                }
            }
            else
            {
                result.KeyWords = SearchKeyWords;
            }

            //Messenger.Default.Send(result, MessengeToken.SearchCallBack);

            //await IocHelper.GetNavigationPage().PopAsync();

            await IocHelper.GetNavigationPage().PushAsync(new BaiduMapView(result));
        }

        #endregion

    }
}
