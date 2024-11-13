
namespace WslGUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.terminateBtn = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.bindingSource1 = new System.Windows.Forms.BindingSource(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.importBtn = new System.Windows.Forms.Button();
            this.movebtn = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.setDefaultVersionBtn = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.exportBtn = new System.Windows.Forms.Button();
            this.setDefaultBtn = new System.Windows.Forms.Button();
            this.setVersionBtn = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.shutdownBtn = new System.Windows.Forms.Button();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.vmmemUsageLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.refreshLbl = new System.Windows.Forms.ToolStripStatusLabel();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // terminateBtn
            // 
            this.terminateBtn.Location = new System.Drawing.Point(111, 185);
            this.terminateBtn.Name = "terminateBtn";
            this.terminateBtn.Size = new System.Drawing.Size(119, 23);
            this.terminateBtn.TabIndex = 0;
            this.terminateBtn.Text = "Terminate";
            this.terminateBtn.UseVisualStyleBackColor = true;
            this.terminateBtn.Click += new System.EventHandler(this.terminateBtn_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.RowTemplate.Height = 25;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(500, 450);
            this.dataGridView1.TabIndex = 1;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
            this.splitContainer1.Panel1MinSize = 500;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.importBtn);
            this.splitContainer1.Panel2.Controls.Add(this.movebtn);
            this.splitContainer1.Panel2.Controls.Add(this.richTextBox1);
            this.splitContainer1.Panel2.Controls.Add(this.button1);
            this.splitContainer1.Panel2.Controls.Add(this.setDefaultVersionBtn);
            this.splitContainer1.Panel2.Controls.Add(this.comboBox2);
            this.splitContainer1.Panel2.Controls.Add(this.exportBtn);
            this.splitContainer1.Panel2.Controls.Add(this.setDefaultBtn);
            this.splitContainer1.Panel2.Controls.Add(this.setVersionBtn);
            this.splitContainer1.Panel2.Controls.Add(this.comboBox1);
            this.splitContainer1.Panel2.Controls.Add(this.shutdownBtn);
            this.splitContainer1.Panel2.Controls.Add(this.splitter1);
            this.splitContainer1.Panel2.Controls.Add(this.terminateBtn);
            this.splitContainer1.Size = new System.Drawing.Size(800, 450);
            this.splitContainer1.SplitterDistance = 500;
            this.splitContainer1.TabIndex = 2;
            // 
            // importBtn
            // 
            this.importBtn.Location = new System.Drawing.Point(18, 185);
            this.importBtn.Name = "importBtn";
            this.importBtn.Size = new System.Drawing.Size(85, 23);
            this.importBtn.TabIndex = 15;
            this.importBtn.Text = "Import";
            this.importBtn.UseVisualStyleBackColor = true;
            this.importBtn.Click += new System.EventHandler(this.ImportBtnClick);
            // 
            // movebtn
            // 
            this.movebtn.Location = new System.Drawing.Point(18, 156);
            this.movebtn.Name = "movebtn";
            this.movebtn.Size = new System.Drawing.Size(85, 23);
            this.movebtn.TabIndex = 14;
            this.movebtn.Text = "Move";
            this.movebtn.UseVisualStyleBackColor = true;
            this.movebtn.Click += new System.EventHandler(this.MovebtnClick);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.SystemColors.Menu;
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Location = new System.Drawing.Point(38, 288);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(246, 96);
            this.richTextBox1.TabIndex = 13;
            this.richTextBox1.Text = "";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(111, 243);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(119, 23);
            this.button1.TabIndex = 12;
            this.button1.Text = "Start in new window";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // setDefaultVersionBtn
            // 
            this.setDefaultVersionBtn.Location = new System.Drawing.Point(165, 398);
            this.setDefaultVersionBtn.Name = "setDefaultVersionBtn";
            this.setDefaultVersionBtn.Size = new System.Drawing.Size(119, 23);
            this.setDefaultVersionBtn.TabIndex = 11;
            this.setDefaultVersionBtn.Text = "Set default version";
            this.setDefaultVersionBtn.UseVisualStyleBackColor = true;
            // 
            // comboBox2
            // 
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "1",
            "2"});
            this.comboBox2.Location = new System.Drawing.Point(38, 399);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(121, 23);
            this.comboBox2.TabIndex = 10;
            // 
            // exportBtn
            // 
            this.exportBtn.Location = new System.Drawing.Point(109, 156);
            this.exportBtn.Name = "exportBtn";
            this.exportBtn.Size = new System.Drawing.Size(119, 23);
            this.exportBtn.TabIndex = 9;
            this.exportBtn.Text = "Export";
            this.exportBtn.UseVisualStyleBackColor = true;
            this.exportBtn.Click += new System.EventHandler(this.ExportBtnClick);
            // 
            // setDefaultBtn
            // 
            this.setDefaultBtn.Location = new System.Drawing.Point(165, 125);
            this.setDefaultBtn.Name = "setDefaultBtn";
            this.setDefaultBtn.Size = new System.Drawing.Size(119, 23);
            this.setDefaultBtn.TabIndex = 7;
            this.setDefaultBtn.Text = "Set as default";
            this.setDefaultBtn.UseVisualStyleBackColor = true;
            this.setDefaultBtn.Click += new System.EventHandler(this.SetDefaultBtnClick);
            // 
            // setVersionBtn
            // 
            this.setVersionBtn.Location = new System.Drawing.Point(165, 96);
            this.setVersionBtn.Name = "setVersionBtn";
            this.setVersionBtn.Size = new System.Drawing.Size(119, 23);
            this.setVersionBtn.TabIndex = 6;
            this.setVersionBtn.Text = "Set version";
            this.setVersionBtn.UseVisualStyleBackColor = true;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "1",
            "2"});
            this.comboBox1.Location = new System.Drawing.Point(16, 96);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 23);
            this.comboBox1.TabIndex = 5;
            // 
            // shutdownBtn
            // 
            this.shutdownBtn.Location = new System.Drawing.Point(111, 214);
            this.shutdownBtn.Name = "shutdownBtn";
            this.shutdownBtn.Size = new System.Drawing.Size(119, 23);
            this.shutdownBtn.TabIndex = 4;
            this.shutdownBtn.Text = "Shutdown (all)";
            this.shutdownBtn.UseVisualStyleBackColor = true;
            this.shutdownBtn.Click += new System.EventHandler(this.shutdownBtn_Click);
            // 
            // splitter1
            // 
            this.splitter1.Location = new System.Drawing.Point(0, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(10, 450);
            this.splitter1.TabIndex = 1;
            this.splitter1.TabStop = false;
            // 
            // refreshTimer
            // 
            this.refreshTimer.Interval = 1000;
            this.refreshTimer.Tick += new System.EventHandler(this.RefreshTimer_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1,
            this.toolStripProgressBar1,
            this.vmmemUsageLbl,
            this.refreshLbl});
            this.statusStrip1.Location = new System.Drawing.Point(0, 512);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 3;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(53, 17);
            this.toolStripStatusLabel1.Text = "VMMEM";
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(100, 16);
            this.toolStripProgressBar1.Value = 50;
            // 
            // vmmemUsageLbl
            // 
            this.vmmemUsageLbl.Name = "vmmemUsageLbl";
            this.vmmemUsageLbl.Size = new System.Drawing.Size(43, 17);
            this.vmmemUsageLbl.Text = "USAGE";
            // 
            // refreshLbl
            // 
            this.refreshLbl.Name = "refreshLbl";
            this.refreshLbl.Size = new System.Drawing.Size(72, 17);
            this.refreshLbl.Text = "Refreshing...";
            this.refreshLbl.Visible = false;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 534);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.splitContainer1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.bindingSource1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button terminateBtn;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.BindingSource bindingSource1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.Timer refreshTimer;
        private System.Windows.Forms.Button shutdownBtn;
        private System.Windows.Forms.Button setVersionBtn;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Button setDefaultBtn;
        private System.Windows.Forms.Button exportBtn;
        private System.Windows.Forms.Button setDefaultVersionBtn;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripStatusLabel vmmemUsageLbl;
        private System.Windows.Forms.ToolStripStatusLabel refreshLbl;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button movebtn;
        private System.Windows.Forms.Button importBtn;
    }
}

