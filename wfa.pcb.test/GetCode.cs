using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;

namespace wfa.pcb.test
{
    public class GetCode
    {
        /// <summary>
        /// 组报文
        /// </summary>
        /// <param name="projectName">截取项目名称的若干个长度进行判断</param>
        /// <param name="max">阈值上限</param>
        /// <param name="min">阈值下限</param>
        /// <param name="mul">倍率</param>
        /// <returns>返回完整报文</returns>
        public static string GetCodeWay(string projectName, double max, double min, double mul)
        {
            string code = "";//完整报文
            string maxcode = "";//阈值上限报文
            string mincode = "";//阈值下限报文
            switch (projectName)
            {
                case "单、三相":
                    mincode = Convert.ToString(min, CultureInfo.InvariantCulture);
                    if (mincode.Length < 4)
                    {
                        mincode = mincode.PadLeft(4, '0');
                    }
                    mincode = mincode.Substring(2, 2) + mincode.Substring(0, 2);
                    code = mincode;
                    break;
                case "单相空载":
                case "单相功率":
                case "三相功率":
                    maxcode = Convert.ToString((int)(max / mul * 100));//运算后剔除小数部分
                    if (maxcode.Length < 4)
                    {
                        maxcode = maxcode.PadLeft(4, '0');//位数不足4位自动高位补0
                    }
                    //反转字符串
                    maxcode = maxcode.Substring(2, 2) + maxcode.Substring(0, 2);
                    mincode = Convert.ToString((int)(min / mul * 100));
                    if (mincode.Length < 4)
                    {
                        mincode = mincode.PadLeft(4, '0');//位数不足4位自动高位补0
                    }
                    mincode = mincode.Substring(2, 2) + mincode.Substring(0, 2);
                    code = maxcode + mincode;
                    break;
                case "单相载波":
                case "单相继电":
                case "单相单片":
                case "单相计量":
                case "三相载波":
                case "三相继电":
                case "三相48":
                case "三相单片":
                case "三相30":
                    maxcode = Convert.ToInt32(max/mul*1000).ToString();
                    if (maxcode.Length < 4)
                    {
                        maxcode = maxcode.PadLeft(4, '0');
                    }
                    maxcode = maxcode.Substring(2, 2) + maxcode.Substring(0, 2);
                    mincode = Convert.ToString((int)(min / mul * 1000));
                    if (mincode.Length < 4)
                    {
                        mincode = mincode.PadLeft(4, '0');
                    }
                    mincode = mincode.Substring(2, 2) + mincode.Substring(0, 2);
                    code = maxcode + mincode;
                    break;
            }
            return code;
        }
    }
}
