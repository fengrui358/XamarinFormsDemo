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
        private BoxView _boxView;

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
                BackgroundColor = Color.White
            };

            var frame = new Frame
            {
                HasShadow = false,
                BackgroundColor = Color.White,
                Padding = new Thickness(10, 10, 10, 10),
                OutlineColor = Color.Gray
            };
            frame.GestureRecognizers.Add(new TapGestureRecognizer {Command = new RelayCommand(ShowSelectedPane)});
            frame.Content = _label;

            absoluteLayout.Children.Add(frame, new Rectangle(0, 0, DeviceInfo.Width, AbsoluteLayout.AutoSize));

            _boxView = new BoxView
            {
                BackgroundColor = Color.FromRgba(128,128,128, 100),
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                IsVisible = false
            };

            absoluteLayout.Children.Add(_boxView, new Rectangle(0, 0, DeviceInfo.Width, DeviceInfo.Height - 300));
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

            _areaSelectedPlane.ShowWeakAction = new WeakAction(() =>
            {
                _boxView.IsVisible = true;
            });

            _areaSelectedPlane.HideWeakAction = new WeakAction(() =>
            {
                _boxView.IsVisible = false;
            });

            base.OnAppearing();
        }

        private void SelectedCallBackHandler(string result)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                _label.Text = result;
            });
        }

        #endregion
    }
}
