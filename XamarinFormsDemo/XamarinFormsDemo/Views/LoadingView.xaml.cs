using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
using Xamarin.Forms;
using XamarinFormsDemo.Const;
using XamarinFormsDemo.Helper;

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
                    if (DeviceInfo.DeviceId == Guid.Empty)
                    {
                        DeviceInfo.DeviceId = Guid.NewGuid();
                        Application.Current.MainPage = new CarouselPageView();
                    }
                    else
                    {


                        if (SimpleIoc.Default.IsRegistered<NavigationPage>(typeof (MainPageView).ToString()))
                        {
                            //Application.Current.MainPage = IocHelper.GetNavigationPage();
                            SimpleIoc.Default.Unregister<NavigationPage>(typeof (MainPageView).ToString());
                        }

                        var mainPage = new NavigationPage(new CarouselImageView());

                        if (Device.OS == TargetPlatform.iOS)
                        {
                            mainPage.BarBackgroundColor = Color.White;
                            mainPage.BarTextColor = Color.Black;
                        }
                        else if (Device.OS == TargetPlatform.Android)
                        {
                            mainPage.BarBackgroundColor = Color.Black;
                            mainPage.BarTextColor = Color.White;
                        }

                        SimpleIoc.Default.Register(() => mainPage, typeof (MainPageView).ToString());

                        Application.Current.MainPage = mainPage;
                    }
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
