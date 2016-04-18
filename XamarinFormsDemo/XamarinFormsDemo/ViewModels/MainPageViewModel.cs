using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using XamarinFormsDemo.Helper;
using XamarinFormsDemo.Views;

namespace XamarinFormsDemo.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        #region 命令

        public RelayCommand<Type> GoToCommand { get; private set; }

        #endregion


        #region 构造

        public MainPageViewModel()
        {
            GoToCommand = new RelayCommand<Type>(GoToCommandHandler);
        }

        #endregion


        #region 私有方法

        private async void GoToCommandHandler(Type parm)
        {
            var navigationPage = IocHelper.GetNavigationPage();

            if (parm == typeof (CarouselPageView))
            {
                await navigationPage.PushAsync(new CarouselPageView());
            }
            else if (parm == typeof (CarouselImageView))
            {
                await navigationPage.PushAsync(new CarouselImageView());
            }
            else if (parm == typeof (BaiduMapView))
            {
                //await navigationPage.PushAsync(new BaiduMapView());
            }
            else if (parm == typeof (AreaSelectedView))
            {
                await navigationPage.PushAsync(new AreaSelectedView());
            }
        }

        #endregion

    }
}
