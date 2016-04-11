using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinFormsDemo.Views;

namespace XamarinFormsDemo.ViewModels
{
    public class MainPageViewModel
    {
        public Command<Type> GoToCommand { get; private set; }

        public MainPageViewModel()
        {
            GoToCommand = new Command<Type>(GoToCommandHandler);
        }

        public async void GoToCommandHandler(Type parm)
        {
            if (parm == typeof (CarouselPageView))
            {
                
            }
        }
    }
}
