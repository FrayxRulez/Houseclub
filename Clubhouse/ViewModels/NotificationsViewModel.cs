using Clubhouse.Common;
using Clubhouse.Models;
using Clubhouse.Navigation;
using Clubhouse.Services;
using Clubhouse.Services.Methods;
using Clubhouse.Views;
using System;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.System;
using Windows.UI.Xaml.Data;

namespace Clubhouse.ViewModels
{
    public class NotificationsViewModel : ViewModelBase
    {
        private readonly IVoiceService _voiceService;

        public NotificationsViewModel(IDataService dataService, IVoiceService voiceService)
            : base(dataService)
        {
            _voiceService = voiceService;

            Items = new ItemCollection(this);

            NavigateCommand = new RelayCommand<Notification>(NavigateExecute);
        }

        public ItemCollection Items { get; private set; }

        public RelayCommand<Notification> NavigateCommand { get; }
        private async void NavigateExecute(Notification notification)
        {
            if (notification.Type == 1)
            {
                NavigationService.Popup.Navigate(typeof(UserPage), notification.UserProfile);
            }
            else if (notification.Type == 5)
            {
                if (Uri.TryCreate(notification.Url, UriKind.Absolute, out Uri uri))
                {
                    await Launcher.LaunchUriAsync(uri);
                }
            }
            else if (notification.Type == 11)
            {
                var response = await DataService.SendAsync(new JoinChannel(notification.Channel));
                if (response != null)
                {
                    _voiceService.JoinChannel(response);
                    NavigationService.Navigate(typeof(RoomPage));
                }
            }
            else if (notification.Type == 14)
            {
                // TODO: invites
            }
            else if (notification.Type == 16)
            {
                // TODO: open event
            }
            else if (notification.Type == 21)
            {
                await Launcher.LaunchUriAsync(new Uri("https://twitter.com/joinclubhouse"));
            }
        }

        public class ItemCollection : ObservableCollection<object>, ISupportIncrementalLoading
        {
            private readonly NotificationsViewModel _viewModel;

            private bool _actionable = true;

            private int _page = 1;
            private bool _hasMoreItems = true;

            public ItemCollection(NotificationsViewModel viewModel)
            {
                _viewModel = viewModel;
            }

            public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
            {
                return AsyncInfo.Run(async token =>
                {
                    var count = 0u;

                    if (_actionable)
                    {
                        var response = await _viewModel.DataService.SendAsync(new GetActionableNotifications(20, _page));
                        if (response.Success)
                        {
                            foreach (var item in response.Notifications)
                            {
                                Add(item);
                                count++;
                            }

                            _page = response.Next ?? 1;
                            _actionable = response.Next.HasValue;
                        }
                    }
                    else
                    {
                        var response = await _viewModel.DataService.SendAsync(new GetNotifications(20, _page));
                        if (response.Success)
                        {
                            foreach (var item in response.Notifications)
                            {
                                Add(item);
                                count++;
                            }

                            _page = response.Next ?? 1;
                            _hasMoreItems = response.Next.HasValue;
                        }
                    }

                    return new LoadMoreItemsResult { Count = count };
                });
            }

            public bool HasMoreItems => _hasMoreItems;
        }
    }
}
