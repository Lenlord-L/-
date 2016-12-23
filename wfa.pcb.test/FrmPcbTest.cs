using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using EMCS.COMM;
using plcSerialPort;

namespace wfa.pcb.test
{
    public partial class FrmPcbTest : Form
    {
        public static int RowCount;
        public static int IniRowCount;
        private readonly int[] _result;
        private EquipmentPlcSerialPort _equipmentplcserialport;

        /// <summary>
        ///     分拣状态，true：正在分拣，不能重复发送分拣命令
        /// </summary>
        private bool _fjWorkStatur;

        private int _setNumn = 24;

        public FrmPcbTest()
        {
            InitializeComponent();
            _result = new int[24];
            cbocheckport.Items.AddRange(SerialPort.GetPortNames());
            cbofjport.Items.AddRange(SerialPort.GetPortNames());
            cboMeterType.SelectedIndex = 0;
        }

        public static int GetSetRow
        {
            set
            {
                if (RowCount != value)
                {
                    RowCount = value;
                }
            }
            get { return RowCount; }
        }

        public static int IniGetSetRow
        {
            set
            {
                if (IniRowCount != value)
                {
                    IniRowCount = value;
                }
            }
            get { return IniRowCount; }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            if (_equipmentplcserialport != null)
            {
                try
                {
                    timer.Enabled = false;
                    //if (_equipmentplcserialport.AllowCheckM500)
                    //{
                    //    //允许检定
                    //    lblAllowCheck.Text = @"允许检定……";
                    //    txtBarcode.Text = _equipmentplcserialport.BarcodeCheck;
                    //    //todo
                    //    //外接线程，处理该处理的事情
                    //    lblchecking.Text = @"准备检定";
                    //    lblcheckOver.Text = @"不急，马上结束...";
                    //}
                    //else
                    //{
                    //    _equipmentplcserialport.BarcodeCheck = string.Empty;
                    //    lblAllowCheck.Text = @"待命……";
                    //    txtBarcode.Text = @"------";
                    //    lblchecking.Text = @"待命……";
                    //    lblcheckOver.Text = @"待命……";
                    //}

                    if (_equipmentplcserialport.AllowFJM500)
                    {
                        //允许检定
                        lblAllowfJ.Text = @"允许检定……";
                        txtBarcodeFJ.Text = _equipmentplcserialport.BarcodeFJ;
                        //控源
                        Kpdev.Adjust_UI2(5, 220, 0, 50, 0, 0, 100, 100, 100, 0, 0, 0, "H", "1.0", 1200, "", 2, 1);
                        gSubFunction.Delay(10);
                        var dictspPcb = new Dictionary<int, SerialPortPcb>();
                        for (var index = 0; index < 12; index++)
                        {
                            var spPcb = new SerialPortPcb("COM" + (index + 1), "2400,E,8,1", index);
                            dictspPcb.Add(index, spPcb);
                        }
                        for (var csIndex = 0; csIndex < 3; csIndex++)
                        {
                            for (var index = 0; index < 12; index++)
                            {
                                if (dictspPcb[index].ReceivedBuff.Count == 0)
                                {
                                    //68 11 11 11 11 11 11 68 20 00 56 16
                                    byte[] sendbuff =
                                    {
                                        0x68, 0x11, 0x11, 0x11, 0x11, 0x11, 0x11, 0x68, 0x20, 0x00, 0x56,
                                        0x16
                                    };
                                    dictspPcb[index].Send(sendbuff); //发送命令
                                }
                            }
                            gSubFunction.Delay(5);
                            var allOk = true;
                            for (var index = 0; index < 12; index++)
                            {
                                if (dictspPcb[index].ReceivedBuff.Count == 0)
                                {
                                    allOk = false;
                                }
                            }
                            if (allOk) break;
                        }
                        var errStation = string.Empty;
                        for (var index = 0; index < 12; index++)
                        {
                            if (dictspPcb[index].ReceivedBuff.Count == 0)
                            {
                                errStation += "," + (index + 1);
                            }
                        }
                        if (!string.IsNullOrEmpty(errStation))
                        {
                            lblAllowCheck.Text = errStation.Substring(1) + @"模块位错误...";
                        }


                        //把不合格表给我找出来，告诉那个表位不合格
                        if (!_fjWorkStatur)
                        {
                            var thread = new Thread(SendFjOver) { IsBackground = true };
                            thread.Start();
                        }
                        Kpdev.Power_Off(1); //降电压电流
                        lblResult.Text = @"全部合格";
                        lblfjOver.Text = @"不急，马上结束...";
                    }
                    else
                    {
                        _equipmentplcserialport.BarcodeFJ = string.Empty;
                        lblAllowfJ.Text = @"待命……";
                        txtBarcodeFJ.Text = @"------";
                        lblResult.Text = @"待命……";
                        lblfjOver.Text = @"待命……";
                    }
                }
                finally
                {
                    timer.Enabled = true;
                }
            }
        }

        private void butStart_Click(object sender, EventArgs e)
        {
            if (_equipmentplcserialport == null)
            {
                _equipmentplcserialport = new EquipmentPlcSerialPort(cbocheckport.Text, cbofjport.Text);
            }
            //timer.Enabled = butStart.Text == @"开始";
            //if (butStart.Text == @"开始")
            //{
            //    butStart.Text = @"停止";
            //}
        }

        private void butOver_Click(object sender, EventArgs e)
        {
            if (_equipmentplcserialport != null) _equipmentplcserialport.SendCheckOver();
        }

        private void butFJOver_Click(object sender, EventArgs e)
        {
            SendFjOver();
        }

        /// <summary>
        ///     分拣发送不合格分拣
        /// </summary>
        private void SendFjOver()
        {
            _fjWorkStatur = true;
            //int[] result = new int[12];
            if (_equipmentplcserialport != null) _equipmentplcserialport.SendResult(_result);
            Thread.Sleep(2000); //等待plc状态变更，放置重复开始分拣
            _fjWorkStatur = false;
        }

        private void butAdjust_UI_Click(object sender, EventArgs e)
        {
            Kpdev.Adjust_UI2(5, 220, 0, 50, 0, 0, 100, 100, 100, 0, 0, 0, "H", "1.0", 1200, "", 2, 1);
        }

        private void ToExcel(string barcode)
        {
        }

        private void IniListView()
        {
            _setNumn = cboMeterType.Text.Contains("三相") ? 12 : 24;
            for (var index = 0; index < _setNumn + 1; index++)
            {
                dataGridView.Columns.Add("M" + index, index == 0 ? "表位" : index.ToString().PadLeft(2, '0'));
                dataGridView.Columns[index].SortMode = DataGridViewColumnSortMode.NotSortable; //不进行排序
                dataGridView.Columns[index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                //居中对齐
                dataGridView.Columns[index].Width = 56;
                dataGridView.Columns[index].SortMode = DataGridViewColumnSortMode.NotSortable;
                dataGridView.Columns[index].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dataGridView.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }
            dataGridView.Columns[0].Width = 200;

            #region 增加第一行

            var row = new DataGridViewRow();
            var textboxcell = new DataGridViewTextBoxCell();
            textboxcell.Value = "项目名称";
            var comboxcells = new DataGridViewCheckBoxCell[_setNumn];
            row.Cells.Add(textboxcell);
            for (var i = 0; i < _setNumn; i++)
            {
                comboxcells[i] = new DataGridViewCheckBoxCell();
                row.Cells.Add(comboxcells[i]);
            }
            dataGridView.Rows.Add(row);
            #endregion

            //string[] readItem = "单相空载功率,空载|单相空载功率因素,空载|单相功率负载，负载".Split(new[] { '|' });
            //for (int index = 0; index < readItem.Length; index++)
            //{
            //    dataGridView.Rows.Add();
            //    dataGridView.Rows[index + 1].Cells[0].Value = readItem[index];
            //}
            var path = Environment.CurrentDirectory + @"\解析.ini"; //指定INI文件路径
            var k = 0;
            var keys = IniHelper.IniGetAllItemKeys(path, "解析项目名称");
            for (var i = 0; i < keys.Length; i++)
            {
                var value = IniHelper.IniGetStringValue(path, "解析项目名称", Convert.ToString(i + 1), null); //每个key所对应的value

                if (cboMeterType.SelectedIndex == 0)
                {
                    if (value.Substring(0, 2) == "单相")
                    {
                        dataGridView.Rows.Add();
                        dataGridView.Rows[k + 1].Cells[0].Value = value;
                        k++;
                    }
                }

                else if (cboMeterType.SelectedIndex == 1)
                {
                    if (value.Substring(0, 2) == "三相")
                    {
                        dataGridView.Rows.Add();
                        dataGridView.Rows[k + 1].Cells[0].Value = value;
                        k++;
                    }
                }
                else
                    continue;
            }
            //dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void cboMeterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            dataGridView.Rows.Clear();
            dataGridView.Columns.Clear();
            IniListView();
        }

        #region 窗体加载事件

        private void FrmPcbTest_Load(object sender, EventArgs e)
        {
            #region 配置信息dgv加载INI文件事件

            dgvConfig.Columns.Clear();
            dgvConfig.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //不能调整标题栏高度
            dgvConfig.AllowUserToAddRows = false; //禁止增加行
            dgvConfig.AllowUserToDeleteRows = false; //禁止删除行
            dgvConfig.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells; //设定包括Header和所有单元格的行高自动调整
            dgvConfig.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvConfig.RowHeadersWidth = 5; //行头宽度
            dgvConfig.Columns.Add("", "项目名称");
            dgvConfig.Columns.Add("", "阈值下线");
            dgvConfig.Columns.Add("", "阈值上线");
            dgvConfig.Columns.Add("", "阈值倍率");
            //dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;//自动调整dgv所有单元格列宽
            #endregion
        }

        #endregion

        private void dataGridView_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //int dgvRows = ((DataGridView)sender).RowCount;
            //using (var b = new SolidBrush(((DataGridView)sender).RowHeadersDefaultCellStyle.ForeColor))
            //{
            //    e.Graphics.DrawString(
            //        (e.RowIndex + 1).ToString(CultureInfo.InvariantCulture)
            //            .PadLeft(dgvRows.ToString(CultureInfo.InvariantCulture).Length, '0'), e.InheritedRowStyle.Font,
            //        b, e.RowBounds.Location.X, e.RowBounds.Location.Y + 5);
            //}
        }

        private void dataGridView_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            try
            {
            }
            catch
            {
                // ignored
            }
        }

        private void 全选ToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        #region 选择配置文件按钮事件

        private void btnChooseIni_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog { Filter = @"配置文件(*.ini)|*.ini|所有文件(*.*)|*.*" };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    dgvConfig.Rows.Clear();
                    var filepath = ofd.FileName;
                    txbCurrentIni.Text = Path.GetFileNameWithoutExtension(filepath);
                    var number = Convert.ToInt32(IniHelper.IniGetStringValue(filepath, "阈值项目数", "阈值项目数", null));
                    //获取所有阈值项目数的个数
                    for (var i = 0; i < number; i++)
                    {
                        var dr = new DataGridViewRow();
                        dr.CreateCells(dgvConfig);
                        dr.Cells[0].Value = IniHelper.IniGetStringValue(
                            filepath, "xm" + double.Parse(Convert.ToString(i + 1)), "项目名称", null);
                        dr.Cells[1].Value = IniHelper.IniGetStringValue(
                            filepath, "xm" + double.Parse(Convert.ToString(i + 1)), "阈值下限", null);
                        dr.Cells[2].Value = IniHelper.IniGetStringValue(
                            filepath, "xm" + double.Parse(Convert.ToString(i + 1)), "阈值上限", null);
                        dr.Cells[3].Value = IniHelper.IniGetStringValue(
                            filepath, "xm" + double.Parse(Convert.ToString(i + 1)), "阈值倍率", null);
                        dgvConfig.Rows.Add(dr);
                        dgvConfig.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;//自动调整dgv所有单元格列宽
                    }
                    if (dgvConfig.Rows.Count <= 1)
                    {
                        MessageBox.Show(@"加载配置文件格式错误！请重试！", @"提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        #endregion

        #region 组报文按钮事件

        private void btnGetCode_Click(object sender, EventArgs e)
        {
            txbCode.Text = "";
            try
            {
                foreach (DataGridViewRow dgvr in dgvConfig.Rows)
                {
                    txbCode.Text += GetCode.GetCodeWay(
                        Convert.ToString(dgvr.Cells[0].Value).Substring(0, 4),
                        Convert.ToDouble(dgvr.Cells[2].Value),
                        Convert.ToDouble(dgvr.Cells[1].Value),
                        Convert.ToDouble(dgvr.Cells[3].Value));
                }
                byte[] asciicode = System.Text.Encoding.Default.GetBytes(txbCurrentIni.Text);
                foreach (var v in asciicode)
                {
                    txbCode.Text += Convert.ToString(v, 16);
                }
                //将完整的报文每隔2个分开显示
                txbCode.Text = Regex.Replace(txbCode.Text, @".{2}", "$0 ");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

        #region 解析按钮事件

        private void btnAnalyse_Click(object sender, EventArgs e)
        {
            try
            {
                txbAnalyse.Text = "";
                if (txbAnalyseCode.Text == "")
                {
                    MessageBox.Show(@"请输入要解析的报文并加载相应的配置文件！", @"提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                var path = Environment.CurrentDirectory + @"\" + txbCurrentAnalyse.Text;
                var inipath = Environment.CurrentDirectory + @"\" + txbCurrentIni.Text;
                var row = dataGridView.Rows.Count; //得到总行数
                var rows = dgvConfig.Rows.Count;
                var codes = txbAnalyseCode.Text.Replace(" ", ""); //获取将要解析的报文并去掉空格
                var startcodes = codes.Substring(0, 18); //取出起始帧
                var data = codes.Substring(18, 2); //模块数据帧
                var datalength = Convert.ToInt32(codes.Substring(20, 2), 16); //数据长度
                var datas = codes.Substring(22, datalength * 2); //取出整个数据
                var mc = Regex.Matches(datas, @"\w{6}"); //使用正则将截取到的数据三个字节分组
                foreach (Match m in mc)
                {
                    var address = m.ToString().Substring(0, 2); //地址
                    double value = Convert.ToInt32(m.ToString().Substring(2, 4)); //后两位字节数据
                    if (dic.ContainsKey(address))//判断dictionary内是否存在地址，如果不存在，跳过当前数据执行下一条数据
                    {
                        var str = dic[address];
                        //for (var i = 0; i < row; i++) //在总行数内循环查找
                        //{
                        //    if (dgvAnalyse.Rows[i].Cells[3].Value.ToString().Trim() == address)
                        //    {
                        //        GetSetRow = i + 1;
                        //        break;
                        //    }
                        //}
                        var name = IniHelper.IniGetStringValue(path, "解析项目名称", str, null); //解析项目名称
                        var mul = Convert.ToDouble(IniHelper.IniGetStringValue(path, "倍率", str, null)); //倍率
                        //写入数据
                        for (var i = 0; i < row; i++)
                        {
                            if (dataGridView.Rows[i].Cells[0].Value.ToString() == name)
                            {
                                GetSetRow = i;
                                break;
                            }
                        }
                        //判断数据是否合格
                        for (int k = 0; k < rows; k++)
                        {
                            string val = dgvConfig.Rows[k].Cells[0].Value.ToString();
                            if (val.StartsWith(name.Split(',')[0]))
                            {
                                IniGetSetRow = k;
                                break;
                            }
                        }
                        switch (name)
                        {
                            #region 功率

                            case "单相空载功率,空载":
                            case "单相空载功率因素,空载":
                            case "单相功率负载，负载":
                            case "单相功率负载因素，负载":
                            case "三相功率，空载":
                            case "三相功率因素，空载":
                            case "三相功率，负载":
                            case "三相功率因素，负载":
                                var text = (value / 100 * mul).ToString(); //计算后的数据
                                txbAnalyse.Text += m + "\t地址：" + address + " " + name + "：" + text + "\r\n";
                                dataGridView.Rows[GetSetRow].Cells[0 + 1].Value = text;
                                if (Convert.ToDouble(text) >=
                                    Convert.ToDouble(dgvConfig.Rows[IniGetSetRow].Cells[0 + 2].Value) ||
                                    Convert.ToDouble(text) <=
                                    Convert.ToDouble(dgvConfig.Rows[IniGetSetRow].Cells[0 + 1].Value))
                                {
                                    dataGridView.Rows[GetSetRow].Cells[0 + 1].Style.BackColor = Color.White;
                                }
                                else
                                {
                                    dataGridView.Rows[GetSetRow].Cells[0 + 1].Style.BackColor = Color.Red;
                                }
                                break;

                            #endregion

                            #region 其他

                            case "单相载波PLC电压，空载":
                            case "单相载波PLC电压纹波，空载":
                            case "单相继电器电压，空载":
                            case "单相继电器电压纹波，空载":
                            case "单相单片机电压，空载":
                            case "单相单片机电压纹波，空载":
                            case "单相计量后电压，空载":
                            case "单相计量后电压纹波，空载":
                            case "单相计量前电压，空载":
                            case "单相计量前电压纹波，空载":
                            case "三相载波PLC电压，空载":
                            case "三相载波PLC电压纹波，空载":
                            case "三相继电器电压，空载":
                            case "三相继电器电压纹波，空载":
                            case "三相485后端电压，空载":
                            case "三相485后端电压纹波，空载":
                            case "三相485前端电压，空载":
                            case "三相485前端电压纹波，空载":
                            case "三相单片机直流电压，空载":
                            case "三相单片机直流电压纹波，空载":
                            case "三相300V电压，空载":
                            case "三相单片机交流电压，空载":
                            case "三相单片机交流电压纹波，空载":
                            case "单相载波PLC电压，负载":
                            case "单相载波PLC电压纹波，负载":
                            case "单相继电器电压，负载":
                            case "单相继电器电压纹波，负载 ":
                            case "单相单片机电压，负载":
                            case "单相单片机电压纹波，负载":
                            case "单相计量后端电压，负载":
                            case "单相计量后端电压纹波，负载":
                            case "单相计量前端电压，负载":
                            case "单相计量前端电压纹波，负载":
                            case "三相载波PLC电压，负载":
                            case "三相载波PLC电压纹波，负载":
                            case "三相/继电器电压，负载":
                            case "三相继电器电压纹波，负载":
                            case "三相485后端电压，负载":
                            case "三相485后端电压纹波，负载":
                            case "三相485前端电压，负载":
                            case "三相485前端电压纹波，负载":
                            case "三相单片机直流电压，负载":
                            case "三相单片机直流电压纹波，负载":
                            case "三相单片机交流电压，负载":
                            case "三相单片机交流电压纹波，负载":
                            case "单相载波PLC电压，短路时":
                            case "单相载波PLC电压纹波，短路恢复后":
                            case "三相载波PLC电压，短路时":
                            case "三相载波PLC电压纹波，短路恢复后":
                                var text1 = (value / 1000 * mul).ToString(); //计算后的数据
                                txbAnalyse.Text += m + "\t地址：" + address + " " + name + "：" + text1 + "\r\n";
                                dataGridView.Rows[GetSetRow].Cells[0 + 1].Value = text1;
                                if (Convert.ToDouble(text1) <=
                                    Convert.ToDouble(dgvConfig.Rows[IniGetSetRow].Cells[0 + 2].Value) &&
                                    Convert.ToDouble(text1) >=
                                    Convert.ToDouble(dgvConfig.Rows[IniGetSetRow].Cells[0 + 1].Value))
                                {
                                    dataGridView.Rows[GetSetRow].Cells[0 + 1].Style.BackColor = Color.White;
                                }
                                else
                                {
                                    dataGridView.Rows[GetSetRow].Cells[0 + 1].Style.BackColor = Color.Red;
                                }
                                break;

                            #endregion
                        }
                    }
                    else
                    {
                        txbAnalyse.Text += m + "\t错误解析" + "\r\n";
                        continue;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;//自动调整dgv所有单元格列宽
        }

        #endregion

        #region 选择解析配置按钮事件

        public Dictionary<string, string> dic = new Dictionary<string, string>();

        private void btnChooseAnalyse_Click(object sender, EventArgs e)
        {
            var ofd = new OpenFileDialog { Filter = @"配置文件(*.ini)|*.ini|所有文件(*.*)|*.*" };
            try
            {
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    //dgvAnalyse.Rows.Clear();
                    var filepath = ofd.FileName;
                    txbCurrentAnalyse.Text = ofd.SafeFileName;
                    foreach (var str in IniHelper.IniGetAllItemKeys(filepath, "解析项目名称"))
                    {
                        dic.Add(IniHelper.IniGetStringValue(filepath, "地址", str, null), str);
                        //var dr = new DataGridViewRow();
                        //dr.CreateCells(dgvAnalyse);
                        //dr.Cells[0].Value = str;
                        //dr.Cells[1].Value = IniHelper.IniGetStringValue(filepath, "解析项目名称", str, null);
                        //dr.Cells[2].Value = IniHelper.IniGetStringValue(filepath, "倍率", str, null);
                        //dr.Cells[3].Value = IniHelper.IniGetStringValue(filepath, "地址", str, null);
                        //dgvAnalyse.Rows.Add(dr);
                    }
                    //if (dgvAnalyse.Rows.Count <= 1)
                    //{
                    //    MessageBox.Show(@"加载配置文件格式错误！请重试！", @"提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion
    }
}