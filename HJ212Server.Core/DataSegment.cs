﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HJ212Server.Core
{
    /// <summary>
    /// 数据段
    /// </summary>
    public class DataSegment
    {
        public const string FieldSeparator = "=";
        public const string CategorySeparator = ",";
        public const string ProjectSeparator = ";";

        /// <summary>
        /// 请求编码
        /// </summary>
        public DateTime QN { get; set; }
        /// <summary>
        /// 系统编码
        /// </summary>
        public SystemCode ST { get; set; }
        /// <summary>
        /// 命令编码
        /// </summary>
        public CommandCode CN { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string PW { get; set; }
        /// <summary>
        /// 设备唯一标识
        /// </summary>
        public string MN { get; set; }
        /// <summary>
        /// 标志位
        /// </summary>
        public Flag Flag { get; set; }
        /// <summary>
        /// 总包数
        /// </summary>
        public int? PNUM { get; set; }
        /// <summary>
        /// 包号
        /// </summary>
        public int? PNO { get; set; }
        /// <summary>
        /// 指令参数
        /// </summary>
        public List<Dictionary<string, object>> CP { get; set; }

        /// <summary>
        /// 指令参数字符串
        /// </summary>
        protected string CPString { get; set; }

        public DataSegment(SystemCode systemCode, CommandCode commandCode, string password, string uniqueCode, Flag flag, int? packetNumber, int? packetNO, List<Dictionary<string, object>> cp)
        {
            QN = DateTime.Now;
            ST = systemCode;
            CN = commandCode;
            PW = password;
            MN = uniqueCode;
            Flag = flag;
            PNUM = packetNumber;
            PNO = packetNO;
            CP = cp;
            CPString = CPToString(cp);
        }

        public override string? ToString()
        {
            List<string> list = new List<string>();
            list.Add($"QN{FieldSeparator}{QN:yyyyMMddHHmmssfff}");
            list.Add($"ST{FieldSeparator}{(int)ST}");
            list.Add($"CN{FieldSeparator}{(int)CN}");
            list.Add($"PW{FieldSeparator}{PW}");
            list.Add($"MN{FieldSeparator}{MN}");
            list.Add($"Flag{FieldSeparator}{Flag}");
            if (Flag.D == 1)
            {
                list.Add($"PNUM{FieldSeparator}{PNUM}");
                list.Add($"PNO{FieldSeparator}{PNO}");
            }
            list.Add($"CP{FieldSeparator}{CPString}");
            return string.Join(ProjectSeparator, list);
        }

        public static string CPToString(List<Dictionary<string, object>> cp)
        {
            return $"&&{string.Join(ProjectSeparator, cp.Select(x => string.Join(CategorySeparator, x.Select(y => $"{y.Key}{FieldSeparator}{y.Value}"))))}&&";
        }

        //public static bool TryParse(string dataSegmentString, out DataSegment dataSegment)
        //{

        //}
    }
}
