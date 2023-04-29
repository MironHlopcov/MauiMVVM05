using Microsoft.Maui.Layouts;
using System.Collections.ObjectModel;

namespace MauiMVVM.Controls;

public class ChipConteiner : ContentView
{
    FlexLayout flex;
    #region ChipList
    public static readonly BindableProperty ChipListProperty =
       BindableProperty.Create(nameof(ChipList), typeof(IEnumerable<Chip>), typeof(ChipConteiner), 
           defaultBindingMode: BindingMode.TwoWay, propertyChanged: ChipPropertyChanged);
    private static void ChipPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        (bindable as ChipConteiner).ChipList = (IList<Chip>)newValue;
        if ((bindable as ChipConteiner).ChipList != null)
        {
            foreach (var chip in (bindable as ChipConteiner).ChipList)
            {
                chip.CloseButtonClicked += (s, e) =>
                {
                    (bindable as ChipConteiner).ChipList.Remove(chip);
                    (bindable as ChipConteiner).flex.Children.Remove(chip);
                    (bindable as ChipConteiner).InvalidateLayout();
                };
                (bindable as ChipConteiner).flex.Children.Add(chip);
            }
            (bindable as ChipConteiner).InvalidateLayout();
        }
      
    }

    public IList<Chip> ChipList
    {
        get => (IList<Chip>)GetValue(ChipListProperty);
        set => SetValue(ChipListProperty, value);
    }
    #endregion

    #region Template
    //public static readonly BindableProperty TemplateProperty =
    //   BindableProperty.CreateAttached("Template", typeof(Border), typeof(ChipConteiner), new Border(),
    //       defaultBindingMode: BindingMode.TwoWay, propertyChanged: TemplatePropertyChanged);
    //private static void TemplatePropertyChanged(BindableObject bindable, object oldValue, object newValue)
    //{
    //    // (bindable as ChipConteiner).Template = (IView)newValue;
    //}
    //public static Border GetTemplate(BindableObject view)
    //{
    //    return (Border)view.GetValue(TemplateProperty);
    //}

    //public static void SetTemplate(BindableObject view, Border value)
    //{
    //    view.SetValue(TemplateProperty, value);
    //}
    ////public Chip Template
    ////{
    ////    get => (Chip)GetValue(TemplateProperty);
    ////    set => SetValue(TemplateProperty, value);
    ////}
    //private Chip chip;
    //public Chip Chip
    //{
    //    get
    //    {
    //        return chip;
    //    }
    //    set
    //    {
    //        if (value != chip)
    //            chip = value;
    //    }
    //}
    #endregion

    public ChipConteiner()
    {
        flex = new FlexLayout()
        {
            AlignContent = FlexAlignContent.Start,
            AlignItems = FlexAlignItems.Start,
            Direction = FlexDirection.Row,
            Wrap = FlexWrap.Wrap,
            Padding = 2,
        };
        Content = flex;
    }
}