using Clubhouse.Navigation;
using Clubhouse.Navigation.Services;
using System.Collections.Generic;
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

        public static bool TryGet<T>(this IDictionary<string, object> dict, string key, out T value)
        {
            bool success;
            if (success = dict.TryGetValue(key, out object tryGetValue))
            {
                value = (T)tryGetValue;
            }
            else
            {
                value = default;
            }
            return success;
        }
    }

    public static class JsonExtensions
    {
        public static bool TryGetNamedUInt64(this JsonElement element, string key, out ulong value)
        {
            if (element.TryGetProperty(key, out JsonElement property))
            {
                return property.TryGetUInt64(out value);
            }

            value = default;
            return false;
        }

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

        public static string GetNamedString(this JsonElement element, string key)
        {
            if (element.TryGetProperty(key, out JsonElement property))
            {
                return property.GetString();
            }

            return null;
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
