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

            MainPage = new LoadingView();
        }
    }
}
