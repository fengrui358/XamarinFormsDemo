using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using XamarinFormsDemo.Models.APIModels;
using XamarinFormsDemo.ViewModels;

namespace XamarinFormsDemo.Views
{
    public partial class SearchSuggestView : ContentPage
    {
        #region 字段

        private SearchSuggestViewModel _viewModel;

        #endregion

        #region 构造

        public SearchSuggestView(string keyWords, LocationModel position = null)
        {
            InitializeComponent();

            _viewModel = new SearchSuggestViewModel(keyWords);
            BindingContext = _viewModel;
        }

        #endregion

        #region 私有方法

        private void Entry_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.TextChangedHandler(e.NewTextValue);
        }

        #endregion

    }
}
