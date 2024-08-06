using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HJ212Server.Core
{
    /// <summary>
    /// 请求命令返回
    /// </summary>
    public enum RequestCommandReturn
    {
        /// <summary>
        /// 准备执行请求
        /// </summary>
        PreparingToExecuteRequest = 1,
        /// <summary>
        /// 请求被拒绝
        /// </summary>
        RequestDenied = 2,
        /// <summary>
        /// PW错误
        /// </summary>
        PWError = 3,
        /// <summary>
        /// MN错误
        /// </summary>
        MNError = 4,
        /// <summary>
        /// ST错误
        /// </summary>
        STError = 5,
        /// <summary>
        /// Flag错误
        /// </summary>
        FlagError = 6,
        /// <summary>
        /// QN错误
        /// </summary>
        QNError = 7,
        /// <summary>
        /// CN错误
        /// </summary>
        CNError = 8,
        /// <summary>
        /// CRC校验错误
        /// </summary>
        CRCError = 9,
        /// <summary>
        /// 未知错误
        /// </summary>
        UnknownError = 10
    }
}
