using NC.Panel.Models;

namespace NC.Panel.Service
{
    public sealed class UserService : IUserService
    {
        /// <summary>
        /// 在工具栏按钮点击后发生。
        /// </summary>
        public event MainToolbarButtonClickEventHandler MainToolbarButtonClickEvent;

        /// <summary>
        /// 当工具栏按钮点击时调用。
        /// </summary>
        /// <param name="args">事件参数。</param>
        private void OnMainToolbarButtonClickEvent(MainToolbarButtonClickEventArgs args)
        {
            var handler = MainToolbarButtonClickEvent;
            if (handler != null) handler(this, args);
        }

        /// <summary>
        /// 新增航路。
        /// </summary>
        /// <param name="routeInfo">航路信息。</param>
        public void AddRoute(RouteInfoModel routeInfo)
        {
            
        }
    }
}