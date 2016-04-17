using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinFormsDemo.Models
{
    public class ImageModel
    {
        public Uri Uri { get; private set; }

        public string Path { get; private set; }

        public ImageSource ImageSource
        {
            get
            {
                if (Uri != null)
                {
                    var urlImageSource = ImageSource.FromUri(Uri);
                    return urlImageSource;
                }
                else if (!string.IsNullOrEmpty(Path))
                {
                    return ImageSource.FromResource(Path, typeof(ImageModel).GetTypeInfo().Assembly);
                }

                return null;
            }
        }

        public ImageModel(Uri uri)
        {
            Uri = uri;
        }

        public ImageModel(string path)
        {
            Path = path;
        }
    }
}
