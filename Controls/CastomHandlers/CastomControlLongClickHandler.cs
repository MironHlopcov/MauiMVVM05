using Microsoft.Maui.Platform;
#if WINDOWS
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
            Microsoft.Maui.Handlers.TabbedViewHandler.Mapper.AppendToMapping(nameof(CastomControlLongClickHandler), (handler, view) =>
            {
                if (view is MyEmpryDataPiker)
                {
#if ANDROID
                    var datePiker = (MyEmpryDataPiker)view;
                    var dateTextBox = handler.PlatformView as Android.Widget.TextView;
                    dateTextBox.Background = null; //to delete underline

                    datePiker.Loaded += (s, e) =>
                    {
                        if(datePiker.MyDatePieker.DateValue<= new DateTime(1900, 1, 1))
                            dateTextBox.Text = "Choose date";
                        else
                            dateTextBox.Text = datePiker.MyDatePieker.DateValue.ToShortDateString();
                    };
                    datePiker.MyDatePieker.PropertyChanged += (s, e) =>
                    {
                        if (e.PropertyName == nameof(MyDatePieker.DateValue))
                        {
                            if ((s as MyDatePieker).DateValue <= new DateTime(1900, 1, 1))
                                dateTextBox.Text = "Choose date";
                            else
                                dateTextBox.Text = (s as MyDatePieker).DateValue.ToShortDateString();
                        }
                    };
                    dateTextBox.Click += (s, e) =>
                    {
                        MauiDatePicker mauiDatePicker = (MauiDatePicker)s;
                        mauiDatePicker.ShowPicker.Invoke();
                        if (datePiker.MyDatePieker.DateValue <= new DateTime(1900, 1, 1))
                        {
                            datePiker.MyDatePieker.DateValue = datePiker.MyDatePieker.LostSelectDate;
                            dateTextBox.Text = datePiker.MyDatePieker.LostSelectDate.ToShortDateString();
                        }
                    };
#endif

#if WINDOWS
                    var datePiker = (MyEmpryDataPiker)view;
                    var dateTextBox = handler.PlatformView as CalendarDatePicker;

                    dateTextBox.BorderThickness= new Microsoft.UI.Xaml.Thickness(0);
                    datePiker.BackgroundColor = Colors.Transparent;

                    datePiker.Loaded += (s, e) =>
                    {
                        dateTextBox.Date = null;
                    };
                    datePiker.MyDatePieker.PropertyChanged += (s, e) =>
                    {
                        if (e.PropertyName == nameof(MyDatePieker.DateValue))
                        {
                            if ((s as MyDatePieker).DateValue <= new DateTime(1900, 1, 1))
                                dateTextBox.Date = null;
                            else
                                dateTextBox.Date = (s as MyDatePieker).DateValue;
                        }
                    };
                    dateTextBox.Closed += (s, e) =>
                    {
                        if (dateTextBox.Date == DateTime.Now.Date)
                            datePiker.MyDatePieker.DateValue = DateTime.Now;
                    };
#endif
                }
            });
        }


    }
}
