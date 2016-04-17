using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using XamarinFormsDemo.Helper;
using XamarinFormsDemo.Views;

namespace XamarinFormsDemo.ViewModels
{
    public class RegisterAddressViewModel:ViewModelBase
    {
        #region 字段

        private string _address;

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
        }

        #endregion

        #region 公共方法

        public void SelectedAddressCommandHandler()
        {
            IocHelper.GetNavigationPage().PushAsync(new SearchSuggestView(Address));
        }

        #endregion

        #region 私有方法

        #endregion
    }
}
