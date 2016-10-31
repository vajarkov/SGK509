using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Timers;
using System.Threading;
using System.Collections.Generic;
using System.Configuration;
using System.Configuration.Install;
using System.Linq;


namespace SGKService
{
	public class SGKService : ServiceBase
	{
		public const string MyServiceName = "SGKService";
		public EventLog eventLog = new EventLog();      	// Переменная для записи в журнал событий
		private System.Timers.Timer timerSrv;           	// Таймер периодичности опроса
		private AppSettingsSection modbusSettings;    		// Переменная для конфигурации файлов с данными
		private AppSettingsSection dbSettings;  			// Переменная для конфигурации порта
		private int periodDBWrite;
		
		#region Инициализация службы
		public SGKService()
		{
			InitializeComponent();
			
			//Отключаем автоматическую запись в журнал
            AutoLog = false;
            
            // Создаем журнал событий и записываем в него
            if (!EventLog.SourceExists("SGKService")) //Если журнал с таким названием не существует
            {
                EventLog.CreateEventSource("SGKService", "SGKService"); // Создаем журнал
            }
            eventLog.Source = "SGKService"; //Помечаем, что будем писать в этот журнал
            
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
		{/*
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
			*/
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
			#region Запись в журнал
			eventLog.WriteEntry("Служба запущена");
			#endregion
			Int32.TryParse(dbSettings.Settings["DBPeriod"].Value, out periodDBWrite);
			#region Инициализация таймера
            //Инициализация таймера
            timerSrv = new System.Timers.Timer();
            //Задание интервала опроса
            timerSrv.Interval =  periodDBWrite*1000;
            //Включение таймера
            timerSrv.Enabled = true;
            //Добавление обработчика на таймер
            //timerSrv.Elapsed += new ElapsedEventHandler(ReadAndModbus);
            //Автоматический взвод таймера 
            timerSrv.AutoReset = true;
            //Старт таймера
            timerSrv.Start();
   
            #endregion
		}
		
		/// <summary>
		/// Stop this service.
		/// </summary>
		protected override void OnStop()
		{
			#region Запись в журнал
            eventLog.WriteEntry("Служба остановлена");
            #endregion
		}
	}
}