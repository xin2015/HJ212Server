using System.Text;

namespace HJ212Server.Core
{
    /// <summary>
    /// 通讯包
    /// </summary>
    public class CommunicationPacket
    {
        /// <summary>
        /// 包头
        /// </summary>
        public string Head { get; set; }
        /// <summary>
        /// 数据段长度
        /// </summary>
        public int DataSegmentLength { get; set; }
        /// <summary>
        /// 数据段
        /// </summary>
        public DataSegment DataSegment { get; set; }
        /// <summary>
        /// CRC校验码
        /// </summary>
        public string CRC { get; set; }
        /// <summary>
        /// 包尾
        /// </summary>
        public string Tail { get; set; }

        /// <summary>
        /// 数据段字符串
        /// </summary>
        protected string DataSegmentString { get; set; }

        public CommunicationPacket(DataSegment dataSegment)
        {
            Head = "##";
            DataSegmentString = dataSegment.ToString();
            DataSegmentLength = DataSegmentString.Length;
            DataSegment = dataSegment;
            CRC = ComputeCRC(DataSegmentString);
            Tail = "\r\n";
        }

        /// <summary>
        /// 计算CRC校验码
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public string ComputeCRC(string text)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(text);
            int crc = CRCHelper.ComputeCRC16(bytes);
            return crc.ToString("X4");
        }

        public override string? ToString()
        {
            return string.Join(string.Empty, Head, DataSegmentLength.ToString("D4"), DataSegmentString, CRC, Tail);
        }
    }
}
