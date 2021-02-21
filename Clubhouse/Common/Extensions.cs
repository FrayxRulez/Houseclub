using Clubhouse.Navigation;
using Clubhouse.Navigation.Services;
using System.Linq;

namespace Clubhouse.Common
{
    class Extensions
    {
    }

    public static class Template10Utils
    {
        public static WindowContext GetWindowWrapper(this INavigationService service)
            => WindowContext.ActiveWrappers.FirstOrDefault(x => x.NavigationServices.Contains(service));

        public static IDispatcherContext GetDispatcherWrapper(this INavigationService service)
            => service.GetWindowWrapper()?.Dispatcher;
    }
}
