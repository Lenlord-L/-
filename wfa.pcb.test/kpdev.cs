using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace wfa.pcb.test
{
    internal class Kpdev
    {
        /// <summary>
        /// 负载点调整(三相电压电流可分别设置)
        /// </summary>
        /// <param name="phase">相线     0  3p3w watt        1 3p3w 60 var     2 3p3w 90 跨相var  
        ///                              3 3p3w 90 移相var   4 3p3w 自然无功   5 3p4w watt        6 3p4w 90跨相var
        ///                              7 3p4w 90移相var    8 3p4w 自然无功   9 1p2w watt</param>
        /// <param name="ratedVolt">被校表额定电压 如220V   则为220</param>
        /// <param name="ratedCurr">被校表额定电流 如1.5(6)A则为1.5</param>
        /// <param name="ratedFreq">被校表额定频率</param>
        /// <param name="phaseSequence">相序     0-正相序 1-逆相序</param>
        /// <param name="revers">电流方向 0-正相   1-反相</param>
        /// <param name="voltPer1">负载点电压百分数 100表示100%</param>
        /// <param name="voltPer2"></param>
        /// <param name="voltPer3"></param>
        /// <param name="currPer1">负载点电流百分数 100表示100%</param>
        /// <param name="currPer2"></param>
        /// <param name="currPer3"></param>
        /// <param name="iabc">负载点合分元 H-合元 A-分A B-分B C-分C H-单相</param>
        /// <param name="cosP">负载点功率因数 取值：1.0 0.5L 0.8C ....</param>
        /// <param name="mconst">表常数</param>
        /// <param name="sModel">sModel</param>
        /// <param name="mPluse">检定圈数</param>
        /// <param name="devPort">装置通讯口 如：COM1则为1,COM2为2,...</param>
        /// <returns></returns>
        [DllImport("kpdev.dll")]
        public static extern bool Adjust_UI2(int phase,  double ratedVolt, double ratedCurr, double ratedFreq, byte phaseSequence,
            byte revers, double voltPer1, double voltPer2, double voltPer3, double currPer1, double currPer2, double currPer3,
            string iabc, string cosP, double mconst,string sModel, double mPluse, byte devPort);


        /// <summary>
        /// 降电压电流 
        /// </summary>
        /// <param name="devPort">装置通讯口 如：COM1则为1,COM2为2,...</param>
        /// <returns></returns>
        [DllImport("kpdev.dll")]
        public static extern bool Power_Off(byte devPort);
    }
}
