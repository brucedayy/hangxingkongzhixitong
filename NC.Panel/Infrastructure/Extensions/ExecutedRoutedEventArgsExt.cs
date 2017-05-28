using System;
using System.Windows;
using System.Windows.Input;

namespace NC.Panel.Infrastructure.Extensions
{
	public static class ExecutedRoutedEventArgsExt
	{
		public static Object GetDataContext(this ExecutedRoutedEventArgs eventArgs)
		{
			try
			{
				return ((FrameworkElement)eventArgs.OriginalSource).DataContext;
			}
			catch (Exception)
			{
				return null;
			}
		}

		public static T GetDataContext<T>(this ExecutedRoutedEventArgs eventArgs)
		{
			try
			{
				return (T)((FrameworkElement)eventArgs.OriginalSource).DataContext;
			}
			catch (Exception)
			{
				return default(T);
			}
		}
	}
}