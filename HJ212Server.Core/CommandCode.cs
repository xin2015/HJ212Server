using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HJ212Server.Core
{
    /// <summary>
    /// 命令编码
    /// </summary>
    public enum CommandCode
    {
        /// <summary>
        /// 提取/上传下位机（现场机）时间
        /// </summary>
        GetSlaveDeviceTime = 1011,
        /// <summary>
        /// 设置下位机（现场机）时间
        /// </summary>
        SetSlaveDeviceTime = 1012,
        /// <summary>
        /// 下位机（现场机）时间校准
        /// </summary>
        SlaveDeviceTimeCalibration = 1013,
        /// <summary>
        /// 提取/上传实时数据间隔
        /// </summary>
        GetLiveDataInterval = 1061,
        /// <summary>
        /// 设置实时数据间隔
        /// </summary>
        SetLiveDataInterval = 1062,
        /// <summary>
        /// 提取/上传分钟数据间隔
        /// </summary>
        GetMinuteDataInterval = 1063,
        /// <summary>
        /// 设置分钟数据间隔
        /// </summary>
        SetMinuteDataInterval = 1064,
        /// <summary>
        /// 设置下位机（现场机）密码
        /// </summary>
        SetSlaveDevicePassword = 1072
    }
}
