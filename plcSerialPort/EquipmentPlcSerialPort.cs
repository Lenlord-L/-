using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using DebugAssert;

namespace plcSerialPort
{
    public class EquipmentPlcSerialPort
    {
        private readonly EquipmentPLC _equipmentCheck;
        private EquipmentPLC _equipmentFj;

        /// <summary>
        /// 读取plc数据间隔
        /// </summary>
        private const int InetervalReadTime = 1000;
        /// <summary>
        /// 等待机器人把不合格表抓走的最大时间，超时就又异常
        /// </summary>
        private const int WaitOutPcbTime = 20000;

        /// <summary>
        ///  PLC通讯管理系统
        /// </summary>
        /// <param name="checkport">检定通讯口</param>
        /// <param name="fjport">分拣通讯口</param>
        public EquipmentPlcSerialPort(string checkport, string fjport)
        {
            if (string.IsNullOrEmpty(checkport) || checkport.ToUpper().IndexOf("COM", StringComparison.Ordinal) == -1)
                throw new Exception("检定串口异常");
            if (string.IsNullOrEmpty(fjport) || fjport.ToUpper().IndexOf("COM", StringComparison.Ordinal) == -1)
                throw new Exception("分拣串口异常");

            try
            {
                _equipmentCheck = new EquipmentPLC(checkport) { Addr = 0x30 };
                Thread thread = new Thread(GetCheckInfo) { IsBackground = true };
                thread.Start();
                _equipmentFj = new EquipmentPLC(fjport) { Addr = 0x31 };
                Thread thread2 = new Thread(GetFjInfo) {IsBackground = true};
                thread2.Start();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region 检定线体

        /// <summary>
        /// true：允许检测；false：不可以检测
        /// </summary>
        public bool AllowCheckM500 { get; set; }

        /// <summary>
        /// 检定线体条码
        /// </summary>
        public string BarcodeCheck { get; set; }

        private void GetCheckInfo()
        {
            while (_equipmentCheck != null)
            {
                Thread.Sleep(InetervalReadTime);
                char[] mRequireValue;
                _equipmentCheck.GetPLCRequierValue(out mRequireValue);
                //允许检定开始
                AllowCheckM500 = mRequireValue != null && mRequireValue[0] == '1';
                if (string.IsNullOrEmpty(BarcodeCheck) && AllowCheckM500)
                {
                    //读取条码信息
                    BarcodeCheck = _equipmentCheck.SendReadBarCodeFromPlc();
                }
            }
        }

        /// <summary>
        /// 检定线体，检定结束通知
        /// </summary>
        /// <returns></returns>
        public bool SendCheckOver()
        {
            lock (this)
            {
                return _equipmentCheck.SendWritePLCValue("M0008", 1, "1");
            }
        }

        #endregion

        #region 分拣线体

        /// <summary>
        ///  分拣线到位信息
        /// </summary>
        public bool AllowFJM500 { get; set; }

        /// <summary>
        /// 分拣条码
        /// </summary>
        public string BarcodeFJ { get; set; }

        /// <summary>
        /// 当前板pbc合格与否，和表位数量一致
        /// </summary>
        /// <param name="results">1：不合格；0：合格</param>
        /// <returns></returns>
        public bool SendResult(int[] results)
        {
            lock (this)
            {
                int length = results.Length < 16 ? 1 : results.Length < 32 ? 2 : 3;
                _equipmentFj.SendWritePLCValue("D0010", length, true, GetToHexResult(results));
                return _equipmentFj.SendWritePLCValue("M0008", 1, "1");
                ;
            }
        }

        private void GetFjInfo()
        {
            while (_equipmentFj != null)
            {
                Thread.Sleep(InetervalReadTime);
                char[] mRequireValue;
                _equipmentFj.GetPLCRequierValue(out mRequireValue);
                //允许检定开始
                AllowFJM500 = mRequireValue != null && mRequireValue[0] == '1';
                if (string.IsNullOrEmpty(BarcodeFJ) && AllowFJM500)
                {
                    //读取条码信息
                    BarcodeCheck = _equipmentFj.SendReadBarCodeFromPlc();
                }
            }

        }

        /// <summary>
        /// 解析报文
        /// </summary>
        /// <param name="resluts">1：不合格，0：合格</param>
        /// <returns></returns>
        private string GetToHexResult(int[] resluts)
        {
            string tempData = string.Empty;
            string returnData = string.Empty;
            try
            {
                if (resluts.Length <= 16)
                {
                    for (int index = 0; index < resluts.Length; index++)
                    {
                        tempData += resluts[resluts.Length - index - 1];
                    }
                    tempData = tempData.PadLeft(16, '0');
                    returnData = (Convert.ToInt32(tempData, 2)).ToString("X2").PadLeft(4, '0');
                }
                else if (resluts.Length <= 32)
                {
                    for (int index = 0; index < 16; index++)
                    {
                        tempData += resluts[16 - index - 1];
                    }
                    returnData = (Convert.ToInt32(tempData.PadLeft(16, '0'), 2)).ToString("X2").PadLeft(4, '0');
                    tempData = string.Empty;
                    for (int index = 0; index < resluts.Length - 16; index++)
                    {
                        tempData += resluts[resluts.Length - index - 1];
                    }
                    returnData += (Convert.ToInt32(tempData.PadLeft(16, '0'), 2)).ToString("X2").PadLeft(4, '0');
                }
                else if (resluts.Length <= 48)
                {
                    for (int index = 0; index < 16; index++)
                    {
                        tempData += resluts[16 - index - 1];
                    }
                    returnData = (Convert.ToInt32(tempData.PadLeft(16, '0'), 2)).ToString("X2").PadLeft(4, '0');
                    tempData = string.Empty;
                    for (int index = 0; index < 16; index++)
                    {
                        tempData += resluts[32 - index - 1];
                    }
                    returnData += (Convert.ToInt32(tempData.PadLeft(16, '0'), 2)).ToString("X2").PadLeft(4, '0');
                    tempData = string.Empty;
                    for (int index = 0; index < resluts.Length - 32; index++)
                    {
                        tempData += resluts[resluts.Length - index - 1];
                    }
                    returnData += (Convert.ToInt32(tempData.PadLeft(16, '0'), 2)).ToString("X2").PadLeft(4, '0');
                }
            }
            catch (Exception ex)
            {
                Log.WriteLog(ToString(), ex);
            }
            return returnData;
        }

        #endregion

    }
}
