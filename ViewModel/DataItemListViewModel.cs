using MauiMVVM.Controls;
using MauiMVVM.Model;
using MauiMVVM.Resources.Constants;
using MauiMVVM.Service;
using MauiMVVM.View;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace MauiMVVM.ViewModel
{
    public class DataItemListViewModel : BaseViewModel
    {

        public event EventHandler SelectionStateChenged;
        protected virtual void OnSelectionStateChenged(EventArgs e) => SelectionStateChenged?.Invoke(this, e);

        public DataItemService DataItemService { get; set; }
        private List<DataItemViewModel> dataItems { get; set; } = new();

        public ObservableCollection<DataItemViewModel> DataItems1 { get; set; }
        public ObservableCollection<DataItemViewModel> DataItems2 { get; set; }

        //ObservableCollection<object> selectedDataItems;

        public ObservableCollection<object> SelectedDataItems { get; set; }
        //{
        //    get
        //    {
        //        return selectedDataItems;
        //    }
        //    set
        //    {
        //        if (selectedDataItems != value)
        //        {

        //            selectedDataItems = value;
        //        }

        //    }
        //}



        public INavigation Navigation { get; set; }
        public Command GetDataItemsComand { get; }
        public Command GetDataItemsDetailPageComand { get; }
        public Command SelectItemsComand { get; }

        private bool isRefreshing { get; set; }
        public bool IsRefreshing
        {
            get => isRefreshing;
            set
            {
                if (isRefreshing == value)
                    return;
                isRefreshing = value;
                OnPropertyChanged(); //если текст не меняется из кода применять нет смысла
            }
        }

        private string refreshButtonText { get; set; }
        public string RefreshButtonText
        {
            get => refreshButtonText;
            set
            {
                if (refreshButtonText == value)
                    return;
                refreshButtonText = value;
                OnPropertyChanged(); //если текст не меняется из кода применять нет смысла
            }
        }

        DataItemViewModel selectedDataItem;
        public DataItemViewModel SelectedDataItem
        {
            get => selectedDataItem;
            set
            {
                if (selectedDataItem == value)
                    return;
                selectedDataItem = value;
            }
        }


        public DataItemListViewModel()
        {

            DataItems1 = new ObservableCollection<DataItemViewModel>();
            DataItems2 = new ObservableCollection<DataItemViewModel>();
            SelectedDataItems = new();
            SelectedDataItems.CollectionChanged += SelectedDataItems_CollectionChanged;
            GetDataItemsComand = new Command(async () => await GetDataItemAsync());
            SearchDataItemsComand = new Command(SearchDataItems);
            FilterDataItemsComand = new Command(GetFilterResult);
            CleanFilterDataItemsComand = new Command(CleanFilter);
            SelectItemsComand = new Command(SelectItems);
            GetDataItemsDetailPageComand = new Command(GetDataItemsDetailPage);
            RefreshButtonText = Constants.GetDatasButton;
        }

        private void SelectedDataItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (DataItemViewModel it in e.NewItems)
                        it.IsSelected = true;
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (DataItemViewModel it in e.OldItems)
                        it.IsSelected = false;
                    break;
                case NotifyCollectionChangedAction.Reset:
                    foreach (DataItemViewModel it in dataItems.Where(x => x.IsSelected))
                        it.IsSelected = false;
                    break;
            }
        }

        async void GetDataItemsDetailPage(object obj)
        {
            if (SelectedDataItems.Count > 0)
            {
                SelectItems(obj);
            }
            else
            {
                var selectItemVm = obj as DataItemViewModel;
                if (selectItemVm != null)
                {
                    var modalPage = new NavigationPage(new ContentPage());
                    var detalpage = new DataItemDetailPage(selectItemVm);
                    detalpage.NavigatingFrom += (o, s) =>
                    {
                        Navigation.PopModalAsync();
                    };
                    await Navigation.PushModalAsync(modalPage);
                    selectItemVm.SetChips();
                    await modalPage.PushAsync(detalpage);

                }
            }
        }
        async void SelectItems(object obj)
        {
            var selectItemVm = obj as DataItemViewModel;
            if (selectItemVm != null)
            {
                if (SelectedDataItems.Contains(selectItemVm))
                    SelectedDataItems.Remove(selectItemVm);
                else
                    SelectedDataItems.Add(selectItemVm);
            }
        }

        async Task GetDataItemAsync()
        {
            IsRefreshing = true;
            if (IsBusy)
                return;
            try
            {
                IsBusy = true;
                if (dataItems.Count != 0)
                    dataItems.Clear();
                var dataItemsFromDb = await DataItemService.GetDataItems();
                //for (int i = 0; i < 10; i++)
                //{
                foreach (var data in dataItemsFromDb)
                {
                    dataItems.Add(new DataItemViewModel(data)
                    {
                        DataItemListViewModel = this
                    });
                }
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to get DataItems: {ex.Message}");
                await Application.Current.MainPage.DisplayAlert("Error", $"Unable to get DataItems: {ex.Message}", "Ok");
            }
            finally
            {
                ClearDataItelsLists();
                foreach (var data in dataItems)
                {
                    AddToDataItelsLists(data);
                    RefreshButtonText = Constants.RefreshButton;
                }
                IsBusy = false;
                IsRefreshing = false;
                InitializeFilter();
            }
        }

        private void AddToDataItelsLists(DataItemViewModel data)
        {
            if (data.Name.StartsWith("M"))
                DataItems1.Add(data);
            else
                DataItems2.Add(data);



        }
        private void ClearDataItelsLists()
        {
            DataItems1.Clear();
            DataItems2.Clear();
        }

        string searchText;
        public string SearchText
        {
            get => searchText;
            set
            {
                if (searchText == value)
                    return;
                searchText = value;
                if (string.IsNullOrWhiteSpace(searchText))
                    SearchDataItems();
                OnPropertyChanged(); //если текст не меняется из кода применять нет смысла
            }
        }

        DateTime searchDates;
        public DateTime SearchDates
        {
            get => searchDates;
            set
            {
                if (searchDates != value)
                {
                    searchDates = value;
                    OnPropertyChanged();//если текст не меняется из кода применять нет смысла
                }
            }
        }
        DateTime searchDates2;
        public DateTime SearchDates2
        {
            get => searchDates2;
            set
            {
                if (searchDates2 != value)
                {
                    searchDates2 = value;
                    OnPropertyChanged();//если текст не меняется из кода применять нет смысла
                }
            }
        }

        private List<GroupItem> filtredFilds = new();
        public List<GroupItem> FiltredFilds
        {
            get => filtredFilds;
            set
            {
                if (filtredFilds == value)
                    return;
                filtredFilds = value;
                OnPropertyChanged(); //если текст не меняется из кода применять нет смысла
            }
        }

        public Command SearchDataItemsComand { get; }
        public Command FilterDataItemsComand { get; }
        public Command CleanFilterDataItemsComand { get; }
        void SearchDataItems()
        {
            //if (string.IsNullOrEmpty(searchText))
            //{
            //    DataItems.Clear();
            //    dataItems.ForEach(d => DataItems.Add(d));
            //}
            //else
            //{
            //    DataItems.Clear();
            //    var result = dataItems.Where(d => d.Name.Contains(searchText)).ToList();
            //    result.ForEach(d => DataItems.Add(d));
            //}
            GetFilterResult();
        }
        private void GetFilterResult()
        {
            List<DataItemViewModel> result = new();
            result.AddRange(dataItems);
            if (!string.IsNullOrEmpty(searchText))
            {
                result = result.Where(d => d.Name.ToLower().Contains(searchText)).ToList();
            }
            if (SearchDates2 > new DateTime(1900, 1, 1))
            {
                if (SearchDates > new DateTime(1900, 1, 1))
                {
                    result = result.Where(d => d.DateTime.Date >= SearchDates.Date).ToList();
                    result = result.Where(d => d.DateTime.Date <= SearchDates2.Date).ToList();
                }
                else
                    result = result.Where(d => d.DateTime.Date == SearchDates2.Date).ToList();
            }
            else
            {
                if (SearchDates > new DateTime(1900, 1, 1))
                {
                    result = result.Where(d => d.DateTime.Date == SearchDates.Date).ToList();
                }
            }

            foreach (var exoFilterItem in filtredFilds)
            {
                if (exoFilterItem.Key == "Name")
                {
                    var selectedValues = exoFilterItem.Items.Where(x => x.Value == true).Select(x => x.Key).ToList();
                    if (selectedValues.Count != 0)
                        result = result.Where(d => selectedValues.Contains(d.Name)).ToList();
                }
            }
            ClearDataItelsLists();
            result.ForEach(d => AddToDataItelsLists(d));
        }
        private void CleanFilter()
        {
            ClearDataItelsLists();
            SearchText = "";
            filtredFilds.ForEach(x => x.Value = false);
            SearchDates = DateTime.MinValue;
            SearchDates2 = DateTime.MinValue;
            dataItems.ForEach(d => AddToDataItelsLists(d));
        }
        void InitializeFilter()
        {
            var names = new List<Item>();
            dataItems.GroupBy(x => x.Name).Select(x => x.First()).ToList()
                .ForEach(x => names.Add(new Item { Key = x.Name }));

            var names2 = new List<Item>();
            dataItems.GroupBy(x => x.Name).Select(x => x.First()).ToList()
                .ForEach(x => names2.Add(new Item { Key = x.Name }));

            var dFiltredFilds = new List<GroupItem>();
            dFiltredFilds.Add(new() { Key = "Name", Items = names.ToArray() });
            FiltredFilds = dFiltredFilds;
        }

    }
}
