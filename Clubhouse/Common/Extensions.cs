using Clubhouse.Navigation;
using Clubhouse.Navigation.Services;
using System.Linq;
using System.Text.Json;

namespace Clubhouse.Common
{
    class Extensions
    {
    }

    public static class JsonExtensions
    {
        public static bool TryGetNamedString(this JsonElement element, string key, out string value)
        {
            if (element.TryGetProperty(key, out JsonElement property))
            {
                value = property.GetString();
                return true;
            }

            value = null;
            return false;
        }
    }

    public static class Template10Utils
    {
        public static WindowContext GetWindowWrapper(this INavigationService service)
            => WindowContext.ActiveWrappers.FirstOrDefault(x => x.NavigationServices.Contains(service));

        public static IDispatcherContext GetDispatcherWrapper(this INavigationService service)
            => service.GetWindowWrapper()?.Dispatcher;
    }
}
