using System;
using System.Collections;
using System.Diagnostics;
using System.ServiceProcess;
using System.Timers;
using System.Threading;
using System.Linq;
using DataTransfer;
using SGKSocketServer;




namespace SGKService
{
	public class SGKService : ServiceBase
	{
		// Имя сервиса
		public const string MyServiceName = "SGKService";
		// Журнал событий
		public EventLog eventLog = new EventLog();
		//Блокировка MainThread
		private object lockMainThread = new object();
		// Таймер периодичности опроса
		private System.Timers.Timer timerSrv;
		// Флаг работы сервиса
		private bool serviceWork = true;
		// Класс для работы с данными
		DataClass dataTransfer = new DataClass();
		// Поток 
		private Thread Worker = null;
		// Сброс таймера
		//AutoResetEvent StopRequest = new AutoResetEvent(false);
		private AsynchronousSocketListener socketServer = new AsynchronousSocketListener();
		
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
			int periodDBWrite = dataTransfer.GetDBPeriod();
			if (periodDBWrite > 0)
			{
				//Инициализация таймера
            	timerSrv = new System.Timers.Timer();
            	//Задание интервала опроса
            	timerSrv.Interval =  periodDBWrite*1000;
            	//Включение таймера
            	timerSrv.Enabled = true;
            	//Добавление обработчика на таймер
            	timerSrv.Elapsed += new ElapsedEventHandler(dataTransfer.WriteDBSignals);
            	//Автоматический взвод таймера 
            	timerSrv.AutoReset = true;
            	//Старт таймера
            	timerSrv.Start();
            	
            	#region Создаем сокет для прослушивания подключений
            	eventLog.WriteEntry("Сокет-сервер запущен");
            	try
            	{
            		AsynchronousSocketListener.StartListening();
            		eventLog.WriteEntry("Сокет-сервер запущен");
            	}
            	catch(Exception ex)
            	{
            		eventLog.WriteEntry(ex.ToString());
            	}
            	#endregion
            	Worker = new Thread(MainThread);
            	Worker.Start();
            	
			}
			else
			{
				eventLog.WriteEntry("Введите период опроса");
			}
            #endregion
            
            
		}


		void MainThread()
		{
			while(serviceWork){
           		try
           		{
           			
           			{
	           			eventLog.WriteEntry("MainThread : Запуск основного потока опроса данных");
	           			
	           			#region Запуск основого потока
            			Thread main = new Thread(dataTransfer.MainThread);
            			//main.Join();
            			#endregion
	           		}
           			
           		}
           		catch(Exception ex)
           		{
           			eventLog.WriteEntry("MainThread : " + ex.ToString());
           		}
			}
           	//if (StopRequest.WaitOne(1000)) 
           	//return;
		}
		
		
		/// <summary>
		/// Stop this service.
		/// </summary>
		protected override void OnStop()
		{
			//StopRequest.Set();
			Worker.Join();
			#region Запись в журнал
            eventLog.WriteEntry("Служба остановлена");
            #endregion
		}
	
	}
	
}