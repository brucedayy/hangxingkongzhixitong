using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace NC.Panel.Infrastructure.Extensions
{
	public static class IEnumerableExt
	{
		public static ObservableCollection<TSource> ToObservableCollection<TSource>(this IEnumerable<TSource> source)
		{
			ObservableCollection<TSource> ret = new ObservableCollection<TSource>();

			foreach (TSource item in source)
			{
				ret.Add(item);
			}
			return ret;
		}
	}
}