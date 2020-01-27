using SkillPool.Core.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SkillPool.Core.Navigation
{
    /// <summary>
    /// 页面导航服务
    /// </summary>
    public interface INavigationService
    {
        /// <summary>
        /// 返回与导航堆栈中的上一页关联的视图模型类型
        /// </summary>
        ViewModelBase PreviousPageViewModel { get; }

        /// <summary>
        /// 启动应用程序时，执行到两个页面(登陆页面or主页面)之一的导航
        /// </summary>
        /// <returns></returns>
        Task InitializeAsync();

        /// <summary>
        /// 执行到特定页面的分层导航（通过ViewModel导航到Page）
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <returns></returns>
        Task NavigateToAsync<TViewModel>() where TViewModel : ViewModelBase;

        /// <summary>
        /// 通过传递参数执行到指定页面的分层导航。
        /// </summary>
        /// <typeparam name="TViewModel"></typeparam>
        /// <param name="parameter">用于页面初始化的参数</param>
        /// <returns></returns>
        Task NavigateToAsync<TViewModel>(object parameter) where TViewModel : ViewModelBase;

        /// <summary>
        /// 从导航堆栈中删除上一页
        /// </summary>
        /// <returns></returns>
        Task RemoveLastFromBackStackAsync();

        /// <summary>
        /// 从导航堆栈中删除所有先前的页面。
        /// </summary>
        /// <returns></returns>
        Task RemoveBackStackAsync();
    }
}
