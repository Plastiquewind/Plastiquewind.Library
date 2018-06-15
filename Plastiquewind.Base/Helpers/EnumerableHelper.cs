using System.Collections;

namespace Plastiquewind.Base.Helpers
{
    public static class EnumerableHelper
    {
        public static int Count(this IEnumerable source)
        {
            if (source is ICollection col)
            {
                return col.Count;
            }

            int c = 0;
            var e = source.GetEnumerator();

            DisposableHelper.DynamicUsing(e, () =>
            {
                while (e.MoveNext())
                {
                    c++;
                }
            });

            return c;
        }
    }
}
