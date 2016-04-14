﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using XamarinFormsDemo.Const;
using XamarinFormsDemo.Controls;
using XamarinFormsDemo.Controls.Carousel;
using XamarinFormsDemo.Models;
using XamarinFormsDemo.ViewModels;

namespace XamarinFormsDemo.Views
{
    public partial class CarouselImageView : ContentPage
    {
        #region 字段

        private RelativeLayout _relativeLayout;
        private CarouselLayout.IndicatorStyleEnum _indicatorStyle;

        #endregion


        #region 构造

        public CarouselImageView()
        {
            InitializeComponent();

            _indicatorStyle = CarouselLayout.IndicatorStyleEnum.Dots;

            BindingContext = new CarouselImageViewModel();

            _relativeLayout = new RelativeLayout
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            var pagesCarousel = CreatePagesCarousel();
            //var dots = CreatePagerIndicatorContainer(); //todo：加点,自动轮播
            _relativeLayout.Children.Add(pagesCarousel,
                Constraint.RelativeToParent((parent) => { return parent.X; }),
                Constraint.RelativeToParent((parent) => { return parent.Y; }),
                Constraint.RelativeToParent((parent) => { return parent.Width; }),
                Constraint.RelativeToParent((parent) => { return parent.Height; })
                );

            carouseContainer.Content = _relativeLayout;
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

        #endregion

    }
}
