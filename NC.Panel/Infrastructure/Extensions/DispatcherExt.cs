using System;
using System.Security.Permissions;
using System.Windows.Threading;

namespace NC.Panel.Infrastructure.Extensions
{
    /// <summary>
    /// 线程调度扩展
    /// </summary>
    public static class DispatcherExt
    {
        /// <summary>
        /// 处理当前在消息队列中的所有Windows消息
        /// </summary>
        [SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        private static void DoEvents(this Dispatcher dispatcher)
        {
            DispatcherFrame frame = new DispatcherFrame();
            dispatcher.BeginInvoke(DispatcherPriority.Background,
                new DispatcherOperationCallback(ExitFrame), frame);
            Dispatcher.PushFrame(frame);
        }

        /// <summary>
        /// 通知所有消息泵必须终止
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
        private static object ExitFrame(object arg)
        {
            ((DispatcherFrame)arg).Continue = false;
            return null;
        }

        /// <summary>
        /// UI线程非阻塞延时
        /// </summary>
        /// <param name="dispatcher">线程调度器。</param>
        /// <param name="delayTime">延时时间（毫秒）</param>
        /// <returns></returns>
        public static void Delay(this Dispatcher dispatcher, int delayTime)
        {
            int tick = Environment.TickCount;
            while (Environment.TickCount - tick < delayTime)
            {
                dispatcher.DoEvents();
            }
        }
    }
}