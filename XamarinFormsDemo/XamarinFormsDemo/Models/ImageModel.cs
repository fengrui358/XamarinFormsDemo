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
        public Uri Uri { get; private set; }

        public ImageSource ImageUrlSource
        {
            get
            {
                if (Uri != null)
                {
                    var urlImageSource = ImageSource.FromUri(Uri);
                    return urlImageSource;
                }

                return null;
            }
        }

        public ImageModel(Uri uri)
        {
            Uri = uri;
        }
    }
}
