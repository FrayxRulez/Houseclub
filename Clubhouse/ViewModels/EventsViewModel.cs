using Clubhouse.Models;
using Clubhouse.Navigation;
using Clubhouse.Services;
using Clubhouse.Services.Methods;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.UI.Xaml.Data;

namespace Clubhouse.ViewModels
{
    public class EventsViewModel : ViewModelBase
    {
        public EventsViewModel(IDataService dataService)
            : base(dataService)
        {
            Items = new ItemCollection(this);
        }

        public ItemCollection Items { get; private set; }

        public class ItemCollection : ObservableCollection<Event>, ISupportIncrementalLoading
        {
            private readonly EventsViewModel _viewModel;

            private int _page = 1;
            private bool _hasMoreItems = true;

            public ItemCollection(EventsViewModel viewModel)
            {
                _viewModel = viewModel;
            }

            public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
            {
                return AsyncInfo.Run(async token =>
                {
                    var count = 0u;

                    var response = await _viewModel.DataService.SendAsync(new GetEvents(25, _page));
                    if (response.Success)
                    {
                        foreach (var item in response.Events)
                        {
                            Add(item);
                            count++;
                        }

                        _page = response.Next ?? 1;
                        _hasMoreItems = response.Next.HasValue;
                    }
                    else
                    {
                        _hasMoreItems = false;
                    }

                    return new LoadMoreItemsResult { Count = count };
                });
            }

            public bool HasMoreItems => _hasMoreItems;
        }
    }
}
