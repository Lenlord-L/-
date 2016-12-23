using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DebugAssert;

namespace wfa.pcb.test
{
    class SerialPortPcb
    {
         private SerialPort _sp;

        /// <summary>
        ///     独立485通讯类模块
        /// </summary>
        /// <param name="portName">端口号，格式:COM4等</param>
        /// <param name="bauteLv">波特率，格式：2400,E,8,1</param>
        /// <param name="meterIndex">表位号，从0开始</param>
         public SerialPortPcb(string portName, string bauteLv,int index)
        {
            Index = index;
            ReceivedBuff = new List<byte>();
            if (string.IsNullOrEmpty(portName) || (portName == "COM0"))
                throw new Exception("端口不能为空");
            try
            {
                #region 初始化485通讯口

                string[] baute = bauteLv.Split(new[] {','});
                int intBaute = int.Parse(baute[0]);
                Parity parity;
                switch (baute[1])
                {
                    case "N":
                        parity = Parity.None;
                        break;
                    case "S":
                        parity = Parity.Space;
                        break;
                    case "O":
                        parity = Parity.Odd;
                        break;
                    default:
                        parity = Parity.Even;
                        break;
                }
                var stopBits = StopBits.One;
                if (int.Parse(baute[3]) == 2)
                    stopBits = StopBits.Two;
                _sp = new SerialPort(portName, intBaute, parity, 8, stopBits)
                {
                    ReceivedBytesThreshold = 1
                };
                _sp.DataReceived += _sp_DataReceived;

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

         public int Index { get; set; }

        /// <summary>
        ///     获取的数据缓存
        /// </summary>
        public List<byte> ReceivedBuff { get; private set; }

        /// 关闭对象
        /// <summary>
        ///     关闭对象
        /// </summary>
        public void Dispose()
        {
            try
            {
                if (_sp == null)
                    return;
                if (_sp.IsOpen)
                {
                    _sp.Close();
                    _sp.Dispose();
                }
                _sp = null;
            }
            catch (Exception ex)
            {
                Log.WriteLog(ToString(), ex);
            }
        }

        /// 关闭端口
        /// <summary>
        ///     关闭端口
        /// </summary>
        /// <returns></returns>
        public void Close()
        {
            try
            {
                if (_sp == null)
                    return;
                if (_sp.IsOpen)
                {
                    _sp.Close();
                    _sp.Dispose();
                }
                _sp = null;
            }
            catch (Exception ex)
            {
                Log.WriteLog(ToString(), ex);
            }
        }

        /// 托管处理接受到的应答报文
        /// <summary>
        ///     托管处理接受到的应答报文
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _sp_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                int ret;
                byte[] buf = new byte[0];
                Thread.Sleep(50);
                Application.DoEvents();
                int tmptick = Environment.TickCount;
                do
                {
                    ret = _sp.BytesToRead;
                    if (ret <= 1) continue;
                    int lastret = buf.Length;
                    Array.Resize(ref buf, ret + buf.Length);
                    _sp.Read(buf, lastret, ret);
                    ReceivedBuff.AddRange(buf);
                } while ((ret != 0) && (Environment.TickCount - tmptick < 500)); //没有缓存即可结束
            }
            catch (Exception ex)
            {
                Log.WriteLog("_sp_DataReceived", ex);
            }
        }

        #region 发送方法重载

        /// <summary>
        ///     发送报文函数
        /// </summary>
        /// <param name="sendbuff">发送报文内容</param>
        /// <returns></returns>
        public bool Send(byte[] sendbuff)
        {
            try
            {
                if (!_sp.IsOpen)
                {
                    _sp.Open();
                }
                _sp.DiscardInBuffer(); //丢弃来自串行驱动程序的接收缓冲区的数据。
                _sp.DiscardOutBuffer();
                ReceivedBuff.Clear(); //清除缓存
                _sp.Write(sendbuff, 0, sendbuff.Length);
                Thread.Sleep(20);
            }
            catch
            {
                return false;
            }
            return true;
        }

        /// <summary>
        ///     发送报文函数
        /// </summary>
        /// <param name="sendbuff">发送报文内容</param>
        /// <returns></returns>
        public bool Send(List<byte> sendbuff)
        {
            return Send(sendbuff.ToArray());
        }

        /// <summary>
        ///     发送报文函数
        /// </summary>
        /// <param name="sendbuff">发送报文内容</param>
        /// <returns></returns>
        public bool Send(string sendbuff)
        {
            try
            {
                if (!_sp.IsOpen)
                {
                    _sp.Open();
                }
                _sp.DiscardInBuffer(); //丢弃来自串行驱动程序的接收缓冲区的数据。
                _sp.DiscardOutBuffer();
                ReceivedBuff.Clear(); //清除缓存
                _sp.Write(sendbuff);
                Thread.Sleep(20);
            }
            catch
            {
                return false;
            }
            return true;
        }

        #endregion
    }
}
