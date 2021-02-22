namespace Clubhouse.Navigation
{
    public class ViewModelLocator
    {
        private readonly Clubhouse.Services.ClubhouseAPIController _dataService;
        private readonly Clubhouse.Services.IVoiceService _voiceService;

        public ViewModelLocator()
        {
            _dataService = new Clubhouse.Services.ClubhouseAPIController();
            _voiceService = new Clubhouse.Services.VoiceService();
        }

        private static ViewModelLocator _current;
        public static ViewModelLocator Current => _current ??= new ViewModelLocator();

        public T Resolve<T>()
        {
            var type = typeof(T);
            if (type == typeof(Clubhouse.ViewModels.LoginViewModel))
            {
                return (T)(object)new Clubhouse.ViewModels.LoginViewModel(_dataService);
            }
            else if (type == typeof(Clubhouse.ViewModels.LoginCompleteViewModel))
            {
                return (T)(object)new Clubhouse.ViewModels.LoginCompleteViewModel(_dataService);
            }
            else if (type == typeof(Clubhouse.ViewModels.MainViewModel))
            {
                return (T)(object)new Clubhouse.ViewModels.MainViewModel(_dataService, _voiceService);
            }
            else if (type == typeof(Clubhouse.ViewModels.RoomViewModel))
            {
                return (T)(object)new Clubhouse.ViewModels.RoomViewModel(_dataService, _voiceService);
            }

            return default;
        }
    }
}
