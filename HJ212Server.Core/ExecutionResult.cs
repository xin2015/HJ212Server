using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HJ212Server.Core
{
    /// <summary>
    /// 执行结果
    /// </summary>
    public enum ExecutionResult
    {
        /// <summary>
        /// 执行成功
        /// </summary>
        Success = 1,
        /// <summary>
        /// 执行失败
        /// </summary>
        Failure = 2,
        /// <summary>
        /// 条件错误（命令请求条件错误）
        /// </summary>
        ConditionError = 3,
        /// <summary>
        /// 通讯超时
        /// </summary>
        CommunicationTimeout = 4,
        /// <summary>
        /// 系统繁忙（系统繁忙不能执行）
        /// </summary>
        SystemBusy = 5,
        /// <summary>
        /// 系统故障
        /// </summary>
        SystemFailure = 6,
        /// <summary>
        /// 没有数据
        /// </summary>
        NoData = 100
    }
}
