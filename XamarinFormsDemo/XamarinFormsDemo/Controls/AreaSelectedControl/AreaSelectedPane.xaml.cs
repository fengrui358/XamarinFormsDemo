using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Helpers;
using Xamarin.Forms;
using XamarinFormsDemo.Const;

namespace XamarinFormsDemo.Controls.AreaSelectedControl
{
    public partial class AreaSelectedPane
    {
        #region 委托

        public WeakAction<string> SelectedCallBack;

        #endregion

        #region 构造

        public AreaSelectedPane()
        {
            InitializeComponent();

            var viewModel = new AreaSelectedPaneViewModel();
            viewModel.SelectedCallBack = new WeakAction<string>(SelectedCallBackHandler);

            BindingContext = viewModel;

            Hide();
        }

        #endregion


        #region 公共方法

        public void Show()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                AbsoluteLayout.SetLayoutBounds(this,
                    new Rectangle(0, DeviceInfo.Height - 300, AbsoluteLayout.AutoSize, 300));
            });
        }

        public void Hide()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                AbsoluteLayout.SetLayoutBounds(this,
                    new Rectangle(0, DeviceInfo.Height, AbsoluteLayout.AutoSize, 300));
            });
        }

        #endregion

        #region 私有方法

        private void CancelButton_OnClicked(object sender, EventArgs e)
        {
            Hide();
        }

        private void SelectedCallBackHandler(string result)
        {
            if (SelectedCallBack != null && SelectedCallBack.IsAlive)
            {
                SelectedCallBack.ExecuteWithObject(result);
            }
        }

        #endregion

    }
}
