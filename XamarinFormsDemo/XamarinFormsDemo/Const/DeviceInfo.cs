using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamarinFormsDemo.Const
{
    public class DeviceInfo
    {
        public static double Width { get; set; }

        public static double Height { get; set; }

        public static int WidthInteger => (int) Width;

        public static int HeightInteger => (int) Height;
    }
}
