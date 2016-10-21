/*
 * Created by SharpDevelop.
 * User: Technical department
 * Date: 17.10.2016
 * Time: 12:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
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
		private ModbusSettings modbusSettings = new ModbusSettings();    										// Переменная для конфигурации файлов с данными
		private AppSettingsSection dbSettings;  									// Переменная для конфигурации порта
		private EventLog events = new EventLog();										// Переменная для записи событий
		private const string serviceName = "SGKService";								// Переменная для имени службы
		private DataSet dsChannels = new DataSet();												// Переменная для обновления справочника Точек отбора
		private DataSet dsUltramat = new DataSet();												// Переменная для обновления справочника Ultramat
		private DataSet dsUnits = new DataSet();												// Переменная для обновления справочника Единиц измерения
		private DataSet dsGases = new DataSet();												// Переменная для обновления справочника Точек отбора
		private DataSet dsDiscretes = new DataSet();											// Переменная для обновления справочника Точек отбора
		private DataSet dsParameters = new DataSet();											// Переменная для обновления справочника Точек отбора
		
		
		#region Конструктор для приложения
		public MainForm()
		{
			InitializeComponent();
			CheckService(serviceName);					// Проверка существования службы
			ConfigurationInit();						// Инициализация конфигурации
			fillRTU();
			// Заполнение протоколов передачи
			//ComboBoxInit(cbProtocol, new string[] {"Modbus RTU", "Modbus TCP"}, "Protocol");
			
		}
		#endregion
		
		#region Метод для заполнения конфигурационных списков данными
		private void ComboBoxInit(ComboBox cbItem, string[] strItems, string section)
		{
			foreach (string item in strItems)
			{
				cbItem.Items.Add(item);
			}
			//cbItem.SelectedItem = SerialPortSection.Settings[section].Value;
		}
		#endregion

		#region Конфигурация программы
		private void ConfigurationInit()
		{
			appConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
			modbusSettings = (ModbusSettings)appConfig.GetSection("ModbusSettings");	
			dbSettings = (AppSettingsSection)appConfig.GetSection("DBSettings");

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
			ComboBoxInit(cbPort, SerialPort.GetPortNames(), "PortName");
			// Заполнение скорости передачи
			ComboBoxInit(cbBaudRate, new string[] { "1200", "2400", "4800", "9600", "19200", "38400", "57600", "115200", "230400" }, "BaudRate");
			// Заполнение четности портов
			ComboBoxInit(cbParity, Enum.GetNames(typeof (Parity)), "Parity");
			// Заполнение Стоп-Битов
			ComboBoxInit(cbStopBit, Enum.GetNames(typeof (StopBits)), "StopBits");
			// Заполнение Битов Данных
			ComboBoxInit(cbDataBits, new string[] { "4", "5", "6", "7", "8", }, "DataBits");
			// Заполнение типов Базы данных
			ComboBoxInit(cbDBType, new string[] {"MS SQL Server", "Oracle", "PostgreSQL", "MySQL"}, "DB");
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
		void GetDataSources(object sender, MouseEventArgs e)
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
		void GetDBList(object sender, EventArgs e)
		{
			var DBList = new Microsoft.SqlServer.Management.Smo.Server(cbDataSource.SelectedItem.ToString());
			foreach(Microsoft.SqlServer.Management.Smo.Database db in DBList.Databases){
				cbDBName.Items.Add(db.Name);
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
						GetParameters((DataGridViewComboBoxColumn)AnalogGrid.Columns[1], "dictChannels");
						GetParameters((DataGridViewComboBoxColumn)AnalogGrid.Columns[2], "dictUltramat");
						GetParameters((DataGridViewComboBoxColumn)AnalogGrid.Columns[3], "dictParameters");
						GetParameters((DataGridViewComboBoxColumn)AnalogGrid.Columns[4], "dictGases");
						GetParameters((DataGridViewComboBoxColumn)AnalogGrid.Columns[5], "dictUnits");
						GetParameters((DataGridViewComboBoxColumn)DiscreteGrid.Columns[1], "dictChannels");
						GetParameters((DataGridViewComboBoxColumn)DiscreteGrid.Columns[2], "dictUltramat");
						GetParameters((DataGridViewComboBoxColumn)DiscreteGrid.Columns[3], "dictDiscretes");
						GetData(ChannelGrid, dsChannels, "dictChannels");
						GetData(UltramatGrid, dsUltramat, "dictUltramat");
						GetData(GasGrid, dsGases, "dictGases");
						GetData(ParamGrid, dsParameters, "dictParameters");
						GetData(DiscGrid, dsDiscretes,"dictDiscretes");
						GetData(UnitGrid, dsUnits,"dictUnits");
						
						
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
		
	#endregion
	
		#region Заполнение ячеек таблиц конфигурации данными
		void GetParameters(DataGridViewComboBoxColumn cbItem, string tableName)
		{
				
			builder.DataSource = cbDataSource.SelectedItem.ToString();
			builder.InitialCatalog = cbDBName.SelectedItem.ToString();
			builder.UserID = tbUserName.Text;
			builder.Password = tbPassword.Text;
						
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
			
		
		
		#region Проверка связи с MSSQL
		bool MsSQLTest()
		{
			builder.DataSource = cbDataSource.SelectedItem.ToString();
			builder.InitialCatalog = cbDBName.SelectedItem.ToString();
			builder.UserID = tbUserName.Text;
			builder.Password = tbPassword.Text;
			
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
		
		
		#region Заполнение справочников из БД
		void GetData(DataGridView dvgItem, DataSet ds, string tblItem)
		{
			builder.DataSource = cbDataSource.SelectedItem.ToString();
			builder.InitialCatalog = cbDBName.SelectedItem.ToString();
			builder.UserID = tbUserName.Text;
			builder.Password = tbPassword.Text;
			
			using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
			{
				SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM " + tblItem, connection);
				//DataTable table = new DataTable();
				adapter.Fill(ds);
				dvgItem.DataSource = ds.Tables[0];
			}
		}
		#endregion
		
		#region Обновление справочников в БД
		private void UpdateDict(DataGridView dvgItem, DataSet ds, string tblItem)
		{
			builder.DataSource = cbDataSource.SelectedItem.ToString();
			builder.InitialCatalog = cbDBName.SelectedItem.ToString();
			builder.UserID = tbUserName.Text;
			builder.Password = tbPassword.Text;
			
			using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
			{
				SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM " + tblItem, connection);
				SqlCommandBuilder build = new SqlCommandBuilder(adapter);
				//DataTable table = new DataTable();
				adapter.Update(ds.Tables[0]);
				//dvgItem.DataSource = table;
			}
		}
		#endregion
		
		#region Сохранение изиенений в справочниках
		void btnDictSave_Click(object sender, EventArgs e)
		{
			UpdateDict(ChannelGrid, dsChannels, "dictChannels");
			UpdateDict(UltramatGrid, dsUltramat, "dictUltramat");
			UpdateDict(GasGrid, dsGases, "dictGases");
			UpdateDict(ParamGrid, dsParameters, "dictParameters");
			UpdateDict(DiscGrid,dsDiscretes,"dictDiscretes");
			UpdateDict(UnitGrid,dsUnits,"dictUnits");
		}
		#endregion
		
		#region Сохранение конфигурации
		void btnProtocolSave_Click(object sender, EventArgs e)
		{
			/*
			if(radioRTU.Checked)
			{
				modbusSettings.ModbusParams["Type"].value = "RTU";
				modbusSettings.ModbusParams["COM"] = cbPort.SelectedText;
				modbusSettings.ModbusParams["BaudRate"] = cbBaudRate.SelectedText;
				modbusSettings.ModbusParams["Parity"] = cbParity.SelectedText;
				modbusSettings.ModbusParams["StopBits"] = cbStopBit.SelectedText;
				modbusSettings.ModbusParams["DataBits"] = cbDataBits.SelectedText;
				modbusSettings.ModbusParams["SlaveId"] = tbModbusRTUSlave.Text;
				
			}
			else if (radioTCP.Checked)
			{
				modbusSettings.ModbusParams["IP"] = tbModbusTCPAddress.Text;
				modbusSettings.ModbusParams["TCPPort"] = tbModbusTCPPort.Text;
				modbusSettings.ModbusParams["SlaveId"] = tbModbusTCPSlave.Text;
			}
			appConfig.Save();
			*/
		}
		#endregion
	}
}
