using MauiMVVM.ViewModel;
using MauiMVVM.Service;
using Microsoft.Maui.Controls;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;
using MauiMVVM.Controls;
using Microsoft.Maui.Devices;
using System.Globalization;
using System.Collections.Specialized;
using System.Collections;

namespace MauiMVVM;

public partial class DataItemListPage : ContentPage
{
    ToolbarItem defaultToolbarItem;
    ToolbarItem selectToolbarItem;

    public DataItemListPage(int index, DataItemListViewModel vm)
    {
        InitializeComponent();
        defaultToolbarItem = new ToolbarItem()
        {
            IconImageSource = "filter_list.png",
            Text = "Filter",
        };
        defaultToolbarItem.Clicked += Filter_Clicked;

        selectToolbarItem = new ToolbarItem()
        {
            IconImageSource = "close.png",
            Text = "Select",
        };
        selectToolbarItem.Clicked += (s,e)=>
        {
            vm.SelectedDataItems.Clear();
        };


        vm.Navigation = this.Navigation;
        BindingContext = vm;
        if (index == 1)
            CollectionView.ItemsSource = vm.DataItems1;
        if (index == 2)
            CollectionView.ItemsSource = vm.DataItems2;
        FilterPanelElement.SetFilter.Clicked += Filter_Clicked;
        FilterPanelElement.ClearFilter.Clicked += Filter_Clicked;
        vm.SelectedDataItems.CollectionChanged += SelectedDataItems_CollectionChanged;
    }

    private void SelectedDataItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
    {
        if ((sender as IList).Count > 0)
        {
            RootConntentPage.ToolbarItems.Clear();
            RootConntentPage.ToolbarItems.Add(selectToolbarItem);
        }
        else
        {
            RootConntentPage.ToolbarItems.Clear();
            RootConntentPage.ToolbarItems.Add(defaultToolbarItem);
        }

    }

    private void Filter_Clicked(object sender, EventArgs e)
    {
#if __MOBILE__
        ShowAndHidenFilterPanel();
        return;
#endif
        ShowAndHidenFilterPanel();

    }
    private void ShowAndHidenFilterPanel()
    {
        FilterScrean.IsVisible = !FilterScrean.IsVisible;
        if (FilterScrean.IsVisible)
        {
            if (DeviceInfo.Current.Idiom == DeviceIdiom.Phone)
            {
                GridScrean.ColumnDefinitions = Columns.Define(Stars(0), Stars(1));
                ListScrean.IsVisible = false;
            }

            if (DeviceInfo.Current.Idiom != DeviceIdiom.Phone)
            {
                GridScrean.ColumnDefinitions = Columns.Define(Stars(75), Stars(25));
            }
        }
        else
        {
            GridScrean.ColumnDefinitions = Columns.Define(Stars(1), Stars(0));
            ListScrean.IsVisible = true;
        }
    }
    private void ShowModalFilterDialog()
    {
        var filterPage = new CastomModalPage(GridScrean);
        Navigation.PushAsync(filterPage);
    }

  
    //    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    //    {
    //#if WINDOWS
    //        var colView = (CollectionView)sender;
    //            if (colView.SelectedItems.Count > 0)
    //            {
    //                colView.SelectionMode = SelectionMode.Multiple;
    //            }
    //            else
    //            {
    //                if (e.PreviousSelection.Count == 0 && colView.SelectedItems.Count == 0)
    //                {
    //                    colView.SelectionMode = SelectionMode.None;
    //                    return;
    //                }
    //                //if ( e.CurrentSelection.Count > 0)
    //                //{
    //                //    colView.SelectionMode = SelectionMode.Multiple;
    //                //    return;
    //                //}
    //                colView.SelectionMode = SelectionMode.None;
    //            }
    //#endif

    //    }

}

