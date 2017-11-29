﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;

namespace Zafiro.Designer
{
    public sealed class DesignerSurface : ItemsControl
    {
        private CompositeDisposable tapDisponsables;

        public DesignerSurface()
        {
            this.DefaultStyleKey = typeof(DesignerSurface);
            tapDisponsables = new CompositeDisposable();

            Observable
                .FromEventPattern<TappedEventHandler, TappedRoutedEventArgs>(h => Tapped += h, h => Tapped -= h)
                .Subscribe(_ => ResetAll());
        }

        private void ResetAll()
        {
            ClearSelection();
            ClearEditMode();
        }

        private void ClearEditMode()
        {
            foreach (var designerItem in Containers)
            {
                designerItem.IsEditing = false;
            }
        }

        private void ClearSelection()
        {
            if (SelectedItems == null)
            {
                return;
            }

            foreach (var selectedItem in SelectedItems)
            {
                var di = (DesignerItem)ContainerFromItem(selectedItem);
                di.IsSelected = false;
            }

            SelectedItems = GetSelectedItems();
        }

        public BindingBase LeftBinding { get; set; }
        public BindingBase TopBinding { get; set; }
        public BindingBase HeightBinding { get; set; }
        public BindingBase WidthBinding { get; set; }

        protected override DependencyObject GetContainerForItemOverride()
        {
            return new DesignerItem();
        }

        protected override bool IsItemItsOwnContainerOverride(object item)
        {
            return item is DesignerItem;
        }

        protected override void PrepareContainerForItemOverride(DependencyObject element, object item)
        {
            var di = (DesignerItem) element;

            di.SetBinding(DesignerItem.LeftProperty, LeftBinding);
            di.SetBinding(DesignerItem.TopProperty, TopBinding);
            di.SetBinding(WidthProperty, WidthBinding);
            di.SetBinding(HeightProperty, HeightBinding);

            var subscriptions = new CompositeDisposable();

            subscriptions.Add(di.SelectionRequest.Subscribe(ea =>
            {
                ea.EventArgs.Handled = true;
                di.IsSelected = true;
                SelectedItems = GetSelectedItems();
            }));

            subscriptions.Add(di.EditRequest.Subscribe(_ => di.IsEditing = true));

            base.PrepareContainerForItemOverride(element, item);
        }

        private IList<object> GetSelectedItems()
        {
            return Containers.Where(x => x.IsSelected).Select(ItemFromContainer).ToList();
        }

        private IEnumerable<DesignerItem> Containers => Items.Select(o => (DesignerItem)ContainerFromItem(o));

        public static readonly DependencyProperty SelectedItemsProperty = DependencyProperty.Register(
            "SelectedItems", typeof(ObservableCollection<object>), typeof(DesignerSurface), new PropertyMetadata(default(IEnumerable)));

        public IEnumerable SelectedItems
        {
            get { return (IEnumerable) GetValue(SelectedItemsProperty); }
            set { SetValue(SelectedItemsProperty, value); }
        }
    }
}
