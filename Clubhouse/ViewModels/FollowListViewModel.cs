using Clubhouse.Common;
using Clubhouse.Navigation;
using Clubhouse.Services;
using Clubhouse.Services.Methods;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Navigation;

namespace Clubhouse.ViewModels
{
    public enum FollowListType
    {
        Followers,
        Following,
        MutualFollows
    }

    public class FollowListViewModel : ViewModelBase
    {
        public FollowListViewModel(IDataService dataService)
            : base(dataService)
        {
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            if (parameter is IDictionary<string, object> data)
            {
                if (data.TryGet("user_id", out ulong userId) && data.TryGet("type", out FollowListType type))
                {
                    Items = new ItemCollection(this, userId, type);
                }
            }

            return base.OnNavigatedToAsync(parameter, mode, state);
        }

        private ItemCollection _items;
        public ItemCollection Items
        {
            get => _items;
            set => Set(ref _items, value);
        }

        public class ItemCollection : ObservableCollection<object>, ISupportIncrementalLoading
        {
            private readonly FollowListViewModel _viewModel;

            private readonly ulong _userId;
            private readonly FollowListType _type;

            private int _page = 1;
            private bool _hasMoreItems = true;

            public ItemCollection(FollowListViewModel viewModel, ulong userId, FollowListType type)
            {
                _viewModel = viewModel;

                _type = type;
                _userId = userId;
            }

            public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
            {
                return AsyncInfo.Run(async token =>
                {
                    var count = 0u;

#pragma warning disable CS8524 // The switch expression does not handle some values of its input type (it is not exhaustive) involving an unnamed enum value.
                    GetFollowListBase request = _type switch
#pragma warning restore CS8524 // The switch expression does not handle some values of its input type (it is not exhaustive) involving an unnamed enum value.
                    {
                        FollowListType.Followers => new GetFollowers(_userId, 20, _page),
                        FollowListType.Following => new GetFollowing(_userId, 20, _page),
                        FollowListType.MutualFollows => new GetMutualFollows(_userId, 20, _page)
                    };

                    var response = await _viewModel.DataService.SendAsync(request);
                    if (response.Success)
                    {
                        if (response.Clubs != null)
                        {
                            foreach (var item in response.Clubs)
                            {
                                Add(item);
                                count++;
                            }
                        }

                        foreach (var item in response.Users)
                        {
                            Add(item);
                            count++;
                        }

                        _page = response.Next ?? 0;
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
