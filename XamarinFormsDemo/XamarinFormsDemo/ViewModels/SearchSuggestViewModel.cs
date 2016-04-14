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
using XamarinFormsDemo.Models.APIModels;

namespace XamarinFormsDemo.ViewModels
{
    public class SearchSuggestViewModel : ViewModelBase
    {
        private Guid _searchSequenceKey;

        private string _searchKeyWords;
        private List<BaiduJson.SuggestModel> _suggestResults;

        private BaiduJson.SuggestModel _selectedItem;

        public string SearchKeyWords
        {
            get { return _searchKeyWords; }
            set
            {
                Set(() => SearchKeyWords, ref _searchKeyWords, value);
            }
        }

        public List<BaiduJson.SuggestModel> SuggestResults
        {
            get { return _suggestResults; }
            set { Set(() => SuggestResults, ref _suggestResults, value); }
        }

        public BaiduJson.SuggestModel SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (_selectedItem != value)
                {
                    //选中
                    _selectedItem = value;

                    SearchKeyWords = _selectedItem.Name;
                    SearchCommandHandler();
                }
            }
        }

        public SearchSuggestViewModel(string keyWords)
        {
            SuggestResults = new List<BaiduJson.SuggestModel>();
            SearchKeyWords = keyWords;
            TextChangedHandler(keyWords);
        }

        public void TextChangedHandler(string newKeyWord)
        {
            if (string.IsNullOrEmpty(newKeyWord))
            {
                SuggestResults = new List<BaiduJson.SuggestModel>();
                return;
            }

            _searchSequenceKey = Guid.NewGuid();

            GetSuggestResult(newKeyWord, _searchSequenceKey);
        }

        public RelayCommand SearchCommand { get; private set; }

        public SearchSuggestViewModel()
        {
            SearchCommand = new RelayCommand(SearchCommandHandler);
        }

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

                //todo:修改范围
                var api =
                    $"http://api.map.baidu.com/place/v2/suggestion?query={encodekeyWord}&region=131&output=json&ak={AppInfo.BaiduMapAk}";

                var httpClient = new HttpClient();
                var response = await httpClient.GetAsync(api);

                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();

                    var objResluts = JsonConvert.DeserializeObject<BaiduJson>(json);

                    if (objResluts != null && objResluts.Result != null)
                    {
                        if (searchSequenceKey == _searchSequenceKey)
                        {
                            //转换此次结果
                            SuggestResults = objResluts.Result;
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
            Messenger.Default.Send(SearchKeyWords, MessengeToken.SearchCallBack);

            await IocHelper.GetNavigationPage().PopAsync();
        }
    }
}
