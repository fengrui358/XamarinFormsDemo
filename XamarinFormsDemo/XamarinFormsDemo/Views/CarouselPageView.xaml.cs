using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Ioc;
using Xamarin.Forms;
using XamarinFormsDemo.Const;
using XamarinFormsDemo.Controls;
using XamarinFormsDemo.Controls.Carousel;
using XamarinFormsDemo.ViewModels;

namespace XamarinFormsDemo.Views
{
    public partial class CarouselPageView : ContentPage
    {
        #region 字段

        private RelativeLayout _relativeLayout;
        private CarouselLayout.IndicatorStyleEnum _indicatorStyle;

        private CarouselPageViewModel _viewModel = new CarouselPageViewModel();

        #endregion

        #region 构造

        public CarouselPageView()
        {
            InitializeComponent();

            BindingContext = _viewModel;
            _viewModel.PropertyChanged += ViewModelOnPropertyChanged;

            _indicatorStyle = CarouselLayout.IndicatorStyleEnum.Dots;

            _relativeLayout = new RelativeLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            var pagesCarousel = CreatePagesCarousel();
            var dots = CreatePagerIndicatorContainer();
            _relativeLayout.Children.Add(pagesCarousel,
                Constraint.RelativeToParent((parent) => { return parent.X; }),
                Constraint.RelativeToParent((parent) => { return parent.Y; }),
                Constraint.RelativeToParent((parent) => { return parent.Width; }),
                Constraint.RelativeToParent((parent) => { return parent.Height; })
                );

            _relativeLayout.Children.Add(dots,
                        Constraint.Constant(0),
                        Constraint.RelativeToView(pagesCarousel,
                            (parent, sibling) => { return sibling.Height - 18; }),
                        Constraint.RelativeToParent(parent => parent.Width),
                        Constraint.Constant(18)
                    );

            carouseContainer.Content = _relativeLayout;
        }

        #endregion

        #region 私有方法

        protected override void OnAppearing()
        {
            AdministrativeRegionCache.Init();

            base.OnAppearing();
        }

        private CarouselLayout CreatePagesCarousel()
        {
            var carousel = new CarouselLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                IndicatorStyle = _indicatorStyle,
                ItemTemplate = new DataTemplate(typeof(ImageModelView))
            };

            carousel.SetBinding(CarouselLayout.ItemsSourceProperty, "ImageModels");
            carousel.SetBinding(CarouselLayout.SelectedItemProperty, "CurrentImage", BindingMode.TwoWay);

            return carousel;
        }

        View CreatePagerIndicatorContainer()
        {
            return new StackLayout
            {
                Children = { CreatePagerIndicators() }
            };
        }

        View CreatePagerIndicators()
        {
            var pagerIndicator = new PagerIndicatorDots { DotSize = 5, DotColor = Color.Black };
            pagerIndicator.SetBinding(PagerIndicatorDots.ItemsSourceProperty, "ImageModels");
            pagerIndicator.SetBinding(PagerIndicatorDots.SelectedItemProperty, "CurrentImage");
            return pagerIndicator;
        }

        private void ViewModelOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            if (propertyChangedEventArgs.PropertyName == "CurrentImage")
            {
                var lastOne = _viewModel.ImageModels.LastOrDefault();
                if (lastOne == _viewModel.CurrentImage)
                {
                    Task.Run(async () =>
                    {
                        await Task.Delay(400);

                        var mainPage = new NavigationPage(new MainPageView());

                        if (Device.OS == TargetPlatform.iOS)
                        {
                            mainPage.BarBackgroundColor = Color.White;
                            mainPage.BarTextColor = Color.Black;
                        }
                        else if (Device.OS == TargetPlatform.Android)
                        {
                            mainPage.BarBackgroundColor = Color.Black;
                            mainPage.BarTextColor = Color.White;
                        }

                        SimpleIoc.Default.Register(() => mainPage, typeof(MainPageView).ToString());

                        Device.BeginInvokeOnMainThread(() =>
                        {
                            Application.Current.MainPage = mainPage;
                        });
                    });
                }
            }
        }

        #endregion

    }
}
