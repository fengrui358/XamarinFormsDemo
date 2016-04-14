using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using Xamarin.Forms;
using XamarinFormsDemo.Const;
using XamarinFormsDemo.Helper;
using XamarinFormsDemo.Hybrid;
using XamarinFormsDemo.Models.APIModels;

namespace XamarinFormsDemo.Views
{
    public partial class BaiduMapView : ContentPage
    {
        #region 私有方法

        private AbsoluteLayout _absoluteLayout;
        private Entry _searchEntry;

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

            var hybirdWeb = new HybridWebView
            {
                Uri = "index.html",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            _absoluteLayout.Children.Add(hybirdWeb, new Rectangle(0, 0, DeviceInfo.Width, DeviceInfo.Height));

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

            Messenger.Default.Register<string>(this, MessengeToken.SearchCallBack, SearchKeyWordCallBack);
        }

        #endregion


        #region 私有方法

        private async void SearchEntryOnFocused(object sender, FocusEventArgs focusEventArgs)
        {
            var keyWords = ((Entry) sender).Text;

            var navigationPage = IocHelper.GetNavigationPage();
            await navigationPage.PushAsync(new SearchSuggestView(keyWords));
        }

        private void SearchKeyWordCallBack(string searchKeyWords)
        {
            if (!string.IsNullOrEmpty(searchKeyWords))
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    _searchEntry.Text = searchKeyWords;
                });

                //todo:定位

                //弹出门牌号输入
                var registerEntry = new Entry
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    BackgroundColor = Color.White,
                    Placeholder = "楼号/门牌号"
                };

                _absoluteLayout.Children.Add(registerEntry,
                    new Rectangle(20, 35 + 15 + _searchEntry.Height, DeviceInfo.Width - 40, AbsoluteLayout.AutoSize));
            }
        }

        #endregion

    }
}
