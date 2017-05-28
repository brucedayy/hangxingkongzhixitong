using System;
using NC.Panel.Models;

namespace NC.Panel.Service
{
    /// <summary>
    /// 主菜单按钮单击事件参数。
    /// </summary>
    public class MainToolbarButtonClickEventArgs:EventArgs
    {
        /// <summary>
        /// 获取或设置按钮类别。
        /// </summary>
        public EToolbarButtonCategory ButtonCategory  { get; set; }

        /// <summary>
        /// 获取或设置是否显示。
        /// </summary>
        public bool IsVisible { get; set; }
    }
}