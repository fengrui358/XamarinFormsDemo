using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using XamarinFormsDemo.Const;

namespace XamarinFormsDemo.Views
{
    public partial class LoadingView : ContentPage
    {
        public LoadingView()
        {
            InitializeComponent();

            
        }

        protected async override void OnAppearing()
        {
            await Task.Run(async () =>
            {
                await Task.Delay(1);

                Device.BeginInvokeOnMainThread(() =>
                {
                    Application.Current.MainPage = new CarouselPageView();
                });
            });

            base.OnAppearing();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            DeviceInfo.Width = (int) width;
            DeviceInfo.Height = (int) height;

            base.OnSizeAllocated(width, height);
        }
    }
}
