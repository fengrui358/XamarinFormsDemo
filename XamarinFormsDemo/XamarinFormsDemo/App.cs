using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight.Ioc;
using Xamarin.Forms;
using XamarinFormsDemo.Const;
using XamarinFormsDemo.Views;

namespace XamarinFormsDemo
{
    public class App : Application
    {
        public App()
        {
            AdministrativeRegionCache.Init();

            // The root page of your application
            var mainPage = new NavigationPage(new MainPageView());
            SimpleIoc.Default.Register(() => mainPage, typeof (MainPageView).ToString());

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
