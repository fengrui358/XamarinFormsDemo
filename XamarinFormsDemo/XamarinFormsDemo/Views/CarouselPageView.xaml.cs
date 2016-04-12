using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using XamarinFormsDemo.Const;
using XamarinFormsDemo.ViewModels;

namespace XamarinFormsDemo.Views
{
    public partial class CarouselPageView
    {
        public CarouselPageView()
        {
            InitializeComponent();

            var pageCount = 3;

            var urls = new string[]
            {
                $"http://7xswtn.com2.z0.glb.clouddn.com/06.jpg?imageView2/1/w/{DeviceInfo.WidthInteger}/h/{DeviceInfo.HeightInteger}/interlace/0/q/100",
                $"http://7xswtn.com2.z0.glb.clouddn.com/01.jpg?imageView2/1/w/{DeviceInfo.WidthInteger}/h/{DeviceInfo.HeightInteger}/interlace/0/q/100",
                $"http://7xswtn.com2.z0.glb.clouddn.com/03.jpg?imageView2/1/w/{DeviceInfo.WidthInteger}/h/{DeviceInfo.HeightInteger}/interlace/0/q/100"
            };

            var imageSource = new ImageSource[pageCount];
            for (int i = 0; i < imageSource.Length; i++)
            {
                imageSource[i] = ImageSource.FromUri(new Uri(urls[i]));
                ((UriImageSource) imageSource[i]).CachingEnabled = true;
                ((UriImageSource) imageSource[i]).CacheValidity = TimeSpan.MaxValue;

                Children.Add(new ContentPage {Content = new Image {Source = imageSource[i], Aspect = Aspect.Fill}});
            }
        }
    }
}
