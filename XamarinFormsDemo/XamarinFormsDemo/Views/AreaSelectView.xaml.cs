using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinFormsDemo.Controls.AreaSelectControl;
using Xamarin.Forms;
using XamarinFormsDemo.Const;

namespace XamarinFormsDemo.Views
{
    public partial class AreaSelectView : ContentPage
    {
        #region 字段

        private AreaSelectPane _areaSelectedPlane;
        private Entry _entry;

        #endregion


        #region 构造

        public AreaSelectView()
        {
            InitializeComponent();

            _entry = new Entry
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Placeholder = "搜索",
                BackgroundColor = Color.White,
                IsEnabled = true
            };
            _entry.Focused += EntryOnFocused;

            absoluteLayout.Children.Add(_entry, new Rectangle(0, 0, DeviceInfo.Width, AbsoluteLayout.AutoSize));
        }

        #endregion


        #region 私有方法

        private void EntryOnFocused(object sender, FocusEventArgs focusEventArgs)
        {
            if (_areaSelectedPlane != null)
            {
                AbsoluteLayout.SetLayoutBounds(_areaSelectedPlane,
                    new Rectangle(0, Height - 300, AbsoluteLayout.AutoSize, 300));
            }
        }

        protected override void OnAppearing()
        {
            _areaSelectedPlane = new AreaSelectPane
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            absoluteLayout.Children.Add(_areaSelectedPlane,
                new Rectangle(0, DeviceInfo.Height, AbsoluteLayout.AutoSize, 300));

            base.OnAppearing();
        }

        #endregion

    }
}
