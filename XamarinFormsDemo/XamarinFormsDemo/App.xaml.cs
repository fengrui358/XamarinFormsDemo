using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using XamarinFormsDemo.Const;
using XamarinFormsDemo.Views;

namespace XamarinFormsDemo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            if (DeviceInfo.DeviceId == Guid.Empty)
            {
                DeviceInfo.DeviceId = Guid.NewGuid();
                MainPage = new LoadingView();
            }
            else
            {
                MainPage = new CarouselImageView();
            }
        }
    }
}
