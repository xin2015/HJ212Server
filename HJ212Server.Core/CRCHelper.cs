using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HJ212Server.Core
{
    public static class CRCHelper
    {
        /// <summary>
        /// ANSI CRC16 校验算法
        /// </summary>
        /// <param name="bytes">待校验的字节数组</param>
        /// <returns>CRC16校验码</returns>
        public static int ComputeCRC16(byte[] bytes)
        {
            int i, j, crc_reg, check;
            crc_reg = 0xFFFF;
            for (i = 0; i < bytes.Length; i++)
            {
                crc_reg = (crc_reg >> 8) ^ bytes[i];
                for (j = 0; j < 8; j++)
                {
                    check = crc_reg & 0x0001;
                    crc_reg >>= 1;
                    if (check == 0x0001)
                    {
                        crc_reg ^= 0xA001;
                    }
                }
            }
            return crc_reg;
        }
    }
}
