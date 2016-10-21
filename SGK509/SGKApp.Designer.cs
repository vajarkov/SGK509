/*
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
		private System.Windows.Forms.Button btnDBType;
		private System.Windows.Forms.Button btnTest;
		private System.Windows.Forms.Button btnDBList;
		private System.Windows.Forms.DataGridView AnalogGrid;
		private System.Windows.Forms.DataGridViewTextBoxColumn id;
		private System.Windows.Forms.DataGridViewComboBoxColumn ComponentColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn AddressColumn;
		private System.Windows.Forms.DataGridViewComboBoxColumn ChannelColumn;
		private System.Windows.Forms.DataGridViewComboBoxColumn UltramatColumn1;
		private System.Windows.Forms.DataGridViewComboBoxColumn GasColumn;
		private System.Windows.Forms.DataGridViewComboBoxColumn UnitColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn1;
		private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn1;
		private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn2;
		private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewComboBoxColumn3;
		private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn2;
		private System.Windows.Forms.TabPage tabAnalog;
		private System.Windows.Forms.TabPage tabDiscrete;
		private System.Windows.Forms.DataGridView DiscreteGrid;
		private System.Windows.Forms.Button btnDiscreteSave;
		private System.Windows.Forms.Button btnAnalogSave;
		private System.Windows.Forms.TabPage tabDict;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label19;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.DataGridView UnitGrid;
		private System.Windows.Forms.DataGridView DiscGrid;
		private System.Windows.Forms.DataGridView GasGrid;
		private System.Windows.Forms.DataGridView ParamGrid;
		private System.Windows.Forms.DataGridView UltramatGrid;
		private System.Windows.Forms.DataGridView ChannelGrid;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Button btnDictSave;
		private System.Windows.Forms.TextBox tbModbusRTUSlave;
		private System.Windows.Forms.Label label21;
		
	
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
			this.tbModbusRTUSlave = new System.Windows.Forms.TextBox();
			this.label21 = new System.Windows.Forms.Label();
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
			this.btnTest = new System.Windows.Forms.Button();
			this.btnDBList = new System.Windows.Forms.Button();
			this.btnDBType = new System.Windows.Forms.Button();
			this.btnDBSave = new System.Windows.Forms.Button();
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
			this.tabAnalog = new System.Windows.Forms.TabPage();
			this.btnAnalogSave = new System.Windows.Forms.Button();
			this.AnalogGrid = new System.Windows.Forms.DataGridView();
			this.id = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.ChannelColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.UltramatColumn1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.ComponentColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.GasColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.UnitColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.AddressColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.tabDiscrete = new System.Windows.Forms.TabPage();
			this.btnDiscreteSave = new System.Windows.Forms.Button();
			this.DiscreteGrid = new System.Windows.Forms.DataGridView();
			this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.dataGridViewComboBoxColumn1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.dataGridViewComboBoxColumn2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.dataGridViewComboBoxColumn3 = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.tabDict = new System.Windows.Forms.TabPage();
			this.btnDictSave = new System.Windows.Forms.Button();
			this.label20 = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.UnitGrid = new System.Windows.Forms.DataGridView();
			this.DiscGrid = new System.Windows.Forms.DataGridView();
			this.GasGrid = new System.Windows.Forms.DataGridView();
			this.ParamGrid = new System.Windows.Forms.DataGridView();
			this.UltramatGrid = new System.Windows.Forms.DataGridView();
			this.ChannelGrid = new System.Windows.Forms.DataGridView();
			this.label15 = new System.Windows.Forms.Label();
			this.tabSGK.SuspendLayout();
			this.tabSerialPort.SuspendLayout();
			this.groupTCP.SuspendLayout();
			this.groupRTU.SuspendLayout();
			this.tabDB.SuspendLayout();
			this.tabAnalog.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.AnalogGrid)).BeginInit();
			this.tabDiscrete.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.DiscreteGrid)).BeginInit();
			this.tabDict.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.UnitGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.DiscGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.GasGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ParamGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.UltramatGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.ChannelGrid)).BeginInit();
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
			this.tabSGK.Controls.Add(this.tabAnalog);
			this.tabSGK.Controls.Add(this.tabDiscrete);
			this.tabSGK.Controls.Add(this.tabDict);
			this.tabSGK.ImeMode = System.Windows.Forms.ImeMode.NoControl;
			this.tabSGK.Location = new System.Drawing.Point(12, 12);
			this.tabSGK.Name = "tabSGK";
			this.tabSGK.SelectedIndex = 0;
			this.tabSGK.Size = new System.Drawing.Size(808, 545);
			this.tabSGK.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
			this.tabSGK.TabIndex = 0;
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
			this.btnProtocolSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnProtocolSave.Location = new System.Drawing.Point(712, 6);
			this.btnProtocolSave.Name = "btnProtocolSave";
			this.btnProtocolSave.Size = new System.Drawing.Size(75, 38);
			this.btnProtocolSave.TabIndex = 5;
			this.btnProtocolSave.Text = "Сохранить";
			this.btnProtocolSave.UseVisualStyleBackColor = true;
			this.btnProtocolSave.Click += new System.EventHandler(this.btnProtocolSave_Click);
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
			this.groupTCP.Size = new System.Drawing.Size(266, 470);
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
			this.groupRTU.Controls.Add(this.tbModbusRTUSlave);
			this.groupRTU.Controls.Add(this.label21);
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
			this.groupRTU.Size = new System.Drawing.Size(267, 470);
			this.groupRTU.TabIndex = 2;
			this.groupRTU.TabStop = false;
			this.groupRTU.Text = "Параметры Modbus RTU";
			// 
			// tbModbusRTUSlave
			// 
			this.tbModbusRTUSlave.Location = new System.Drawing.Point(121, 136);
			this.tbModbusRTUSlave.Name = "tbModbusRTUSlave";
			this.tbModbusRTUSlave.Size = new System.Drawing.Size(121, 20);
			this.tbModbusRTUSlave.TabIndex = 2;
			this.tbModbusRTUSlave.Text = "0";
			// 
			// label21
			// 
			this.label21.Location = new System.Drawing.Point(15, 139);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(100, 23);
			this.label21.TabIndex = 6;
			this.label21.Text = "Адрес устройства";
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
			this.tabDB.Controls.Add(this.btnTest);
			this.tabDB.Controls.Add(this.btnDBList);
			this.tabDB.Controls.Add(this.btnDBType);
			this.tabDB.Controls.Add(this.btnDBSave);
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
			// btnTest
			// 
			this.btnTest.Enabled = false;
			this.btnTest.Location = new System.Drawing.Point(12, 216);
			this.btnTest.Name = "btnTest";
			this.btnTest.Size = new System.Drawing.Size(112, 32);
			this.btnTest.TabIndex = 6;
			this.btnTest.Text = "Проверить связь";
			this.btnTest.UseVisualStyleBackColor = true;
			this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
			// 
			// btnDBList
			// 
			this.btnDBList.Enabled = false;
			this.btnDBList.Location = new System.Drawing.Point(383, 53);
			this.btnDBList.Name = "btnDBList";
			this.btnDBList.Size = new System.Drawing.Size(172, 23);
			this.btnDBList.TabIndex = 5;
			this.btnDBList.Text = "Список доступных БД";
			this.btnDBList.UseVisualStyleBackColor = true;
			this.btnDBList.Click += new System.EventHandler(this.GetDBList);
			// 
			// btnDBType
			// 
			this.btnDBType.Enabled = false;
			this.btnDBType.Location = new System.Drawing.Point(383, 18);
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
			this.btnDBSave.Location = new System.Drawing.Point(12, 254);
			this.btnDBSave.Name = "btnDBSave";
			this.btnDBSave.Size = new System.Drawing.Size(112, 32);
			this.btnDBSave.TabIndex = 4;
			this.btnDBSave.Text = "Сохранить";
			this.btnDBSave.UseVisualStyleBackColor = true;
			// 
			// cbDBName
			// 
			this.cbDBName.Enabled = false;
			this.cbDBName.FormattingEnabled = true;
			this.cbDBName.Location = new System.Drawing.Point(134, 162);
			this.cbDBName.Name = "cbDBName";
			this.cbDBName.Size = new System.Drawing.Size(243, 21);
			this.cbDBName.TabIndex = 3;
			this.cbDBName.SelectedIndexChanged += new System.EventHandler(this.cbDBName_SelectedIndexChanged);
			// 
			// cbDataSource
			// 
			this.cbDataSource.Enabled = false;
			this.cbDataSource.FormattingEnabled = true;
			this.cbDataSource.Location = new System.Drawing.Point(134, 53);
			this.cbDataSource.Name = "cbDataSource";
			this.cbDataSource.Size = new System.Drawing.Size(243, 21);
			this.cbDataSource.TabIndex = 3;
			this.cbDataSource.SelectedIndexChanged += new System.EventHandler(this.cbDataSource_SelectedIndexChanged);
			// 
			// tbPassword
			// 
			this.tbPassword.Location = new System.Drawing.Point(134, 125);
			this.tbPassword.Name = "tbPassword";
			this.tbPassword.PasswordChar = '*';
			this.tbPassword.Size = new System.Drawing.Size(243, 20);
			this.tbPassword.TabIndex = 2;
			// 
			// tbUserName
			// 
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
			this.cbDBType.SelectedIndexChanged += new System.EventHandler(this.cbDBType_SelectedIndexChanged);
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
			this.label10.Location = new System.Drawing.Point(12, 18);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(100, 23);
			this.label10.TabIndex = 0;
			this.label10.Text = "Тип Базы данных";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tabAnalog
			// 
			this.tabAnalog.Controls.Add(this.btnAnalogSave);
			this.tabAnalog.Controls.Add(this.AnalogGrid);
			this.tabAnalog.Location = new System.Drawing.Point(4, 22);
			this.tabAnalog.Name = "tabAnalog";
			this.tabAnalog.Padding = new System.Windows.Forms.Padding(3);
			this.tabAnalog.Size = new System.Drawing.Size(800, 519);
			this.tabAnalog.TabIndex = 3;
			this.tabAnalog.Text = "Аналоговые сигналы";
			this.tabAnalog.UseVisualStyleBackColor = true;
			// 
			// btnAnalogSave
			// 
			this.btnAnalogSave.Location = new System.Drawing.Point(712, 6);
			this.btnAnalogSave.Name = "btnAnalogSave";
			this.btnAnalogSave.Size = new System.Drawing.Size(75, 38);
			this.btnAnalogSave.TabIndex = 0;
			this.btnAnalogSave.Text = "Сохранить";
			this.btnAnalogSave.UseVisualStyleBackColor = true;
			// 
			// AnalogGrid
			// 
			this.AnalogGrid.AllowUserToOrderColumns = true;
			this.AnalogGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
			this.AnalogGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
			this.AnalogGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.AnalogGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
			this.id,
			this.ChannelColumn,
			this.UltramatColumn1,
			this.ComponentColumn,
			this.GasColumn,
			this.UnitColumn,
			this.AddressColumn});
			this.AnalogGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.AnalogGrid.Location = new System.Drawing.Point(3, 3);
			this.AnalogGrid.Name = "AnalogGrid";
			this.AnalogGrid.Size = new System.Drawing.Size(794, 513);
			this.AnalogGrid.TabIndex = 2;
			this.AnalogGrid.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.AutoIncriment);
			// 
			// id
			// 
			this.id.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.id.HeaderText = "№ Канала";
			this.id.Name = "id";
			this.id.ReadOnly = true;
			this.id.Width = 77;
			// 
			// ChannelColumn
			// 
			this.ChannelColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.ChannelColumn.HeaderText = "Точки отбора";
			this.ChannelColumn.Name = "ChannelColumn";
			this.ChannelColumn.Width = 73;
			// 
			// UltramatColumn1
			// 
			this.UltramatColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.UltramatColumn1.HeaderText = "Ultramat";
			this.UltramatColumn1.Name = "UltramatColumn1";
			this.UltramatColumn1.Width = 52;
			// 
			// ComponentColumn
			// 
			this.ComponentColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.ComponentColumn.HeaderText = "Параметр";
			this.ComponentColumn.Name = "ComponentColumn";
			this.ComponentColumn.Width = 64;
			// 
			// GasColumn
			// 
			this.GasColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.GasColumn.HeaderText = "Газ";
			this.GasColumn.Name = "GasColumn";
			this.GasColumn.Width = 38;
			// 
			// UnitColumn
			// 
			this.UnitColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.UnitColumn.HeaderText = "Единицы измерения";
			this.UnitColumn.Name = "UnitColumn";
			this.UnitColumn.Width = 105;
			// 
			// AddressColumn
			// 
			this.AddressColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.AddressColumn.HeaderText = "Адрес Modbus";
			this.AddressColumn.Name = "AddressColumn";
			this.AddressColumn.Width = 96;
			// 
			// tabDiscrete
			// 
			this.tabDiscrete.Controls.Add(this.btnDiscreteSave);
			this.tabDiscrete.Controls.Add(this.DiscreteGrid);
			this.tabDiscrete.Location = new System.Drawing.Point(4, 22);
			this.tabDiscrete.Name = "tabDiscrete";
			this.tabDiscrete.Padding = new System.Windows.Forms.Padding(3);
			this.tabDiscrete.Size = new System.Drawing.Size(800, 519);
			this.tabDiscrete.TabIndex = 4;
			this.tabDiscrete.Text = "Дискретные сигналы";
			this.tabDiscrete.UseVisualStyleBackColor = true;
			// 
			// btnDiscreteSave
			// 
			this.btnDiscreteSave.Location = new System.Drawing.Point(712, 6);
			this.btnDiscreteSave.Name = "btnDiscreteSave";
			this.btnDiscreteSave.Size = new System.Drawing.Size(75, 38);
			this.btnDiscreteSave.TabIndex = 2;
			this.btnDiscreteSave.Text = "Сохранить";
			this.btnDiscreteSave.UseVisualStyleBackColor = true;
			// 
			// DiscreteGrid
			// 
			this.DiscreteGrid.AllowUserToOrderColumns = true;
			this.DiscreteGrid.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
			this.DiscreteGrid.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.Disable;
			this.DiscreteGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DiscreteGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
			this.dataGridViewTextBoxColumn1,
			this.dataGridViewComboBoxColumn1,
			this.dataGridViewComboBoxColumn2,
			this.dataGridViewComboBoxColumn3,
			this.dataGridViewTextBoxColumn2});
			this.DiscreteGrid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.DiscreteGrid.Location = new System.Drawing.Point(3, 3);
			this.DiscreteGrid.Name = "DiscreteGrid";
			this.DiscreteGrid.Size = new System.Drawing.Size(794, 513);
			this.DiscreteGrid.TabIndex = 1;
			this.DiscreteGrid.RowsAdded += new System.Windows.Forms.DataGridViewRowsAddedEventHandler(this.AutoIncriment);
			// 
			// dataGridViewTextBoxColumn1
			// 
			this.dataGridViewTextBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.dataGridViewTextBoxColumn1.HeaderText = "№ Канала";
			this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
			this.dataGridViewTextBoxColumn1.ReadOnly = true;
			this.dataGridViewTextBoxColumn1.Width = 77;
			// 
			// dataGridViewComboBoxColumn1
			// 
			this.dataGridViewComboBoxColumn1.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.dataGridViewComboBoxColumn1.HeaderText = "Точки отбора";
			this.dataGridViewComboBoxColumn1.Name = "dataGridViewComboBoxColumn1";
			this.dataGridViewComboBoxColumn1.Width = 73;
			// 
			// dataGridViewComboBoxColumn2
			// 
			this.dataGridViewComboBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.dataGridViewComboBoxColumn2.HeaderText = "Ultramat";
			this.dataGridViewComboBoxColumn2.Name = "dataGridViewComboBoxColumn2";
			this.dataGridViewComboBoxColumn2.Width = 52;
			// 
			// dataGridViewComboBoxColumn3
			// 
			this.dataGridViewComboBoxColumn3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.dataGridViewComboBoxColumn3.HeaderText = "Параметр";
			this.dataGridViewComboBoxColumn3.Name = "dataGridViewComboBoxColumn3";
			this.dataGridViewComboBoxColumn3.Width = 64;
			// 
			// dataGridViewTextBoxColumn2
			// 
			this.dataGridViewTextBoxColumn2.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.AllCells;
			this.dataGridViewTextBoxColumn2.HeaderText = "Адрес Modbus";
			this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
			this.dataGridViewTextBoxColumn2.Width = 96;
			// 
			// tabDict
			// 
			this.tabDict.Controls.Add(this.btnDictSave);
			this.tabDict.Controls.Add(this.label20);
			this.tabDict.Controls.Add(this.label19);
			this.tabDict.Controls.Add(this.label18);
			this.tabDict.Controls.Add(this.label17);
			this.tabDict.Controls.Add(this.label16);
			this.tabDict.Controls.Add(this.UnitGrid);
			this.tabDict.Controls.Add(this.DiscGrid);
			this.tabDict.Controls.Add(this.GasGrid);
			this.tabDict.Controls.Add(this.ParamGrid);
			this.tabDict.Controls.Add(this.UltramatGrid);
			this.tabDict.Controls.Add(this.ChannelGrid);
			this.tabDict.Controls.Add(this.label15);
			this.tabDict.Location = new System.Drawing.Point(4, 22);
			this.tabDict.Name = "tabDict";
			this.tabDict.Padding = new System.Windows.Forms.Padding(3);
			this.tabDict.Size = new System.Drawing.Size(800, 519);
			this.tabDict.TabIndex = 5;
			this.tabDict.Text = "Справочники";
			this.tabDict.UseVisualStyleBackColor = true;
			// 
			// btnDictSave
			// 
			this.btnDictSave.Location = new System.Drawing.Point(712, 6);
			this.btnDictSave.Name = "btnDictSave";
			this.btnDictSave.Size = new System.Drawing.Size(75, 38);
			this.btnDictSave.TabIndex = 12;
			this.btnDictSave.Text = "Сохранить";
			this.btnDictSave.UseVisualStyleBackColor = true;
			this.btnDictSave.Click += new System.EventHandler(this.btnDictSave_Click);
			// 
			// label20
			// 
			this.label20.Location = new System.Drawing.Point(397, 205);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(121, 23);
			this.label20.TabIndex = 11;
			this.label20.Text = "Единицы измерения";
			// 
			// label19
			// 
			this.label19.Location = new System.Drawing.Point(215, 205);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(129, 23);
			this.label19.TabIndex = 10;
			this.label19.Text = "Дискретные сигналы";
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(27, 205);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(100, 23);
			this.label18.TabIndex = 9;
			this.label18.Text = "Параметры";
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(397, 13);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(100, 23);
			this.label17.TabIndex = 8;
			this.label17.Text = "Газы";
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(215, 13);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(100, 23);
			this.label16.TabIndex = 7;
			this.label16.Text = "Ultramat";
			// 
			// UnitGrid
			// 
			this.UnitGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.UnitGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			this.UnitGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.UnitGrid.Location = new System.Drawing.Point(397, 231);
			this.UnitGrid.Name = "UnitGrid";
			this.UnitGrid.Size = new System.Drawing.Size(170, 150);
			this.UnitGrid.TabIndex = 6;
			// 
			// DiscGrid
			// 
			this.DiscGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.DiscGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			this.DiscGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.DiscGrid.Location = new System.Drawing.Point(215, 231);
			this.DiscGrid.Name = "DiscGrid";
			this.DiscGrid.Size = new System.Drawing.Size(176, 150);
			this.DiscGrid.TabIndex = 5;
			// 
			// GasGrid
			// 
			this.GasGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.GasGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			this.GasGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.GasGrid.Location = new System.Drawing.Point(397, 39);
			this.GasGrid.Name = "GasGrid";
			this.GasGrid.Size = new System.Drawing.Size(170, 150);
			this.GasGrid.TabIndex = 4;
			// 
			// ParamGrid
			// 
			this.ParamGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.ParamGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			this.ParamGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.ParamGrid.Location = new System.Drawing.Point(27, 231);
			this.ParamGrid.Name = "ParamGrid";
			this.ParamGrid.Size = new System.Drawing.Size(182, 150);
			this.ParamGrid.TabIndex = 3;
			// 
			// UltramatGrid
			// 
			this.UltramatGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.UltramatGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			this.UltramatGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.UltramatGrid.Location = new System.Drawing.Point(215, 39);
			this.UltramatGrid.Name = "UltramatGrid";
			this.UltramatGrid.Size = new System.Drawing.Size(176, 150);
			this.UltramatGrid.TabIndex = 2;
			// 
			// ChannelGrid
			// 
			this.ChannelGrid.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
			this.ChannelGrid.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
			this.ChannelGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.ChannelGrid.Location = new System.Drawing.Point(27, 39);
			this.ChannelGrid.Name = "ChannelGrid";
			this.ChannelGrid.Size = new System.Drawing.Size(182, 150);
			this.ChannelGrid.TabIndex = 1;
			// 
			// label15
			// 
			this.label15.Location = new System.Drawing.Point(27, 13);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(100, 23);
			this.label15.TabIndex = 0;
			this.label15.Text = "Точки отбора";
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
			this.groupRTU.PerformLayout();
			this.tabDB.ResumeLayout(false);
			this.tabDB.PerformLayout();
			this.tabAnalog.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.AnalogGrid)).EndInit();
			this.tabDiscrete.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.DiscreteGrid)).EndInit();
			this.tabDict.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.UnitGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.DiscGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.GasGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ParamGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.UltramatGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.ChannelGrid)).EndInit();
			this.ResumeLayout(false);

		}

		

		}
	}

