namespace CarouselPanelSample
{
    using System;
    using System.Linq;
    using System.Windows.Controls;
    using System.Windows.Interactivity;
    using Microsoft.Expression.Controls;
    public class PathListBoxBehavior : Behavior<PathListBox>
    {
        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.SelectionChanged += AssociatedObjectOnSelectionChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            AssociatedObject.SelectionChanged -= AssociatedObjectOnSelectionChanged;
        }

        private void AssociatedObjectOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AssociatedObject.LayoutPaths.Any() && e.AddedItems != null && e.AddedItems.Count > 0)
            {
                var layoutPath = AssociatedObject.LayoutPaths[0];
                var selectedIndex = AssociatedObject.SelectedIndex;
                var itemCount = layoutPath.ActualCapacity;

                layoutPath.Start = 0.5 - ((1.0 / itemCount) * selectedIndex);
            }
        }
    }
}