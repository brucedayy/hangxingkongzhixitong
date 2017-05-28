using System;

namespace NC.Panel.Infrastructure.Extensions
{
	public static class StringExt
	{
		public static string[] Split(this string str, string separator)
		{
			return str.Split(separator, StringSplitOptions.RemoveEmptyEntries);
		}

		public static string[] Split(this string str, string separator, StringSplitOptions options)
		{
			string[] sep = new string[] { separator };
			return str.Split(sep, options);
		}

		public static string[] Split(this string str, string separator, int count, StringSplitOptions options)
		{
			string[] sep = new string[] { separator };
			return str.Split(sep, count, options);
		}
	}
}