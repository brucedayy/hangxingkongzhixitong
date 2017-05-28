#region

using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Threading;
using System.Windows.Threading;

#endregion

namespace NC.Panel.Infrastructure.Core
{
    /// <summary>
    /// The safe observable.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class SafeObservable<T> : IList<T>, INotifyCollectionChanged
    {
        #region Fields

        /// <summary>
        /// The collection.
        /// </summary>
        private readonly IList<T> _collection = new List<T>();

        /// <summary>
        /// The dispatcher.
        /// </summary>
        private readonly Dispatcher _dispatcher;

        /// <summary>
        /// The sync.
        /// </summary>
        private readonly ReaderWriterLock _sync = new ReaderWriterLock();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SafeObservable{T}"/> class.
        /// </summary>
        public SafeObservable()
        {
            this._dispatcher = Dispatcher.CurrentDispatcher;
        }

        #endregion

        #region Public Events

        /// <summary>
        /// The collection changed.
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the count.
        /// </summary>
        public int Count
        {
            get
            {
                this._sync.AcquireReaderLock(Timeout.Infinite);
                int result = this._collection.Count;
                this._sync.ReleaseReaderLock();
                return result;
            }
        }

        /// <summary>
        /// Gets a value indicating whether is read only.
        /// </summary>
        public bool IsReadOnly
        {
            get
            {
                return this._collection.IsReadOnly;
            }
        }

        #endregion

        #region Public Indexers

        /// <summary>
        /// The this.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <returns>
        /// The T.
        /// </returns>
        public T this[int index]
        {
            get
            {
                this._sync.AcquireReaderLock(Timeout.Infinite);
                T result = this._collection[index];
                this._sync.ReleaseReaderLock();
                return result;
            }

            set
            {
                this._sync.AcquireWriterLock(Timeout.Infinite);
                if (this._collection.Count == 0 || this._collection.Count <= index)
                {
                    this._sync.ReleaseWriterLock();
                    return;
                }

                this._collection[index] = value;
                this._sync.ReleaseWriterLock();
            }
        }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        public void Add(T item)
        {
            if (Thread.CurrentThread == this._dispatcher.Thread)
            {
                this.DoAdd(item);
            }
            else
            {
                this._dispatcher.BeginInvoke((Action)(() => { this.DoAdd(item); }));
            }
        }

        /// <summary>
        /// The clear.
        /// </summary>
        public void Clear()
        {
            if (Thread.CurrentThread == this._dispatcher.Thread)
            {
                this.DoClear();
            }
            else
            {
                this._dispatcher.BeginInvoke((Action)(() => { this.DoClear(); }));
            }
        }

        /// <summary>
        /// The contains.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <returns>
        /// The System.Boolean.
        /// </returns>
        public bool Contains(T item)
        {
            this._sync.AcquireReaderLock(Timeout.Infinite);
            bool result = this._collection.Contains(item);
            this._sync.ReleaseReaderLock();
            return result;
        }

        /// <summary>
        /// The copy to.
        /// </summary>
        /// <param name="array">
        /// The array.
        /// </param>
        /// <param name="arrayIndex">
        /// The array index.
        /// </param>
        public void CopyTo(T[] array, int arrayIndex)
        {
            this._sync.AcquireWriterLock(Timeout.Infinite);
            this._collection.CopyTo(array, arrayIndex);
            this._sync.ReleaseWriterLock();
        }

        /// <summary>
        /// The get enumerator.
        /// </summary>
        /// <returns>
        /// The System.Collections.Generic.IEnumerator`1[T -&gt; T].
        /// </returns>
        public IEnumerator<T> GetEnumerator()
        {
            return this._collection.GetEnumerator();
        }

        /// <summary>
        /// The index of.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <returns>
        /// The System.Int32.
        /// </returns>
        public int IndexOf(T item)
        {
            this._sync.AcquireReaderLock(Timeout.Infinite);
            int result = this._collection.IndexOf(item);
            this._sync.ReleaseReaderLock();
            return result;
        }

        /// <summary>
        /// The insert.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <param name="item">
        /// The item.
        /// </param>
        public void Insert(int index, T item)
        {
            if (Thread.CurrentThread == this._dispatcher.Thread)
            {
                this.DoInsert(index, item);
            }
            else
            {
                this._dispatcher.BeginInvoke((Action)(() => { this.DoInsert(index, item); }));
            }
        }

        /// <summary>
        /// The remove.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <returns>
        /// The System.Boolean.
        /// </returns>
        public bool Remove(T item)
        {
            if (Thread.CurrentThread == this._dispatcher.Thread)
            {
                return this.DoRemove(item);
            }
            else
            {
                DispatcherOperation op = this._dispatcher.BeginInvoke(new Func<T, bool>(this.DoRemove), item);
                if (op == null || op.Result == null)
                {
                    return false;
                }

                return (bool)op.Result;
            }
        }

        /// <summary>
        /// The remove at.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        public void RemoveAt(int index)
        {
            if (Thread.CurrentThread == this._dispatcher.Thread)
            {
                this.DoRemoveAt(index);
            }
            else
            {
                this._dispatcher.BeginInvoke((Action)(() => { this.DoRemoveAt(index); }));
            }
        }

        #endregion

        #region Explicit Interface Methods

        /// <summary>
        /// The get enumerator.
        /// </summary>
        /// <returns>
        /// The System.Collections.IEnumerator.
        /// </returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return this._collection.GetEnumerator();
        }

        #endregion

        #region Methods

        /// <summary>
        /// The do add.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        private void DoAdd(T item)
        {
            this._sync.AcquireWriterLock(Timeout.Infinite);
            this._collection.Add(item);
            if (this.CollectionChanged != null)
            {
                this.CollectionChanged(
                    this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
            }

            this._sync.ReleaseWriterLock();
        }

        /// <summary>
        /// The do clear.
        /// </summary>
        private void DoClear()
        {
            this._sync.AcquireWriterLock(Timeout.Infinite);
            this._collection.Clear();
            if (this.CollectionChanged != null)
            {
                this.CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }

            this._sync.ReleaseWriterLock();
        }

        /// <summary>
        /// The do insert.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        /// <param name="item">
        /// The item.
        /// </param>
        private void DoInsert(int index, T item)
        {
            this._sync.AcquireWriterLock(Timeout.Infinite);
            this._collection.Insert(index, item);
            if (this.CollectionChanged != null)
            {
                this.CollectionChanged(
                    this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item, index));
            }

            this._sync.ReleaseWriterLock();
        }

        /// <summary>
        /// The do remove.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <returns>
        /// The System.Boolean.
        /// </returns>
        private bool DoRemove(T item)
        {
            this._sync.AcquireWriterLock(Timeout.Infinite);
            int index = this._collection.IndexOf(item);
            if (index == -1)
            {
                this._sync.ReleaseWriterLock();
                return false;
            }

            bool result = this._collection.Remove(item);
            if (result && this.CollectionChanged != null)
            {
                this.CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }

            this._sync.ReleaseWriterLock();
            return result;
        }

        /// <summary>
        /// The do remove at.
        /// </summary>
        /// <param name="index">
        /// The index.
        /// </param>
        private void DoRemoveAt(int index)
        {
            this._sync.AcquireWriterLock(Timeout.Infinite);
            if (this._collection.Count == 0 || this._collection.Count <= index)
            {
                this._sync.ReleaseWriterLock();
                return;
            }

            this._collection.RemoveAt(index);
            if (this.CollectionChanged != null)
            {
                this.CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }

            this._sync.ReleaseWriterLock();
        }

        #endregion
    }
}