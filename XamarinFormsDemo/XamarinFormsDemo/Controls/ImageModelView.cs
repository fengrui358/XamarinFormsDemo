using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinFormsDemo.Const;
using XamarinFormsDemo.Models;

namespace XamarinFormsDemo.Controls
{
    public class ImageModelView : ContentView
    {
        public ImageModelView()
        {            
            var imageSource = new UriImageSource();
            imageSource.SetBinding(UriImageSource.UriProperty, "Uri");

            Content = new Image
            {
                Source = imageSource,
                Aspect = Aspect.Fill
            };
        }
    }
}
