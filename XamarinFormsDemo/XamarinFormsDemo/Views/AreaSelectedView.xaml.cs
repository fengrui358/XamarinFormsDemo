using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Helpers;
using XamarinFormsDemo.Controls.AreaSelectedControl;
using Xamarin.Forms;
using XamarinFormsDemo.Const;

namespace XamarinFormsDemo.Views
{
    public partial class AreaSelectedView : ContentPage
    {
        #region 字段

        private AreaSelectedPane _areaSelectedPlane;
        private Label _label;

        #endregion


        #region 构造

        public AreaSelectedView()
        {
            InitializeComponent();

            _label = new Label
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                Text = "省份/城市/区县/乡/村",
                BackgroundColor = Color.White,
            };

            var container = new ContentView {Padding = new Thickness(10, 10, 10, 10), BackgroundColor = Color.White};
            container.GestureRecognizers.Add(new TapGestureRecognizer {Command = new RelayCommand(ShowSelectedPane)});
            container.Content = _label;

            absoluteLayout.Children.Add(container, new Rectangle(0, 0, DeviceInfo.Width, AbsoluteLayout.AutoSize));
        }

        #endregion


        #region 私有方法

        private void ShowSelectedPane()
        {
            _areaSelectedPlane?.Show();
        }

        protected override void OnAppearing()
        {
            _areaSelectedPlane = new AreaSelectedPane
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            _areaSelectedPlane.SelectedCallBack = new WeakAction<string>(SelectedCallBackHandler);

            absoluteLayout.Children.Add(_areaSelectedPlane);

            base.OnAppearing();
        }

        private void SelectedCallBackHandler(string result)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                _label.Text = result;

                _areaSelectedPlane.Hide();
            });
        }

        #endregion

    }
}
