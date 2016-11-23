using System;
using System.Collections;
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
		// Имя сервиса
		public const string MyServiceName = "SGKService";
		// Журнал событий
		public EventLog eventLog = new EventLog();
		// Таймер периодичности опроса
		private System.Timers.Timer timerSrv;
		// Конфигурации файлов с данными
		private AppSettingsSection modbusSettings;
		// Конфигурации порта
		private AppSettingsSection dbSettings;
		// Периода записи в БД
		private int periodDBWrite;
		
		// Флаг работы сервиса
		private bool serviceWork = true;
		
		// Поток 
		private Thread Worker;
		
		// Сброс таймера
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
	
	}
	
}