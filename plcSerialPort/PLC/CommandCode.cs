
namespace plcSerialPort
{
    /// <summary>
    /// 通讯命令，FX系列PLC有4个通讯命令，他们分别为：读、写、强制通、强制断
    /// </summary>
    public enum CommandCode : int
    {
        /// 读软继电器状态、数据,目标软继电器：X、Y、M、S、T、C、D
        /// <summary>
        /// 读软继电器状态、数据,目标软继电器：X、Y、M、S、T、C、D
        /// </summary>
        读命令 = 0,
        /// <summary>
        /// 写软继电器状态、数据，目标软继电器：X、Y、M、S、T、C、D
        /// </summary>
        写命令 = 1,
        强制通命令 = 7,
        强制断命令 = 8
    }
}
