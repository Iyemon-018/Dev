namespace CarouselPanelSample01
{
    using System;
    using System.Linq;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Interactivity;
    using System.Windows.Media.Animation;
    using Microsoft.Expression.Controls;

    public class PathListBoxBehavior : Behavior<PathListBox>
    {
        /// <summary>
        /// LayoutPath Start の最大値
        /// </summary>
        private const double MaximumLength = 1.0;

        /// <summary>
        /// 前回選択されていたインデックス
        /// </summary>
        private int _previousSelectedIndex;

        /// <summary>
        /// LayoutPath のStart の初期値を設定、または取得します。
        /// </summary>
        public double DefaultStart { get; set; }

        protected override void OnAttached()
        {
            base.OnAttached();

            if (AssociatedObject != null)
            {
                AssociatedObject.SelectionChanged += AssociatedObjectOnSelectionChanged;
            }
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();

            if (AssociatedObject != null)
            {
                AssociatedObject.SelectionChanged -= AssociatedObjectOnSelectionChanged;
            }
        }

        private void AssociatedObjectOnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems == null || e.AddedItems.Count < 1 || !AssociatedObject.LayoutPaths.Any()) return;

            LayoutPath target = AssociatedObject.LayoutPaths.First();
            int selectedIndex = AssociatedObject.SelectedIndex;
            int itemCount = AssociatedObject.Items.Count;

            // アニメーションする移動量を計算する。
            double from;
            double to = DefaultStart - MaximumLength / itemCount * selectedIndex;
            target.Start = to;

            if (_previousSelectedIndex == 0 && selectedIndex == itemCount - 1)
            {
                from = DefaultStart - MaximumLength;
            }
            else if (_previousSelectedIndex == itemCount - 1 && selectedIndex == 0)
            {
                from = DefaultStart + MaximumLength / itemCount;
            }
            else
            {
                from = target.Start;
            }

            // Storyboardを作成する。
            var frames = new DoubleAnimationUsingKeyFrames();
            frames.KeyFrames.Add(new EasingDoubleKeyFrame(from, KeyTime.FromTimeSpan(TimeSpan.Zero)));
            frames.KeyFrames.Add(new EasingDoubleKeyFrame(to, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(200))));
            Storyboard.SetTarget(frames, target);
            Storyboard.SetTargetProperty(frames, new PropertyPath(LayoutPath.StartProperty));
            frames.Freeze();
            var storyboard = new Storyboard();
            storyboard.Children.Add(frames);
            storyboard.Freeze();

            AssociatedObject.BeginStoryboard(storyboard);
            _previousSelectedIndex = selectedIndex;
        }
    }
}