using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace plcSerialPort
{
    internal class PlcProtocal
    {
        public PlcProtocal()
        {
            Addr = 0x30;
        }
        private CommandCode _commandCode;
        private byte[] _data;
        private byte[] _len;
        private byte[] _plcAddr;
        private byte[] _rw;

        /// <summary>
        /// 站号，检定为30，分拣31
        /// </summary>
        public byte Addr { get; set; }

        public string Data
        {
            set { _data = Encoding.ASCII.GetBytes(value); }
        }

        /// <summary>
        ///     操作字节长度
        /// </summary>
        public int Length
        {
            set { _len = Encoding.ASCII.GetBytes(value.ToString("X2")); }
        }

        /// <summary>
        ///     数据段,例：M500，D90等
        /// </summary>
        public string PLC_Addr
        {
            set
            {
                _plcAddr = Encoding.ASCII.GetBytes(value);
                switch (value.Substring(0, 1).ToUpper())
                {
                    case "M": //按位来写内容，只有0和1区分
                        break;
                    case "D": //按字来区分，一个字=4位
                        _rw = _commandCode == CommandCode.写命令
                            ? new byte[] {0x57, 0x57, 0x30}
                            : new byte[] {0x57, 0x52, 0x30};
                        break;
                }
            }
        }

        /// <summary>
        ///     通讯命令，FX系列PLC有4个通讯命令，他们分别为：读、写、强制通、强制断
        /// </summary>
        public CommandCode CommandCode
        {
            set
            {
                _commandCode = value;
                switch (value)
                {
                    case CommandCode.读命令:
                        _rw = new byte[] {0x42, 0x52, 0x30};
                        break;
                    case CommandCode.写命令:
                        _rw = new byte[] {0x42, 0x57, 0x30};
                        break;
                    case CommandCode.强制断命令:
                    case CommandCode.强制通命令:
                        break;
                }
            }
        }

        public byte[] GetProtocol()
        {
            var protocol = new List<byte> {0x05, 0x30, Addr, 0x46, 0x46}; //完整报文
            protocol.AddRange(_rw); //CMD,读写
            protocol.AddRange(_plcAddr); //地址
            protocol.AddRange(_len); //长度
            if ((_data != null) && (_data.Length > 0))
                protocol.AddRange(_data);
            int cs = GetCS(protocol.ToArray(), 1, protocol.Count - 1);
            protocol.AddRange(Encoding.ASCII.GetBytes((cs % 0X100).ToString("X2")));
            //protocol.AddRange(new byte[] { 0x0d, 0x0a });
            return protocol.ToArray();
        }

        /// <summary>
        ///     统计校验和
        /// </summary>
        /// <param name="buff">被统计的变量</param>
        /// <param name="istart">开始位置</param>
        /// <param name="iend">结束位置，整个数组的全局位置</param>
        private byte GetCS(byte[] buff, int istart, int iend) //计算校验和
        {
            Debug.Assert(buff != null && istart >= 0 && iend > 0 && iend <= buff.Length);
            byte s = 0;
            for (var i = istart; i <= iend; i++) s += buff[i];
            return s;
        }
    }
}