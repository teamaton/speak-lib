using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Media;

namespace SpeakFriend.Utilities.Wpf
{
    public static class UiUtilities
    {
        public static T FindLogicalAncestorByType<T>(this DependencyObject startElement) where T : DependencyObject
        {
            return (T)FindLogicalAncestor(startElement, o => o is T);
        }

        /// <summary>
        /// Finds the visual descendant.
        /// </summary>
        /// <param name="startElement">The start element.</param>
        /// <param name="condition">The condition.</param>
        /// <returns></returns>
        public static DependencyObject FindVisualDescendant(this DependencyObject startElement, Predicate<DependencyObject> condition)
        {
            if (startElement != null)
            {
                if (condition(startElement))
                {
                    return startElement;
                }
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(startElement); ++i)
                {
                    DependencyObject o = FindVisualDescendant(VisualTreeHelper.GetChild(startElement, i), condition);
                    if (o != null)
                    {
                        return o;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Finds the logical ancestor according to the predicate.
        /// </summary>
        /// <param name="startElement">The start element.</param>
        /// <param name="condition">The condition.</param>
        /// <returns></returns>
        public static DependencyObject FindLogicalAncestor(this DependencyObject startElement, Predicate<DependencyObject> condition)
        {
            DependencyObject o = startElement;
            while ((o != null) && !condition(o))
            {
                o = LogicalTreeHelper.GetParent(o);
            }
            return o;
        }
    }
}
