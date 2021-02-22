using Clubhouse.Common;
using Clubhouse.Models;
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
    public class FollowListViewModel : ViewModelBase
    {
        public FollowListViewModel(ClubhouseAPIController dataService)
            : base(dataService)
        {
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            if (parameter is IDictionary<string, object> data)
            {
                if (data.TryGet("user_id", out ulong userId) && data.TryGet("followers", out bool followers))
                {
                    Items = new ItemCollection(this, userId, followers);
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

        public class ItemCollection : ObservableCollection<FullUser>, ISupportIncrementalLoading
        {
            private readonly FollowListViewModel _viewModel;

            private readonly ulong _userId;
            private readonly bool _followers;

            private int _page = 1;
            private bool _hasMoreItems = true;

            public ItemCollection(FollowListViewModel viewModel, ulong userId, bool followers)
            {
                _viewModel = viewModel;

                _followers = followers;
                _userId = userId;
            }

            public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
            {
                return AsyncInfo.Run(async token =>
                {
                    var count = 0u;

                    if (_followers)
                    {
                        var response = await _viewModel.DataService.SendAsync(new GetFollowers(_userId, 20, _page));
                        if (response != null)
                        {
                            foreach (var item in response.Users)
                            {
                                Add(item);
                                count++;
                            }

                            _page++;
                            _hasMoreItems = count > 0 && Count < response.Count;
                        }
                        else
                        {
                            _hasMoreItems = false;
                        }
                    }
                    else
                    {
                        var response = await _viewModel.DataService.SendAsync(new GetFollowing(_userId, 20, _page));
                        if (response != null)
                        {
                            foreach (var item in response.Users)
                            {
                                Add(item);
                                count++;
                            }

                            _page++;
                            _hasMoreItems = count > 0 && Count < response.Count;
                        }
                        else
                        {
                            _hasMoreItems = false;
                        }
                    }


                    return new LoadMoreItemsResult { Count = count };
                });
            }

            public bool HasMoreItems => _hasMoreItems;
        }
    }
}
