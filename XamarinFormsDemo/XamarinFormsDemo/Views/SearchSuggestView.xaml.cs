using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XamarinFormsDemo.Views
{
    public partial class SearchSuggestView : ContentPage
    {
        private string _keyWords;

        public SearchSuggestView(string keyWords)
        {
            InitializeComponent();

            _keyWords = keyWords;
        }
    }
}
