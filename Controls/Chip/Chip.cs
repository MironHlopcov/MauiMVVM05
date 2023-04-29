using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Controls.Shapes;

namespace MauiMVVM.Controls;

public class Chip : Border
{
    public event EventHandler CloseButtonClicked;
    protected virtual void OnCloseButtonClicked(EventArgs e) => CloseButtonClicked?.Invoke(this, e);

    public event EventHandler Checkd;
    protected virtual void OnCheckd(EventArgs e) => Checkd?.Invoke(this, e);

    //Border border;
    ImageButton selectionDetectImage;
    Button button;
    ImageButton closeImage;


    #region Text
    public static readonly BindableProperty TextProperty =
       BindableProperty.Create(nameof(Text), typeof(string), typeof(Chip), "Text",
           defaultBindingMode: BindingMode.OneWay, propertyChanged: TextPropertyChanged);
    private static void TextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {

        (bindable as Chip).button.Text = (string)newValue;
        //(bindable as ChipsConteiner).UpdateChildrenLayout();
    }

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }
    #endregion
    #region IsChacked
    public static readonly BindableProperty IsChackedProperty =
       BindableProperty.Create(nameof(IsChacked), typeof(bool), typeof(Chip), false,
           defaultBindingMode: BindingMode.TwoWay, propertyChanged: IsChackedPropertyChanged);
    private static void IsChackedPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (newValue != oldValue)
            (bindable as Chip).ChangeSelectionState((bool)newValue);
    }

    public bool IsChacked
    {
        get
        {
            return (bool)GetValue(IsChackedProperty);
        }
        set
        {
            if (value != IsChacked)
                SetValue(IsChackedProperty, value);
        }
    }
    #endregion
    #region ShowCloseButton
    public static readonly BindableProperty ShowCloseButtonProperty =
       BindableProperty.Create(nameof(ShowCloseButton), typeof(bool), typeof(Chip), true,
           defaultBindingMode: BindingMode.OneWay, propertyChanged: ShowCloseButtonPropertyChanged);
    private static void ShowCloseButtonPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
       (bindable as Chip).closeImage.IsVisible = (bool)newValue;
    }

    public bool ShowCloseButton
    {
        get
        {
            return (bool)GetValue(ShowCloseButtonProperty);
        }
        set
        {
            SetValue(ShowCloseButtonProperty, value);
        }
    }
    #endregion

    public Chip()
    {
        var stack= new StackLayout();
        var grid = new Grid()
        {
            RowDefinitions =
            {
                new RowDefinition(),
            },
            ColumnDefinitions =
            {
                new ColumnDefinition{Width = new GridLength(1,GridUnitType.Auto) },
                new ColumnDefinition{Width = new GridLength(1,GridUnitType.Auto) },
                new ColumnDefinition{Width = new GridLength(1,GridUnitType.Auto) }
            },
            
        };
        button = new Button()
        {
            BorderColor = Colors.Transparent,
            Padding = new Thickness(5, 0, 0, 0),
            Text = Text,
           
            BackgroundColor = Colors.Transparent,
        };
        selectionDetectImage = new ImageButton()
        {
            HeightRequest = button.FontSize * 1.2,
            WidthRequest = button.FontSize * 1.2,
            IsVisible = false,
            BackgroundColor = Colors.Transparent,
            Source = "empty_icon.png",
        };
        closeImage = new ImageButton()
        {
            HeightRequest = button.FontSize * 1.2,
            WidthRequest = button.FontSize * 1.2,
            IsVisible = ShowCloseButton,
            BackgroundColor = Colors.Transparent,
            Source = "close_ic.png",
        };
        selectionDetectImage.Clicked += Button_Clicked;
        button.Clicked += Button_Clicked;
        closeImage.Clicked += CloseImage_Clicked;

        grid.Add(selectionDetectImage);
        grid.Add(button);
        grid.Add(closeImage);
        grid.SetColumn(selectionDetectImage, 0);
        grid.SetColumn(button, 1);
        grid.SetColumn(closeImage, 2);

        //border.Content = grid;
        //Content = border;
        stack.Add(grid);
        Content = stack;

        void Button_Clicked(object sender, EventArgs e)
        {
            IsChacked = !IsChacked;
            OnCheckd(e);
        }
        void CloseImage_Clicked(object sender, EventArgs e) => OnCloseButtonClicked(e);
    }
    private void ChangeSelectionState(bool isChecked)
    {
        if (isChecked)
        {
            button.Padding = new Thickness(0, 0, 5, 0);
            selectionDetectImage.IsVisible = true;
            closeImage.IsVisible = false;
            //Stroke = new SolidColorBrush(this.BackgroundColor);

            if (this.BackgroundColor!=null)
                this.BackgroundColor = this.BackgroundColor.WithAlpha(0.3F);

            //if (isChecked != IsChacked)
            //    IsChacked = true;

           
        }
        else
        {
            button.Padding = new Thickness(5, 0, 0, 0);
            selectionDetectImage.IsVisible = false;
            if(ShowCloseButton)
                closeImage.IsVisible = true;
            if (this.BackgroundColor != null)
                this.BackgroundColor = this.BackgroundColor.WithAlpha(1F);
            //if (isChecked != IsChacked)
            //    IsChacked = false;
        }
    }
}

