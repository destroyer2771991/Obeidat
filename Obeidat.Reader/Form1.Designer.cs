namespace Obeidat.Reader
{
    partial class Form1
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
            this.pgSql = new System.Windows.Forms.TabPage();
            this.btnLoadData = new System.Windows.Forms.Button();
            this.combLevel = new System.Windows.Forms.ComboBox();
            this.txtConnectionString = new System.Windows.Forms.TextBox();
            this.pgFile = new System.Windows.Forms.TabPage();
            this.button2 = new System.Windows.Forms.Button();
            this.txtFilePath = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.fileSelected = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.btnApply = new System.Windows.Forms.Button();
            this.Method = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.URL = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.IP = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.Username = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.Client = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.Data = new System.Windows.Forms.TextBox();
            this.txtInfo = new System.Windows.Forms.RichTextBox();
            this.dataGrid = new System.Windows.Forms.DataGridView();
            this.label11 = new System.Windows.Forms.Label();
            this.FileName = new System.Windows.Forms.TextBox();
            this.Caption = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.LineNumber = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.DeviceName = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.txtBeginTime = new System.Windows.Forms.DateTimePicker();
            this.txEndTime = new System.Windows.Forms.DateTimePicker();
            this.Filters = new System.Windows.Forms.GroupBox();
            this.datePickerBegin = new System.Windows.Forms.DateTimePicker();
            this.datePickerEnd = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txtProjectName = new System.Windows.Forms.TextBox();
            this.pgSql.SuspendLayout();
            this.pgFile.SuspendLayout();
            this.tabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).BeginInit();
            this.Filters.SuspendLayout();
            this.SuspendLayout();
            // 
            // pgSql
            // 
            this.pgSql.Controls.Add(this.txtProjectName);
            this.pgSql.Controls.Add(this.label9);
            this.pgSql.Controls.Add(this.label2);
            this.pgSql.Controls.Add(this.datePickerEnd);
            this.pgSql.Controls.Add(this.datePickerBegin);
            this.pgSql.Controls.Add(this.btnLoadData);
            this.pgSql.Controls.Add(this.combLevel);
            this.pgSql.Controls.Add(this.txtConnectionString);
            this.pgSql.Location = new System.Drawing.Point(4, 22);
            this.pgSql.Name = "pgSql";
            this.pgSql.Padding = new System.Windows.Forms.Padding(3);
            this.pgSql.Size = new System.Drawing.Size(913, 66);
            this.pgSql.TabIndex = 1;
            this.pgSql.Text = "SQL";
            this.pgSql.UseVisualStyleBackColor = true;
            // 
            // btnLoadData
            // 
            this.btnLoadData.Location = new System.Drawing.Point(828, 17);
            this.btnLoadData.Name = "btnLoadData";
            this.btnLoadData.Size = new System.Drawing.Size(79, 35);
            this.btnLoadData.TabIndex = 19;
            this.btnLoadData.Text = "Load data";
            this.btnLoadData.UseVisualStyleBackColor = true;
            this.btnLoadData.Click += new System.EventHandler(this.btnLoadData_Click);
            // 
            // combLevel
            // 
            this.combLevel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.combLevel.FormattingEnabled = true;
            this.combLevel.Items.AddRange(new object[] {
            "Exception",
            "Information",
            "Debug"});
            this.combLevel.Location = new System.Drawing.Point(486, 31);
            this.combLevel.Name = "combLevel";
            this.combLevel.Size = new System.Drawing.Size(121, 21);
            this.combLevel.TabIndex = 6;
            this.combLevel.Text = "Level";
            // 
            // txtConnectionString
            // 
            this.txtConnectionString.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtConnectionString.Location = new System.Drawing.Point(6, 7);
            this.txtConnectionString.Name = "txtConnectionString";
            this.txtConnectionString.Size = new System.Drawing.Size(601, 22);
            this.txtConnectionString.TabIndex = 3;
            this.txtConnectionString.Text = "Insert log sql connection string...";
            this.txtConnectionString.Enter += new System.EventHandler(this.txtConnectionString_Enter);
            this.txtConnectionString.Leave += new System.EventHandler(this.txtConnectionString_Leave);
            // 
            // pgFile
            // 
            this.pgFile.Controls.Add(this.button2);
            this.pgFile.Controls.Add(this.txtFilePath);
            this.pgFile.Controls.Add(this.button1);
            this.pgFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pgFile.Location = new System.Drawing.Point(4, 22);
            this.pgFile.Name = "pgFile";
            this.pgFile.Padding = new System.Windows.Forms.Padding(3);
            this.pgFile.Size = new System.Drawing.Size(913, 66);
            this.pgFile.TabIndex = 0;
            this.pgFile.Text = "File (text or Xml)";
            this.pgFile.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(815, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(92, 35);
            this.button2.TabIndex = 35;
            this.button2.Text = "Load data";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // txtFilePath
            // 
            this.txtFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFilePath.Location = new System.Drawing.Point(138, 20);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.Size = new System.Drawing.Size(666, 21);
            this.txtFilePath.TabIndex = 1;
            this.txtFilePath.Text = "Or insert file path here ...";
            this.txtFilePath.Enter += new System.EventHandler(this.txtFilePath_Enter);
            this.txtFilePath.Leave += new System.EventHandler(this.txtFilePath_Leave);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(129, 26);
            this.button1.TabIndex = 0;
            this.button1.Text = "Browse..";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.pgFile);
            this.tabControl1.Controls.Add(this.pgSql);
            this.tabControl1.Location = new System.Drawing.Point(12, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(921, 92);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // fileSelected
            // 
            this.fileSelected.FileName = "logFile";
            this.fileSelected.RestoreDirectory = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(10, 99);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Log information:";
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(337, 153);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(92, 35);
            this.btnApply.TabIndex = 6;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // Method
            // 
            this.Method.Location = new System.Drawing.Point(8, 45);
            this.Method.Name = "Method";
            this.Method.Size = new System.Drawing.Size(100, 22);
            this.Method.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 16);
            this.label3.TabIndex = 8;
            this.label3.Text = "Method";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(114, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 16);
            this.label4.TabIndex = 10;
            this.label4.Text = "URL";
            // 
            // URL
            // 
            this.URL.Location = new System.Drawing.Point(117, 45);
            this.URL.Name = "URL";
            this.URL.Size = new System.Drawing.Size(100, 22);
            this.URL.TabIndex = 9;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(223, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 16);
            this.label5.TabIndex = 12;
            this.label5.Text = "IP";
            // 
            // IP
            // 
            this.IP.Location = new System.Drawing.Point(226, 45);
            this.IP.Name = "IP";
            this.IP.Size = new System.Drawing.Size(100, 22);
            this.IP.TabIndex = 11;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 75);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 16);
            this.label6.TabIndex = 14;
            this.label6.Text = "Username";
            // 
            // Username
            // 
            this.Username.Location = new System.Drawing.Point(8, 91);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(100, 22);
            this.Username.TabIndex = 13;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(114, 75);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 16);
            this.label7.TabIndex = 16;
            this.label7.Text = "Client";
            // 
            // Client
            // 
            this.Client.Location = new System.Drawing.Point(117, 91);
            this.Client.Name = "Client";
            this.Client.Size = new System.Drawing.Size(100, 22);
            this.Client.TabIndex = 15;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(223, 75);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(37, 16);
            this.label8.TabIndex = 18;
            this.label8.Text = "Data";
            // 
            // Data
            // 
            this.Data.Location = new System.Drawing.Point(226, 91);
            this.Data.Name = "Data";
            this.Data.Size = new System.Drawing.Size(100, 22);
            this.Data.TabIndex = 17;
            // 
            // txtInfo
            // 
            this.txtInfo.Location = new System.Drawing.Point(12, 126);
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.ReadOnly = true;
            this.txtInfo.Size = new System.Drawing.Size(471, 161);
            this.txtInfo.TabIndex = 4;
            this.txtInfo.Text = "";
            // 
            // dataGrid
            // 
            this.dataGrid.AllowUserToAddRows = false;
            this.dataGrid.AllowUserToDeleteRows = false;
            this.dataGrid.AllowUserToOrderColumns = true;
            this.dataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGrid.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dataGrid.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dataGrid.Location = new System.Drawing.Point(0, 313);
            this.dataGrid.Name = "dataGrid";
            this.dataGrid.Size = new System.Drawing.Size(942, 236);
            this.dataGrid.TabIndex = 19;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(223, 114);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(67, 16);
            this.label11.TabIndex = 25;
            this.label11.Text = "FileName";
            // 
            // FileName
            // 
            this.FileName.Location = new System.Drawing.Point(226, 130);
            this.FileName.Name = "FileName";
            this.FileName.Size = new System.Drawing.Size(100, 22);
            this.FileName.TabIndex = 24;
            // 
            // Caption
            // 
            this.Caption.Location = new System.Drawing.Point(8, 130);
            this.Caption.Name = "Caption";
            this.Caption.Size = new System.Drawing.Size(100, 22);
            this.Caption.TabIndex = 20;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(114, 114);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(81, 16);
            this.label12.TabIndex = 23;
            this.label12.Text = "LineNumber";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(5, 114);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(54, 16);
            this.label13.TabIndex = 21;
            this.label13.Text = "Caption";
            // 
            // LineNumber
            // 
            this.LineNumber.Location = new System.Drawing.Point(117, 130);
            this.LineNumber.Name = "LineNumber";
            this.LineNumber.Size = new System.Drawing.Size(100, 22);
            this.LineNumber.TabIndex = 22;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(223, 153);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(66, 16);
            this.label14.TabIndex = 31;
            this.label14.Text = "End Time";
            // 
            // DeviceName
            // 
            this.DeviceName.Location = new System.Drawing.Point(8, 169);
            this.DeviceName.Name = "DeviceName";
            this.DeviceName.Size = new System.Drawing.Size(100, 22);
            this.DeviceName.TabIndex = 26;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(114, 153);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(77, 16);
            this.label15.TabIndex = 29;
            this.label15.Text = "Begin Time";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(5, 153);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(88, 16);
            this.label16.TabIndex = 27;
            this.label16.Text = "Device name";
            // 
            // txtBeginTime
            // 
            this.txtBeginTime.CustomFormat = "HH:mm:ss";
            this.txtBeginTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txtBeginTime.Location = new System.Drawing.Point(117, 169);
            this.txtBeginTime.Name = "txtBeginTime";
            this.txtBeginTime.ShowUpDown = true;
            this.txtBeginTime.Size = new System.Drawing.Size(100, 22);
            this.txtBeginTime.TabIndex = 32;
            this.txtBeginTime.Value = new System.DateTime(2018, 11, 24, 0, 0, 0, 0);
            // 
            // txEndTime
            // 
            this.txEndTime.CustomFormat = "HH:mm:ss";
            this.txEndTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.txEndTime.Location = new System.Drawing.Point(226, 169);
            this.txEndTime.Name = "txEndTime";
            this.txEndTime.ShowUpDown = true;
            this.txEndTime.Size = new System.Drawing.Size(100, 22);
            this.txEndTime.TabIndex = 33;
            this.txEndTime.Value = new System.DateTime(2018, 11, 24, 23, 59, 59, 0);
            // 
            // Filters
            // 
            this.Filters.Controls.Add(this.txEndTime);
            this.Filters.Controls.Add(this.txtBeginTime);
            this.Filters.Controls.Add(this.label14);
            this.Filters.Controls.Add(this.DeviceName);
            this.Filters.Controls.Add(this.label15);
            this.Filters.Controls.Add(this.label16);
            this.Filters.Controls.Add(this.label11);
            this.Filters.Controls.Add(this.FileName);
            this.Filters.Controls.Add(this.Caption);
            this.Filters.Controls.Add(this.label12);
            this.Filters.Controls.Add(this.label13);
            this.Filters.Controls.Add(this.LineNumber);
            this.Filters.Controls.Add(this.btnApply);
            this.Filters.Controls.Add(this.label3);
            this.Filters.Controls.Add(this.label8);
            this.Filters.Controls.Add(this.label5);
            this.Filters.Controls.Add(this.IP);
            this.Filters.Controls.Add(this.Data);
            this.Filters.Controls.Add(this.Username);
            this.Filters.Controls.Add(this.Method);
            this.Filters.Controls.Add(this.label4);
            this.Filters.Controls.Add(this.label7);
            this.Filters.Controls.Add(this.label6);
            this.Filters.Controls.Add(this.URL);
            this.Filters.Controls.Add(this.Client);
            this.Filters.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Filters.Location = new System.Drawing.Point(494, 99);
            this.Filters.Name = "Filters";
            this.Filters.Size = new System.Drawing.Size(435, 196);
            this.Filters.TabIndex = 34;
            this.Filters.TabStop = false;
            this.Filters.Text = "Filters";
            // 
            // datePickerBegin
            // 
            this.datePickerBegin.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datePickerBegin.CustomFormat = "dd/MM/yyyy";
            this.datePickerBegin.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datePickerBegin.Location = new System.Drawing.Point(613, 32);
            this.datePickerBegin.Name = "datePickerBegin";
            this.datePickerBegin.ShowUpDown = true;
            this.datePickerBegin.Size = new System.Drawing.Size(100, 20);
            this.datePickerBegin.TabIndex = 33;
            this.datePickerBegin.Value = new System.DateTime(2018, 11, 26, 0, 0, 0, 0);
            // 
            // datePickerEnd
            // 
            this.datePickerEnd.CalendarFont = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.datePickerEnd.CustomFormat = "dd/MM/yyyy";
            this.datePickerEnd.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.datePickerEnd.Location = new System.Drawing.Point(722, 32);
            this.datePickerEnd.Name = "datePickerEnd";
            this.datePickerEnd.ShowUpDown = true;
            this.datePickerEnd.Size = new System.Drawing.Size(100, 20);
            this.datePickerEnd.TabIndex = 34;
            this.datePickerEnd.Value = new System.DateTime(2018, 11, 27, 0, 0, 0, 0);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(613, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 13);
            this.label2.TabIndex = 35;
            this.label2.Text = "Begin Date";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(719, 7);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(52, 13);
            this.label9.TabIndex = 36;
            this.label9.Text = "End Date";
            // 
            // txtProjectName
            // 
            this.txtProjectName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtProjectName.Location = new System.Drawing.Point(380, 31);
            this.txtProjectName.Name = "txtProjectName";
            this.txtProjectName.Size = new System.Drawing.Size(100, 21);
            this.txtProjectName.TabIndex = 37;
            this.txtProjectName.Text = "Project Name";
            this.txtProjectName.Enter += new System.EventHandler(this.txtProjectName_Enter);
            this.txtProjectName.Leave += new System.EventHandler(this.txtProjectName_Leave);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(942, 549);
            this.Controls.Add(this.Filters);
            this.Controls.Add(this.dataGrid);
            this.Controls.Add(this.txtInfo);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.label1);
            this.Name = "Form1";
            this.Text = "Obeidat Log Reader";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.pgSql.ResumeLayout(false);
            this.pgSql.PerformLayout();
            this.pgFile.ResumeLayout(false);
            this.pgFile.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dataGrid)).EndInit();
            this.Filters.ResumeLayout(false);
            this.Filters.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabPage pgSql;
        private System.Windows.Forms.Button btnLoadData;
        private System.Windows.Forms.ComboBox combLevel;
        private System.Windows.Forms.TextBox txtConnectionString;
        private System.Windows.Forms.TabPage pgFile;
        private System.Windows.Forms.TextBox txtFilePath;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.OpenFileDialog fileSelected;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.TextBox Method;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox URL;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox IP;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox Username;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox Client;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox Data;
        private System.Windows.Forms.RichTextBox txtInfo;
        private System.Windows.Forms.DataGridView dataGrid;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox FileName;
        private System.Windows.Forms.TextBox Caption;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.TextBox LineNumber;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.TextBox DeviceName;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.DateTimePicker txtBeginTime;
        private System.Windows.Forms.DateTimePicker txEndTime;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox Filters;
        private System.Windows.Forms.DateTimePicker datePickerEnd;
        private System.Windows.Forms.DateTimePicker datePickerBegin;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtProjectName;
    }
}

