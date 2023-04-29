using MauiMVVM.Controls;
using MauiMVVM.Model;
using Microsoft.Maui.Controls.Shapes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiMVVM.ViewModel
{
    public class DataItemViewModel : BaseViewModel
    {
        public DataItem DataItem { get; private set; }
        public DataItemViewModel()
        {
            DataItem = new DataItem();
        }
        public DataItemViewModel(DataItem item)
        {
            this.DataItem = item;
        }

        public void SetChips()
        {
            Chips = new ObservableCollection<Chip>();
            Chips.CollectionChanged += (s, o) =>
            {

            };
            foreach (var st in DataItem.Name.ToCharArray())
            {
                var chip = new Chip()
                {
                    Text = $"{st}",
                    StrokeShape = new RoundRectangle { CornerRadius = 5 },
                    BackgroundColor = Color.FromArgb("#e1e1e1"),
                    Stroke = Colors.DarkGrey,
                    StrokeThickness = 0.5,
                    Padding = 5,
                };
                chip.Checkd += (s, e) =>
                {
                    Name = DataItem.Name.Replace(chip.Text, null);
                };
                //chip.CloseButtonClicked += (s, e) => 
                //{
                //    Chips.Remove(chip);
                //    OnPropertyChanged(nameof(Chip));
                //};
                Chips.Add(chip);
            }
        }

        private DataItemListViewModel dataItemListViewModel;
        public DataItemListViewModel DataItemListViewModel
        {
            get => dataItemListViewModel;
            set
            {
                if (dataItemListViewModel != value)
                {
                    dataItemListViewModel = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Name
        {
            get => DataItem.Name;
            set
            {
                if (DataItem.Name != value)
                {
                    DataItem.Name = value;
                    OnPropertyChanged();
                }
            }
        }
        public DateTime DateTime
        {
            get => DataItem.DateTime;
            set
            {
                if (DataItem.DateTime == value)
                    return;
                DataItem.DateTime = value;
                OnPropertyChanged();

            }
        }
        public string Image
        {
            get => DataItem.Image;
            set
            {
                if (DataItem.Image == value)
                    return;
                DataItem.Image = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Chip> Chips { get; set; }
    }
}
