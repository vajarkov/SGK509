/*
 * Created by SharpDevelop.
 * User: Technical department
 * Date: 17.10.2016
 * Time: 12:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;

using System.Configuration;
using System.ServiceProcess;
using System.Configuration.Install;
using System.Diagnostics;
using System.IO.Ports;
using System.Data;
using System.Threading;
using MSDataBase;


namespace SGK509
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		private System.Configuration.Configuration appConfig;							// Переменная для чтения конфигурации
		private ServiceController controller;   										// Переменная для работы со службой
		private AppSettingsSection modbusSettings;    									// Переменная для конфигурации файлов с данными
		private AppSettingsSection dbSettings;  										// Переменная для конфигурации порта
		private EventLog events = new EventLog();										// Переменная для записи событий
		private const string serviceName = "SGKService";								// Переменная для имени службы
		private DataSet dsChannels = new DataSet();										// Переменная для обновления справочника Точек отбора
		private DataSet dsUltramat = new DataSet();										// Переменная для обновления справочника Ultramat
		private DataSet dsUnits = new DataSet();										// Переменная для обновления справочника Единиц измерения
		private DataSet dsGases = new DataSet();										// Переменная для обновления справочника Точек отбора
		private DataSet dsDiscretes = new DataSet();									// Переменная для обновления справочника Точек отбора
		private DataSet dsParameters = new DataSet();									// Переменная для обновления справочника Точек отбора
		private DataSet dsAnalogConf = new DataSet();									// Переменная для конфигурации аналоговых сигналов
		private DataSet dsDiscreteConf = new DataSet();									// Переменная для конфигурации дискретных сигналов
		private IDataBase dbSource;														// Переменная для работы с БД
		
		#region Конструктор для приложения
		public MainForm()
		{
			InitializeComponent();
			CheckService(serviceName);					// Проверка существования службы
			ConfigurationInit();						// Инициализация конфигурации
		}
		#endregion
		
		#region Метод для заполнения конфигурационных списков данными
		private void ComboBoxInit(ComboBox cbItem, string[] strItems, string section)
		{
			foreach (string item in strItems)
			{
				cbItem.Items.Add(item);
			}
			if ((!String.IsNullOrEmpty(section))&&(!String.IsNullOrEmpty(dbSettings.Settings[section].Value)))
				cbItem.SelectedItem = dbSettings.Settings[section].Value;
		}
		#endregion

		#region Конфигурация программы
		private void ConfigurationInit()
		{
			// Чтение конфигурационного файла программы
			appConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			// Считывание конфигурации БД
			dbSettings = (AppSettingsSection)appConfig.GetSection("DBSettings");
			// Считывание конфигурации Modbus
			modbusSettings = (AppSettingsSection)appConfig.GetSection("ModbusSettings");
			
			// Заполнение конфигурационных 
			fillRTU();
			GetConfigModbus();
			GetConfigDB();			
		}
		#endregion
		
		#region Заполнение данных для Modbus RTU
		void fillRTU()
		{
			// Заполнение доступных портов
			ComboBoxInit(cbPort, SerialPort.GetPortNames(), "");
			// Заполнение скорости передачи
			ComboBoxInit(cbBaudRate, new string[] { "1200", "2400", "4800", "9600", "19200", "38400", "57600", "115200", "230400" }, "");
			// Заполнение четности портов
			ComboBoxInit(cbParity, Enum.GetNames(typeof (Parity)), "");
			// Заполнение Стоп-Битов
			ComboBoxInit(cbStopBit, Enum.GetNames(typeof (StopBits)), "");
			// Заполнение Битов Данных
			ComboBoxInit(cbDataBits, new string[] { "4", "5", "6", "7", "8", }, "");
		}
		#endregion
		
		#region Загрузка параметров для Modbus
		void GetConfigModbus()
		{
		// Проверка есть ли данные
			if (!String.IsNullOrEmpty(modbusSettings.Settings["MBType"].Value))
			{
				// Modbus RTU
				if(modbusSettings.Settings["MBType"].Value == "RTU")
				{
					radioRTU.Checked = true;
					// COM-порт
					if(!String.IsNullOrEmpty(modbusSettings.Settings["MBSerialPort"].Value))
						cbPort.Text = modbusSettings.Settings["MBSerialPort"].Value;
					// Скорость
					if(!String.IsNullOrEmpty(modbusSettings.Settings["MBBaudRate"].Value))
						cbBaudRate.Text = modbusSettings.Settings["MBBaudRate"].Value;
					// Четность
					if(!String.IsNullOrEmpty(modbusSettings.Settings["MBParity"].Value))
						cbParity.Text = modbusSettings.Settings["MBParity"].Value;
					// Стоп-бит
					if(!String.IsNullOrEmpty(modbusSettings.Settings["MBStopBit"].Value))
						cbStopBit.Text = modbusSettings.Settings["MBStopBit"].Value;
					// Биты данных
					if(!String.IsNullOrEmpty(modbusSettings.Settings["MBDataBits"].Value))
						cbDataBits.Text = modbusSettings.Settings["MBDataBits"].Value;
					// Адрес устройства Modbus
					if(!String.IsNullOrEmpty(modbusSettings.Settings["MBSlave"].Value))
						tbModbusRTUSlave.Text = modbusSettings.Settings["MBSlave"].Value;
				}
				// Modbus TCP
				else if (modbusSettings.Settings["MBType"].Value == "TCP")
				{
					radioTCP.Checked = true;
					// IP адрес
					if(!String.IsNullOrEmpty(modbusSettings.Settings["MBIPAddress"].Value))
						tbModbusTCPAddress.Text = modbusSettings.Settings["MBIPAddress"].Value;
					// TCP порт
					if(!String.IsNullOrEmpty(modbusSettings.Settings["MBTCPPort"].Value))
						tbModbusTCPPort.Text = modbusSettings.Settings["MBTCPPort"].Value;
					// Адрес устройства Modbus
					if(!String.IsNullOrEmpty(modbusSettings.Settings["MBSlave"].Value))
						tbModbusTCPSlave.Text = modbusSettings.Settings["MBSlave"].Value;
				}
			}
		}
		#endregion
			
		
		#region Загрузка параметров БД 
		void GetConfigDB()
		{
			// Заполнение типов Базы данных
			ComboBoxInit(cbDBType, new string[] {"MS SQL Server", "Oracle", "PostgreSQL", "MySQL"}, "DBType");
			
			// Заполнение периода опроса
			ComboBoxInit(cbPeriod, new string[] {"1 секунда", "5 секунд", "30 секунд", "1 минута", "5 минут"}, "DBPeriod");
			// Выбор источника данных БД из конфигурации, если он прописан
			if (!String.IsNullOrEmpty(dbSettings.Settings["DBDataSource"].Value))
			{
				cbDataSource.Enabled = true;
				Thread dataSourceThread = new Thread(new ThreadStart(GetDataSources));
				dataSourceThread.Start();
				dataSourceThread.Join();
				cbDataSource.Text = dbSettings.Settings["DBDataSource"].Value;
			}	
			// Заполнение пользователя из конфигурации, если он прописан
			if (!String.IsNullOrEmpty(dbSettings.Settings["DBUser"].Value))
				tbUserName.Text = dbSettings.Settings["DBUser"].Value;
			// Заполнение пароля из конфигурации, если он прописан
			if (!String.IsNullOrEmpty(dbSettings.Settings["DBPassword"].Value))
				tbPassword.Text = dbSettings.Settings["DBPassword"].Value;
			
			// Выбор БД из конфигурации, если он прописан
			if (!String.IsNullOrEmpty(dbSettings.Settings["DBName"].Value))
			{
				
				cbDBName.Enabled = true;
				dbSource = new MSDataBase.DataBase();
				cbDBName.Items.Clear();
				dbSource.SetDBConnectionParameters(cbDataSource.Text, tbUserName.Text, tbPassword.Text);
				cbDBName.DataSource = dbSource.GetDBList();
				cbDBName.Text = dbSettings.Settings["DBName"].Value;
			}
				
		}
		#endregion
			
	#region Работа со службой
		#region Расстановка действий при установленной службе
		private void CheckService(string svcName)
		{
			// Если служба установлена
			if (ServiceIsExisted(svcName))
			{
				// Проверяем статус службы и выставляем действия
				CheckStatus(svcName);
				btnDelete.Enabled = true;    // Кнокпа "Установить" активна
				btnInstall.Enabled = false;  // Кнокпа "Удалить" заблокирована
			}
			else
			{
				btnDelete.Enabled = false;   // Кнокпа "Установить" заблокирована
				btnInstall.Enabled = true;   // Кнокпа "Удалить" активна
				btnStop.Enabled = false;  // Кнокпа "Стоп" заблокирована
				btnStart.Enabled = false; // Кнокпа "Старт" заблокирована
			}
		}
		#endregion
		
		#region Проверить запущена ли служба SGKService
		private void CheckStatus(string svcName)
		{
			// Cоздаем переменную с указателем на службу
			controller = new ServiceController(svcName);
			// Усли служба запущена
			if (controller.Status == ServiceControllerStatus.Running)
			{
				btnStop.Enabled = true;   // Кнокпа "Стоп" активна
				btnStart.Enabled = false; // Кнокпа "Старт" заблокирована
			}
			else
			{
				btnStop.Enabled = false;  // Кнокпа "Стоп" заблокирована
				btnStart.Enabled = true;  // Кнокпа "Старт" активна
			}

		}
		#endregion
		
		#region Проверить установлена ли служба SGKService
		private bool ServiceIsExisted(string p)
		{
			ServiceController[] services = ServiceController.GetServices();
			foreach (ServiceController s in services)
			{
				if (s.ServiceName == p)
					return true;
			}
			return false;
		}
		#endregion
		
		#region Установка службы
		void btnInstall_Click(object sender, EventArgs e)
		{
			// Аргументы для установки службы
			string[] args = {serviceName+".exe"};
			// Если служба не существует
			if(!ServiceIsExisted(serviceName))
			{
				try
				{
					ManagedInstallerClass.InstallHelper(args);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
					return;
				}
			}
			CheckService(serviceName); // Проверяем установлена ли служба и ее статус
		}
		#endregion
		
		#region Удаление службы
		void btnDelete_Click(object sender, EventArgs e)
		{
			// Аргументы для удаления службы
			string[] args = { "/u",serviceName + ".exe" };
			// Если служба существует
			if (ServiceIsExisted(serviceName))
			{
				try
				{
					ManagedInstallerClass.InstallHelper(args);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.Message);
					return;
				}
			}
			CheckService(serviceName); // Проверяем установлена ли служба и ее статус
		}
		#endregion
		
		#region Запуск службы
		void btnStart_Click(object sender, EventArgs e)
		{
			// Если служба существует
			if (ServiceIsExisted(serviceName))
			{
				controller.Start();
				btnStop.Enabled = true;
				btnStart.Enabled = false;
			}
		}
		#endregion
		
		#region Остановка службы
		void btnStop_Click(object sender, EventArgs e)
		{
			// Если служба существует
			if (ServiceIsExisted(serviceName))
			{
				controller.Stop();
				btnStop.Enabled = false;
				btnStart.Enabled = true;
			}
		}
		#endregion
	#endregion
	
	#region Работа с протоколом Modbus
		
		
		#region Выбор протокола Modbus TCP
		void radioTCP_CheckedChanged(object sender, EventArgs e)
		{
			groupRTU.Enabled = false;
			groupTCP.Enabled = true;
		}
		#endregion
		
		#region Выбор протокола Modbus RTU
		void radioRTU_CheckedChanged(object sender, EventArgs e)
		{
			groupRTU.Enabled = true;
			groupTCP.Enabled = false;
		}
		#endregion
	#endregion
	
	#region Работа с БД
	
		#region Выбор типа базы дынных
		void GetDataSources()
		{
			switch (cbDBType.SelectedItem.ToString())
			{
				case "MS SQL Server":
					dbSource = new MSDataBase.DataBase();
					cbDataSource.DataSource = dbSource.Connect();
					cbDataSource.Enabled = true;
					tbUserName.Text = "sa";
					break;
				case "Oracle":
					//OracleConnect();
					break;
				case "PosgreSQL":
					//PostgreSQLConnect();
					break;
				case "MySQL":
					//MySQLConnect();
					break;
				default:
					break;
			}
		}
		#endregion
		
		
		
		#region Выбран тип БД
		void cbDBType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!String.IsNullOrEmpty(cbDBType.SelectedItem.ToString()))
			{
				btnDBType.Enabled = true;
			}
		}
		#endregion
		
		#region Выбран сервер БД
		void cbDataSource_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!String.IsNullOrEmpty(cbDataSource.SelectedItem.ToString()))
				btnDBList.Enabled = true;
		}
		#endregion
		
		#region Выбрана БД
		void cbDBName_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!String.IsNullOrEmpty(cbDataSource.SelectedItem.ToString()))
			{
				btnDBSave.Enabled = true;
				btnTest.Enabled = true;
			}
		}
		#endregion
		
		#region Проверка связи с БД
		void btnTest_Click(object sender, EventArgs e)
		{
			dbSource.SetDBConnectionParameters(cbDataSource.Text, cbDBName.Text, tbUserName.Text, tbPassword.Text);
			if(dbSource.Test())
			{
				MessageBox.Show("Связь установлена");
				ReloadParameters();
				ReloadData();
			}
			else
			{
				MessageBox.Show("Проверьте параметры соединения");
			}
			
		}
		#endregion
	#endregion
	
	#region Работа с конфигурацией
	
		#region Обновление списков параметров
		void ReloadParameters()
		{
			dbSource.GetParameters((DataGridViewComboBoxColumn)AnalogGrid.Columns[1], "dictChannels");
			dbSource.GetParameters((DataGridViewComboBoxColumn)AnalogGrid.Columns[2], "dictUltramat");
			dbSource.GetParameters((DataGridViewComboBoxColumn)AnalogGrid.Columns[3], "dictParameters");
			dbSource.GetParameters((DataGridViewComboBoxColumn)AnalogGrid.Columns[4], "dictGases");
			dbSource.GetParameters((DataGridViewComboBoxColumn)AnalogGrid.Columns[5], "dictUnits");
			dbSource.GetParameters((DataGridViewComboBoxColumn)DiscreteGrid.Columns[1], "dictChannels");
			dbSource.GetParameters((DataGridViewComboBoxColumn)DiscreteGrid.Columns[2], "dictUltramat");
			dbSource.GetParameters((DataGridViewComboBoxColumn)DiscreteGrid.Columns[3], "dictDiscretes");
		}
		#endregion
		
		#region Обновление данных из справочников
		void ReloadData()
		{
			
			dbSource.GetDict(ChannelGrid, dsChannels, "dictChannels");
			dbSource.GetDict(UltramatGrid, dsUltramat, "dictUltramat");
			dbSource.GetDict(GasGrid, dsGases, "dictGases");
			dbSource.GetDict(ParamGrid, dsParameters, "dictParameters");
			dbSource.GetDict(DiscGrid, dsDiscretes, "dictDiscretes");
			dbSource.GetDict(UnitGrid, dsUnits, "dictUnits");
			dbSource.GetConfig(DiscreteGrid, "confDiscrete");
			dbSource.GetConfig(AnalogGrid, "confAnalog");
		}
		#endregion
		
		#region Сохранение конфигурации Modbus
		void btnProtocolSave_Click(object sender, EventArgs e)
		{
			if(radioRTU.Checked)
			{
				modbusSettings.Settings["MBType"].Value = "RTU";
				modbusSettings.Settings["MBSerialport"].Value = cbPort.Text;
				modbusSettings.Settings["MBBaudRate"].Value = cbBaudRate.Text;
				modbusSettings.Settings["MBParity"].Value = cbParity.Text;
				modbusSettings.Settings["MBStopBit"].Value = cbStopBit.Text;
				modbusSettings.Settings["MBDataBits"].Value = cbDataBits.Text;
				modbusSettings.Settings["MBSlave"].Value = tbModbusRTUSlave.Text;
				
			}
			else if (radioTCP.Checked)
			{
				modbusSettings.Settings["MBType"].Value = "TCP";
				modbusSettings.Settings["MBIPAddress"].Value = tbModbusTCPAddress.Text;
				modbusSettings.Settings["MBTCPPort"].Value = tbModbusTCPPort.Text;
				modbusSettings.Settings["MBSlave"].Value = tbModbusTCPSlave.Text;
			}
			
			// Сохранение конфигурации
			try
			{
				appConfig.Save(ConfigurationSaveMode.Modified);
				ConfigurationManager.RefreshSection("ModbusSettings");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		#endregion
		
		
		#region Сохранение конфигурации БД
		void btnDBSave_Click(object sender, EventArgs e)
		{
			dbSettings.Settings["DBType"].Value = cbDBType.Text;			// Тип БД
			dbSettings.Settings["DBDataSource"].Value = cbDataSource.Text;	// Источник данных
			dbSettings.Settings["DBName"].Value = cbDBName.Text;			// Имя БД
			dbSettings.Settings["DBUser"].Value = tbUserName.Text;			// Пользователь
			dbSettings.Settings["DBPassword"].Value = tbPassword.Text;		// Пароль
			dbSettings.Settings["DBPeriod"].Value = cbPeriod.Text;			// Период опроса
			// Сохранение конфигурации
			try
			{
				appConfig.Save(ConfigurationSaveMode.Modified);
				ConfigurationManager.RefreshSection("DBSettings");
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
		}
		#endregion
		
	#endregion
		
	#region Вспомогательные функции
		#region Автоматическая нумерация строк для сигналов 
		void AutoIncriment(object sender, DataGridViewRowsAddedEventArgs e)
		{
			DataGridView dgv = (DataGridView)sender;
			if(e.RowCount==1)
			{
				dgv.Rows[e.RowIndex-1].Cells[0].Value = e.RowIndex;
			}
			else
			{
				for(int i = e.RowIndex-1; i < (e.RowIndex + e.RowCount); i++)
					dgv.Rows[i].Cells[0].Value = i;
			}
		}
		#endregion
		
		
		
		#region Сохранение изиенений в справочниках
		void btnDictSave_Click(object sender, EventArgs e)
		{
			dbSource.UpdateDict(ChannelGrid, dsChannels, "dictChannels");
			dbSource.UpdateDict(UltramatGrid, dsUltramat, "dictUltramat");
			dbSource.UpdateDict(GasGrid, dsGases, "dictGases");
			dbSource.UpdateDict(ParamGrid, dsParameters, "dictParameters");
			dbSource.UpdateDict(DiscGrid,dsDiscretes,"dictDiscretes");
			dbSource.UpdateDict(UnitGrid,dsUnits,"dictUnits");
			ReloadParameters();
		}
		#endregion
		
		
		
		#region Нажатие кнопки получения источников данных 
		void btnDBType_Click(object sender, EventArgs e)
		{
			GetDataSources();
		}
		#endregion
		
		#region Нажатие кнопки получения списка БД
		void btnDBList_Click(object sender, EventArgs e)
		{
			cbDBName.Items.Clear();
			cbDBName.DataSource = dbSource.GetDBList();
			cbDBName.Enabled = true;
		}
		#endregion
		
		#region Нажатие сохранение конфигурации дискретных параметров
		void btnDiscreteSave_Click(object sender, EventArgs e)
		{
			dbSource.UpdateConfig(DiscreteGrid, "confDiscrete");
		
		}
		#endregion
		
		#region Нажатие сохранение конфигурации аналогох параметров
		void btnAnalogSave_Click(object sender, EventArgs e)
		{
			dbSource.UpdateConfig(AnalogGrid, "confAnalog");
		}
		#endregion
	#endregion
		
		
		
	}
}
