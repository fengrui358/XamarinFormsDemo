using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Xamarin.Forms;
using XamarinFormsDemo.Views;

namespace XamarinFormsDemo.ViewModels
{
    public class MainPageViewModel : ViewModelBase
    {
        public Command<Type> GoToCommand { get; private set; }

        public MainPageViewModel()
        {
            GoToCommand = new Command<Type>(GoToCommandHandler);
        }

        public async void GoToCommandHandler(Type parm)
        {
            var navigationPage = SimpleIoc.Default.GetInstance<NavigationPage>(typeof(MainPageView).ToString());

            if (parm == typeof (CarouselPageView))
            {
                await navigationPage.PushAsync(new CarouselPageView());
            }
            else if(parm == typeof(CarouselImageView))
            {
                await navigationPage.PushAsync(new CarouselImageView());
            }
        }
    }
}
