using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinFormsDemo.Interface;

namespace XamarinFormsDemo.Helper
{
    public class TimerFactory
    {
        static Lazy<IAdvancedTimer> _fileSystem = new Lazy<IAdvancedTimer>(() => CreateFileSystem(), System.Threading.LazyThreadSafetyMode.PublicationOnly);

        /// <summary>
        /// The implementation of <see cref="IFileSystem"/> for the current platform
        /// </summary>
        public static IAdvancedTimer Current
        {
            get
            {
                IAdvancedTimer ret = _fileSystem.Value;
                if (ret == null)
                {
                    throw new NotImplementedException();
                }
                return ret;
            }
        }

        static IAdvancedTimer CreateFileSystem()
        {
#if NETFX_CORE || WINDOWS_PHONE
			return new WinRTFileSystem();
#elif SILVERLIGHT
			return new IsoStoreFileSystem();
#elif FILE_SYSTEM
            return new DesktopFileSystem();
#elif __ANDROID__ || __IOS__
            return new XamarinFormsDemo.Helper.AdvancedTimerImplementation();
#else
            return null;
#endif
        }

        public static IAdvancedTimer GetAdvancedTimer()
        {
#if __ANDROID__ || __IOS__
            return new XamarinFormsDemo.Helper.AdvancedTimerImplementation();
#endif
            throw new NotImplementedException();
        }
    }
}
