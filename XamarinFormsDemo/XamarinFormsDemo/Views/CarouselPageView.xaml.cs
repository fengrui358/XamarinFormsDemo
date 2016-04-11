using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using XamarinFormsDemo.ViewModels;

namespace XamarinFormsDemo.Views
{
    public partial class CarouselPageView
    {
        public CarouselPageView()
        {
            InitializeComponent();

            BindingContext = new CarouselPageViewModel();
        }
    }
}
