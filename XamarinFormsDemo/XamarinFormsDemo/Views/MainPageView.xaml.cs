using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using XamarinFormsDemo.Const;
using XamarinFormsDemo.ViewModels;

namespace XamarinFormsDemo.Views
{
    public partial class MainPageView : ContentPage
    {
        #region 构造

        public MainPageView()
        {
            InitializeComponent();

            BindingContext = new MainPageViewModel();
        }

        #endregion


        #region 私有方法

        #endregion
    }
}
