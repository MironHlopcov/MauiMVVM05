using CommunityToolkit.Maui.Markup;
using MauiMVVM.ViewModel;
using Microsoft.Maui.Platform;

#if WINDOWS
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MauiMVVM.Controls
{
    public class CastomControlLongClickHandler
    {
        public CastomControlLongClickHandler()
        {
            Microsoft.Maui.Handlers.BorderHandler.Mapper.AppendToMapping(nameof(CastomControlLongClickHandler), (handler, view) =>
            {

                if (view is LongPressedItem)
                {
#if WINDOWS
                    var uiElement = view as LongPressedItem;
                    var reghtTapGestureRecognizer = new TapGestureRecognizer();
                    reghtTapGestureRecognizer.Buttons = ButtonsMask.Secondary;
                    reghtTapGestureRecognizer.Tapped += (s, e) =>
                    {
                        var vw = view as LongPressedItem;
                        if (vw.Select.CanExecute(null))
                        {
                            vw.Select.Execute(vw.ClickParameter);
                        }
                    };
                    var leftTapGestureRecognizer = new TapGestureRecognizer();
                    leftTapGestureRecognizer.Buttons = ButtonsMask.Primary;
                    leftTapGestureRecognizer.Tapped += (s, e) =>
                    {
                        var vw = view as LongPressedItem;
                        if (vw.Click.CanExecute(null))
                        {
                            vw.Click.Execute(vw.ClickParameter);
                        }
                    };
                    uiElement.GestureRecognizers.Add(reghtTapGestureRecognizer);
                    uiElement.GestureRecognizers.Add(leftTapGestureRecognizer);

                    //////////////////var uiElement = view as LongPressedItem;
                    //////////////////var reghtTapGestureRecognizer = new TapGestureRecognizer();
                    //////////////////reghtTapGestureRecognizer.Buttons = ButtonsMask.Secondary;
                    //////////////////reghtTapGestureRecognizer.Tapped += (s, e) =>
                    //////////////////{
                    //////////////////    //Microsoft.Maui.Controls.VisualStateManager.GoToState(uiElement, "Checked");

                    //////////////////    var vw = view as LongPressedItem;
                    //////////////////    if (vw.Select.CanExecute(null))
                    //////////////////    {
                    //////////////////        vw.Select.Execute(vw.ClickParameter);
                    //////////////////    }
                    //////////////////};
                    //////////////////var leftTapGestureRecognizer = new TapGestureRecognizer();
                    //////////////////leftTapGestureRecognizer.Buttons = ButtonsMask.Primary;
                    //////////////////leftTapGestureRecognizer.Tapped += (s, e) =>
                    //////////////////{
                    //////////////////    //var vw = view as LongPressedItem;
                    //////////////////    //if (vw.Click.CanExecute(null))
                    //////////////////    //{
                    //////////////////    //    vw.Click.Execute(vw.ClickParameter);
                    //////////////////    //}
                    //////////////////};
                    //////////////////uiElement.GestureRecognizers.Add(reghtTapGestureRecognizer);
                    //////////////////uiElement.GestureRecognizers.Add(leftTapGestureRecognizer);


                    //var uiElement = handler.PlatformView as UIElement;
                    ////uiElement.Tapped += (s, e) =>
                    ////{
                    ////    //var vw = view as LongPressedItem;
                    ////    //if (vw.Select.CanExecute(null))
                    ////    //{
                    ////    //    vw.Select.Execute(vw.ClickParameter);
                    ////    //}
                    ////};
                    ////uiElement.DoubleTapped += (s, e) =>
                    ////{
                    ////    var vw = view as LongPressedItem;
                    ////    if (vw.Click.CanExecute(null))
                    ////    {
                    ////        vw.Click.Execute(vw.ClickParameter);
                    ////    }
                    ////};


#endif
#if ANDROID
                   handler.PlatformView.LongClick += (s,e)=>
                   {
                       var vw = view as LongPressedItem;
                       if (vw.Select.CanExecute(null))
                       {
                           vw.Select.Execute(vw.ClickParameter);
                       }
                   };
                   handler.PlatformView.Click += (s,e)=>
                   {
                       var vw = view as LongPressedItem;
                       if (vw.Click.CanExecute(null))
                       {
                           vw.Click.Execute(vw.ClickParameter);
                       }
                   };
#endif
#if IOS
  
			//handler.PlatformView.UserInteractionEnabled = true;  
			//handler.PlatformView.AddGestureRecognizer(new UILongPressGestureRecognizer(HandleLongClick));  
#endif

                }
            });




        }

      
//#if WINDOWS
//    private void PlatformView_Holding(object sender, Microsoft.UI.Xaml.Input.HoldingRoutedEventArgs e)  
//	{  
//        //Touch can produce a Holding action, but mouse devices generally can't.  
//        //see https://learn.microsoft.com/en-us/uwp/api/windows.ui.xaml.uielement.holding?view=winrt-22621  
//    }  
//#endif
//#if IOS
  //  private void HandleLongClick(UILongPressGestureRecognizer sender)  
  //  {  
		////do something  
  //  }  
//#endif
//#if ANDROID
//        private void PlatformView_LongClick(object sender, Android.Views.View.LongClickEventArgs e)
//        {
//            // do something  
//        }
//        private void PlatformView_Click(object sender, EventArgs e)
//        {
//           // throw new NotImplementedException();
//        }
//#endif


    }
}
