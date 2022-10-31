using CommunityToolkit.Maui.Markup;
using Microsoft.Maui.Controls;

namespace MauiMVVM.Controls
{
    public class ModalPage : ContentPage
    {
        private Microsoft.Maui.Controls.View views;
        public ModalPage(Microsoft.Maui.Controls.View view)
        {
            views = view;
            Content = views;
        }
    }
}
