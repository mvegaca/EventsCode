﻿using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Controls;
using Windows.Foundation;

namespace AppStudio.Uwp.Controls
{
    public sealed partial class Carousel : Control
    {
        private Panel _container = null;
        private Button _prevArrow = null;
        private Button _nextArrow = null;

        private RectangleGeometry _clip = null;

        private List<object> _items = null;

        public Carousel()
        {
            _items = new List<object>();
            this.DefaultStyleKey = typeof(Carousel);
        }

        protected override void OnApplyTemplate()
        {
            _container = base.GetTemplateChild("container") as Panel;
            _prevArrow = base.GetTemplateChild("prevArrow") as Button;
            _nextArrow = base.GetTemplateChild("nextArrow") as Button;

            _clip = base.GetTemplateChild("clip") as RectangleGeometry;

            this.BuildSlots();
            this.ItemsSourceChanged(this.ItemsSource as IEnumerable);

            _container.ManipulationInertiaStarting += OnManipulationInertiaStarting;
            _container.ManipulationDelta += OnManipulationDelta;
            _container.ManipulationCompleted += OnManipulationCompleted;
            _container.ManipulationMode = ManipulationModes.TranslateX | ManipulationModes.TranslateInertia | ManipulationModes.System;

            _prevArrow.Click += OnPrevArrowClick;
            _nextArrow.Click += OnNextArrowClick;

            this.SizeChanged += OnSizeChanged;

            base.OnApplyTemplate();
        }

        protected override Size MeasureOverride(Size availableSize)
        {
            double contentWidth = availableSize.Width;
            double width = contentWidth / MaxItems;
            double height = width / AspectRatio;
            if (height < MinHeight)
            {
                height = MinHeight;
                width = height * AspectRatio;
            }
            if (height > MaxHeight)
            {
                height = MaxHeight;
                width = height * AspectRatio;
            }
            var size = new Size(width, height);
            base.MeasureOverride(size);
            return size;
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            var size = base.ArrangeOverride(finalSize);

            double slotWidth = Math.Round(Math.Min(size.Width, Math.Max(_container.ActualWidth / (double)MaxItems, size.Height * AspectRatio)), 2);
            double factor = Math.Round(_slotWidth / slotWidth, 2);
            factor = factor == 0 ? 1 : factor;
            _slotWidth = Math.Round(slotWidth, 2);
            _offset = Math.Round((_offset / factor).Mod(_slotWidth), 2);

            var positions = GetPositions(_slotWidth).ToArray();
            var controls = _container.Children.Cast<CarouselSlot>().OrderBy(r => r.X).ToArray();
            for (int n = 0; n < controls.Length; n++)
            {
                var position = positions[n];
                var control = controls[n];
                control.MoveX(position.X + _offset);
                control.Width = _slotWidth;
                control.Height = _container.ActualHeight;
            }

            return size;
        }

        protected override void OnPointerEntered(PointerRoutedEventArgs e)
        {
            _prevArrow.FadeIn(500.0);
            _nextArrow.FadeIn(500.0);
            base.OnPointerEntered(e);
        }
        protected override void OnPointerExited(PointerRoutedEventArgs e)
        {
            _prevArrow.FadeOut(500.0);
            _nextArrow.FadeOut(500.0);
            base.OnPointerExited(e);
        }

        private void OnPrevArrowClick(object sender, RoutedEventArgs e)
        {
            this.AnimatePrev();
        }
        private void OnNextArrowClick(object sender, RoutedEventArgs e)
        {
            this.AnimateNext();
        }

        private void OnSizeChanged(object sender, SizeChangedEventArgs e)
        {
            _prevArrow.Height = e.NewSize.Height;
            _nextArrow.Height = e.NewSize.Height;
            _clip.Rect = new Rect(new Point(), _container.GetSize());
        }
    }
}
