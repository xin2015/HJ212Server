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
        /// <summary>
        /// 标准版本号（0：HJ/T 212-2005，1：HJ 212-2017）
        /// </summary>
        public int Version { get; set; }
        /// <summary>
        /// 是否拆分包（1：是（数据包中包含总包数和包号），0：否（数据包中不含总包数和包号））
        /// </summary>
        public int D { get; set; }
        /// <summary>
        /// 是否应答（1：是，0：否）
        /// </summary>
        public int A { get; set; }

        public Flag(int version, int d, int a)
        {
            Version = version;
            D = d;
            A = a;
        }

        public override string? ToString()
        {
            int flag = Version << 2 + D << 1 + A;
            return flag.ToString();
        }

        public static bool TryParse(string flagString, out Flag flag)
        {
            int flagInt;
            if (int.TryParse(flagString, out flagInt))
            {
                flag = new Flag(flagInt >> 2, (flagInt >> 1) & 0x0001, flagInt & 0x0001);
                return true;
            }
            flag = new Flag(0, 0, 0);
            return false;
        }
    }
}
