namespace wfa.pcb.test
{
    partial class FrmPcbTest
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.dataGridView = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.全选ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel2 = new System.Windows.Forms.Panel();
            this.grpFenjian = new System.Windows.Forms.GroupBox();
            this.butFJOver = new System.Windows.Forms.Button();
            this.lblfjOver = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lblAllowfJ = new System.Windows.Forms.Label();
            this.lblResult = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtBarcodeFJ = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.grpCheck = new System.Windows.Forms.GroupBox();
            this.butAdjust_UI = new System.Windows.Forms.Button();
            this.butOver = new System.Windows.Forms.Button();
            this.lblcheckOver = new System.Windows.Forms.Label();
            this.lblAllowCheck = new System.Windows.Forms.Label();
            this.lblchecking = new System.Windows.Forms.Label();
            this.txtBarcode = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txbAnalyse = new System.Windows.Forms.TextBox();
            this.txbCurrentAnalyse = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.txbAnalyseCode = new System.Windows.Forms.TextBox();
            this.btnAnalyse = new System.Windows.Forms.Button();
            this.btnChooseAnalyse = new System.Windows.Forms.Button();
            this.txbCode = new System.Windows.Forms.TextBox();
            this.btnGetCode = new System.Windows.Forms.Button();
            this.dgvConfig = new System.Windows.Forms.DataGridView();
            this.grpSerialPort = new System.Windows.Forms.GroupBox();
            this.txbCurrentIni = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.btnChooseIni = new System.Windows.Forms.Button();
            this.cboMeterType = new System.Windows.Forms.ComboBox();
            this.label11 = new System.Windows.Forms.Label();
            this.butStart = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbofjport = new System.Windows.Forms.ComboBox();
            this.cbocheckport = new System.Windows.Forms.ComboBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.grpFenjian.SuspendLayout();
            this.grpCheck.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConfig)).BeginInit();
            this.grpSerialPort.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1042, 659);
            this.tabControl1.TabIndex = 4;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage1.Controls.Add(this.dataGridView);
            this.tabPage1.Controls.Add(this.panel2);
            this.tabPage1.Location = new System.Drawing.Point(4, 26);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1034, 629);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "检定信息";
            // 
            // dataGridView
            // 
            this.dataGridView.AllowUserToAddRows = false;
            this.dataGridView.AllowUserToDeleteRows = false;
            this.dataGridView.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView.Location = new System.Drawing.Point(3, 218);
            this.dataGridView.Name = "dataGridView";
            this.dataGridView.RowTemplate.Height = 23;
            this.dataGridView.Size = new System.Drawing.Size(1028, 408);
            this.dataGridView.TabIndex = 6;
            this.dataGridView.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView_DataError);
            this.dataGridView.RowPostPaint += new System.Windows.Forms.DataGridViewRowPostPaintEventHandler(this.dataGridView_RowPostPaint);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.全选ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(101, 26);
            // 
            // 全选ToolStripMenuItem
            // 
            this.全选ToolStripMenuItem.Name = "全选ToolStripMenuItem";
            this.全选ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.全选ToolStripMenuItem.Text = "全选";
            this.全选ToolStripMenuItem.Click += new System.EventHandler(this.全选ToolStripMenuItem_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.grpFenjian);
            this.panel2.Controls.Add(this.grpCheck);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1028, 215);
            this.panel2.TabIndex = 5;
            // 
            // grpFenjian
            // 
            this.grpFenjian.Controls.Add(this.butFJOver);
            this.grpFenjian.Controls.Add(this.lblfjOver);
            this.grpFenjian.Controls.Add(this.label10);
            this.grpFenjian.Controls.Add(this.lblAllowfJ);
            this.grpFenjian.Controls.Add(this.lblResult);
            this.grpFenjian.Controls.Add(this.label7);
            this.grpFenjian.Controls.Add(this.txtBarcodeFJ);
            this.grpFenjian.Controls.Add(this.label9);
            this.grpFenjian.Controls.Add(this.label8);
            this.grpFenjian.Location = new System.Drawing.Point(375, 6);
            this.grpFenjian.Name = "grpFenjian";
            this.grpFenjian.Size = new System.Drawing.Size(340, 198);
            this.grpFenjian.TabIndex = 7;
            this.grpFenjian.TabStop = false;
            this.grpFenjian.Text = "分拣线信息";
            // 
            // butFJOver
            // 
            this.butFJOver.Font = new System.Drawing.Font("宋体", 12F);
            this.butFJOver.Location = new System.Drawing.Point(278, 152);
            this.butFJOver.Name = "butFJOver";
            this.butFJOver.Size = new System.Drawing.Size(51, 30);
            this.butFJOver.TabIndex = 4;
            this.butFJOver.Text = "放行";
            this.butFJOver.UseVisualStyleBackColor = true;
            this.butFJOver.Click += new System.EventHandler(this.butFJOver_Click);
            // 
            // lblfjOver
            // 
            this.lblfjOver.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblfjOver.ForeColor = System.Drawing.Color.Blue;
            this.lblfjOver.Location = new System.Drawing.Point(88, 152);
            this.lblfjOver.Name = "lblfjOver";
            this.lblfjOver.Size = new System.Drawing.Size(184, 30);
            this.lblfjOver.TabIndex = 1;
            this.lblfjOver.Text = "已经通知……";
            this.lblfjOver.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(9, 159);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(88, 16);
            this.label10.TabIndex = 1;
            this.label10.Text = "通知分拣：";
            // 
            // lblAllowfJ
            // 
            this.lblAllowfJ.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAllowfJ.ForeColor = System.Drawing.Color.Blue;
            this.lblAllowfJ.Location = new System.Drawing.Point(88, 38);
            this.lblAllowfJ.Name = "lblAllowfJ";
            this.lblAllowfJ.Size = new System.Drawing.Size(184, 30);
            this.lblAllowfJ.TabIndex = 1;
            this.lblAllowfJ.Text = "待命……";
            this.lblAllowfJ.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblResult
            // 
            this.lblResult.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblResult.ForeColor = System.Drawing.Color.Blue;
            this.lblResult.Location = new System.Drawing.Point(88, 112);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(184, 30);
            this.lblResult.TabIndex = 1;
            this.lblResult.Text = "1,9不合格";
            this.lblResult.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(9, 45);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 16);
            this.label7.TabIndex = 1;
            this.label7.Text = "到位状态：";
            // 
            // txtBarcodeFJ
            // 
            this.txtBarcodeFJ.Location = new System.Drawing.Point(88, 78);
            this.txtBarcodeFJ.Name = "txtBarcodeFJ";
            this.txtBarcodeFJ.Size = new System.Drawing.Size(184, 26);
            this.txtBarcodeFJ.TabIndex = 2;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(9, 120);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 16);
            this.label9.TabIndex = 1;
            this.label9.Text = "检测结论：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(9, 82);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(88, 16);
            this.label8.TabIndex = 1;
            this.label8.Text = "条码信息：";
            // 
            // grpCheck
            // 
            this.grpCheck.Controls.Add(this.butAdjust_UI);
            this.grpCheck.Controls.Add(this.butOver);
            this.grpCheck.Controls.Add(this.lblcheckOver);
            this.grpCheck.Controls.Add(this.lblAllowCheck);
            this.grpCheck.Controls.Add(this.lblchecking);
            this.grpCheck.Controls.Add(this.txtBarcode);
            this.grpCheck.Controls.Add(this.label6);
            this.grpCheck.Controls.Add(this.label5);
            this.grpCheck.Controls.Add(this.label4);
            this.grpCheck.Controls.Add(this.label3);
            this.grpCheck.Location = new System.Drawing.Point(5, 6);
            this.grpCheck.Name = "grpCheck";
            this.grpCheck.Size = new System.Drawing.Size(339, 198);
            this.grpCheck.TabIndex = 6;
            this.grpCheck.TabStop = false;
            this.grpCheck.Text = "检测线信息";
            // 
            // butAdjust_UI
            // 
            this.butAdjust_UI.Font = new System.Drawing.Font("宋体", 12F);
            this.butAdjust_UI.Location = new System.Drawing.Point(282, 110);
            this.butAdjust_UI.Name = "butAdjust_UI";
            this.butAdjust_UI.Size = new System.Drawing.Size(51, 30);
            this.butAdjust_UI.TabIndex = 5;
            this.butAdjust_UI.Text = "控源";
            this.butAdjust_UI.UseVisualStyleBackColor = true;
            this.butAdjust_UI.Click += new System.EventHandler(this.butAdjust_UI_Click);
            // 
            // butOver
            // 
            this.butOver.Font = new System.Drawing.Font("宋体", 12F);
            this.butOver.Location = new System.Drawing.Point(282, 152);
            this.butOver.Name = "butOver";
            this.butOver.Size = new System.Drawing.Size(51, 30);
            this.butOver.TabIndex = 4;
            this.butOver.Text = "放行";
            this.butOver.UseVisualStyleBackColor = true;
            this.butOver.Click += new System.EventHandler(this.butOver_Click);
            // 
            // lblcheckOver
            // 
            this.lblcheckOver.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblcheckOver.ForeColor = System.Drawing.Color.Blue;
            this.lblcheckOver.Location = new System.Drawing.Point(92, 152);
            this.lblcheckOver.Name = "lblcheckOver";
            this.lblcheckOver.Size = new System.Drawing.Size(184, 30);
            this.lblcheckOver.TabIndex = 1;
            this.lblcheckOver.Text = "等待检测结束……";
            this.lblcheckOver.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblAllowCheck
            // 
            this.lblAllowCheck.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAllowCheck.ForeColor = System.Drawing.Color.Blue;
            this.lblAllowCheck.Location = new System.Drawing.Point(92, 38);
            this.lblAllowCheck.Name = "lblAllowCheck";
            this.lblAllowCheck.Size = new System.Drawing.Size(184, 30);
            this.lblAllowCheck.TabIndex = 1;
            this.lblAllowCheck.Text = "待命……";
            this.lblAllowCheck.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblchecking
            // 
            this.lblchecking.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblchecking.ForeColor = System.Drawing.Color.Blue;
            this.lblchecking.Location = new System.Drawing.Point(92, 112);
            this.lblchecking.Name = "lblchecking";
            this.lblchecking.Size = new System.Drawing.Size(184, 30);
            this.lblchecking.TabIndex = 1;
            this.lblchecking.Text = "检定中……";
            this.lblchecking.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtBarcode
            // 
            this.txtBarcode.Location = new System.Drawing.Point(92, 78);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Size = new System.Drawing.Size(184, 26);
            this.txtBarcode.TabIndex = 2;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 159);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 16);
            this.label6.TabIndex = 1;
            this.label6.Text = "结束放行：";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(11, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 16);
            this.label5.TabIndex = 1;
            this.label5.Text = "检定状态：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(11, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 16);
            this.label4.TabIndex = 1;
            this.label4.Text = "条码信息：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 45);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(88, 16);
            this.label3.TabIndex = 1;
            this.label3.Text = "到位状态：";
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.Control;
            this.tabPage2.Controls.Add(this.txbAnalyse);
            this.tabPage2.Controls.Add(this.txbCurrentAnalyse);
            this.tabPage2.Controls.Add(this.label13);
            this.tabPage2.Controls.Add(this.txbAnalyseCode);
            this.tabPage2.Controls.Add(this.btnAnalyse);
            this.tabPage2.Controls.Add(this.btnChooseAnalyse);
            this.tabPage2.Controls.Add(this.txbCode);
            this.tabPage2.Controls.Add(this.btnGetCode);
            this.tabPage2.Controls.Add(this.dgvConfig);
            this.tabPage2.Controls.Add(this.grpSerialPort);
            this.tabPage2.Location = new System.Drawing.Point(4, 26);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(1034, 629);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "配置信息";
            // 
            // txbAnalyse
            // 
            this.txbAnalyse.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbAnalyse.Location = new System.Drawing.Point(638, 400);
            this.txbAnalyse.Multiline = true;
            this.txbAnalyse.Name = "txbAnalyse";
            this.txbAnalyse.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txbAnalyse.Size = new System.Drawing.Size(388, 223);
            this.txbAnalyse.TabIndex = 12;
            // 
            // txbCurrentAnalyse
            // 
            this.txbCurrentAnalyse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbCurrentAnalyse.Location = new System.Drawing.Point(852, 206);
            this.txbCurrentAnalyse.Name = "txbCurrentAnalyse";
            this.txbCurrentAnalyse.Size = new System.Drawing.Size(174, 26);
            this.txbCurrentAnalyse.TabIndex = 11;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("宋体", 18F);
            this.label13.Location = new System.Drawing.Point(638, 202);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(226, 24);
            this.label13.TabIndex = 10;
            this.label13.Text = "当前解析配置文件：";
            // 
            // txbAnalyseCode
            // 
            this.txbAnalyseCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbAnalyseCode.Location = new System.Drawing.Point(638, 238);
            this.txbAnalyseCode.Multiline = true;
            this.txbAnalyseCode.Name = "txbAnalyseCode";
            this.txbAnalyseCode.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txbAnalyseCode.Size = new System.Drawing.Size(388, 156);
            this.txbAnalyseCode.TabIndex = 9;
            // 
            // btnAnalyse
            // 
            this.btnAnalyse.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAnalyse.Font = new System.Drawing.Font("宋体", 18F);
            this.btnAnalyse.Location = new System.Drawing.Point(586, 400);
            this.btnAnalyse.Name = "btnAnalyse";
            this.btnAnalyse.Size = new System.Drawing.Size(46, 226);
            this.btnAnalyse.TabIndex = 8;
            this.btnAnalyse.Text = "解析报文";
            this.btnAnalyse.UseVisualStyleBackColor = true;
            this.btnAnalyse.Click += new System.EventHandler(this.btnAnalyse_Click);
            // 
            // btnChooseAnalyse
            // 
            this.btnChooseAnalyse.Font = new System.Drawing.Font("宋体", 18F);
            this.btnChooseAnalyse.Location = new System.Drawing.Point(586, 184);
            this.btnChooseAnalyse.Name = "btnChooseAnalyse";
            this.btnChooseAnalyse.Size = new System.Drawing.Size(46, 210);
            this.btnChooseAnalyse.TabIndex = 7;
            this.btnChooseAnalyse.Text = "选择解析配置文件";
            this.btnChooseAnalyse.UseVisualStyleBackColor = true;
            this.btnChooseAnalyse.Click += new System.EventHandler(this.btnChooseAnalyse_Click);
            // 
            // txbCode
            // 
            this.txbCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txbCode.Location = new System.Drawing.Point(464, 6);
            this.txbCode.Multiline = true;
            this.txbCode.Name = "txbCode";
            this.txbCode.ReadOnly = true;
            this.txbCode.Size = new System.Drawing.Size(562, 172);
            this.txbCode.TabIndex = 6;
            // 
            // btnGetCode
            // 
            this.btnGetCode.Font = new System.Drawing.Font("宋体", 18F);
            this.btnGetCode.Location = new System.Drawing.Point(410, 6);
            this.btnGetCode.Name = "btnGetCode";
            this.btnGetCode.Size = new System.Drawing.Size(48, 172);
            this.btnGetCode.TabIndex = 5;
            this.btnGetCode.Text = "组报文";
            this.btnGetCode.UseVisualStyleBackColor = true;
            this.btnGetCode.Click += new System.EventHandler(this.btnGetCode_Click);
            // 
            // dgvConfig
            // 
            this.dgvConfig.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.dgvConfig.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dgvConfig.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvConfig.Location = new System.Drawing.Point(8, 184);
            this.dgvConfig.Name = "dgvConfig";
            this.dgvConfig.RowTemplate.Height = 23;
            this.dgvConfig.Size = new System.Drawing.Size(572, 442);
            this.dgvConfig.TabIndex = 4;
            // 
            // grpSerialPort
            // 
            this.grpSerialPort.Controls.Add(this.txbCurrentIni);
            this.grpSerialPort.Controls.Add(this.label12);
            this.grpSerialPort.Controls.Add(this.btnChooseIni);
            this.grpSerialPort.Controls.Add(this.cboMeterType);
            this.grpSerialPort.Controls.Add(this.label11);
            this.grpSerialPort.Controls.Add(this.butStart);
            this.grpSerialPort.Controls.Add(this.label2);
            this.grpSerialPort.Controls.Add(this.label1);
            this.grpSerialPort.Controls.Add(this.cbofjport);
            this.grpSerialPort.Controls.Add(this.cbocheckport);
            this.grpSerialPort.Location = new System.Drawing.Point(8, 6);
            this.grpSerialPort.Name = "grpSerialPort";
            this.grpSerialPort.Size = new System.Drawing.Size(396, 172);
            this.grpSerialPort.TabIndex = 3;
            this.grpSerialPort.TabStop = false;
            this.grpSerialPort.Text = "串口配置";
            // 
            // txbCurrentIni
            // 
            this.txbCurrentIni.Location = new System.Drawing.Point(150, 140);
            this.txbCurrentIni.Name = "txbCurrentIni";
            this.txbCurrentIni.ReadOnly = true;
            this.txbCurrentIni.Size = new System.Drawing.Size(136, 26);
            this.txbCurrentIni.TabIndex = 8;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(0, 143);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(112, 16);
            this.label12.TabIndex = 7;
            this.label12.Text = "当前选中配置:";
            // 
            // btnChooseIni
            // 
            this.btnChooseIni.Font = new System.Drawing.Font("宋体", 18F);
            this.btnChooseIni.Location = new System.Drawing.Point(297, 101);
            this.btnChooseIni.Name = "btnChooseIni";
            this.btnChooseIni.Size = new System.Drawing.Size(83, 65);
            this.btnChooseIni.TabIndex = 5;
            this.btnChooseIni.Text = "选择配置文件";
            this.btnChooseIni.UseVisualStyleBackColor = true;
            this.btnChooseIni.Click += new System.EventHandler(this.btnChooseIni_Click);
            // 
            // cboMeterType
            // 
            this.cboMeterType.FormattingEnabled = true;
            this.cboMeterType.Items.AddRange(new object[] {
            "单相",
            "三相"});
            this.cboMeterType.Location = new System.Drawing.Point(150, 98);
            this.cboMeterType.Name = "cboMeterType";
            this.cboMeterType.Size = new System.Drawing.Size(136, 24);
            this.cboMeterType.TabIndex = 6;
            this.cboMeterType.SelectedIndexChanged += new System.EventHandler(this.cboMeterType_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(0, 101);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(88, 16);
            this.label11.TabIndex = 5;
            this.label11.Text = "设备类型：";
            // 
            // butStart
            // 
            this.butStart.Font = new System.Drawing.Font("宋体", 18F);
            this.butStart.Location = new System.Drawing.Point(297, 25);
            this.butStart.Name = "butStart";
            this.butStart.Size = new System.Drawing.Size(83, 58);
            this.butStart.TabIndex = 4;
            this.butStart.Text = "开始";
            this.butStart.UseVisualStyleBackColor = true;
            this.butStart.Click += new System.EventHandler(this.butStart_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(0, 63);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "分拣线485通讯口：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 28);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "检定线485通讯口：";
            // 
            // cbofjport
            // 
            this.cbofjport.FormattingEnabled = true;
            this.cbofjport.Location = new System.Drawing.Point(150, 60);
            this.cbofjport.Name = "cbofjport";
            this.cbofjport.Size = new System.Drawing.Size(136, 24);
            this.cbofjport.TabIndex = 0;
            // 
            // cbocheckport
            // 
            this.cbocheckport.FormattingEnabled = true;
            this.cbocheckport.Location = new System.Drawing.Point(150, 25);
            this.cbocheckport.Name = "cbocheckport";
            this.cbocheckport.Size = new System.Drawing.Size(136, 24);
            this.cbocheckport.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Location = new System.Drawing.Point(4, 26);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1034, 629);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "报文解析";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // FrmPcbTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1042, 659);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "FrmPcbTest";
            this.Text = "电路板测试系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FrmPcbTest_Load);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.grpFenjian.ResumeLayout(false);
            this.grpFenjian.PerformLayout();
            this.grpCheck.ResumeLayout(false);
            this.grpCheck.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvConfig)).EndInit();
            this.grpSerialPort.ResumeLayout(false);
            this.grpSerialPort.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox grpSerialPort;
        private System.Windows.Forms.Button butStart;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbofjport;
        private System.Windows.Forms.ComboBox cbocheckport;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ComboBox cboMeterType;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox grpCheck;
        private System.Windows.Forms.Button butAdjust_UI;
        private System.Windows.Forms.Button butOver;
        private System.Windows.Forms.Label lblcheckOver;
        private System.Windows.Forms.Label lblAllowCheck;
        private System.Windows.Forms.Label lblchecking;
        private System.Windows.Forms.TextBox txtBarcode;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DataGridView dataGridView;
        private System.Windows.Forms.GroupBox grpFenjian;
        private System.Windows.Forms.Button butFJOver;
        private System.Windows.Forms.Label lblfjOver;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblAllowfJ;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtBarcodeFJ;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 全选ToolStripMenuItem;
        private System.Windows.Forms.DataGridView dgvConfig;
        private System.Windows.Forms.Button btnChooseIni;
        private System.Windows.Forms.TextBox txbCurrentIni;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox txbCode;
        private System.Windows.Forms.Button btnGetCode;
        private System.Windows.Forms.TextBox txbAnalyseCode;
        private System.Windows.Forms.Button btnAnalyse;
        private System.Windows.Forms.Button btnChooseAnalyse;
        private System.Windows.Forms.TextBox txbAnalyse;
        private System.Windows.Forms.TextBox txbCurrentAnalyse;
        private System.Windows.Forms.Label label13;
    }
}

