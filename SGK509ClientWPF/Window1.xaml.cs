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
using Microsoft.Windows.Controls;
using System.Data.Linq;
using Interfaces;


namespace SGK509ClientWPF
{
	/// <summary>
	/// Interaction logic for Window1.xaml
	/// </summary>
	public partial class Window1 : Window
	{
		private System.Configuration.Configuration appConfig;	// Переменная для чтения конфигурации
		private ServiceController controller;   				// Переменная для работы со службой
		private AppSettingsSection modbusSettings;    			// Переменная для конфигурации файлов с данными
		private AppSettingsSection dbSettings;  				// Переменная для конфигурации порта
		private EventLog events = new EventLog();				// Переменная для записи событий
		private const string serviceName = "SGKService";		// Переменная для имени службы
		private DataSet dsChannels = new DataSet();				// Переменная для обновления справочника Точек отбора
		private DataSet dsUltramat = new DataSet();				// Переменная для обновления справочника Ultramat
		private DataSet dsUnits = new DataSet();				// Переменная для обновления справочника Единиц измерения
		private DataSet dsGases = new DataSet();				// Переменная для обновления справочника Точек отбора
		private DataSet dsDiscretes = new DataSet();			// Переменная для обновления справочника Точек отбора
		private DataSet dsParameters = new DataSet();			// Переменная для обновления справочника Точек отбора
		private DataSet dsAnalogConf = new DataSet();			// Переменная для конфигурации аналоговых сигналов
		private DataSet dsDiscreteConf = new DataSet();			// Переменная для конфигурации дискретных сигналов
		private IDataBase dbSource;								// Переменная для работы с БД
		
		#region Конструктор приложения
		public Window1()
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
			
			// Заполнение конфигурационных данных
			fillRTU();			// Параметры Modbus RTU
			GetConfigModbus();	// Считывание параметров Modbus из файла
			GetConfigDB();		// Считывание параметров БД из файла
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
			if (!EventLog.SourceExists("SGKService"))
            {
            	// Создаем журнал
                EventLog.CreateEventSource("SGKService", "SGKService"); 
            }
            events.Log = "SGKService";
            events.Source = "SGKService";
            // Заполняем таблицу для отображения
            if (events.Entries.Count > 0)
            {	
            	dgEvents.ItemsSource = null;
            	//dgEvents.Items.Clear();
            	dgEvents.ItemsSource = events.Entries;
            	dgEvents.Items.Refresh();
            	dgEvents.Items.SortDescriptions.Clear();
			    dgEvents.Items.SortDescriptions.Add(new SortDescription(dgEvents.Columns[0].SortMemberPath, ListSortDirection.Descending));
            	/*//Если есть записи
            	foreach (EventLogEntry entry in events.Entries)
                {
            		//Добавляем запись
            		//DataGridRow grid new DataGridRow();
					dgEvents.Items.Add(new {DateEvent = entry.TimeGenerated, EventMessage = entry.Message});
                    //Берем полседнюю запись
                    var row = (DataGridRow)dgEvents.Items[dgEvents.Items.Count - 1];
            			//dgEvents.ItemContainerGenerator.ContainerFromIndex(dgEvents.Items.Count - 1);
                    // И разукрашиваем...
            		if (entry.EntryType == EventLogEntryType.Error)
                    {
            			row.Background = Brushes.Red;
            			row.Foreground = Brushes.White;
            			// Если ошибка
            			/*dgEvents.ItemContainerGenerator.StatusChanged += (s, evnt) =>
    					{
       							if (dgEvents.ItemContainerGenerator.Status ==  GeneratorStatus.ContainersGenerated)
       							{
          							var row = (DataGridRow)dgEvents.ItemContainerGenerator
                                               .ContainerFromIndex(dgEvents.Items.Count - 1);
          							row.Background = Brushes.Red;
          							row.Foreground = Brushes.White;
       							}
    					};
                    }
                    else
                    {	
                    	row.Background = Brushes.LightBlue;
            			row.Foreground = Brushes.DarkBlue;
                    	/*
                    	// Обычное сообщение
                    	dgEvents.ItemContainerGenerator.StatusChanged += (s, evnt) =>
    					{
       							if (dgEvents.ItemContainerGenerator.Status == GeneratorStatus.ContainersGenerated)
       							{
          							var row = (DataGridRow)dgEvents.ItemContainerGenerator.ContainerFromItem(dgEvents.Items.Get);
          							row.Background = Brushes.LightBlue;
          							row.Foreground = Brushes.DarkBlue;
       							}
    					};
                    }
                }*/
            	// Сортируем по убыванию
            	foreach (var col in dgEvents.Columns)
    			{
        			col.SortDirection = null;
    			}
            	dgEvents.Columns[0].SortDirection = ListSortDirection.Descending;
            	dgEvents.Items.Refresh();
            }
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
	#endregion
	
	#region Работа с конфигурацией
	
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
			// Загрузка конфигурации для дискретных сигналов
			dbSource.GetData(DiscreteGrid, dsDiscreteConf, "confDiscrete");
			// Загрузка конфигурации для аналоговых сигналов
			dbSource.GetData(AnalogGrid, dsAnalogConf, "confAnalog");
		}
		#endregion
		
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
			try
			{
				appConfig.Save(ConfigurationSaveMode.Modified);
				ConfigurationManager.RefreshSection("ModbusSettings");
			}
			catch (Exception ex)
			{
				System.Windows.MessageBox.Show(ex.Message);
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
			dbSettings.Settings["DBPassword"].Value = tbPassword.Password;	// Пароль
			dbSettings.Settings["DBPeriod"].Value = cbPeriod.Text;			// Период опроса
			// Сохранение конфигурации
			try
			{
				appConfig.Save(ConfigurationSaveMode.Modified);
				ConfigurationManager.RefreshSection("DBSettings");
			}
			catch (Exception ex)
			{
				System.Windows.MessageBox.Show(ex.Message);
			}
		}
		#endregion
		
	#endregion
		
	#region Вспомогательные функции
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
			dbSource.UpdateData(DiscGrid,dsDiscretes,"dictDiscretes");
			// Обновление данных в справочнике единиц измерения
			dbSource.UpdateData(UnitGrid,dsUnits,"dictUnits");
			// Обновление параметров в конфигурации
			ReloadParameters();
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
	#endregion
	}
}