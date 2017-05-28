using System;
using GalaSoft.MvvmLight;

namespace NC.Panel.Models
{
    /// <summary>
    /// 航路信息模型。
    /// </summary>
    public class RouteInfoModel : ObservableObject
    {
        private string _routeName;
        /// <summary>
        /// 获取或设置航路名称。
        /// </summary>
        public string RouteName
        {
            get { return _routeName; }
            set { Set(ref _routeName, value); }
        }

        private DateTime _createTime;
        /// <summary>
        /// 获取或设置创建时间。
        /// </summary>
        public DateTime CreateTime
        {
            get { return _createTime; }
            set { Set(ref _createTime, value); }
        }

    }
}