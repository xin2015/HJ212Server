using System.Text;

namespace HJ212Server.Core
{
    /// <summary>
    /// 通讯包
    /// </summary>
    public class CommunicationPacket
    {
        public const string HeadConst = "##";
        public const string TailConst = "\r\n";
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
            Head = HeadConst;
            DataSegmentString = dataSegment.ToString();
            DataSegmentLength = DataSegmentString.Length;
            DataSegment = dataSegment;
            CRC = ComputeCRC(DataSegmentString);
            Tail = TailConst;
        }

        /// <summary>
        /// 计算CRC校验码
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ComputeCRC(string text)
        {
            byte[] bytes = Encoding.ASCII.GetBytes(text);
            int crc = CRCHelper.ComputeCRC16(bytes);
            return crc.ToString("X4");
        }

        public override string? ToString()
        {
            return string.Join(string.Empty, Head, DataSegmentLength.ToString("D4"), DataSegmentString, CRC, Tail);
        }

        //public static bool TryParse(string communicationPacketString, out CommunicationPacket communicationPacket)
        //{
        //    if (communicationPacketString.StartsWith(HeadConst) && communicationPacketString.EndsWith(TailConst))
        //    {
        //        string dataSegmentLengthString = communicationPacketString.Substring(HeadConst.Length, 4);
        //        string dataSegmentString = communicationPacketString.Substring(HeadConst.Length + 4, communicationPacketString.Length - HeadConst.Length - 4 - 4 - TailConst.Length);
        //        string crcString = communicationPacketString.Substring(communicationPacketString.Length - TailConst.Length - 4, 4);
        //        int dataSegmentLength;
        //        if(int.TryParse(dataSegmentLengthString,out dataSegmentLength)&&dataSegmentLength == dataSegmentString.Length)
        //        {
        //            if(crcString == ComputeCRC(dataSegmentString))
        //            {
        //                // TODO
        //                communicationPacket = new CommunicationPacket();
        //                return true;
        //            }
        //        }
        //    }
        //    return false;
        //}
    }
}
