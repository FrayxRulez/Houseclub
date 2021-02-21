namespace Clubhouse.Navigation
{
    public class ViewModelLocator
    {
        private readonly Clubhouse.Services.ClubhouseAPIController _dataService;

        public ViewModelLocator()
        {
            _dataService = new Clubhouse.Services.ClubhouseAPIController();
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
                return (T)(object)new Clubhouse.ViewModels.MainViewModel(_dataService);
            }

            return default;
        }
    }
}
