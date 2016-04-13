using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
using Xamarin.Forms;
using XamarinFormsDemo.Hybrid;

namespace XamarinFormsDemo.Views
{
    public partial class BaiduMapView : ContentPage
    {
        private RelativeLayout _relativeLayout;
        private Entry _searchEntry;

        public BaiduMapView()
        {
            InitializeComponent();

            //todo:使用xaml构建该页面
            _relativeLayout = new RelativeLayout
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

            _relativeLayout.Children.Add(hybirdWeb,
                        Constraint.RelativeToParent((parent) => { return parent.X; }),
                        Constraint.RelativeToParent((parent) => { return parent.Y; }),
                        Constraint.RelativeToParent((parent) => { return parent.Width; }),
                        Constraint.RelativeToParent((parent) => { return parent.Height; })
                    );

            var _searchEntry = new Entry{HorizontalOptions = LayoutOptions.FillAndExpand, BackgroundColor = Color.White, Placeholder = "搜索", TextColor = Color.Gray};
            _searchEntry.Focused += SearchEntryOnFocused;

            _relativeLayout.Children.Add(_searchEntry, Constraint.RelativeToParent(parent => parent.X + 20),
                Constraint.RelativeToParent(parent => parent.Y + 35),
                Constraint.RelativeToParent(parent => parent.Width - 40), Constraint.Constant(45));

            Content = _relativeLayout;
        }

        private async void SearchEntryOnFocused(object sender, FocusEventArgs focusEventArgs)
        {
            var keyWords = ((Entry) sender).Text;

            var navigationPage = SimpleIoc.Default.GetInstance<NavigationPage>(typeof(MainPageView).ToString());
            await navigationPage.PushAsync(new SearchSuggestView(keyWords));
        }
    }
}
