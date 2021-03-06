﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SWE2_TourPlanner.Factory
{
    public class ViewModelCreator
    {
        public static readonly DependencyProperty FactoryTypeProperty =
            DependencyProperty.RegisterAttached("FactoryType", typeof(Type), typeof(ViewModelCreator),
                new FrameworkPropertyMetadata(null,
                    OnFactoryTypeChanged));


        public static Type GetFactoryType(DependencyObject d)
        {
            return (Type)d.GetValue(FactoryTypeProperty);
        }

        public static void SetFactoryType(DependencyObject d, Type value)
        {
            d.SetValue(FactoryTypeProperty, value);
        }

        private static void OnFactoryTypeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            FrameworkElement element = (FrameworkElement)d;
            IViewModelFactory factory = Activator.CreateInstance(GetFactoryType(d)) as IViewModelFactory;
            if (factory == null)
                throw new InvalidOperationException("Your type does not implement the IViewModelFactory.");
            element.DataContext = factory.CreateViewModel(d);
        }
    }
}
