using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Newtonsoft.Json;
using Xamarin.Forms;
using XamarinFormsDemo.Const;
using XamarinFormsDemo.Helper;
using XamarinFormsDemo.Models.APIModels;
using XamarinFormsDemo.Views;

namespace XamarinFormsDemo.ViewModels
{
    public class RegisterAddressViewModel:ViewModelBase
    {
        #region 字段

        private string _address;
        private LocationModel _position;

        #endregion

        #region 属性

        public string Address
        {
            get { return _address; }
            set { Set(() => Address, ref _address, value); }
        }

        #endregion

        #region 命令

        public RelayCommand SelectedAddressCommand { get; private set; }

        #endregion

        #region 构造

        public RegisterAddressViewModel()
        {
            SelectedAddressCommand = new RelayCommand(SelectedAddressCommandHandler);

            if (!string.IsNullOrEmpty(PositionHelper.Address))
            {
                Address = PositionHelper.Address;
            }
            else
            {
                SetBaiduPosition();
            }

            Messenger.Default.Register<string>(this, MessengeToken.SearchCallBack, (s) =>
            {
                Address = s;
            });
        }

        #endregion

        #region 公共方法

        public void SelectedAddressCommandHandler()
        {
            IocHelper.GetNavigationPage().PushAsync(new SearchSuggestView(Address, _position));
        }

        #endregion

        #region 私有方法

        private async void SetBaiduPosition()
        {
            _position = await PositionHelper.GetBaiduPosition();

            SetPositionAddress(_position);
        }

        private async void SetPositionAddress(LocationModel position)
        {
            //获取逆地址解析
            if (_position != null)
            {
                try
                {
                    var api =
                        $"http://api.map.baidu.com/geocoder/v2/?ak={AppInfo.BaiduMapAk}&location={position.Lat},{position.Lng}&output=json&pois=1";

                    using (var httpClient = new HttpClient())
                    {
                        var response = await httpClient.GetAsync(new Uri(api));

                        if (response.IsSuccessStatusCode)
                        {
                            var json = await response.Content.ReadAsStringAsync();

                            var objResluts = JsonConvert.DeserializeObject<BaiduJsonGetAddressFromPosition>(json);
                            if (!string.IsNullOrEmpty(objResluts?.Result.Formatted_Address))
                            {
                                Address = objResluts.Result.Formatted_Address;
                                //PositionHelper.Address = Address;

                                PositionHelper.City = objResluts.Result.AddressComponent.City;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex);
                }
            }
        }

        #endregion
    }
}
