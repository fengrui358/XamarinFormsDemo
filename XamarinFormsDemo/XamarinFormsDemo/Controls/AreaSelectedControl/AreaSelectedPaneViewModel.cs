using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Helpers;
using XamarinFormsDemo.Const;
using XamarinFormsDemo.Models;

namespace XamarinFormsDemo.Controls.AreaSelectedControl
{
    public class AreaSelectedPaneViewModel : ViewModelBase
    {
        #region 字段

        private List<AdministrativeRegion> _provinceList;
        private List<AdministrativeRegion> _cityList;
        private List<AdministrativeRegion> _counyList;

        private AdministrativeRegion _provinceSelectedItem;
        private AdministrativeRegion _citySelectedItem;
        private AdministrativeRegion _counySelectedItem;

        #endregion


        #region 属性

        public List<AdministrativeRegion> ProvinceList
        {
            get { return _provinceList; }
            set
            {
                if (value != null && _provinceList != value)
                {
                    _provinceList = value;
                   
                    RaisePropertyChanged();
                    ProvinceSelectedItem = _provinceList.First();
                }
            }
        }

        public AdministrativeRegion ProvinceSelectedItem
        {
            get { return _provinceSelectedItem; }
            set
            {
                if (value != null && _provinceSelectedItem != value)
                {
                    _provinceSelectedItem = value;
                    
                    RaisePropertyChanged();
                    CityList = _provinceSelectedItem.Children;
                }
            }
        }

        public List<AdministrativeRegion> CityList
        {
            get { return _cityList; }
            set
            {
                if (value != null && _cityList != value)
                {
                    _cityList = value;

                    RaisePropertyChanged();
                    CitySelectedItem = _cityList.First();
                }
            }
        }

        public AdministrativeRegion CitySelectedItem
        {
            get { return _citySelectedItem; }
            set
            {
                if (value != null && _citySelectedItem != value)
                {
                    _citySelectedItem = value;

                    RaisePropertyChanged();
                    CounyList = _citySelectedItem.Children;
                }
            }
        }

        public List<AdministrativeRegion> CounyList
        {
            get { return _counyList; }
            set
            {
                if (value != null && _counyList != value)
                {
                    _counyList = value;

                    RaisePropertyChanged();
                    CounySelectedItem = _counyList.First();
                }
            }
        }

        public AdministrativeRegion CounySelectedItem
        {
            get { return _counySelectedItem; }
            set { Set(() => CounySelectedItem, ref _counySelectedItem, value); }
        }

        #endregion

        #region 命令

        public RelayCommand OkCommand { get; private set; }

        #endregion

        #region 委托

        public WeakAction<string> SelectedCallBack;

        #endregion

        #region 构造

        public AreaSelectedPaneViewModel()
        {
            ProvinceList = AdministrativeRegionCache.AdministrativeRegionList.ToList();

            OkCommand = new RelayCommand(OkCommandHandler);
        }

        #endregion

        #region 私有方法

        private async void OkCommandHandler()
        {
            if (SelectedCallBack != null && SelectedCallBack.IsAlive)
            {
                SelectedCallBack.ExecuteWithObject(
                    $"{ProvinceSelectedItem.AreaName}{CitySelectedItem.AreaName}{CounySelectedItem.AreaName}");
            }
        }

        #endregion
    }
}
