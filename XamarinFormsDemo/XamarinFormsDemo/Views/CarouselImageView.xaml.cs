using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using XamarinFormsDemo.Const;
using XamarinFormsDemo.Controls;
using XamarinFormsDemo.Controls.Carousel;
using XamarinFormsDemo.Interface;
using XamarinFormsDemo.Models;
using XamarinFormsDemo.ViewModels;

namespace XamarinFormsDemo.Views
{
    public partial class CarouselImageView : ContentPage
    {
        #region 字段

        private RelativeLayout _relativeLayout;
        private CarouselLayout.IndicatorStyleEnum _indicatorStyle;
        private CarouselImageViewModel _viewModel;
        private bool _isCurrentPage;
        private IAdvancedTimer _timer;

        #endregion


        #region 构造

        public CarouselImageView()
        {
            InitializeComponent();

            _indicatorStyle = CarouselLayout.IndicatorStyleEnum.Dots;

            _viewModel = new CarouselImageViewModel();
            BindingContext = _viewModel;

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

            _timer = DependencyService.Get<IAdvancedTimer>(DependencyFetchTarget.NewInstance);
            _timer.InitTimer(2000, Carousel, true);
        }

        #endregion


        #region 私有方法

        private CarouselLayout CreatePagesCarousel()
        {
            var carousel = new CarouselLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                IndicatorStyle = _indicatorStyle,
                ItemTemplate = new DataTemplate(typeof (ImageModelView))
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

        protected async override void OnAppearing()
        {
            _isCurrentPage = true;

            await Task.Delay(100);
            _timer.StartTimer();
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            _isCurrentPage = false;
            _timer.StopTimer();
            base.OnDisappearing();
        }

        private void Carousel(object sender, EventArgs args)
        {
            if (!_isCurrentPage)
            {
                return;
            }

            var index = _viewModel.ImageModels.IndexOf(_viewModel.CurrentImage);
            if (index == _viewModel.ImageModels.Count - 1)
            {
                _viewModel.CurrentImage = _viewModel.ImageModels[0];
            }
            else
            {
                _viewModel.CurrentImage = _viewModel.ImageModels[++index];
            }
        }

        #endregion

    }
}
