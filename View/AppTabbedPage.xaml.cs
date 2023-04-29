using MauiMVVM.Model;
using MauiMVVM.Service;
using MauiMVVM.ViewModel;

namespace MauiMVVM.View;

public partial class AppTabbedPage : TabbedPage
{
    public AppTabbedPage()
    {
        
        InitializeComponent();
        DataItemService dataItemService = new DataItemService();
        var vm = new DataItemListViewModel()
        {
            DataItemService = dataItemService
        };
        NavigationPage navigationPage = new NavigationPage(new DataItemListPage(1, vm));
        navigationPage.IconImageSource = "dotnet_bot.png";
       // navigationPage.Title = "Schedule";

        NavigationPage navigationPage2 = new NavigationPage(new DataItemListPage(2, vm));
        //navigationPage2.IconImageSource = "dotnet_bot.png";
        navigationPage2.Title = "Schedule2";

        Children.Add(navigationPage);
        Children.Add(navigationPage2);
    }
}