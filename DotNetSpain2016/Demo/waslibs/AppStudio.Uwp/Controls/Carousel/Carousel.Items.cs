﻿using System;
using System.Linq;
using System.Collections;
using System.Collections.Specialized;

using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Controls;

namespace AppStudio.Uwp.Controls
{
    partial class Carousel
    {
        #region ItemsSource
        public object ItemsSource
        {
            get { return (object)GetValue(ItemsSourceProperty); }
            set { SetValue(ItemsSourceProperty, value); }
        }

        private static void ItemsSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (!(e.NewValue is IEnumerable))
            {
                throw new ArgumentException("ItemsSource");
            }

            var control = d as Carousel;

            control.DetachNotificationEvents(e.OldValue as INotifyCollectionChanged);
            control.AttachNotificationEvents(e.NewValue as INotifyCollectionChanged);

            control.ItemsSourceChanged(e.NewValue as IEnumerable);
        }

        private void AttachNotificationEvents(INotifyCollectionChanged notifyCollection)
        {
            if (notifyCollection != null)
            {
                notifyCollection.CollectionChanged += OnCollectionChanged;
            }
        }

        private void DetachNotificationEvents(INotifyCollectionChanged notifyCollection)
        {
            if (notifyCollection != null)
            {
                notifyCollection.CollectionChanged -= OnCollectionChanged;
            }
        }

        public static readonly DependencyProperty ItemsSourceProperty = DependencyProperty.Register("ItemsSource", typeof(object), typeof(Carousel), new PropertyMetadata(null, ItemsSourceChanged));
        #endregion

        private void ItemsSourceChanged(IEnumerable items)
        {
            if (_container != null)
            {
                int index = -1;
                ClearChildren();
                if (items != null)
                {
                    foreach (var item in items)
                    {
                        AddItem(item);
                        index = 0;
                    }
                }
                this.SelectedIndex = index;
                this.ArrangeItems();
            }
        }

        private void OnCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (_container != null)
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Reset:
                        ClearChildren();
                        break;
                    case NotifyCollectionChangedAction.Add:
                        int index = e.NewStartingIndex;
                        foreach (var item in e.NewItems)
                        {
                            AddItem(item, index++);
                        }
                        break;
                    case NotifyCollectionChangedAction.Remove:
                        foreach (var item in e.OldItems)
                        {
                            RemoveItem(item);
                        }
                        break;
                    case NotifyCollectionChangedAction.Replace:
                    case NotifyCollectionChangedAction.Move:
                    default:
                        break;
                }
                this.ArrangeItems();
            }
        }

        private void ClearChildren()
        {
            this.SelectedIndex = -1;
            _items.Clear();
        }

        private void AddItem(object item, int index = -1)
        {
            index = index < 0 ? _items.Count : index;
            _items.Insert(index, item);
            this.SelectedIndex = Math.Max(0, this.SelectedIndex);
        }

        private void RemoveItem(object item)
        {
            _items.Remove(item);
            this.SelectedIndex = Math.Min(_items.Count - 1, this.SelectedIndex);
        }

        private void ArrangeItems()
        {
            if (_container != null)
            {
                var slots = _container.Children.Cast<ContentControl>().OrderBy(r => r.GetTranslateX()).ToArray();
                for (int n = 0; n < slots.Length; n++)
                {
                    if (_items.Count > 0)
                    {
                        int index = GetItemIndex(n);
                        var item = _items[index];
                        if (slots[n].Content != item)
                        {
                            slots[n].Content = null;
                            slots[n].Content = item;
                        }
                    }
                    else
                    {
                        slots[n].Content = null;
                    }
                }
            }
        }

        private int GetItemIndex(int n)
        {
            int index = (this.SelectedIndex + n - 1);
            return index.Mod(_items.Count);
        }
    }
}
