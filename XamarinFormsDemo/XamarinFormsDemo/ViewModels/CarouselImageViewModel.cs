using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;
using XamarinFormsDemo.Const;
using XamarinFormsDemo.Helper;
using XamarinFormsDemo.Models;
using XamarinFormsDemo.Views;

namespace XamarinFormsDemo.ViewModels
{
    public class CarouselImageViewModel : ViewModelBase
    {
        #region 字段

        private List<ImageModel> _imageModels;
        private ImageModel _currentImage;

        #endregion

        #region 命令

        public RelayCommand<Type> GoToCommand { get; private set; }

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
            GoToCommand = new RelayCommand<Type>(GoToCommandHandler);

            var height = DeviceInfo.Height / 3;

            var urls = new string[]
            {
                $"http://7xswtn.com2.z0.glb.clouddn.com/02.jpg?imageView2/1/w/{DeviceInfo.Width}/h/{height}/interlace/0/q/100",
                $"http://7xswtn.com2.z0.glb.clouddn.com/09.jpg?imageView2/1/w/{DeviceInfo.Width}/h/{height}/interlace/0/q/100",
                $"http://7xswtn.com2.z0.glb.clouddn.com/07.jpg?imageView2/1/w/{DeviceInfo.Width}/h/{height}/interlace/0/q/100"
            };

            var temp = new List<ImageModel>();

            for (int i = 0; i < urls.Length; i++)
            {
                temp.Add(new ImageModel(new Uri(urls[i])));
            }

            ImageModels = temp;

            CurrentImage = ImageModels.FirstOrDefault();
        }

        #endregion

        #region 私有方法

        private async void GoToCommandHandler(Type parm)
        {
            var navigationPage = IocHelper.GetNavigationPage();

            if (parm == typeof(AreaSelectedView))
            {
                await navigationPage.PushAsync(new AreaSelectedView());
            }
            else if (parm == typeof(RegisterAddressView))
            {
                await navigationPage.PushAsync(new RegisterAddressView());
            }
            //else if (parm == typeof(BaiduMapView))
            //{
            //    await navigationPage.PushAsync(new BaiduMapView());
            //}
            //else if (parm == typeof(AreaSelectedView))
            //{
            //    await navigationPage.PushAsync(new AreaSelectedView());
            //}
        }

        #endregion
    }
}
