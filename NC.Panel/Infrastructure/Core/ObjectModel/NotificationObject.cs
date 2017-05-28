#region Imports (5)

using System;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization.Formatters.Binary;

#endregion Imports (5)

namespace NC.Panel.Infrastructure.Core.ObjectModel
{
    /// <summary>
    /// 属性改变通知基类
    /// </summary>
    [Serializable]
    public class NotificationObject : INotifyPropertyChanged
    {
        #region Events (1)

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion Events (1)

        #region Methods (3)

        public T Clone<T>()
        {
            var stream = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, this);
            stream.Position = 0;
            var obj = (T)formatter.Deserialize(stream);
            return obj;
        }

        /// <summary>
        ///     触发属性值改变通知事件
        /// </summary>
        /// <param name="propertyName">
        ///     属性名称
        ///     <remarks>为null则自动获取属性名称</remarks>
        /// </param>
        protected void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        ///     触发属性值改变通知事件
        /// </summary>
        /// <typeparam name="T">属性值类型</typeparam>
        /// <param name="propertyValue">属性值</param>
        /// <param name="newValue">属性的新值</param>
        /// <param name="propertyName">属性名称</param>
        protected void RaisePropertyChanged<T>(ref T propertyValue, T newValue,
                    [CallerMemberName] string propertyName = "")
        {
            if (Equals(propertyValue, newValue))
                return;

            propertyValue = newValue;
            RaisePropertyChanged(propertyName);
        }

        #endregion Methods (3)
    }
}
