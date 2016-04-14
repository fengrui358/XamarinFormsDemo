using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using XamarinFormsDemo.Const;
using XamarinFormsDemo.Models;

namespace XamarinFormsDemo.Controls.AreaSelectControl
{
    public class AreaSelectPaneViewModel : ViewModelBase
    {
        private List<AdministrativeRegion> _provinceList;
        private List<AdministrativeRegion> _cityList;
        private List<AdministrativeRegion> _counyList;

        private AdministrativeRegion _provinceSelectedItem;
        private AdministrativeRegion _citySelectedItem;
        private AdministrativeRegion _counySelectedItem;

        public List<AdministrativeRegion> ProvinceList
        {
            get { return _provinceList; }
            set
            {
                if (value != null && _provinceList != value)
                {
                    _provinceList = value;

                    ProvinceSelectedItem = _provinceList.First();
                    RaisePropertyChanged();
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

                    //联动城市
                    CityList = _provinceSelectedItem.Children;
                    RaisePropertyChanged();
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

                    CitySelectedItem = _cityList.First();
                    RaisePropertyChanged();
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

                    CounyList = _citySelectedItem.Children;
                    RaisePropertyChanged();
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

                    CounySelectedItem = _counyList.First();
                    RaisePropertyChanged();
                }
            }
        }

        public AdministrativeRegion CounySelectedItem
        {
            get { return _counySelectedItem; }
            set { Set(() => CounySelectedItem, ref _counySelectedItem, value); }
        }

        public AreaSelectPaneViewModel()
        {
            ProvinceList = AdministrativeRegionCache.AdministrativeRegionList.ToList();
        }
    }
}
