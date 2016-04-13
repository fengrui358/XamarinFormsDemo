using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace XamarinFormsDemo.ViewModels
{
    public class SearchSuggestViewModel : ViewModelBase
    {
        private string _searchKeyWords;
        private List<string> _suggestResults; 

        public string SearchKeyWords
        {
            get { return _searchKeyWords; }
            set
            {
                var x = value;
                Set(() => SearchKeyWords, ref _searchKeyWords, value);
            }
        }

        /// <summary>
        /// 用整组替换的方式，效率好些
        /// </summary>
        public List<string> SuggestResults
        {
            get { return _suggestResults; }
            set { Set(() => SuggestResults, ref _suggestResults, value); }
        } 

        public SearchSuggestViewModel(string keyWords)
        {
            _suggestResults = new List<string>();
            SearchKeyWords = keyWords;
            TextChangedHandler(keyWords);
        }

        public void TextChangedHandler(string newKeyWord)
        {
            if (string.IsNullOrEmpty(newKeyWord))
            {
                _suggestResults = new List<string>();
            }


        }
    }
}
