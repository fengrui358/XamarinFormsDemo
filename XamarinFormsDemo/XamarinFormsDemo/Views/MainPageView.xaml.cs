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
        public MainPageView()
        {
            InitializeComponent();

            BindingContext = new MainPageViewModel();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            DeviceInfo.Width = (int) width;
            DeviceInfo.Height = (int) height;

            base.OnSizeAllocated(width, height);
        }
    }
}
