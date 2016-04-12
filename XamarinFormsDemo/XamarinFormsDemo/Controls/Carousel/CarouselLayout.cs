﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;
using XamarinFormsDemo.Helper;
using XamarinFormsDemo.Interface;

namespace XamarinFormsDemo.Controls.Carousel
{
	public class CarouselLayout : ScrollView
	{
		public enum IndicatorStyleEnum
		{
			None,
			Dots,
			Tabs
		}

		readonly StackLayout _stack;
		IAdvancedTimer _selectedItemTimer;

		int _selectedIndex;

		public CarouselLayout ()
		{
			Orientation = ScrollOrientation.Horizontal;

			_stack = new StackLayout {
				Orientation = StackOrientation.Horizontal,
				Spacing = 0
			};

			Content = _stack;

		    var s = TimerFactory.Current;

            _selectedItemTimer = TimerFactory.GetAdvancedTimer();
            _selectedItemTimer.InitTimer(300, SelectedItemTimerElapsed, false);
        }

		public IndicatorStyleEnum IndicatorStyle { get; set; }

		public IList<View> Children {
			get {
				return _stack.Children;
			}
		}

		private bool _layingOutChildren;
		protected override void LayoutChildren (double x, double y, double width, double height)
		{
			base.LayoutChildren (x, y, width, height);
			if (_layingOutChildren) return;

			_layingOutChildren = true;
			foreach (var child in Children) child.WidthRequest = width;
			_layingOutChildren = false;
		}

		public static readonly BindableProperty SelectedIndexProperty =
			BindableProperty.Create<CarouselLayout, int> (
				carousel => carousel.SelectedIndex,
				0,
				BindingMode.TwoWay,
				propertyChanged: (bindable, oldValue, newValue) => {
				((CarouselLayout)bindable).UpdateSelectedItem ();
			}
			);

		public int SelectedIndex {
			get {
				return (int)GetValue (SelectedIndexProperty);
			}
			set {
				SetValue (SelectedIndexProperty, value);
			}
		}

		void UpdateSelectedItem ()
		{
			_selectedItemTimer.StopTimer();
			_selectedItemTimer.StartTimer();
		}

		void SelectedItemTimerElapsed (object sender, EventArgs e) {
			SelectedItem = SelectedIndex > -1 ? Children [SelectedIndex].BindingContext : null;
		}

		public static readonly BindableProperty ItemsSourceProperty =
			BindableProperty.Create<CarouselLayout, IList> (
				view => view.ItemsSource,
				null,
				propertyChanging: (bindableObject, oldValue, newValue) => {
				((CarouselLayout)bindableObject).ItemsSourceChanging ();
			},
				propertyChanged: (bindableObject, oldValue, newValue) => {
				((CarouselLayout)bindableObject).ItemsSourceChanged ();
			}
			);

		public IList ItemsSource {
			get {
				return (IList)GetValue (ItemsSourceProperty);
			}
			set {
				SetValue (ItemsSourceProperty, value);
			}
		}

		void ItemsSourceChanging ()
		{
			if (ItemsSource == null) return;
			_selectedIndex = ItemsSource.IndexOf (SelectedItem);
		}

		void ItemsSourceChanged ()
		{
			_stack.Children.Clear ();
			foreach (var item in ItemsSource) {
				var view = (View)ItemTemplate.CreateContent ();
				var bindableObject = view as BindableObject;
				if (bindableObject != null)
					bindableObject.BindingContext = item;
				_stack.Children.Add (view);
			}

			if (_selectedIndex >= 0) SelectedIndex = _selectedIndex;
		}

		public DataTemplate ItemTemplate {
			get;
			set;
		}

		public static readonly BindableProperty SelectedItemProperty = 
			BindableProperty.Create<CarouselLayout, object> (
				view => view.SelectedItem,
				null,
				BindingMode.TwoWay,
				propertyChanged: (bindable, oldValue, newValue) => {
				((CarouselLayout)bindable).UpdateSelectedIndex ();
			}
			);

		public object SelectedItem {
			get {
				return GetValue (SelectedItemProperty);
			}
			set {
				SetValue (SelectedItemProperty, value);
			}
		}

		void UpdateSelectedIndex ()
		{
			if (SelectedItem == BindingContext) return;

			SelectedIndex = Children
				.Select (c => c.BindingContext)
				.ToList ()
				.IndexOf (SelectedItem);
		}
	}
}