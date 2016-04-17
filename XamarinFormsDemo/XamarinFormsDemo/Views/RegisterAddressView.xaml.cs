using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using XamarinFormsDemo.ViewModels;

namespace XamarinFormsDemo.Views
{
    public partial class RegisterAddressView : ContentPage
    {
        private RegisterAddressViewModel _viewModel;

        public RegisterAddressView()
        {
            InitializeComponent();

            _viewModel = new RegisterAddressViewModel();
            BindingContext = _viewModel;
        }

        private void VisualElement_OnFocused(object sender, FocusEventArgs e)
        {
            _viewModel.SelectedAddressCommandHandler();
        }
    }
}
