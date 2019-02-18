namespace MyOS
{
    partial class Simulation
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.cpuText = new System.Windows.Forms.TextBox();
            this.memoryText = new System.Windows.Forms.TextBox();
            this.limitText = new System.Windows.Forms.TextBox();
            this.baseText = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.processText = new System.Windows.Forms.TextBox();
            this.resourcesList = new System.Windows.Forms.ListBox();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.idLabel = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.startToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.stopToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.jqLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.jqList = new System.Windows.Forms.ListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.rqLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.rqList = new System.Windows.Forms.ListBox();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel3 = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel6 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel7 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.dqLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.dqList = new System.Windows.Forms.ListBox();
            this.processesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSet = new MyOS.DataSet();
            this.dataSetBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.processBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.dataSetBindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.ID = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Base = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Limit = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MemorySize = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.State = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Burst_Time = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.groupBox3.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.processesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.processBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetBindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.BackColor = System.Drawing.SystemColors.Control;
            this.groupBox3.Controls.Add(this.label11);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.cpuText);
            this.groupBox3.Controls.Add(this.memoryText);
            this.groupBox3.Controls.Add(this.limitText);
            this.groupBox3.Controls.Add(this.baseText);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Controls.Add(this.processText);
            this.groupBox3.Controls.Add(this.resourcesList);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Controls.Add(this.label3);
            this.groupBox3.Controls.Add(this.label2);
            this.groupBox3.Controls.Add(this.idLabel);
            this.groupBox3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox3.Location = new System.Drawing.Point(304, 27);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(252, 189);
            this.groupBox3.TabIndex = 2;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Processor";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(225, 108);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(24, 13);
            this.label11.TabIndex = 23;
            this.label11.Text = "sec";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(225, 79);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(19, 13);
            this.label10.TabIndex = 22;
            this.label10.Text = "kb";
            // 
            // cpuText
            // 
            this.cpuText.Location = new System.Drawing.Point(184, 105);
            this.cpuText.Name = "cpuText";
            this.cpuText.Size = new System.Drawing.Size(40, 20);
            this.cpuText.TabIndex = 21;
            // 
            // memoryText
            // 
            this.memoryText.Location = new System.Drawing.Point(184, 76);
            this.memoryText.Name = "memoryText";
            this.memoryText.Size = new System.Drawing.Size(40, 20);
            this.memoryText.TabIndex = 20;
            // 
            // limitText
            // 
            this.limitText.Location = new System.Drawing.Point(74, 105);
            this.limitText.Name = "limitText";
            this.limitText.Size = new System.Drawing.Size(40, 20);
            this.limitText.TabIndex = 19;
            // 
            // baseText
            // 
            this.baseText.Location = new System.Drawing.Point(74, 76);
            this.baseText.Name = "baseText";
            this.baseText.Size = new System.Drawing.Size(40, 20);
            this.baseText.TabIndex = 18;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(7, 108);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(31, 13);
            this.label7.TabIndex = 17;
            this.label7.Text = "Limit:";
            // 
            // processText
            // 
            this.processText.Location = new System.Drawing.Point(74, 44);
            this.processText.Name = "processText";
            this.processText.Size = new System.Drawing.Size(150, 20);
            this.processText.TabIndex = 9;
            this.processText.Text = "No Process Running";
            this.processText.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // resourcesList
            // 
            this.resourcesList.FormattingEnabled = true;
            this.resourcesList.Location = new System.Drawing.Point(74, 136);
            this.resourcesList.Name = "resourcesList";
            this.resourcesList.Size = new System.Drawing.Size(150, 43);
            this.resourcesList.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(7, 153);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(61, 13);
            this.label9.TabIndex = 6;
            this.label9.Text = "Resources:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(148, 108);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 13);
            this.label8.TabIndex = 5;
            this.label8.Text = "Time:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 79);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 13);
            this.label5.TabIndex = 3;
            this.label5.Text = "Base:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(148, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Size:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(44, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(132, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Currently Running Process";
            // 
            // idLabel
            // 
            this.idLabel.AutoSize = true;
            this.idLabel.Location = new System.Drawing.Point(6, 47);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(62, 13);
            this.idLabel.TabIndex = 0;
            this.idLabel.Text = "Process ID:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.Desktop;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.startToolStripMenuItem,
            this.stopToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(715, 24);
            this.menuStrip1.TabIndex = 4;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // startToolStripMenuItem
            // 
            this.startToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Window;
            this.startToolStripMenuItem.Name = "startToolStripMenuItem";
            this.startToolStripMenuItem.Size = new System.Drawing.Size(43, 20);
            this.startToolStripMenuItem.Text = "Start";
            this.startToolStripMenuItem.Click += new System.EventHandler(this.startToolStripMenuItem_Click);
            // 
            // stopToolStripMenuItem
            // 
            this.stopToolStripMenuItem.ForeColor = System.Drawing.SystemColors.Window;
            this.stopToolStripMenuItem.Name = "stopToolStripMenuItem";
            this.stopToolStripMenuItem.Size = new System.Drawing.Size(41, 20);
            this.stopToolStripMenuItem.Text = "Stop";
            this.stopToolStripMenuItem.Click += new System.EventHandler(this.stopToolStripMenuItem_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.jqLabel);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.jqList);
            this.groupBox2.Location = new System.Drawing.Point(12, 27);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(143, 189);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Job Queue";
            // 
            // jqLabel
            // 
            this.jqLabel.AutoSize = true;
            this.jqLabel.Location = new System.Drawing.Point(123, 169);
            this.jqLabel.Name = "jqLabel";
            this.jqLabel.Size = new System.Drawing.Size(13, 13);
            this.jqLabel.TabIndex = 10;
            this.jqLabel.Text = "0";
            this.jqLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 169);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(38, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "Count:";
            // 
            // jqList
            // 
            this.jqList.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.jqList.FormattingEnabled = true;
            this.jqList.Location = new System.Drawing.Point(6, 19);
            this.jqList.MultiColumn = true;
            this.jqList.Name = "jqList";
            this.jqList.Size = new System.Drawing.Size(130, 147);
            this.jqList.TabIndex = 6;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.rqLabel);
            this.groupBox4.Controls.Add(this.label4);
            this.groupBox4.Controls.Add(this.rqList);
            this.groupBox4.Location = new System.Drawing.Point(12, 222);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(143, 190);
            this.groupBox4.TabIndex = 8;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Ready Queue";
            // 
            // rqLabel
            // 
            this.rqLabel.AutoSize = true;
            this.rqLabel.Location = new System.Drawing.Point(123, 169);
            this.rqLabel.Name = "rqLabel";
            this.rqLabel.Size = new System.Drawing.Size(13, 13);
            this.rqLabel.TabIndex = 12;
            this.rqLabel.Text = "0";
            this.rqLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 169);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Count:";
            // 
            // rqList
            // 
            this.rqList.BackColor = System.Drawing.Color.Khaki;
            this.rqList.FormattingEnabled = true;
            this.rqList.Location = new System.Drawing.Point(6, 19);
            this.rqList.MultiColumn = true;
            this.rqList.Name = "rqList";
            this.rqList.Size = new System.Drawing.Size(130, 147);
            this.rqList.TabIndex = 6;
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(98, 17);
            this.toolStripStatusLabel1.Text = "Job Queue Monitor";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.ForeColor = System.Drawing.Color.Khaki;
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(112, 17);
            this.toolStripStatusLabel2.Text = "Ready Queue Monitor";
            // 
            // toolStripStatusLabel3
            // 
            this.toolStripStatusLabel3.ForeColor = System.Drawing.Color.MistyRose;
            this.toolStripStatusLabel3.Name = "toolStripStatusLabel3";
            this.toolStripStatusLabel3.Size = new System.Drawing.Size(93, 17);
            this.toolStripStatusLabel3.Text = "Processor Monitor";
            // 
            // statusStrip1
            // 
            this.statusStrip1.BackColor = System.Drawing.SystemColors.Desktop;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripStatusLabel5,
            this.toolStripStatusLabel2,
            this.toolStripStatusLabel6,
            this.toolStripStatusLabel3,
            this.toolStripStatusLabel7,
            this.toolStripStatusLabel4});
            this.statusStrip1.Location = new System.Drawing.Point(0, 426);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.Professional;
            this.statusStrip1.Size = new System.Drawing.Size(715, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(13, 17);
            this.toolStripStatusLabel5.Text = "*";
            // 
            // toolStripStatusLabel6
            // 
            this.toolStripStatusLabel6.ForeColor = System.Drawing.SystemColors.Window;
            this.toolStripStatusLabel6.Name = "toolStripStatusLabel6";
            this.toolStripStatusLabel6.Size = new System.Drawing.Size(13, 17);
            this.toolStripStatusLabel6.Text = "*";
            // 
            // toolStripStatusLabel7
            // 
            this.toolStripStatusLabel7.ForeColor = System.Drawing.SystemColors.Window;
            this.toolStripStatusLabel7.Name = "toolStripStatusLabel7";
            this.toolStripStatusLabel7.Size = new System.Drawing.Size(13, 17);
            this.toolStripStatusLabel7.Text = "*";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.ForeColor = System.Drawing.Color.LightGreen;
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(113, 17);
            this.toolStripStatusLabel4.Text = "Device Queue Monitor";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.dqLabel);
            this.groupBox5.Controls.Add(this.label6);
            this.groupBox5.Controls.Add(this.dqList);
            this.groupBox5.Location = new System.Drawing.Point(562, 27);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(143, 189);
            this.groupBox5.TabIndex = 9;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Device Queue";
            // 
            // dqLabel
            // 
            this.dqLabel.AutoSize = true;
            this.dqLabel.Location = new System.Drawing.Point(123, 169);
            this.dqLabel.Name = "dqLabel";
            this.dqLabel.Size = new System.Drawing.Size(13, 13);
            this.dqLabel.TabIndex = 10;
            this.dqLabel.Text = "0";
            this.dqLabel.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(6, 169);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 9;
            this.label6.Text = "Count:";
            // 
            // dqList
            // 
            this.dqList.BackColor = System.Drawing.Color.LightGreen;
            this.dqList.FormattingEnabled = true;
            this.dqList.Location = new System.Drawing.Point(6, 19);
            this.dqList.MultiColumn = true;
            this.dqList.Name = "dqList";
            this.dqList.Size = new System.Drawing.Size(130, 147);
            this.dqList.TabIndex = 6;
            // 
            // dataSet
            // 
            this.dataSet.DataSetName = "DataSet";
            this.dataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // dataSetBindingSource
            // 
            this.dataSetBindingSource.DataSource = this.dataSet;
            this.dataSetBindingSource.Position = 0;
            // 
            // processBindingSource
            // 
            this.processBindingSource.DataMember = "Process";
            this.processBindingSource.DataSource = this.dataSet;
            // 
            // dataSetBindingSource1
            // 
            this.dataSetBindingSource1.DataSource = this.dataSet;
            this.dataSetBindingSource1.Position = 0;
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.ID,
            this.Base,
            this.Limit,
            this.MemorySize,
            this.State,
            this.Burst_Time});
            this.dataGridView1.Location = new System.Drawing.Point(304, 222);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.Size = new System.Drawing.Size(401, 190);
            this.dataGridView1.TabIndex = 10;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // ID
            // 
            this.ID.FillWeight = 30F;
            this.ID.HeaderText = "ID";
            this.ID.MinimumWidth = 3;
            this.ID.Name = "ID";
            this.ID.ReadOnly = true;
            this.ID.Width = 30;
            // 
            // Base
            // 
            this.Base.FillWeight = 40F;
            this.Base.HeaderText = "Base";
            this.Base.MaxInputLength = 1000;
            this.Base.Name = "Base";
            this.Base.ReadOnly = true;
            this.Base.Width = 40;
            // 
            // Limit
            // 
            this.Limit.FillWeight = 40F;
            this.Limit.HeaderText = "Limit";
            this.Limit.MaxInputLength = 10000;
            this.Limit.Name = "Limit";
            this.Limit.ReadOnly = true;
            this.Limit.Width = 40;
            // 
            // MemorySize
            // 
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.MemorySize.DefaultCellStyle = dataGridViewCellStyle3;
            this.MemorySize.FillWeight = 82F;
            this.MemorySize.HeaderText = "Memory Size";
            this.MemorySize.Name = "MemorySize";
            this.MemorySize.ReadOnly = true;
            this.MemorySize.Width = 82;
            // 
            // State
            // 
            this.State.FillWeight = 85F;
            this.State.HeaderText = "State";
            this.State.Name = "State";
            this.State.ReadOnly = true;
            this.State.Width = 85;
            // 
            // Burst_Time
            // 
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.TopCenter;
            this.Burst_Time.DefaultCellStyle = dataGridViewCellStyle4;
            this.Burst_Time.FillWeight = 80F;
            this.Burst_Time.HeaderText = "Remaining Burst Time";
            this.Burst_Time.Name = "Burst_Time";
            this.Burst_Time.ReadOnly = true;
            this.Burst_Time.Width = 80;
            // 
            // Simulation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ClientSize = new System.Drawing.Size(715, 448);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.groupBox5);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Controls.Add(this.groupBox3);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Simulation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Simulation";
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.processesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.processBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataSetBindingSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem startToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem stopToolStripMenuItem;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.ListBox rqList;
        private System.Windows.Forms.ListBox jqList;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel3;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.Label jqLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label rqLabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label dqLabel;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ListBox dqList;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel6;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel7;
        private System.Windows.Forms.Label idLabel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox resourcesList;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox processText;
        private System.Windows.Forms.TextBox baseText;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox cpuText;
        private System.Windows.Forms.TextBox memoryText;
        private System.Windows.Forms.TextBox limitText;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.BindingSource processesBindingSource;
        private System.Windows.Forms.BindingSource dataSetBindingSource1;
        private DataSet dataSet;
        private System.Windows.Forms.BindingSource dataSetBindingSource;
        private System.Windows.Forms.BindingSource processBindingSource;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn ID;
        private System.Windows.Forms.DataGridViewTextBoxColumn Base;
        private System.Windows.Forms.DataGridViewTextBoxColumn Limit;
        private System.Windows.Forms.DataGridViewTextBoxColumn MemorySize;
        private System.Windows.Forms.DataGridViewTextBoxColumn State;
        private System.Windows.Forms.DataGridViewTextBoxColumn Burst_Time;
    }
}