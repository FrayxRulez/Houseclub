using Clubhouse.Navigation;
using Clubhouse.Navigation.Services;
using System.Linq;
using System.Text.Json;
using Windows.ApplicationModel;

namespace Clubhouse.Common
{
    public static class Extensions
    {
        public static string GetVersion(this Package package)
        {
            PackageId packageId = package.Id;
            PackageVersion version = packageId.Version;

            if (version.Revision > 0)
            {
                return string.Format("{0}.{1}.{3} ({2}) {4}", version.Major, version.Minor, version.Build, version.Revision, packageId.Architecture);
            }

            return string.Format("{0}.{1} ({2}) {3}", version.Major, version.Minor, version.Build, packageId.Architecture);
        }
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
