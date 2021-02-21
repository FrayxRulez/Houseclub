using System.ComponentModel;

namespace Clubhouse.Navigation
{
    public class CancelEventArgs<T> : CancelEventArgs
    {
        public CancelEventArgs(T value)
        {
            Value = value;
        }

        public T Value { get; private set; }
    }
}
