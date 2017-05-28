using NC.Panel.Models;

namespace NC.Panel.Service
{
    /// <summary>
    /// 表示将处理主工具栏按钮点击时事件的方法。
    /// </summary>
    /// <param name="sender">触发者。</param>
    /// <param name="args">事件参数。</param>
    public delegate void MainToolbarButtonClickEventHandler(object sender, MainToolbarButtonClickEventArgs args);
}