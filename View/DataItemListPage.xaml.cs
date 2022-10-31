using MauiMVVM.ViewModel;
using MauiMVVM.Service;
using Microsoft.Maui.Controls;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;
using MauiMVVM.Controls;

namespace MauiMVVM;

public partial class DataItemListPage : ContentPage
{
    public DataItemListPage()
    {
        DataItemService dataItemService = new DataItemService();
        InitializeComponent();
        BindingContext = new DataItemListViewModel()
        {
            Navigation = this.Navigation,
            DataItemService = dataItemService
        };
        FilterPanelElement.SetFilter.Clicked += Filter_Clicked;
        FilterPanelElement.ClearFilter.Clicked += Filter_Clicked;
    }

    private void Filter_Clicked(object sender, EventArgs e)
    {
#if __MOBILE__
        ShowModalFilterDialog();
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
}

