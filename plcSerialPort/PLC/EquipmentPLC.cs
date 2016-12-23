using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Net.Mime;
using System.Threading;
using System.Windows.Forms;
using DebugAssert;
using EMCS.COMM;

namespace plcSerialPort
{
    internal class EquipmentPLC
    {
        private const int MaxDelayTimes = 1500; //通讯最大延时时间
        private const int TryTimes = 3; //报文重试次数默认为3
        private long _lngmaxcommtime; //通讯时候，开始计时的标识
        private SerialPort _sp; //系统通讯端口
        /// <summary>
        /// 地址，检定位30，分拣31
        /// </summary>
        public byte Addr { get; set; }

        /// 构造函数，初始化通讯端口
        /// <summary>
        ///     构造函数，初始化通讯端口
        /// </summary>
        /// <param name="portName"> 要使用的端口（例如 COM1）。</param>
        public EquipmentPLC(string portName)
        {
            CancelSend = false;
            try
            {
                if (_sp == null)
                {
                    _sp = new SerialPort(portName, 9600, Parity.Odd, 7, StopBits.One);
                    _sp.DtrEnable = true;
                    _sp.RtsEnable = true;
                    _sp.Open();
                }
                else
                {
                    if (!_sp.IsOpen)
                        _sp.Open();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// 取消通讯
        /// <summary>
        ///     true：取消通讯；17,771
        /// </summary>
        public bool CancelSend { get; set; }

        /// <summary>
        ///     停止读取PLC监视值
        /// </summary>
        public bool StopReadMonitor { get; set; }

        ~EquipmentPLC()
        {
            if (_sp != null)
            {
                _sp.Close();
                _sp.Dispose();
                _sp = null;
            }
        }

        /// <summary>
        ///     读取M500-M509值，判断各个设备节点PLC是否在请求
        /// </summary>
        /// <param name="mRequireValue"></param>
        public bool GetPLCRequierValue(out char[] mRequireValue)
        {
            mRequireValue = new char[0];
            if (StopReadMonitor) return false;

            #region [组报文]

            const int length = 1;
            var plcProtocal = new PlcProtocal
            {
                CommandCode = CommandCode.读命令,
                PLC_Addr = "M0500",
                Length = length,
                Data = string.Empty,
                Addr= Addr 
            };
            byte[] sendArr = plcProtocal.GetProtocol();

            #endregion

            #region [发送并解析]

            mRequireValue = new char[length];
            lock (this)
            {
                for (int tryIndex = 0; tryIndex < TryTimes; tryIndex++)
                {
                    var buf = new char[0];
                    _sp.DiscardInBuffer(); //丢弃来自串行驱动程序的接收缓冲区的数据。
                    _sp.DiscardOutBuffer();
                    _sp.Write(sendArr, 0, sendArr.Length);
                    Thread.Sleep(50);
                    Application.DoEvents();
                    _lngmaxcommtime = Environment.TickCount; //从此刻开始到结束，需要1500毫秒的时间来记录。
                    do
                    {
                        int ret = _sp.BytesToRead;
                        int lastret = buf.Length;
                        if ((ret <= 3) && (lastret <= 0)) continue;
                        Array.Resize(ref buf, ret + buf.Length); //给缓存数组进行重新分配
                        _sp.Read(buf, lastret, ret);
                        //TODO
                        //解析……
                        if ((buf.Length < (length + 6)) || (buf[length + 5] != 0x03)) continue;
                        for (int plcIndex = 0; plcIndex < mRequireValue.Length; plcIndex++)
                        {
                            mRequireValue[plcIndex] = buf[plcIndex + 5];
                        }
                        return true;
                    } while ((Environment.TickCount - _lngmaxcommtime) < MaxDelayTimes); //写入字节不超过1500ms
                } //tryTimes
            } //Lock

            #endregion

            return false;
        }

        /// <summary>
        ///     读取PLC地址
        /// </summary>
        /// <param name="plcAddr">地址码</param>
        /// <param name="length">要读取的长度</param>
        /// <param name="mRequireValue">返回值</param>
        public void SendReadPLCValue(string plcAddr, int length, out char[] mRequireValue)
        {
            #region [组报文]

            int _length = length;
            var plcProtocal = new PlcProtocal
            {
                CommandCode = CommandCode.读命令,
                PLC_Addr = plcAddr,
                Length = _length,
                Data = string.Empty,
                Addr = Addr 
            };
            byte[] sendArr = plcProtocal.GetProtocol();

            #endregion

            #region [发送并解析]

            mRequireValue = new char[_length];
            lock (this)
            {
                for (int tryIndex = 0; tryIndex < TryTimes; tryIndex++)
                {
                    var buf = new char[0];
                    _sp.DiscardInBuffer(); //丢弃来自串行驱动程序的接收缓冲区的数据。
                    _sp.DiscardOutBuffer();
                    _sp.Write(sendArr, 0, sendArr.Length);
                    Thread.Sleep(50);
                    Application.DoEvents();
                    _lngmaxcommtime = Environment.TickCount; //从此刻开始到结束，需要1500毫秒的时间来记录。
                    do
                    {
                        int ret = _sp.BytesToRead;
                        int lastret = buf.Length;
                        if ((ret > 3) || (lastret > 0))
                        {
                            Array.Resize(ref buf, ret + buf.Length); //给缓存数组进行重新分配
                            ret = _sp.Read(buf, lastret, ret);
                            //TODO
                            //解析……
                            if (((buf.Length >= (_length + 6)) && buf[_length + 5] == 0x03) ||
                                ((buf.Length >= (_length*4 + 6)) && buf[_length*4 + 5] == 0x03))
                            {
                                mRequireValue = new char[buf.Length - 6];
                                Array.Copy(buf, 5, mRequireValue, 0, buf.Length - 6);
                                return;
                            }
                        }
                    } while ((Environment.TickCount - _lngmaxcommtime) < MaxDelayTimes); //写入字节不超过1500ms
                } //tryTimes
            } //Lock

            #endregion
        }

        public bool SendWritePLCValue(string plcAddr, int length, string writeData)
        {
            #region [组报文]

            int _length = length;
            var plcProtocal = new PlcProtocal
            {
                CommandCode = CommandCode.写命令,
                PLC_Addr = plcAddr,
                Length = _length,
                Data = writeData,
                Addr = Addr 
            };
            byte[] sendArr = plcProtocal.GetProtocol();

            #endregion

            #region [发送并解析]

            lock (this)
            {
                for (int tryIndex = 0; tryIndex < TryTimes; tryIndex++)
                {
                    var buf = new char[0];
                    _sp.DiscardInBuffer(); //丢弃来自串行驱动程序的接收缓冲区的数据。
                    _sp.DiscardOutBuffer();
                    _sp.Write(sendArr, 0, sendArr.Length);
                    Log.WriteLogSendEMByte(string.Format("send=>{0}:{1}", plcAddr, gSubFunction.PrintMsg(sendArr, 0, " ")));
                    Thread.Sleep(50);
                    Application.DoEvents();
                    _lngmaxcommtime = Environment.TickCount; //从此刻开始到结束，需要1500毫秒的时间来记录。
                    do
                    {
                        int ret = _sp.BytesToRead;
                        int lastret = buf.Length;
                        if ((ret > 3) || (lastret > 0))
                        {
                            Array.Resize(ref buf, ret + buf.Length); //给缓存数组进行重新分配
                            _sp.Read(buf, lastret, ret);
                            //TODO
                            //解析……
                            if (buf.Length >= (_length + 4) && buf[0] == 6)
                            {
                                return true;
                            }
                        }
                    } while ((Environment.TickCount - _lngmaxcommtime) < MaxDelayTimes); //写入字节不超过1500ms
                } //tryTimes
            } //Lock

            #endregion

            return false;
        }

        public bool SendWritePLCValue(string plcAddr, int len, bool command, string writeData)
        {


            #region [发送并解析]

            lock (this)
            {
                #region [组报文]

                var plcProtocal = new PlcProtocal
                {
                    PLC_Addr = plcAddr,
                    Length = len,
                    Data = writeData,
                    CommandCode= CommandCode.写命令
                };
                byte[] sendArr = plcProtocal.GetProtocol();

                #endregion

                try
                {
                    if (!_sp.IsOpen)
                        _sp.Open();
                }
                catch { return false; }
                for (int tryIndex = 0; tryIndex < TryTimes; tryIndex++)
                {
                    var buf = new char[0];
                    _sp.DiscardInBuffer(); //丢弃来自串行驱动程序的接收缓冲区的数据。
                    _sp.DiscardOutBuffer();
                    _sp.Write(sendArr, 0, sendArr.Length);
                    Thread.Sleep(150);
                    //Application.DoEvents();
                    _lngmaxcommtime = Environment.TickCount; //从此刻开始到结束，需要1500毫秒的时间来记录。
                    do
                    {
                        int ret = _sp.BytesToRead;
                        int lastret = buf.Length;
                        if ((ret < 1) && (lastret <= 0)) continue;
                        Array.Resize(ref buf, ret + buf.Length); //给缓存数组进行重新分配
                        _sp.Read(buf, lastret, ret);
                        if (buf.Length > 0 && buf[0] == 0x06) return true;
                        Thread.Sleep(100);
                    } while ((Environment.TickCount - _lngmaxcommtime) < MaxDelayTimes); //写入字节不超过1500ms
                } //tryTimes

            } //Lock
            #endregion

            return false;
        }

        public string SendReadBarCodeFromPlc()
        {
            #region [组报文]

            var plcProtocal = new PlcProtocal
            {
                CommandCode = CommandCode.读命令,
                PLC_Addr = "D0080",
                Length = 3,
                Data = string.Empty,
                Addr = Addr
            };
            byte[] sendbuff = plcProtocal.GetProtocol();

            #endregion
            //byte[] sendbuff = new byte[]
            //{0x5, 0x30, 0x30, 0x46, 0x46, 0x42, 0x52, 0x30, 0x44, 0x30, 0x30, 0x38, 0x30, 0x30, 0x33, 0x31, 0x46};
            return ReadBarCode(sendbuff);
        }

        string ReadBarCode(byte[] sendArr)
        {
            #region [发送并解析]

            string barcode = string.Empty;
            int len =3;
            lock (this)
            {
                try
                {
                    if (!_sp.IsOpen)
                        _sp.Open();
                }
                catch
                {
                    return string.Empty;
                }
                for (int tryIndex = 0; tryIndex < TryTimes; tryIndex++)
                {
                    var buf = new char[0];
                    _sp.DiscardInBuffer(); //丢弃来自串行驱动程序的接收缓冲区的数据。
                    _sp.DiscardOutBuffer();
                    _sp.Write(sendArr, 0, sendArr.Length);
                    Log.WriteLogSendEMByte("send=>" + gSubFunction.PrintMsg(sendArr, 0, " "));
                    Thread.Sleep(50);
                    Application.DoEvents();
                    int _lngmaxcommtime = Environment.TickCount; //从此刻开始到结束，需要1500毫秒的时间来记录。
                    do
                    {
                        int ret = _sp.BytesToRead;
                        int lastret = buf.Length;
                        if ((ret <= 3) && (lastret <= 0)) continue;
                        Array.Resize(ref buf, ret + buf.Length); //给缓存数组进行重新分配
                        _sp.Read(buf, lastret, ret);
                        if ((buf.Length < (len*4 + 4)) || (buf[0] != 0x02)) continue;
                        Log.WriteLogSendEMByte("back=>" + PrintMsg(buf));
                        string ss = string.Empty;
                        for (int i = 0; i < len *2; i++)
                        {
                            if (buf[i * 2 + 5] == 0x33)
                                ss += buf[i * 2 + 6];
                        }
                        char[] cs = ss.ToCharArray();
                        Array.Reverse(cs);
                        ss = new string(cs);
                        return ss.Trim(new[] { '\0' });
                    } while ((Environment.TickCount - _lngmaxcommtime) < MaxDelayTimes); //写入字节不超过1500ms
                } //tryTimes
            } //Lock
            return string.Empty;
            #endregion
        }

        /// <summary>
        ///     打印
        /// </summary>
        /// <param name="arr">需要打印的字节数组</param>
        /// <returns></returns>
        private string PrintMsg(IEnumerable<char> arr)
        {
            if (arr == null) return string.Empty;
            var msg = "";
            try
            {
                msg = arr.Aggregate(msg, (current, t) => current + (" " + t));
            }
            catch (Exception ex)
            {
                Log.WriteLog("PrintMsg", ex);
            }
            return msg.Trim();
        }

       
    }
}