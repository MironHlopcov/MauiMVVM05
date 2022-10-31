using MauiMVVM.Service;
using MauiMVVM.ViewModel;
using CommunityToolkit.Maui.Markup;
using Microsoft.Maui.Controls.Platform;

namespace MauiMVVM.Controls;

public partial class FilterPanel
{
    public Dictionary<string, string[]> filtredFilds;
    Command SearchButtonCommand { get; set; }
    Command CleanButtonCommand { get; set; }
    public Button SetFilter { get; set; }
    public Button ClearFilter { get; set; }


    public FilterPanel()
	{
        InitializeComponent();
        SetFilter = SearchButton;
        ClearFilter = CleanButton;
    }
    //private bool isHiden;
    //public bool IsHiden
    //{
    //    get=> this.IsVisible;
    //    set
    //    {
    //        this.IsVisible = value;
    //    }
    //}

    //public void CleanButton_Clicked(object sender, EventArgs e)
    //{
    //    SearchBar.Text = null;
       
    //}
}