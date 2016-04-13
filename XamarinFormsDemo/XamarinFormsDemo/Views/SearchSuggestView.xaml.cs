using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using XamarinFormsDemo.ViewModels;

namespace XamarinFormsDemo.Views
{
    public partial class SearchSuggestView : ContentPage
    {
        private SearchSuggestViewModel _viewModel;

        public SearchSuggestView(string keyWords)
        {
            InitializeComponent();

            _viewModel = new SearchSuggestViewModel(keyWords);
            BindingContext = _viewModel;
        }

        private void Entry_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            _viewModel.TextChangedHandler(e.NewTextValue);
        }
    }
}
