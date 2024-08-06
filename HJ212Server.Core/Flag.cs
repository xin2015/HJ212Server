using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HJ212Server.Core
{
    /// <summary>
    /// 标志位
    /// </summary>
    public class Flag
    {
        public const string HJ2122017 = "000001";
        /// <summary>
        /// 标准版本号（000000：HJ/T 212-2005，000001：HJ 212-2017）
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// 是否拆分包（1：是（数据包中包含总包数和包号），0：否（数据包中不含总包数和包号））
        /// </summary>
        public bool D { get; set; }
        /// <summary>
        /// 是否应答（1：是，0：否）
        /// </summary>
        public bool A { get; set; }

        public Flag()
        {
            Version = HJ2122017;
        }

        public Flag(string version, bool d, bool a)
        {
            Version = version;
            D = d;
            A = a;
        }

        public override string? ToString()
        {
            string flag = string.Join(string.Empty, Version, D, A);
            return flag;
        }
    }
}
