namespace Clubhouse.ViewModels.Delegates
{
    public interface IListViewDelegate : IViewModelDelegate
    {
        T ElementFromItem<T>(object item) where T : class;
    }
}
