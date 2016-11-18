using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Timers;
using System.Threading;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Install;
using System.Linq;
using DataTypes;
using Interfaces;
using ModbusReader;
using MSDataBase;
using System.Globalization;




namespace SGKService
{
	public class SGKService : ServiceBase
	{
		public const string MyServiceName = "SGKService";
		// Переменная для записи в журнал событий
		public EventLog eventLog = new EventLog();
		// Таймер периодичности опроса
		private System.Timers.Timer timerSrv;
		// Переменная для конфигурации файлов с данными
		private AppSettingsSection modbusSettings;
		// Переменная для конфигурации порта
		private AppSettingsSection dbSettings;
		// Переменная периода записи в БД
		private int periodDBWrite;
		// Переменная для хранения значений аналоговых сигналов
		private Dictionary<int, AnalogSignal> AnalogSignals = new Dictionary<int, AnalogSignal>();
		// Переменная для хранения значений дискретных сигналов
		private Dictionary<int, DiscreteSignal> DiscreteSignals = new Dictionary<int, DiscreteSignal>();
		private bool serviceWork = true;
		private IModbusReader modbusReader;
		private IDataBase dbSource;
		private byte slaveId;
		private Thread Worker;
		AutoResetEvent StopRequest = new AutoResetEvent(false);
		
		#region Инициализация службы
		public SGKService()
		{
			InitializeComponent();
			
			//Отключаем автоматическую запись в журнал
            AutoLog = false;
            
            // Создаем журнал событий и записываем в него
            if (!EventLog.SourceExists(MyServiceName)) //Если журнал с таким названием не существует
            {
                EventLog.CreateEventSource(MyServiceName, MyServiceName); // Создаем журнал
            }
            eventLog.Source = MyServiceName; //Помечаем, что будем писать в этот журнал
            
            // Путь к конфигурации 
            string exePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SGK509ClientWPF.exe");
            // Откртытие конфигурационного файла
            System.Configuration.Configuration appConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).HasFile ? ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None) : ConfigurationManager.OpenExeConfiguration(exePath);
         
            // Считывание конфигурации БД
			dbSettings = (AppSettingsSection)appConfig.GetSection("DBSettings");
			// Считывание конфигурации Modbus
			modbusSettings = (AppSettingsSection)appConfig.GetSection("ModbusSettings");
			// Заполнение конфигурационных данных
			//GetConfigModbus();	// Считывание параметров Modbus из файла
			//GetConfigDB();		// Считывание параметров БД из файла        
		}
		#endregion
		
		#region Загрузка параметров для Modbus
		void GetConfigModbus()
		{
			// Проверка есть ли данные
			if (!String.IsNullOrEmpty(modbusSettings.Settings["MBType"].Value))
			{
				slaveId = Convert.ToByte(modbusSettings.Settings["MBSlave"].Value);
				// Modbus RTU
				if(modbusSettings.Settings["MBType"].Value == "RTU")
				{
					// COM-порт
					try
					{
						modbusReader = new ModbusRTUReader(
							modbusSettings.Settings["MBSerialPort"].Value, Convert.ToInt32(modbusSettings.Settings["MBBaudRate"].Value), (System.IO.Ports.Parity) Enum.Parse(typeof(System.IO.Ports.Parity), modbusSettings.Settings["MBParity"].Value),
							Convert.ToInt16(modbusSettings.Settings["MBDataBits"].Value),
							(System.IO.Ports.StopBits) Enum.Parse(typeof(System.IO.Ports.StopBits), modbusSettings.Settings["MBStopBit"].Value));
						
					}
					catch (Exception ex)
					{
						eventLog.WriteEntry("Configuratin : " + ex.Message);
					}
				}
				// Modbus TCP
				else if (modbusSettings.Settings["MBType"].Value == "TCP")
				{
					try
					{
					// Создание устройства Modbus TCP
						modbusReader = new ModbusTCPReader(
						modbusSettings.Settings["MBIPAddress"].Value,
						Convert.ToInt32(modbusSettings.Settings["MBTCPPort"].Value));
					}
					catch (Exception ex)
					{
						eventLog.WriteEntry("Configuratin : " + ex.Message);
					}
				}
			}
		}
		#endregion
		
		#region Загрузка конфигурации БД
		void GetConfigDB()
		{
			dbSource = new DataBase();
			dbSource.SetDBConnectionParameters(
				dbSettings.Settings["DBDataSource"].Value,
				dbSettings.Settings["DBName"].Value,
				dbSettings.Settings["DBUser"].Value,
				dbSettings.Settings["DBPassword"].Value);
			
		}
		#endregion
				
		private void InitializeComponent()
		{
			this.ServiceName = MyServiceName;
			// 
            // SKGService
            // 
            this.CanHandlePowerEvent = true;
            this.CanPauseAndContinue = true;
            this.CanShutdown = true;
            //this.ServiceName = "SGKService";
		}
		
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			// TODO: Add cleanup code here (if required)
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// Start this service.
		/// </summary>
		protected override void OnStart(string[] args)
		{
			// Запись в журнал
			eventLog.WriteEntry("Служба запущена");
			
			#region Задание интервала и метода выполнения
			Int32.TryParse(dbSettings.Settings["DBPeriod"].Value, out periodDBWrite);
			eventLog.WriteEntry(periodDBWrite.ToString());
			if (periodDBWrite > 0)
			{
				//Инициализация таймера
            	timerSrv = new System.Timers.Timer();
            	//Задание интервала опроса
            	timerSrv.Interval =  periodDBWrite*1000;
            	//Включение таймера
            	timerSrv.Enabled = true;
            	//Добавление обработчика на таймер
            	timerSrv.Elapsed += new ElapsedEventHandler(WriteDBSignals);
            	//Автоматический взвод таймера 
            	timerSrv.AutoReset = true;
            	//Старт таймера
            	timerSrv.Start();
            	// Считывание конфигурации Modbus
            	GetConfigModbus();
            	if (modbusReader != null)
            	{
            		// Считывание конфигурации БД
            		GetConfigDB();
            	}
            	// Запуск основого потока
            	Worker = new Thread(MainThread);
            	Worker.Start();
			}
			else
			{
				eventLog.WriteEntry("Введите период опроса");
			}
            #endregion
            
            #region Создаем сокет для прослушивания подключений
            
            #endregion
		}

        #region Основной поток опроса данных через Modbus	
		void MainThread()
		{
           while(serviceWork){
           try
           {
           		// Заполняем структуру для дискретных сигналов
	       		foreach(KeyValuePair<int, int> item in dbSource.GetParams("confDiscrete"))
	       		{
	       			// Создаем элемент Дискретного сигнала
	       			DiscreteSignal signal = new DiscreteSignal();
	       			signal.Modbus_address = item.Value;				// Адрес Modbus
	       			signal.Timestamp = DateTime.Now;				// Время записи
	       			// Считываем значение дискретного сигнала
	       			signal.Value = modbusReader.ReadDiscrete(slaveId, Convert.ToUInt16(item.Value));
	            	// Если ключа нет в списке
	       			if (!DiscreteSignals.ContainsKey(item.Key))
	       			{
	       				// Добавляем в список
	            		DiscreteSignals.Add(item.Key, signal);
	            	}
	            	else 
	            	{
	            		// Обновляем значение
	            		DiscreteSignals[item.Key] = signal;
	            	}
	            }
	            // Заполняем структуру для аналоговых сигналов
	            foreach(KeyValuePair<int, int> item in dbSource.GetParams("confAnalog"))
	            {
	            	// Создаем элемент для Аналогового сигнала
	            	AnalogSignal signal = new AnalogSignal();
	            	signal.Modbus_address = item.Value;
	            	signal.Timestamp = DateTime.Now;
	            		
	            	signal.Value = modbusReader.ReadAnalog(slaveId, Convert.ToUInt16(item.Value));
	            	//eventLog.WriteEntry(signal.Value.ToString());
	            	if (!AnalogSignals.ContainsKey(item.Key))
	            	{
	            		AnalogSignals.Add(item.Key, signal);
	            	}
	            	else 
	            	{
	            		AnalogSignals[item.Key] = signal;
	            	}
	        	}
         	}
           catch(Exception ex)
           {
            	eventLog.WriteEntry(ex.Message);
           }
           	
           if (StopRequest.WaitOne(1000)) 
           	return;
           
		}
	}
		#endregion
		
		#region Запись сигналов в БД
		void WriteDBSignals(object sender, EventArgs e)
		{
			foreach(KeyValuePair <int, DiscreteSignal> item in DiscreteSignals)
			{
				try
				{
					DiscreteSignal signal = (DiscreteSignal) item.Value;
					dbSource.InsertSignal("discrete_log", item.Key.ToString(), signal.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"), signal.Value.ToString());
				} 
				catch(Exception ex)
				{
					eventLog.WriteEntry(ex.Message);
				}
			}
			
			foreach(KeyValuePair <int, AnalogSignal> item in AnalogSignals)
			{
				try
				{
					AnalogSignal signal = (AnalogSignal) item.Value;
					//eventLog.WriteEntry(signal.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"));
					dbSource.InsertSignal("analog_log", item.Key.ToString(), signal.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"), signal.Value.ToString("F", CultureInfo.InvariantCulture));
				} 
				catch(Exception ex)
				{
					eventLog.WriteEntry(ex.Message);
				}
			}
		}
		#endregion
		
		
		
		/// <summary>
		/// Stop this service.
		/// </summary>
		protected override void OnStop()
		{
			StopRequest.Set();
			Worker.Join();
			#region Запись в журнал
            eventLog.WriteEntry("Служба остановлена");
            #endregion
		}
		
		//protected override 
	}
}