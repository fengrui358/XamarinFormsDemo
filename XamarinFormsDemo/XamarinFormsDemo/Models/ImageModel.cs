using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinFormsDemo.Models
{
    public class ImageModel
    {
        public string Url { get; private set; }

        public ImageSource ImageUrlSource
        {
            get
            {
                if (!string.IsNullOrEmpty(Url))
                {
                    var urlImageSource = ImageSource.FromUri(new Uri(Url));
                    return urlImageSource;
                }

                return null;
            }
        }

        public ImageModel(string url)
        {
            Url = url;
        }
    }
}
