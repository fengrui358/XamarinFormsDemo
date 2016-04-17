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

        private static Guid _deviceId;

        public static int Width { get; set; }

        public static int Height
        {
            //get { return Device.OS == TargetPlatform.iOS ? _height : _height - 20; }
            get { return _height; }
            set { _height = value; }
        }

        public static Guid DeviceId
        {
            get
            {
                if (_deviceId == Guid.Empty && Application.Current.Properties.ContainsKey(nameof(DeviceId)))
                {
                    _deviceId = Guid.Parse(Application.Current.Properties[nameof(DeviceId)].ToString());
                }

                return _deviceId;
            }
            set
            {
                if (_deviceId != value)
                {
                    _deviceId = value;
                    Application.Current.Properties[nameof(DeviceId)] = _deviceId;
                }
            }
        }
    }
}
