namespace MauiMVVM.Controls;

public partial class CastomModalPage : ContentPage
{
    private Microsoft.Maui.Controls.View views;
    public CastomModalPage(Microsoft.Maui.Controls.View view)
	{
	    views= view;
        CastomModalPageGrid = (Grid)views;
        InitializeComponent();
    }
}