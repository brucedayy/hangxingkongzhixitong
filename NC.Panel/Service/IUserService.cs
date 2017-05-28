using NC.Panel.Models;

namespace NC.Panel.Service
{
    public interface IUserService
    {
        /// <summary>
        /// 在工具栏按钮点击后发生。
        /// </summary>
        event MainToolbarButtonClickEventHandler MainToolbarButtonClickEvent;

        /// <summary>
        /// 新增航路。
        /// </summary>
        /// <param name="routeInfo">航路信息。</param>
        void AddRoute(RouteInfoModel routeInfo);
    }
}