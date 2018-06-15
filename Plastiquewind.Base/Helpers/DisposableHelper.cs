using System;

namespace Plastiquewind.Base.Helpers
{
    public static class DisposableHelper
    {
        public static void DynamicUsing(object resource, Action action)
        {
            try
            {
                action();
            }
            finally
            {
                if (resource is IDisposable d)
                {
                    d.Dispose();
                }
            }
        }
    }
}
