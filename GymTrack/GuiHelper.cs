using System.Windows.Media;
using System.Windows;

namespace GymTrack
{
    /// <summary>
    /// Helps to find ancestors and childs of UI objects
    /// </summary>
    public static class GuiHelper
    {
        /// <summary>
        /// Finds ancestor UI object of provided one
        /// </summary>
        /// <typeparam name="T">Type of object that is required to be returned</typeparam>
        /// <param name="current">Object, which parent is required to be searched for</param>
        /// <returns>T typed ancestor UI object</returns>
        public static T FindAncestor<T>(DependencyObject current) where T : DependencyObject
        {
            do
            {
                if (current is T ancestor)
                {
                    return ancestor;
                }
                current = VisualTreeHelper.GetParent(current);
            } while (current != null);
            return null;
        }
        /// <summary>
        /// Recursively finds child UI object of provided one
        /// </summary>
        /// <typeparam name="T">Type of object that is required to be returned</typeparam>
        /// <param name="parent">Object, which child is required to be searched for</param>
        /// <param name="name">NameProperty of child object</param>
        /// <returns>T typed ancestor UI object</returns>
        public static T FindVisualChild<T>(DependencyObject parent, string name) where T : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(parent); i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                if (child is T typedChild && (string)child.GetValue(FrameworkElement.NameProperty) == name)
                {
                    return typedChild;
                }
                else
                {
                    T childOfChild = FindVisualChild<T>(child, name);
                    if (childOfChild != null)
                    {
                        return childOfChild;
                    }
                }
            }
            return null;
        }
    }
}
