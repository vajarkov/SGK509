﻿/*
 * Created by SharpDevelop.
 * User: Technical department
 * Date: 17.10.2016
 * Time: 12:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace SGK509
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		private System.Windows.Forms.Button btnInstall;
		private System.Windows.Forms.Button btnDelete;
		private System.Windows.Forms.Button btnStart;
		private System.Windows.Forms.Button btnStop;
		private System.Windows.Forms.TabControl tabSGK;
		private System.Windows.Forms.TabPage tabData;
		private System.Windows.Forms.TabPage tabDB;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TabPage tabSerialPort;
		private System.Windows.Forms.GroupBox groupTCP;
		private System.Windows.Forms.GroupBox groupRTU;
		private System.Windows.Forms.RadioButton radioTCP;
		private System.Windows.Forms.RadioButton radioRTU;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ComboBox cbDataBits;
		private System.Windows.Forms.ComboBox cbStopBit;
		private System.Windows.Forms.ComboBox cbParity;
		private System.Windows.Forms.ComboBox cbBaudRate;
		private System.Windows.Forms.ComboBox cbPort;
		private System.Windows.Forms.TabPage tabChannels;
		private System.Windows.Forms.TextBox tbModbusTCPSlave;
		private System.Windows.Forms.TextBox tbModbusTCPPort;
		private System.Windows.Forms.TextBox tbModbusTCPAddress;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button btnProtocolSave;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.ComboBox cbDBType;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.ComboBox cbDBName;
		private System.Windows.Forms.ComboBox cbDataSource;
		private System.Windows.Forms.TextBox tbPassword;
		private System.Windows.Forms.TextBox tbUserName;
		private System.Windows.Forms.Button btnDBSave;
		private System.Windows.Forms.Button btnDBTest;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Button btnDBType;
	
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.btnInstall = new System.Windows.Forms.Button();
			this.btnDelete = new System.Windows.Forms.Button();
			this.btnStart = new System.Windows.Forms.Button();
			this.btnStop = new System.Windows.Forms.Button();
			this.tabSGK = new System.Windows.Forms.TabControl();
			this.tabData = new System.Windows.Forms.TabPage();
			this.tabSerialPort = new System.Windows.Forms.TabPage();
			this.btnProtocolSave = new System.Windows.Forms.Button();
			this.radioTCP = new System.Windows.Forms.RadioButton();
			this.radioRTU = new System.Windows.Forms.RadioButton();
			this.groupTCP = new System.Windows.Forms.GroupBox();
			this.tbModbusTCPSlave = new System.Windows.Forms.TextBox();
			this.tbModbusTCPPort = new System.Windows.Forms.TextBox();
			this.tbModbusTCPAddress = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.groupRTU = new System.Windows.Forms.GroupBox();
			this.cbDataBits = new System.Windows.Forms.ComboBox();
			this.cbStopBit = new System.Windows.Forms.ComboBox();
			this.cbParity = new System.Windows.Forms.ComboBox();
			this.cbBaudRate = new System.Windows.Forms.ComboBox();
			this.cbPort = new System.Windows.Forms.ComboBox();
			this.label6 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.tabDB = new System.Windows.Forms.TabPage();
			this.button2 = new System.Windows.Forms.Button();
			this.btnDBType = new System.Windows.Forms.Button();
			this.btnDBSave = new System.Windows.Forms.Button();
			this.btnDBTest = new System.Windows.Forms.Button();
			this.cbDBName = new System.Windows.Forms.ComboBox();
			this.cbDataSource = new System.Windows.Forms.ComboBox();
			this.tbPassword = new System.Windows.Forms.TextBox();
			this.tbUserName = new System.Windows.Forms.TextBox();
			this.cbDBType = new System.Windows.Forms.ComboBox();
			this.label14 = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.tabChannels = new System.Windows.Forms.TabPage();
			this.tabSGK.SuspendLayout();
			this.tabSerialPort.SuspendLayout();
			this.groupTCP.SuspendLayout();
			this.groupRTU.SuspendLayout();
			this.tabDB.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnInstall
			// 
			this.btnInstall.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnInstall.Location = new System.Drawing.Point(617, 458);
			this.btnInstall.Name = "btnInstall";
			this.btnInstall.Size = new System.Drawing.Size(75, 37);
			this.btnInstall.TabIndex = 0;
			this.btnInstall.Text = "Установить Службу";
			this.btnInstall.UseVisualStyleBackColor = true;
			this.btnInstall.Click += new System.EventHandler(this.btnInstall_Click);
			// 
			// btnDelete
			// 
			this.btnDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnDelete.Location = new System.Drawing.Point(717, 458);
			this.btnDelete.Name = "btnDelete";
			this.btnDelete.Size = new System.Drawing.Size(75, 37);
			this.btnDelete.TabIndex = 0;
			this.btnDelete.Text = "Удалить Службу";
			this.btnDelete.UseVisualStyleBackColor = true;
			this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
			// 
			// btnStart
			// 
			this.btnStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnStart.Location = new System.Drawing.Point(617, 501);
			this.btnStart.Name = "btnStart";
			this.btnStart.Size = new System.Drawing.Size(75, 35);
			this.btnStart.TabIndex = 0;
			this.btnStart.Text = "Запустить Службы";
			this.btnStart.UseVisualStyleBackColor = true;
			this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
			// 
			// btnStop
			// 
			this.btnStop.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnStop.Location = new System.Drawing.Point(717, 501);
			this.btnStop.Name = "btnStop";
			this.btnStop.Size = new System.Drawing.Size(75, 35);
			this.btnStop.TabIndex = 0;
			this.btnStop.Text = "Остановить Службу";
			this.btnStop.UseVisualStyleBackColor = true;
			this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
			// 
			// tabSGK
			// 
			this.tabSGK.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
			| System.Windows.Forms.AnchorStyles.Left) 
			| System.Windows.Forms.AnchorStyles.Right)));
			this.tabSGK.Controls.Add(this.tabData);
			this.tabSGK.Controls.Add(this.tabSerialPort);
			this.tabSGK.Controls.Add(this.tabDB);
			this.tabSGK.Controls.Add(this.tabChannels);
			this.tabSGK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.tabSGK.Location = new System.Drawing.Point(0, 1);
			this.tabSGK.Name = "tabSGK";
			this.tabSGK.SelectedIndex = 0;
			this.tabSGK.Size = new System.Drawing.Size(808, 545);
			this.tabSGK.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
			this.tabSGK.TabIndex = 1;
			// 
			// tabData
			// 
			this.tabData.Location = new System.Drawing.Point(4, 22);
			this.tabData.Name = "tabData";
			this.tabData.Padding = new System.Windows.Forms.Padding(3);
			this.tabData.Size = new System.Drawing.Size(800, 519);
			this.tabData.TabIndex = 1;
			this.tabData.Text = "Данные";
			this.tabData.UseVisualStyleBackColor = true;
			// 
			// tabSerialPort
			// 
			this.tabSerialPort.Controls.Add(this.btnProtocolSave);
			this.tabSerialPort.Controls.Add(this.radioTCP);
			this.tabSerialPort.Controls.Add(this.radioRTU);
			this.tabSerialPort.Controls.Add(this.groupTCP);
			this.tabSerialPort.Controls.Add(this.groupRTU);
			this.tabSerialPort.Controls.Add(this.label1);
			this.tabSerialPort.Location = new System.Drawing.Point(4, 22);
			this.tabSerialPort.Name = "tabSerialPort";
			this.tabSerialPort.Padding = new System.Windows.Forms.Padding(3);
			this.tabSerialPort.Size = new System.Drawing.Size(800, 519);
			this.tabSerialPort.TabIndex = 0;
			this.tabSerialPort.Text = "Протокол данных";
			this.tabSerialPort.UseVisualStyleBackColor = true;
			// 
			// btnProtocolSave
			// 
			this.btnProtocolSave.Location = new System.Drawing.Point(553, 32);
			this.btnProtocolSave.Name = "btnProtocolSave";
			this.btnProtocolSave.Size = new System.Drawing.Size(75, 23);
			this.btnProtocolSave.TabIndex = 5;
			this.btnProtocolSave.Text = "Сохранить";
			this.btnProtocolSave.UseVisualStyleBackColor = true;
			// 
			// radioTCP
			// 
			this.radioTCP.Location = new System.Drawing.Point(281, 5);
			this.radioTCP.Name = "radioTCP";
			this.radioTCP.Size = new System.Drawing.Size(104, 24);
			this.radioTCP.TabIndex = 4;
			this.radioTCP.TabStop = true;
			this.radioTCP.Text = "Modbus TCP";
			this.radioTCP.UseVisualStyleBackColor = true;
			this.radioTCP.CheckedChanged += new System.EventHandler(this.radioTCP_CheckedChanged);
			// 
			// radioRTU
			// 
			this.radioRTU.Location = new System.Drawing.Point(91, 6);
			this.radioRTU.Name = "radioRTU";
			this.radioRTU.Size = new System.Drawing.Size(104, 24);
			this.radioRTU.TabIndex = 0;
			this.radioRTU.TabStop = true;
			this.radioRTU.Text = "Modbus RTU";
			this.radioRTU.UseVisualStyleBackColor = true;
			this.radioRTU.CheckedChanged += new System.EventHandler(this.radioRTU_CheckedChanged);
			// 
			// groupTCP
			// 
			this.groupTCP.Controls.Add(this.tbModbusTCPSlave);
			this.groupTCP.Controls.Add(this.tbModbusTCPPort);
			this.groupTCP.Controls.Add(this.tbModbusTCPAddress);
			this.groupTCP.Controls.Add(this.label9);
			this.groupTCP.Controls.Add(this.label8);
			this.groupTCP.Controls.Add(this.label7);
			this.groupTCP.Enabled = false;
			this.groupTCP.Location = new System.Drawing.Point(281, 32);
			this.groupTCP.Name = "groupTCP";
			this.groupTCP.Size = new System.Drawing.Size(266, 481);
			this.groupTCP.TabIndex = 3;
			this.groupTCP.TabStop = false;
			this.groupTCP.Text = "Параметры Modbus TCP";
			// 
			// tbModbusTCPSlave
			// 
			this.tbModbusTCPSlave.Location = new System.Drawing.Point(112, 71);
			this.tbModbusTCPSlave.Name = "tbModbusTCPSlave";
			this.tbModbusTCPSlave.Size = new System.Drawing.Size(135, 20);
			this.tbModbusTCPSlave.TabIndex = 1;
			this.tbModbusTCPSlave.Text = "0";
			// 
			// tbModbusTCPPort
			// 
			this.tbModbusTCPPort.Location = new System.Drawing.Point(112, 48);
			this.tbModbusTCPPort.Name = "tbModbusTCPPort";
			this.tbModbusTCPPort.Size = new System.Drawing.Size(135, 20);
			this.tbModbusTCPPort.TabIndex = 1;
			this.tbModbusTCPPort.Text = "502";
			// 
			// tbModbusTCPAddress
			// 
			this.tbModbusTCPAddress.Location = new System.Drawing.Point(112, 25);
			this.tbModbusTCPAddress.Name = "tbModbusTCPAddress";
			this.tbModbusTCPAddress.Size = new System.Drawing.Size(135, 20);
			this.tbModbusTCPAddress.TabIndex = 1;
			// 
			// label9
			// 
			this.label9.Location = new System.Drawing.Point(6, 74);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(100, 23);
			this.label9.TabIndex = 0;
			this.label9.Text = "Адрес устройства";
			// 
			// label8
			// 
			this.label8.Location = new System.Drawing.Point(6, 51);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(100, 23);
			this.label8.TabIndex = 0;
			this.label8.Text = "Порт";
			// 
			// label7
			// 
			this.label7.Location = new System.Drawing.Point(6, 28);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(100, 23);
			this.label7.TabIndex = 0;
			this.label7.Text = "IP или DNS";
			// 
			// groupRTU
			// 
			this.groupRTU.Controls.Add(this.cbDataBits);
			this.groupRTU.Controls.Add(this.cbStopBit);
			this.groupRTU.Controls.Add(this.cbParity);
			this.groupRTU.Controls.Add(this.cbBaudRate);
			this.groupRTU.Controls.Add(this.cbPort);
			this.groupRTU.Controls.Add(this.label6);
			this.groupRTU.Controls.Add(this.label5);
			this.groupRTU.Controls.Add(this.label4);
			this.groupRTU.Controls.Add(this.label3);
			this.groupRTU.Controls.Add(this.label2);
			this.groupRTU.Enabled = false;
			this.groupRTU.Location = new System.Drawing.Point(8, 32);
			this.groupRTU.Name = "groupRTU";
			this.groupRTU.Size = new System.Drawing.Size(267, 481);
			this.groupRTU.TabIndex = 2;
			this.groupRTU.TabStop = false;
			this.groupRTU.Text = "Параметры Modbus RTU";
			// 
			// cbDataBits
			// 
			this.cbDataBits.FormattingEnabled = true;
			this.cbDataBits.Location = new System.Drawing.Point(121, 116);
			this.cbDataBits.Name = "cbDataBits";
			this.cbDataBits.Size = new System.Drawing.Size(121, 21);
			this.cbDataBits.TabIndex = 5;
			// 
			// cbStopBit
			// 
			this.cbStopBit.FormattingEnabled = true;
			this.cbStopBit.Location = new System.Drawing.Point(121, 93);
			this.cbStopBit.Name = "cbStopBit";
			this.cbStopBit.Size = new System.Drawing.Size(121, 21);
			this.cbStopBit.TabIndex = 4;
			// 
			// cbParity
			// 
			this.cbParity.FormattingEnabled = true;
			this.cbParity.Location = new System.Drawing.Point(121, 70);
			this.cbParity.Name = "cbParity";
			this.cbParity.Size = new System.Drawing.Size(121, 21);
			this.cbParity.TabIndex = 3;
			// 
			// cbBaudRate
			// 
			this.cbBaudRate.FormattingEnabled = true;
			this.cbBaudRate.Location = new System.Drawing.Point(121, 47);
			this.cbBaudRate.Name = "cbBaudRate";
			this.cbBaudRate.Size = new System.Drawing.Size(121, 21);
			this.cbBaudRate.TabIndex = 2;
			// 
			// cbPort
			// 
			this.cbPort.FormattingEnabled = true;
			this.cbPort.Location = new System.Drawing.Point(121, 24);
			this.cbPort.Name = "cbPort";
			this.cbPort.Size = new System.Drawing.Size(121, 21);
			this.cbPort.TabIndex = 1;
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(15, 116);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(100, 23);
			this.label6.TabIndex = 0;
			this.label6.Text = "Биты данных";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(15, 93);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(100, 23);
			this.label5.TabIndex = 0;
			this.label5.Text = "Стоп бит";
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(15, 70);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(100, 23);
			this.label4.TabIndex = 0;
			this.label4.Text = "Четность";
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(15, 47);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(100, 23);
			this.label3.TabIndex = 0;
			this.label3.Text = "Скорость";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(15, 24);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(100, 23);
			this.label2.TabIndex = 0;
			this.label2.Text = "Порт";
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
			this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
			this.label1.Location = new System.Drawing.Point(8, 6);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(100, 23);
			this.label1.TabIndex = 1;
			this.label1.Text = "Протокол";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// tabDB
			// 
			this.tabDB.Controls.Add(this.button2);
			this.tabDB.Controls.Add(this.btnDBType);
			this.tabDB.Controls.Add(this.btnDBSave);
			this.tabDB.Controls.Add(this.btnDBTest);
			this.tabDB.Controls.Add(this.cbDBName);
			this.tabDB.Controls.Add(this.cbDataSource);
			this.tabDB.Controls.Add(this.tbPassword);
			this.tabDB.Controls.Add(this.tbUserName);
			this.tabDB.Controls.Add(this.cbDBType);
			this.tabDB.Controls.Add(this.label14);
			this.tabDB.Controls.Add(this.label13);
			this.tabDB.Controls.Add(this.label12);
			this.tabDB.Controls.Add(this.label11);
			this.tabDB.Controls.Add(this.label10);
			this.tabDB.Location = new System.Drawing.Point(4, 22);
			this.tabDB.Name = "tabDB";
			this.tabDB.Padding = new System.Windows.Forms.Padding(3);
			this.tabDB.Size = new System.Drawing.Size(800, 519);
			this.tabDB.TabIndex = 2;
			this.tabDB.Text = "База Данных";
			this.tabDB.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(383, 125);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(172, 23);
			this.button2.TabIndex = 5;
			this.button2.Text = "Список доступных БД";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// btnDBType
			// 
			this.btnDBType.Location = new System.Drawing.Point(383, 16);
			this.btnDBType.Name = "btnDBType";
			this.btnDBType.Size = new System.Drawing.Size(172, 23);
			this.btnDBType.TabIndex = 5;
			this.btnDBType.Text = "Список доступных серверов";
			this.btnDBType.UseVisualStyleBackColor = true;
			this.btnDBType.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GetDataSources);
			// 
			// btnDBSave
			// 
			this.btnDBSave.Enabled = false;
			this.btnDBSave.Location = new System.Drawing.Point(12, 226);
			this.btnDBSave.Name = "btnDBSave";
			this.btnDBSave.Size = new System.Drawing.Size(112, 32);
			this.btnDBSave.TabIndex = 4;
			this.btnDBSave.Text = "Сохранить";
			this.btnDBSave.UseVisualStyleBackColor = true;
			// 
			// btnDBTest
			// 
			this.btnDBTest.Enabled = false;
			this.btnDBTest.Location = new System.Drawing.Point(12, 188);
			this.btnDBTest.Name = "btnDBTest";
			this.btnDBTest.Size = new System.Drawing.Size(112, 32);
			this.btnDBTest.TabIndex = 4;
			this.btnDBTest.Text = "Проверить связь";
			this.btnDBTest.UseVisualStyleBackColor = true;
			// 
			// cbDBName
			// 
			this.cbDBName.Enabled = false;
			this.cbDBName.FormattingEnabled = true;
			this.cbDBName.Location = new System.Drawing.Point(134, 162);
			this.cbDBName.Name = "cbDBName";
			this.cbDBName.Size = new System.Drawing.Size(243, 21);
			this.cbDBName.TabIndex = 3;
			// 
			// cbDataSource
			// 
			this.cbDataSource.Enabled = false;
			this.cbDataSource.FormattingEnabled = true;
			this.cbDataSource.Location = new System.Drawing.Point(134, 53);
			this.cbDataSource.Name = "cbDataSource";
			this.cbDataSource.Size = new System.Drawing.Size(243, 21);
			this.cbDataSource.TabIndex = 3;
			// 
			// tbPassword
			// 
			this.tbPassword.Enabled = false;
			this.tbPassword.Location = new System.Drawing.Point(134, 125);
			this.tbPassword.Name = "tbPassword";
			this.tbPassword.Size = new System.Drawing.Size(243, 20);
			this.tbPassword.TabIndex = 2;
			// 
			// tbUserName
			// 
			this.tbUserName.Enabled = false;
			this.tbUserName.Location = new System.Drawing.Point(134, 91);
			this.tbUserName.Name = "tbUserName";
			this.tbUserName.Size = new System.Drawing.Size(243, 20);
			this.tbUserName.TabIndex = 2;
			// 
			// cbDBType
			// 
			this.cbDBType.FormattingEnabled = true;
			this.cbDBType.Location = new System.Drawing.Point(134, 18);
			this.cbDBType.Name = "cbDBType";
			this.cbDBType.Size = new System.Drawing.Size(243, 21);
			this.cbDBType.TabIndex = 1;
			// 
			// label14
			// 
			this.label14.Location = new System.Drawing.Point(12, 122);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(100, 23);
			this.label14.TabIndex = 0;
			this.label14.Text = "Пароль";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label13
			// 
			this.label13.Location = new System.Drawing.Point(12, 83);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(100, 28);
			this.label13.TabIndex = 0;
			this.label13.Text = "Имя пользователя";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(12, 162);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(100, 23);
			this.label12.TabIndex = 0;
			this.label12.Text = "Имя Базы данных";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(12, 51);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(100, 23);
			this.label11.TabIndex = 0;
			this.label11.Text = "Источник данных";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(12, 28);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(100, 23);
			this.label10.TabIndex = 0;
			this.label10.Text = "Тип Базы данных";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tabChannels
			// 
			this.tabChannels.Location = new System.Drawing.Point(4, 22);
			this.tabChannels.Name = "tabChannels";
			this.tabChannels.Padding = new System.Windows.Forms.Padding(3);
			this.tabChannels.Size = new System.Drawing.Size(800, 519);
			this.tabChannels.TabIndex = 3;
			this.tabChannels.Text = "Каналы";
			this.tabChannels.UseVisualStyleBackColor = true;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(804, 544);
			this.Controls.Add(this.btnStart);
			this.Controls.Add(this.btnStop);
			this.Controls.Add(this.btnDelete);
			this.Controls.Add(this.btnInstall);
			this.Controls.Add(this.tabSGK);
			this.Name = "MainForm";
			this.Text = "SGK509";
			this.tabSGK.ResumeLayout(false);
			this.tabSerialPort.ResumeLayout(false);
			this.groupTCP.ResumeLayout(false);
			this.groupTCP.PerformLayout();
			this.groupRTU.ResumeLayout(false);
			this.tabDB.ResumeLayout(false);
			this.tabDB.PerformLayout();
			this.ResumeLayout(false);

		}
	}
}
