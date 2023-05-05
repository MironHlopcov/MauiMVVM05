using MauiMVVM.Model;
using MauiMVVM.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiMVVM.Controls
{
    public class LongPressedItem : Border, INotifyPropertyChanged
    {
        public static readonly BindableProperty ClickProperty = BindableProperty.Create(nameof(Click), typeof(ICommand), typeof(LongPressedItem));
        public static readonly BindableProperty SelectProperty = BindableProperty.Create(nameof(Select), typeof(ICommand), typeof(LongPressedItem));

        public static readonly BindableProperty ClickParameterProperty =
            BindableProperty.Create(nameof(ClickParameter), typeof(object), typeof(LongPressedItem));

        public ICommand Click
        {
            get => (ICommand)GetValue(ClickProperty);
            set => SetValue(ClickProperty, value);
        }
        public ICommand Select
        {
            get => (ICommand)GetValue(SelectProperty);
            set
            {
                SetValue(SelectProperty, value);
            }
        }

        public object ClickParameter
        {
            get => GetValue(ClickParameterProperty);
            set => SetValue(ClickParameterProperty, value);
        }

        public LongPressedItem()
        {
           
        }

      
    }
}
