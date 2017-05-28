using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Threading;

namespace NC.Panel.Infrastructure.Core
{
    /// <summary>
    ///     表示一个动态数据集合，支持跨线程更新，它可在添加、删除项目或刷新整个列表时提供通知。
    ///     <remarks>确保集合在UI线程上实例化，后续修改集合可以从任何线程来完成。</remarks>
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class AsyncObservableCollection<T> : ObservableCollection<T>
    {
        // 获取当前线程的SynchronizationContext对象
        //private readonly SynchronizationContext _synchronizationContext = SynchronizationContext.Current;

        // 获取当前线程的Dispatcher对象
        private readonly Dispatcher _dispatcher = Dispatcher.CurrentDispatcher;

        /// <summary>
        /// 无参构造函数。
        /// </summary>
        public AsyncObservableCollection()
        {
        }

        /// <summary>
        /// 有参构造函数。
        /// </summary>
        /// <param name="list"></param>
        public AsyncObservableCollection(IEnumerable<T> list)
            : base(list)
        {
        }

        /// <summary>
        /// 重写集合改变事件处理函数。
        /// </summary>
        /// <param name="e"></param>
        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            //if (SynchronizationContext.Current == _synchronizationContext)
            //    // 如果操作发生在UI线程中，不需要进行跨线程执行
            //    RaiseCollectionChanged(e);
            //else
            //    // 如果操作发生在非UI线程中
            //    _synchronizationContext.Send(RaiseCollectionChanged, e);


            if (Dispatcher.CurrentDispatcher == _dispatcher)
                // 如果操作发生在UI线程中，不需要进行跨线程执行
                RaiseCollectionChanged(e);
            else
                // 如果操作发生在非UI线程中
                _dispatcher.InvokeAsync(delegate { RaiseCollectionChanged(e); }, DispatcherPriority.DataBind);
        }

        /// <summary>
        /// 重写属性改变事件处理函数。
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            //if (SynchronizationContext.Current == _synchronizationContext)
            //    // 如果操作发生在UI线程中，不需要进行跨线程执行
            //    RaisePropertyChanged(e);
            //else
            //    // 如果操作发生在非UI线程中
            //    _synchronizationContext.Send(RaisePropertyChanged, e);

            if (Dispatcher.CurrentDispatcher == _dispatcher)
                // 如果操作发生在UI线程中，不需要进行跨线程执行
                RaisePropertyChanged(e);
            else
                // 如果操作发生在非UI线程中
                _dispatcher.InvokeAsync(delegate { RaisePropertyChanged(e); }, DispatcherPriority.DataBind);
        }

        /// <summary>
        /// 触发集合改变事件。
        /// </summary>
        /// <param name="param"></param>
        private void RaiseCollectionChanged(object param)
        {
            base.OnCollectionChanged((NotifyCollectionChangedEventArgs)param);
        }

        /// <summary>
        /// 触发属性改变事件。
        /// </summary>
        /// <param name="param"></param>
        private void RaisePropertyChanged(object param)
        {
            base.OnPropertyChanged((PropertyChangedEventArgs)param);
        }
    }
}