using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinFormsDemo.Const;

namespace XamarinFormsDemo.Controls
{
    public class ImageModelView : ContentView
    {
        public ImageModelView()
        {
            var height = DeviceInfo.Height / 3;

            Content = new Image
            {
                //todo：没有绑定
                Source = ImageSource.FromUri(new Uri($"http://7xswtn.com2.z0.glb.clouddn.com/06.jpg?imageView2/1/w/{DeviceInfo.Width}/h/{height}/interlace/0/q/100")),
                Aspect = Aspect.Fill
            };
        }
    }
}
