using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SpeakFriend.Utilities.Wpf
{
    public static class ListViewUtils
    {
        public static TDataItem GetDataItemItem<TDataItem>(this ListView listView, RoutedEventArgs e)
        {
            var dependencyObject = (DependencyObject)e.OriginalSource;

            return GetDataItem<TDataItem>(dependencyObject, listView);
        }

        public static TDataItem GetDataItemItem<TDataItem>(this ListView listView, DependencyObject dependencyObject)
        {
            return GetDataItem<TDataItem>(dependencyObject, listView);
        }

        private static TDataItem GetDataItem<TDataItem>(DependencyObject dependencyObject, ListView listView)
        {
            while ((dependencyObject != null) && !(dependencyObject is ListViewItem))
                dependencyObject = VisualTreeHelper.GetParent(dependencyObject);

            return (TDataItem)listView.ItemContainerGenerator.ItemFromContainer(dependencyObject);
        }
    }
}
