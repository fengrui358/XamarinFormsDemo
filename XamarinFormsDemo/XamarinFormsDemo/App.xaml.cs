using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
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

            AdministrativeRegionCache.Init();

            // The root page of your application
            var mainPage = new NavigationPage(new MainPageView());

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
            
            SimpleIoc.Default.Register(() => mainPage, typeof(MainPageView).ToString());

            MainPage = mainPage;
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
