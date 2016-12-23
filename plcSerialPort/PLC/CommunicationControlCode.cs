
namespace plcSerialPort
{
    /// <summary>
    /// 通讯控制符
    /// </summary>
    public enum CommunicationControlCode
    {
        /// <summary>
        /// 计算机发出的请求
        /// </summary>
        ENQ = 5,
        /// <summary>
        /// PLC对计算机发出请求的确认回答
        /// </summary>
        ACK = 6,
        /// <summary>
        /// PLC对计算机发出请求的否认回答
        /// </summary>
        NAK = 15,
        /// <summary>
        /// 帧头开始标识
        /// </summary>
        STX = 2,
        /// <summary>
        /// 帧尾结束标识
        /// </summary>
        ETX = 3
    }
}
