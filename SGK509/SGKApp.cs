/*
 * Created by SharpDevelop.
 * User: Technical department
 * Date: 17.10.2016
 * Time: 12:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Text;
//using System.ComponentModel;
//using System.Text;
//using System.Collections.Generic;
//using System.Drawing;
using System.Windows.Forms;
//using SGK509;
using SGKService;
using System.Configuration;
using System.ServiceProcess;
using System.Configuration.Install;
using System.Diagnostics;
//using System.Collections.Specialized;
using System.Collections.Generic;
using System.IO.Ports;
using System.Data;
using System.Data.Sql;
using Microsoft.SqlServer.Management;
using Microsoft.SqlServer.Server;
using Microsoft.SqlServer.Management.Smo;
using System.Data.SqlClient;
using System.Threading;


namespace SGK509
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		private SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder(); 	// Переменная для строки соединения
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
			
			#region Загрузка параметров для Modbus
			// Заполнение конфигурационных 
			fillRTU();
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
			#endregion
			
			#region Загрузка параметров БД 
			// Заполнение типов Базы данных
			ComboBoxInit(cbDBType, new string[] {"MS SQL Server", "Oracle", "PostgreSQL", "MySQL"}, "DBType");
			
			// Заполнение периода опроса
			ComboBoxInit(cbPeriod, new string[] {"1 секунда", "5 секунд", "30 секунд", "1 минута", "5 минут"}, "DBPeriod");

			// Заполнение пользователя из конфигурации, если он прописан
			if (!String.IsNullOrEmpty(dbSettings.Settings["DBUser"].Value))
				tbUserName.Text = dbSettings.Settings["DBUser"].Value;
			
			// Заполнение пароля из конфигурации, если он прописан
			if (!String.IsNullOrEmpty(dbSettings.Settings["DBPassword"].Value))
				tbPassword.Text = dbSettings.Settings["DBPassword"].Value;
			
			// Выбор источника данных БД из конфигурации, если он прописан
			if (!String.IsNullOrEmpty(dbSettings.Settings["DBDataSource"].Value))
			{
				cbDataSource.Enabled = true;
				Thread dataSourceThread = new Thread(new ThreadStart(GetDataSources));
				dataSourceThread.Start();
				dataSourceThread.Join();
				cbDataSource.Text = dbSettings.Settings["DBDataSource"].Value;
			
			}
				
			// Выбор БД из конфигурации, если он прописан
			if (!String.IsNullOrEmpty(dbSettings.Settings["DBName"].Value))
			{
				cbDBName.Enabled = true;
				Thread dbListThread = new Thread(new ThreadStart(GetDBList));
				dbListThread.Start();
				dbListThread.Join();
				cbDBName.Text = dbSettings.Settings["DBName"].Value;
			}
				
			
			#endregion
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
			//MessageBox.Show(cbDBType.SelectedItem.ToString());
			switch (cbDBType.SelectedItem.ToString())
			{
				case "MS SQL Server":
					MsSQLConnect();
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
		
		#region Считывание доступных баз данных для MS SQL Server
		void MsSQLConnect()
		{
			SqlDataSourceEnumerator instance = SqlDataSourceEnumerator.Instance;
			System.Data.DataTable tableDataSources = new System.Data.DataTable();
			if (tableDataSources.Rows.Count == 0)
			{
				tableDataSources = instance.GetDataSources();
				
				List<string> listServers =  new List<string>();
				
				foreach(DataRow rowServer in tableDataSources.Rows)
				{
					if(String.IsNullOrEmpty(rowServer["InstanceName"].ToString()))
						listServers.Add(rowServer["ServerName"].ToString());
					else
						listServers.Add(rowServer["ServerName"] + "\\" + rowServer["InstanceName"]);
				}
				
				cbDataSource.DataSource = listServers;
				cbDataSource.Enabled = true;
				tbUserName.Text = "sa";
			}
			
		}
		#endregion
		
		#region Выбран тип БД
		void cbDBType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!String.IsNullOrEmpty(cbDBType.SelectedItem.ToString()))
				btnDBType.Enabled = true;
		}
		#endregion
		
		#region Выбран сервер БД
		void cbDataSource_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!String.IsNullOrEmpty(cbDataSource.SelectedItem.ToString()))
				btnDBList.Enabled = true;
		}
		#endregion
		
		#region Получение списка БД
		void GetDBList()
		{
			cbDBName.Items.Clear();
			var DBList = new Microsoft.SqlServer.Management.Smo.Server(cbDataSource.SelectedItem.ToString());
			DBList.ConnectionContext.LoginSecure = false;
			DBList.ConnectionContext.Login = tbUserName.Text;
			DBList.ConnectionContext.Password = tbPassword.Text;
			try
			{
				foreach(Microsoft.SqlServer.Management.Smo.Database db in DBList.Databases){
					cbDBName.Items.Add(db.Name);
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			cbDBName.Enabled = true;
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
			switch (cbDBType.SelectedItem.ToString())
			{
				case "MS SQL Server":
					if(MsSQLTest())
					{
						MessageBox.Show("Связь есть");
						ReloadParameters();
						ReloadData();
					}
					else
					{
						MessageBox.Show("Связи нет. Проверьте настройки");
					}
					break;
				case "Oracle":
					//OracleTest();
					break;
				case "PosgreSQL":
					//PostgreSQLTest();
					break;
				case "MySQL":
					//MySQLTest();
					break;
				default:
					break;
			}
		}
		#endregion
		#region Проверка связи с MSSQL
		bool MsSQLTest()
		{
			GetConnectionString();
			
			using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
			{
				try
				{
					connection.Open();
					return true;
				}
				catch (SqlException)
				{
					return false;
				}
			}
			
		}
		#endregion
		
		#region Заполнение ячеек таблиц конфигурации данными
		void GetParameters(DataGridViewComboBoxColumn cbItem, string tableName)
		{
				
			GetConnectionString();
						
			using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
			{
				try
				{
					connection.Open();
					
					using (SqlCommand command = new SqlCommand("SELECT id, name from " + tableName, connection))
					{
						SqlDataAdapter adapter = new SqlDataAdapter();
						adapter.SelectCommand = command;
						DataTable table = new DataTable();
						table.Locale = System.Globalization.CultureInfo.InvariantCulture;
						adapter.Fill(table);
						cbItem.DataSource = table;
						cbItem.DisplayMember = "name";
						cbItem.ValueMember = "id";
					}                                         
				}
				catch (Exception e){
					MessageBox.Show(e.Message);
				}
			}
		}
		#endregion
		
		#region Заполнение данными конфигурации комплекса
		void GetData(DataGridView dvgItem, string tblName)
		{
			GetConnectionString();
			
			using(SqlConnection connection = new SqlConnection(builder.ConnectionString))
			{
				SqlCommand command = new SqlCommand("SELECT * FROM " + tblName, connection);
				connection.Open();
				SqlDataReader reader = command.ExecuteReader();
				if(reader.HasRows)
				{
					while(reader.Read())
					{
						dvgItem.Rows.Add(reader.GetInt32(0), reader.GetInt32(1),reader.GetInt32(2), reader.GetInt32(3), reader.GetInt32(4));
					}
				}
				reader.Close();
			}
		}
		
		#endregion
		
		#region Заполнение справочников из БД
		void GetData(DataGridView dvgItem, DataSet ds, string tblItem)
		{
			GetConnectionString();
			
			using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
			{
				SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM " + tblItem, connection);
				adapter.Fill(ds);
				dvgItem.DataSource = ds.Tables[0];
			}
		}
		#endregion
		
		#region Обновление справочников в БД
		private void UpdateData(DataGridView dvgItem, DataSet ds, string tblItem)
		{
			GetConnectionString();
			
			using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
			{
				SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM " + tblItem, connection);
				SqlCommandBuilder build = new SqlCommandBuilder(adapter);
				adapter.Update(ds.Tables[0]);
			}
		}
		#endregion
		
		#region Обновление данных конфигурации комплекса
		private void UpdateData(DataGridView dvgItem, string tblItem)
		{
			GetConnectionString();
			
			using(SqlConnection connection = new SqlConnection(builder.ConnectionString))
			{
				SqlCommand command = new SqlCommand("DELETE FROM " + tblItem, connection);
				connection.Open();
				command.ExecuteNonQuery();
				StringBuilder query = new StringBuilder();
				query.AppendFormat("INSERT INTO {0} VALUES ", tblItem);
				for(int i = 0; i < dvgItem.Rows.Count - 1; i++)
				{ 
					query.Append("(");
					for (int j = 0; j < dvgItem.Rows[i].Cells.Count; j++ )
    				{
						query.AppendFormat("{0}, ", dvgItem.Rows[i].Cells[j].Value.ToString());
    				}
					query.Remove(query.Length-2,2);
					query.Append("),");
				
				}
				
				query.Remove(query.Length-1, 1);
				
				
				command = new SqlCommand(query.ToString(), connection);
				command.ExecuteNonQuery();
				
			}
		}
		#endregion
		
		#region Создание строки соединения
		void GetConnectionString()
		{
			builder.DataSource = cbDataSource.Text;
			builder.InitialCatalog = cbDBName.Text;
			builder.UserID = tbUserName.Text;
			builder.Password = tbPassword.Text;
		}
		#endregion
		
		
	#endregion
	
	#region Работа с конфигурацией
	
		#region Обновление списков параметров
		void ReloadParameters()
		{
			GetParameters((DataGridViewComboBoxColumn)AnalogGrid.Columns[1], "dictChannels");
			GetParameters((DataGridViewComboBoxColumn)AnalogGrid.Columns[2], "dictUltramat");
			GetParameters((DataGridViewComboBoxColumn)AnalogGrid.Columns[3], "dictParameters");
			GetParameters((DataGridViewComboBoxColumn)AnalogGrid.Columns[4], "dictGases");
			GetParameters((DataGridViewComboBoxColumn)AnalogGrid.Columns[5], "dictUnits");
			GetParameters((DataGridViewComboBoxColumn)DiscreteGrid.Columns[1], "dictChannels");
			GetParameters((DataGridViewComboBoxColumn)DiscreteGrid.Columns[2], "dictUltramat");
			GetParameters((DataGridViewComboBoxColumn)DiscreteGrid.Columns[3], "dictDiscretes");
		}
		#endregion
		
		#region Обновление данных из справочников
		void ReloadData()
		{
			
			GetData(ChannelGrid, dsChannels, "dictChannels");
			GetData(UltramatGrid, dsUltramat, "dictUltramat");
			GetData(GasGrid, dsGases, "dictGases");
			GetData(ParamGrid, dsParameters, "dictParameters");
			GetData(DiscGrid, dsDiscretes, "dictDiscretes");
			GetData(UnitGrid, dsUnits, "dictUnits");
			GetData(DiscreteGrid, "confDiscrete");
			GetData(AnalogGrid, "confAnalog");
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
			// Тип БД
			dbSettings.Settings["DBType"].Value = cbDBType.Text;
			// Источник данных
			dbSettings.Settings["DBDataSource"].Value = cbDataSource.Text;
			// Имя БД
			dbSettings.Settings["DBName"].Value = cbDBName.Text;
			// Пользователь
			dbSettings.Settings["DBUser"].Value = tbUserName.Text;
			// Пароль
			dbSettings.Settings["DBPassword"].Value = tbPassword.Text;
			// Период опроса
			dbSettings.Settings["DBPeriod"].Value = cbPeriod.Text;
			
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
			UpdateData(ChannelGrid, dsChannels, "dictChannels");
			UpdateData(UltramatGrid, dsUltramat, "dictUltramat");
			UpdateData(GasGrid, dsGases, "dictGases");
			UpdateData(ParamGrid, dsParameters, "dictParameters");
			UpdateData(DiscGrid,dsDiscretes,"dictDiscretes");
			UpdateData(UnitGrid,dsUnits,"dictUnits");
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
			GetDBList();
		}
		#endregion
		
		#region Нажатие сохранение конфигурации аналогох параметров
		void btnDiscreteSave_Click(object sender, EventArgs e)
		{
			UpdateData(DiscreteGrid, "confDiscrete");
		
		}
		void btnAnalogSave_Click(object sender, EventArgs e)
		{
			UpdateData(AnalogGrid, "confAnalog");
		}
		#endregion
		
		
		
	}
}
