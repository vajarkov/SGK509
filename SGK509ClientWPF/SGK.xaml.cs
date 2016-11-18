/*
 * Created by SharpDevelop.
 * User: Technical department
 * Date: 10/28/2016
 * Time: 12:46
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows;
using System.Configuration;
using System.ServiceProcess;
using System.Configuration.Install;
using System.Diagnostics;
using System.IO.Ports;
using System.Data;
using System.ComponentModel;
using System.Windows.Data;
using Microsoft.Windows.Controls;
using Interfaces;
using System.Collections.Generic;
using System.Net;
using Microsoft.Win32;

namespace SGK509ClientWPF
{
	/// <summary>
	/// Логика основного приложения Клиент СГК509
	/// </summary>
	public partial class SGK : Window
	{
		#region Переменные для работы со службой
		private const string serviceName = "SGKService";		// Название службы
		private ServiceController controller;   				// Контроллер службы службой
		#endregion
		
		#region Переменные для работы с настройками программы
		private System.Configuration.Configuration appConfig;	// Конфигурации
		private AppSettingsSection dbSettings;  				// База данных
		private AppSettingsSection modbusSettings;    			// Modbus
		private AppSettingsSection serviceSettings;				// Служба
		private AppSettingsSection clientSettings;				// Клиент
		#endregion
		
		#region Переменные для работы с журналом событий
		private EventLog events = new EventLog();				// Журнал событий
		#endregion
		
		#region Переменные для работы с БД
		private DataSet dsChannels = new DataSet();				// справочника Точек отбора
		private DataSet dsUltramat = new DataSet();				// справочника Ultramat
		private DataSet dsUnits = new DataSet();				// справочника Единиц измерения
		private DataSet dsGases = new DataSet();				// справочника Точек отбора
		private DataSet dsDiscretes = new DataSet();			// справочника Точек отбора
		private DataSet dsParameters = new DataSet();			// справочника Точек отбора
		private DataSet dsTypes = new DataSet();				// справочника Типов аналоговых данных
		private DataSet dsAnalogConf = new DataSet();			// конфигурации аналоговых сигналов
		private DataSet dsDiscreteConf = new DataSet();			// конфигурации дискретных сигналов
		private IDataBase dbSource;								// работы с БД
		#endregion
		
		#region Конструктор приложения
		public SGK()
		{
			InitializeComponent();
			CheckService(serviceName);	// Проверка существования службы
			ConfigurationInit();		// Инициализация конфигурации
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
			// Считывание конфигурации Службы
			serviceSettings = (AppSettingsSection)appConfig.GetSection("ServiceSettings");
			// Считывание конфигурации Клиента
			clientSettings = (AppSettingsSection)appConfig.GetSection("ClientSettings");
			
			// Заполнение конфигурационных данных
			fillRTU();			// Параметры Modbus RTU
			GetConfigModbus();	// Считывание параметров Modbus из файла
			GetConfigDB();		// Считывание параметров БД из файла
			GetConfigService();	// Считывание параметров Службы из файла
			GetConfigClient();	// Считывание параметров Клиента из файла
		}
		#endregion
		
		#region Метод для заполнения конфигурационных списков данными
		private void ComboBoxInit(System.Windows.Controls.ComboBox cbItem, string[] strItems, string section)
		{
			// Перебор массива данных 
			foreach (string item in strItems)
			{
				cbItem.Items.Add(item);
			}
			if ((!String.IsNullOrEmpty(section))&&(!String.IsNullOrEmpty(dbSettings.Settings[section].Value)))
				cbItem.SelectedItem = dbSettings.Settings[section].Value;
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
					radioRTU.IsChecked = true;
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
					radioTCP.IsChecked = true;
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
			ComboBoxInit(cbPeriod, new string[] {"1", "5", "30", "60", "180", "300"}, "DBPeriod");
			// Выбор источника данных БД из конфигурации, если он прописан
			if (!String.IsNullOrEmpty(dbSettings.Settings["DBDataSource"].Value))
			{
				cbDataSource.IsEnabled = true;
				//GetDataSources();
				//Thread dataSourceThread = new Thread(new ThreadStart(GetDataSources));
				//dataSourceThread.Start();
				//dataSourceThread.Join();
				cbDataSource.Text = dbSettings.Settings["DBDataSource"].Value;
			}	
			// Заполнение пользователя из конфигурации, если он прописан
			if (!String.IsNullOrEmpty(dbSettings.Settings["DBUser"].Value))
				tbUserName.Text = dbSettings.Settings["DBUser"].Value;
			// Заполнение пароля из конфигурации, если он прописан
			if (!String.IsNullOrEmpty(dbSettings.Settings["DBPassword"].Value))
				tbPassword.Password = dbSettings.Settings["DBPassword"].Value;
			// Выбор БД из конфигурации, если он прописан
			if (!String.IsNullOrEmpty(dbSettings.Settings["DBName"].Value))
			{
				cbDBName.IsEnabled = true;
				dbSource = new MSDataBase.DataBase();
				cbDBName.Items.Clear();
				dbSource.SetDBConnectionParameters(cbDataSource.Text, dbSettings.Settings["DBName"].Value, tbUserName.Text, tbPassword.Password);
				//cbDBName.ItemsSource = dbSource.GetDBList();
				cbDBName.Text = dbSettings.Settings["DBName"].Value;
			}
			if((!String.IsNullOrEmpty(cbDataSource.Text)) && (!String.IsNullOrEmpty(cbDBName.Text)) && (!String.IsNullOrEmpty(tbUserName.Text)) && (!String.IsNullOrEmpty(tbPassword.Password)))
			{
				dbSource.SetDBConnectionParameters(cbDataSource.Text, cbDBName.Text, tbUserName.Text, tbPassword.Password);
				ReloadParameters();
				ReloadData();
				btnTest.IsEnabled = true;
			}
		}
		#endregion
		
		#region Загрузка параметров Службы
		void GetConfigService()
		{
			// IP адрес службы
			if(!String.IsNullOrEmpty(serviceSettings.Settings["IP"].Value))
				tbServiceIP.Text = serviceSettings.Settings["IP"].Value;
			// Порт службы
			if(!String.IsNullOrEmpty(serviceSettings.Settings["Port"].Value))
				tbServicePort.Text = serviceSettings.Settings["Port"].Value;
		}
		#endregion
		
		#region Загрузка параметров Клиента
		void GetConfigClient()
		{
			// IP адрес службы
			//if(!String.IsNullOrEmpty(clientSettings.Settings["alarmAna"].Value))
			//	tbServiceIP.Text = clientSettings.Settings["alarm"].Value;
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
				btnDelete.IsEnabled = true;    	// Кнокпа "Установить" активна
				btnInstall.IsEnabled = false;  	// Кнокпа "Удалить" заблокирована
			}
			else
			{
				btnDelete.IsEnabled = false;   	// Кнокпа "Установить" заблокирована
				btnInstall.IsEnabled = true;   	// Кнокпа "Удалить" активна
				btnStop.IsEnabled = false;	   	// Кнокпа "Стоп" заблокирована
				btnStart.IsEnabled = false; 	// Кнокпа "Старт" заблокирована
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
				btnStop.IsEnabled = true;   // Кнокпа "Стоп" активна
				btnStart.IsEnabled = false; // Кнокпа "Старт" заблокирована
			}
			else
			{
				btnStop.IsEnabled = false;  // Кнокпа "Стоп" заблокирована
				btnStart.IsEnabled = true;  // Кнокпа "Старт" активна
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
					System.Windows.MessageBox.Show(ex.Message);
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
					System.Windows.MessageBox.Show(ex.Message);
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
				btnStop.IsEnabled = true;
				btnStart.IsEnabled = false;
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
				btnStop.IsEnabled = false;
				btnStart.IsEnabled = true;
			}
		}
		#endregion
		
		#region Обновление данных событий службы
		void btnRefresh_Click(object sender, EventArgs e)
		{
			string machineName;
			// Если IP не указан, то используем локальный адрес
			if(String.IsNullOrEmpty(tbServiceIP.Text))
				machineName = "127.0.0.1";
			else
				// Иначе берем тот, что указан
				machineName = tbServiceIP.Text;
			// Получаем DNS из IP адреса
			string hostName = Dns.GetHostEntry(machineName).HostName.Split('.')[0];
			
			EventSourceCreationData creationData = new EventSourceCreationData(serviceName,serviceName);
			creationData.MachineName = hostName;
				
			// Если журнал существует
			if (EventLog.SourceExists("SGKService", hostName))
            {
				// Считываем источник журнала
				string logName = EventLog.LogNameFromSourceName("SGKService", hostName);
				// Если не совпадает с нужным
				if (logName != "SGKService")
				{
					// Удаляем источник
					EventLog.DeleteEventSource("SGKService", hostName);
					// Создаем нужный источник
					EventLog.CreateEventSource(creationData);
					
				}
				
			} else {
				// Создаем журнал
                EventLog.CreateEventSource(creationData); 
                
			}
			// Имя журнала 
            events.Log = "SGKService";
            // Имя источника
            events.Source = "SGKService";
            // Имя компьютера
            events.MachineName = hostName;
           	
            // Заполняем таблицу для отображения
            if (events.Entries.Count > 0)
            {	
            	// Если таблица пустая, то привязываем ее к журналу 
            	if(dgEvents.ItemsSource == null)
            		dgEvents.ItemsSource = events.Entries;
            	// Обновляем записи
            	CollectionViewSource.GetDefaultView(dgEvents.ItemsSource).Refresh();
            	// Очищаем описание сортировки
            	dgEvents.Items.SortDescriptions.Clear();
			    // Созадем описание сортировки
            	dgEvents.Items.SortDescriptions.Add(new SortDescription(dgEvents.Columns[0].SortMemberPath, ListSortDirection.Descending));
            	
            	// Очищаем сортировку всех столбцов
            	foreach (var col in dgEvents.Columns)
    			{
        			col.SortDirection = null;
    			}
            	// Задаем сортировку времени по убыванию (последняя запись вверху)
            	dgEvents.Columns[0].SortDirection = ListSortDirection.Descending;
            	// Обновляем записи
            	dgEvents.Items.Refresh();
            }
		}
		#endregion
		
		#region Очистка журнала событий
		void btnClear_Click(object sender, RoutedEventArgs e)
		{
			string machineName;
			// Если IP не указан, то используем локальный адрес
			if(String.IsNullOrEmpty(tbServiceIP.Text))
				machineName = "127.0.0.1";
			else
				// Иначе берем тот, что указан
				machineName = tbServiceIP.Text;
			// Получаем DNS из IP адреса
			string hostName = Dns.GetHostEntry(machineName).HostName.Split('.')[0];
			
			EventSourceCreationData creationData = new EventSourceCreationData(serviceName,serviceName);
			creationData.MachineName = hostName;
				
			// Если журнал существует
			if (EventLog.SourceExists("SGKService", hostName))
            {
				// Считываем источник журнала
				string logName = EventLog.LogNameFromSourceName("SGKService", hostName);
				// Если не совпадает с нужным
				if (logName != "SGKService")
				{
					// Удаляем источник
					EventLog.DeleteEventSource("SGKService", hostName);
					// Создаем нужный источник
					EventLog.CreateEventSource(creationData);
					
				}
				
			} else {
				// Создаем журнал
                EventLog.CreateEventSource(creationData); 
                
			}
			// Имя журнала 
            events.Log = "SGKService";
            // Имя источника
            events.Source = "SGKService";
            // Имя компьютера
            events.MachineName = hostName;
            
            events.Clear();
            dgEvents.ItemsSource = null;
		}
		#endregion
		
		#region Нажатие на событие в журнале	
		void dgEvents_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
		{
			IList<DataGridCellInfo> cells = e.AddedCells;
    		foreach (DataGridCellInfo di in cells)
    		{
        		
    			EventLogEntry dvr = (EventLogEntry)di.Item;
        		MessageBox.Show(dvr.Message.ToString(), dvr.TimeGenerated.ToString());
        		break;
    		}
		}
		#endregion
		
		#region Обновление статуса Службы на форме
		void tabItem1_GotFocus(object sender, RoutedEventArgs e)
		{
			CheckService(serviceName);
		}
		#endregion
	#endregion
	
	#region Работа с протоколом Modbus
		#region Выбор протокола Modbus TCP
		void radioTCP_Checked(object sender, RoutedEventArgs e)
		{
			groupRTU.IsEnabled = false;
			groupTCP.IsEnabled = true;
		}
		#endregion
		
		#region Выбор протокола Modbus RTU
		void radioRTU_Checked(object sender, RoutedEventArgs e)
		{
			groupRTU.IsEnabled = true;
			groupTCP.IsEnabled = false;
		}
		#endregion
		
		
	#endregion
	
	#region Работа с БД
	
		#region Автоматическая нумерация строк для сигналов 
		/*void AutoIncriment(object sender, DataGrid e)
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
		}*/
		#endregion
		
		#region Выбор типа базы дынных
		void GetDataSources()
		{
			switch (cbDBType.SelectedItem.ToString())
			{
				case "MS SQL Server":
					dbSource = new MSDataBase.DataBase();
					cbDataSource.ItemsSource = dbSource.Connect();
					cbDataSource.IsEnabled = true;
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
				btnDBType.IsEnabled = true;
			}
		}
		#endregion
		
		#region Выбран сервер БД
		void cbDataSource_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!String.IsNullOrEmpty(cbDataSource.SelectedItem.ToString()))
				btnDBList.IsEnabled = true;
		}
		#endregion
		
		#region Выбрана БД
		void cbDBName_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (!String.IsNullOrEmpty(cbDataSource.SelectedItem.ToString()))
			{
				btnDBSave.IsEnabled = true;
				btnTest.IsEnabled = true;
			}
		}
		#endregion
		
		#region Проверка связи с БД
		void btnTest_Click(object sender, EventArgs e)
		{
			dbSource.SetDBConnectionParameters(cbDataSource.Text, cbDBName.Text, tbUserName.Text, tbPassword.Password);
			if(dbSource.Test())
			{
				System.Windows.MessageBox.Show("Связь установлена");
				ReloadParameters();
				ReloadData();
			}
			else
			{
				System.Windows.MessageBox.Show("Проверьте параметры соединения");
			}
			
		}
		#endregion
		
		#region Сохранение изиенений в справочниках
		void btnDictSave_Click(object sender, EventArgs e)
		{
			// Обновление данных в справочнике точек отбора
			dbSource.UpdateData(ChannelGrid, dsChannels, "dictChannels");
			// Обновление данных в справочнике приборов
			dbSource.UpdateData(UltramatGrid, dsUltramat, "dictUltramat");
			// Обновление данных в справочнике газов
			dbSource.UpdateData(GasGrid, dsGases, "dictGases");
			// Обновление данных в справочнике аналоговых параметров
			dbSource.UpdateData(ParamGrid, dsParameters, "dictParameters");
			// Обновление данных в справочнике дискретных сигналов
			dbSource.UpdateData(DiscGrid, dsDiscretes, "dictDiscretes");
			// Обновление данных в справочнике единиц измерения
			dbSource.UpdateData(UnitGrid, dsUnits, "dictUnits");
			// Обновление данных в справочнике типов данных
			dbSource.UpdateData(TypeGrid, dsTypes, "dictTypes");
			// Обновление параметров в конфигурации
			ReloadParameters();
			// Обновление данных в гридах
			ReloadData();
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
			dbSource.SetDBConnectionParameters(cbDataSource.Text, tbUserName.Text, tbPassword.Password);
			cbDBName.ItemsSource = dbSource.GetDBList();
			cbDBName.IsEnabled = true;
		}
		#endregion
	
		#region Нажатие сохранение конфигурации дискретных параметров
		void btnDiscreteSave_Click(object sender, EventArgs e)
		{
			dbSource.UpdateData(DiscreteGrid, dsDiscreteConf, "confDiscrete");
		}
		#endregion
	
		#region Нажатие сохранение конфигурации аналогох параметров
		void btnAnalogSave_Click(object sender, EventArgs e)
		{
			dbSource.UpdateData(AnalogGrid, dsAnalogConf, "confAnalog");
		}
		#endregion
		
		#region Обновление списков параметров
		void ReloadParameters()
		{
			// Заполнение точек отбора для аналоговых сигналов
			dbSource.GetParameters((DataGridComboBoxColumn)AnalogGrid.Columns[1], "dictChannels");
			// Заполнение ULTRAMAT для аналоговых сигналов
			dbSource.GetParameters((DataGridComboBoxColumn)AnalogGrid.Columns[2], "dictUltramat");
			// Заполнение параметров для аналоговых сигналов
			dbSource.GetParameters((DataGridComboBoxColumn)AnalogGrid.Columns[3], "dictParameters");
			// Заполнение газов для аналоговых сигналов
			dbSource.GetParameters((DataGridComboBoxColumn)AnalogGrid.Columns[4], "dictGases");
			// Заполнение единиц измерения для аналоговых сигналов
			dbSource.GetParameters((DataGridComboBoxColumn)AnalogGrid.Columns[5], "dictUnits");
			// Заполнение типов данных для аналоговых сигналов
			dbSource.GetParameters((DataGridComboBoxColumn)AnalogGrid.Columns[6], "dictTypes");
			// Заполнение точек отбора для дискретных сигналов
			dbSource.GetParameters((DataGridComboBoxColumn)DiscreteGrid.Columns[1], "dictChannels");
			// Заполнение ULTRAMAT для дискретных сигналов
			dbSource.GetParameters((DataGridComboBoxColumn)DiscreteGrid.Columns[2], "dictUltramat");
			// Заполнение дискретных параметров для дискретных сигналов
			dbSource.GetParameters((DataGridComboBoxColumn)DiscreteGrid.Columns[3], "dictDiscretes");
		}
		#endregion
		
		#region Обновление данных из справочников
		void ReloadData()
		{	
			// Загрузка справочника точек отбора
			dbSource.GetData(ChannelGrid, dsChannels, "dictChannels");
			// Загрузка справочника приборов ULTRAMAT
			dbSource.GetData(UltramatGrid, dsUltramat, "dictUltramat");
			// Загрузка справочника газов
			dbSource.GetData(GasGrid, dsGases, "dictGases");
			// Загрузка справочника аналоговых параметров
			dbSource.GetData(ParamGrid, dsParameters, "dictParameters");
			// Загрузка справочника дискретных сигналов
			dbSource.GetData(DiscGrid, dsDiscretes, "dictDiscretes");
			// Загрузка справочника единиц измерения
			dbSource.GetData(UnitGrid, dsUnits, "dictUnits");
			// Загрузка справочника типов данных
			dbSource.GetData(TypeGrid, dsTypes, "dictTypes");
			// Загрузка конфигурации для дискретных сигналов
			dbSource.GetData(DiscreteGrid, dsDiscreteConf, "confDiscrete");
			// Загрузка конфигурации для аналоговых сигналов
			dbSource.GetData(AnalogGrid, dsAnalogConf, "confAnalog");
		}
		#endregion
		
	#endregion
	
	#region Работа с конфигурацией
		
		#region Сохранение конфигурации Modbus
		void btnProtocolSave_Click(object sender, EventArgs e)
		{
			if(radioRTU.IsChecked==true)
			{
				modbusSettings.Settings["MBType"].Value = "RTU";
				modbusSettings.Settings["MBSerialport"].Value = cbPort.Text;
				modbusSettings.Settings["MBBaudRate"].Value = cbBaudRate.Text;
				modbusSettings.Settings["MBParity"].Value = cbParity.Text;
				modbusSettings.Settings["MBStopBit"].Value = cbStopBit.Text;
				modbusSettings.Settings["MBDataBits"].Value = cbDataBits.Text;
				modbusSettings.Settings["MBSlave"].Value = tbModbusRTUSlave.Text;		
			}
			else if (radioTCP.IsChecked==true)
			{
				modbusSettings.Settings["MBType"].Value = "TCP";
				modbusSettings.Settings["MBIPAddress"].Value = tbModbusTCPAddress.Text;
				modbusSettings.Settings["MBTCPPort"].Value = tbModbusTCPPort.Text;
				modbusSettings.Settings["MBSlave"].Value = tbModbusTCPSlave.Text;
			}
			
			// Сохранение конфигурации
			SaveConfig("ModbusSettings");
		}
		#endregion
		
		#region Сохранение конфигурации БД
		void btnDBSave_Click(object sender, EventArgs e)
		{
			dbSettings.Settings["DBType"].Value = cbDBType.Text;			// Тип БД
			dbSettings.Settings["DBDataSource"].Value = cbDataSource.Text;	// Источник данных
			dbSettings.Settings["DBName"].Value = cbDBName.Text;			// Имя БД
			dbSettings.Settings["DBUser"].Value = tbUserName.Text;			// Пользователь
			dbSettings.Settings["DBPassword"].Value = tbPassword.Password;	// Пароль
			dbSettings.Settings["DBPeriod"].Value = cbPeriod.Text;			// Период опроса
			// Сохранение конфигурации
			SaveConfig("DBSettings");
		}
		#endregion
		
		#region Сохранение конфигурации Службы	
		void btnServiceSave_Click(object sender, RoutedEventArgs e)
		{
			serviceSettings.Settings["IP"].Value = tbServiceIP.Text;		// IP адрес службы
			serviceSettings.Settings["Port"].Value = tbServicePort.Text;	// Порт службы
			// Сохранение конфигурации
			SaveConfig("ServiceSettings");
		}
		#endregion
		
		#region Сохранение конфигурации
		void SaveConfig(string sectionName)
		{
			try
			{
				appConfig.Save(ConfigurationSaveMode.Modified);
				ConfigurationManager.RefreshSection(sectionName);
			}
			catch (Exception ex)
			{
				System.Windows.MessageBox.Show(ex.Message);
			}
			
		}
		#endregion
		
		#region Выбор сигнала для Аналогового сигнала
		void btnAnalogAlarm_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Filter = "Звуки *.wav |*.wav";
			if (dialog.ShowDialog() == true)
			{
				clientSettings.Settings["alarmAnalog"].Value = dialog.FileName;
				SaveConfig("ClientSettings");
			}
		}
		#endregion
		
		#region Воспроизведение Аналогового аларма
		void btnAnalogAlarmPlay_Click(object sender, RoutedEventArgs e)
		{
			System.Media.SoundPlayer player = new System.Media.SoundPlayer();
			if(!String.IsNullOrEmpty(clientSettings.Settings["alarmAnalog"].Value))
			{
				player.SoundLocation = clientSettings.Settings["alarmAnalog"].Value;
				player.Load();
				player.Play();
			}
		}
		#endregion
		
		#region Выбор сигнала для Дискретного сигнала
		void btnDiscreteAlarm_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Filter = "Звуки *.wav |*.wav";
			if (dialog.ShowDialog() == true)
			{
				clientSettings.Settings["alarmDiscrete"].Value = dialog.FileName;
				SaveConfig("ClientSettings");
			}
		}
		#endregion
		
		#region Воспроизведение Дискретного аларма
		void btnDiscreteAlarmPlay_Click(object sender, RoutedEventArgs e)
		{
			System.Media.SoundPlayer player = new System.Media.SoundPlayer();
			if(!String.IsNullOrEmpty(clientSettings.Settings["alarmDiscrete"].Value))
			{
				player.SoundLocation = clientSettings.Settings["alarmDiscrete"].Value;
				player.Load();
				player.Play();
			}
		}
		#endregion
		
	#endregion
	}
}