using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using Xamarin.Forms;
using XamarinFormsDemo.Const;
using XamarinFormsDemo.Models;

namespace XamarinFormsDemo.ViewModels
{
    public class CarouselImageViewModel : ViewModelBase
    {
        #region 字段

        private List<ImageModel> _imageModels;
        private ImageModel _currentImage;

        #endregion


        #region 属性

        public List<ImageModel> ImageModels
        {
            get { return _imageModels; }
            set { Set(() => ImageModels, ref _imageModels, value); }
        }

        public ImageModel CurrentImage
        {
            get { return _currentImage; }
            set { Set(() => CurrentImage, ref _currentImage, value); }
        }

        #endregion


        #region 构造

        public CarouselImageViewModel()
        {
            var urls = new string[]
            {
                $"http://7xswtn.com2.z0.glb.clouddn.com/06.jpg?imageView2/1/w/{DeviceInfo.Width}/h/{DeviceInfo.Height}/interlace/0/q/100",
                $"http://7xswtn.com2.z0.glb.clouddn.com/01.jpg?imageView2/1/w/{DeviceInfo.Width}/h/{DeviceInfo.Height}/interlace/0/q/100",
                $"http://7xswtn.com2.z0.glb.clouddn.com/03.jpg?imageView2/1/w/{DeviceInfo.Width}/h/{DeviceInfo.Height}/interlace/0/q/100"
            };

            var temp = new List<ImageModel>();

            for (int i = 0; i < urls.Length; i++)
            {
                temp.Add(new ImageModel(urls[i]));
            }

            ImageModels = temp;

            CurrentImage = ImageModels.FirstOrDefault();
        }

        #endregion

    }
}
