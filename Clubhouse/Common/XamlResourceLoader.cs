using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml.Resources;

namespace Clubhouse.Common
{
    public class XamlResourceLoader : CustomXamlResourceLoader
    {
        private readonly ResourceLoader _loader;

        public XamlResourceLoader()
        {
            _loader = ResourceLoader.GetForViewIndependentUse("Resources");
        }

        protected override object GetResource(string resourceId, string objectType, string propertyName, string propertyType)
        {
            return _loader.GetString(resourceId);
        }
    }
}
