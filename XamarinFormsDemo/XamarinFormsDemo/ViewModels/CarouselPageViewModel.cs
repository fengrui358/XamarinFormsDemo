using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinFormsDemo.Annotations;

namespace XamarinFormsDemo.ViewModels
{
    public class CarouselPageViewModel : INotifyPropertyChanged
    {
        private IEnumerable<Image> _carouselImageSource;

        public IEnumerable<Image> CarouselImageSource
        {
            get { return _carouselImageSource; }
            set
            {
                _carouselImageSource = value;
                OnPropertyChanged();
            }
        }

        public CarouselPageViewModel()
        {
            CarouselImageSource = new List<Image>();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
