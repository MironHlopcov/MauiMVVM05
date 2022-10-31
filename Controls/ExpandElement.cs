
using CommunityToolkit.Maui.Converters;
using CommunityToolkit.Maui.Markup;
using CommunityToolkit.Maui.Markup.LeftToRight;
using MauiMVVM.ViewModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using static CommunityToolkit.Maui.Markup.GridRowsColumns;

namespace MauiMVVM.Controls
{
    internal class ExpandElement : ContentView
    {

        GroupItem groupItem = new();

        #region GroupExpandItem
        public static readonly BindableProperty GroupExpandItemProperty = BindableProperty.Create(nameof(GroupExpandItem),
          typeof(GroupItem),
          typeof(ExpandElement),
          defaultValue: new GroupItem(),
          defaultBindingMode: BindingMode.OneWay,
          propertyChanged: FiltredGroupExpandItemPropertyChanged);
        private static void FiltredGroupExpandItemPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue == null)
                return;
            var control = (ExpandElement)bindable;
            control.groupItem = (GroupItem)newValue;
            control.Initialize();

        }

        public GroupItem GroupExpandItem
        {
            get
            {
                return (GroupItem)base.GetValue(GroupExpandItemProperty);
            }

            set
            {
                base.SetValue(GroupExpandItemProperty, value);
            }
        }
        #endregion

        public ExpandElement()
        {
        }

        private void Initialize()
        {
            groupItem = GroupExpandItem;
            var groupNameView = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.Fill,
                Children =
                {
                    new HorizontalStackLayout
                    {
                        Children=
                        {
                              new CheckBox{ BindingContext = groupItem}
                             .Bind(nameof(Item.Value))
                             .Invoke(checbox=>checbox.CheckedChanged+=Checbox_NameChanged),
                              new Label{BindingContext = groupItem,
                                  VerticalOptions = LayoutOptions.Center}
                             .Bind(nameof(Item.Key)),
                        }
                    },
                }
            };
            var itemListView = new CollectionView
            {
                ItemsSource = groupItem.Items,
                ItemTemplate = new DataTemplate(() =>
                {
                    HorizontalStackLayout views = new HorizontalStackLayout
                    {
                        Padding = new Thickness(20, 0, 0, 0),
                        Children =
                        {
                           new CheckBox().Bind(nameof(Item.Value))
                           .Invoke(checbox=>checbox.CheckedChanged+=Checbox_ValueChanged),
                           new Label{ VerticalOptions = LayoutOptions.Center}
                           .Bind(nameof(Item.Key))
                        }
                    };
                    return views;
                })
            };

            var view = new Grid
            {
                ColumnDefinitions = Columns.Define(Star, Auto),
                RowDefinitions = Rows.Define(Auto, Auto),
                BindingContext = groupItem,
                Children =
                {
                   groupNameView
                   .Row(0)
                   .TapGesture(()=>{ groupItem.IsExpanded=!groupItem.IsExpanded; }),
                   itemListView
                        .Row(1)
                        .ColumnSpan(2)
                        .Bind(Grid.IsVisibleProperty, ".IsExpanded"),
                   new HorizontalStackLayout
                   {
                       Children=
                       {
                            new Image{Source = "expand_more.png", HorizontalOptions=LayoutOptions.End}
                            .Size(15,25)
                            .Bind(Grid.IsVisibleProperty,
                            nameof(Item.IsExpanded),
                            converter: new InvertedBoolConverter()),


                            new Image{Source = "expand_less.png", HorizontalOptions=LayoutOptions.End}
                            .Size(15,25)
                            .Bind(Grid.IsVisibleProperty, nameof(Item.IsExpanded))
                       }
                   }.Column(1)
                    .Margin(new Thickness(0,0,20,0))
                    .TapGesture(()=>{
                        groupItem.IsExpanded=!groupItem.IsExpanded;
                    })
                }
            };
            Content = view;
            void Checbox_NameChanged(object sender, CheckedChangedEventArgs e)
            {
                if (e.Value == true)
                {
                    if (!groupItem.Items.Any(x => x.Value == true))
                        groupItem.Value = false;
                    return;
                }
                if (e.Value == false)
                    foreach (var v in groupItem.Items)
                        v.Value = false;
            }
            void Checbox_ValueChanged(object sender, CheckedChangedEventArgs e)
            {
                if (e.Value == true)
                {
                    groupItem.Value = true;
                    return;
                }
                if (!groupItem.Items.Any(x => x.Value == true))
                {
                    groupItem.Value = false;
                    return;
                }
            }
        }

        public void ClearExpandable()
        {
            groupItem.Value = false;
            foreach (var item in groupItem.Items)
            {
                item.Value = false;
            }
        }
    }
}