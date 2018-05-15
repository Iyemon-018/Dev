namespace MarqueeTextAnimationSample
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media.Animation;

    public enum MarqueeVelocity
    {
        Slow

        , Normal

        , Fast
    }

    [TemplatePart(Name = PartScrollPanelName, Type = typeof(Canvas))]
    [TemplatePart(Name = PartTextName, Type = typeof(TextBlock))]
    [TemplatePart(Name = PartTextCopyName, Type = typeof(TextBlock))]
    public class MarqueeText : Control
    {
        #region Const

        private const string PartScrollPanelName = "PART_ScrollPanel";

        private const string PartTextName = "PART_Text";

        private const string PartTextCopyName = "PART_TextCopy";

        public static readonly DependencyProperty TextProperty =
                DependencyProperty.Register("Text"
                                            , typeof(string)
                                            , typeof(MarqueeText)
                                            , new FrameworkPropertyMetadata("Marquee text."
                                                                            , FrameworkPropertyMetadataOptions.AffectsRender
                                                                            , TextPropertyChanged));

        public static readonly DependencyProperty VelocityProperty =
                DependencyProperty.Register("Velocity"
                                            , typeof(MarqueeVelocity)
                                            , typeof(MarqueeText)
                                            , new FrameworkPropertyMetadata(MarqueeVelocity.Normal
                                                                            , FrameworkPropertyMetadataOptions.AffectsRender
                                                                            , VelocityPropertyChanged));

        #endregion

        #region Fields

        private Canvas _partScrollPanel;

        private TextBlock _partText;

        private TextBlock _partTextCopy;

        private double _velocityValue = 40;

        #endregion

        #region Ctor

        static MarqueeText()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MarqueeText), new FrameworkPropertyMetadata(typeof(MarqueeText)));
        }

        #endregion

        #region Properties

        public string Text
        {
            get => (string)GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public MarqueeVelocity Velocity
        {
            get => (MarqueeVelocity)GetValue(VelocityProperty);
            set => SetValue(VelocityProperty, value);
        }

        #endregion

        #region Methods

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _partScrollPanel = GetTemplateChild(PartScrollPanelName) as Canvas;
            _partText = GetTemplateChild(PartTextName) as TextBlock;
            _partTextCopy = GetTemplateChild(PartTextCopyName) as TextBlock;

            if (_partText != null)
            {
                _partText.SizeChanged += PartText_OnSizeChanged;
            }
        }

        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);

            InitializePosition();
        }

        private void InitializePosition()
        {
            if (_partScrollPanel == null || _partText == null || _partTextCopy == null) return;

            double textWidth = _partText.ActualWidth;
            Canvas.SetLeft(_partText, 0);

            double copyTextPosition = textWidth + ActualWidth;
            Canvas.SetLeft(_partTextCopy, copyTextPosition);
            
            TimeSpan waitTime = TimeSpan.FromSeconds(3);
            double distance = _partText.ActualWidth + ActualWidth;
            double toValue = 0 - distance;
            double time = distance / _velocityValue;
            TimeSpan toKeyTime = waitTime.Add(TimeSpan.FromSeconds(time));

            DoubleAnimationUsingKeyFrames keyFrames = new DoubleAnimationUsingKeyFrames();
            keyFrames.KeyFrames.Add(new EasingDoubleKeyFrame(0, TimeSpan.Zero));
            keyFrames.KeyFrames.Add(new EasingDoubleKeyFrame(0, waitTime));
            keyFrames.KeyFrames.Add(new EasingDoubleKeyFrame(toValue, toKeyTime));
            Storyboard.SetTarget(keyFrames, _partScrollPanel);
            Storyboard.SetTargetProperty(keyFrames, new PropertyPath(Canvas.LeftProperty));
            keyFrames.Freeze();
            Storyboard storyboard = new Storyboard
                                 {
                                     RepeatBehavior = RepeatBehavior.Forever
                                 };
            storyboard.Children.Add(keyFrames);
            storyboard.Freeze();
            storyboard.Begin();
        }

        private void OnTextChanged(string newValue)
        {
            InitializePosition();
        }

        private void OnVelocityChanged(MarqueeVelocity newValue)
        {
            switch (newValue)
            {
                case MarqueeVelocity.Slow:
                    _velocityValue = 20;
                    break;
                case MarqueeVelocity.Normal:
                    _velocityValue = 50;
                    break;
                case MarqueeVelocity.Fast:
                    _velocityValue = 100;
                    break;
            }

            InitializePosition();
        }

        private void PartText_OnSizeChanged(object sender, SizeChangedEventArgs sizeChangedEventArgs)
        {
            InitializePosition();
        }

        private static void TextPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as MarqueeText)?.OnTextChanged((string)e.NewValue);
        }

        private static void VelocityPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as MarqueeText)?.OnVelocityChanged((MarqueeVelocity)e.NewValue);
        }

        #endregion
    }
}