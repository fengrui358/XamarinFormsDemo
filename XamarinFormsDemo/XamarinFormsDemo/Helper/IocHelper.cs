using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
using Xamarin.Forms;
using XamarinFormsDemo.Views;

namespace XamarinFormsDemo.Helper
{
    public class IocHelper
    {
        public static NavigationPage GetNavigationPage()
        {
            return SimpleIoc.Default.GetInstance<NavigationPage>(typeof (MainPageView).ToString());
        }
    }
}
