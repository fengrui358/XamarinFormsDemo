using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinFormsDemo.Const
{
    public class DeviceInfo
    {
        private static int _height;

        public static int Width { get; set; }

        public static int Height
        {
            get { return Device.OS == TargetPlatform.iOS ? _height : _height - 20; }
            set { _height = value; }
        }
    }
}
