using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Ioc;
using Xamarin.Forms;
using XamarinFormsDemo.Controls.AreaSelectControl;
using XamarinFormsDemo.Helper;
using XamarinFormsDemo.Views;

namespace XamarinFormsDemo.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public RelayCommand<Type> GoToCommand { get; private set; }

        public MainPageViewModel()
        {
            GoToCommand = new RelayCommand<Type>(GoToCommandHandler);
        }

        public async void GoToCommandHandler(Type parm)
        {
            var navigationPage = IocHelper.GetNavigationPage();

            if (parm == typeof (CarouselPageView))
            {
                await navigationPage.PushAsync(new CarouselPageView());
            }
            else if(parm == typeof(CarouselImageView))
            {
                await navigationPage.PushAsync(new CarouselImageView());
            }
            else if(parm == typeof(BaiduMapView))
            {
                await navigationPage.PushAsync(new BaiduMapView());
            }
            else if(parm == typeof(AreaSelectView))
            {
                await navigationPage.PushAsync(new AreaSelectView());
            }
        }
    }
}
