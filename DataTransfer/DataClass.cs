/*
 * Created by SharpDevelop.
 * User: Technical department
 * Date: 23.11.2016
 * Time: 9:58
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using DataTypes;
using MSDataBase;
using ModbusReader;
using Interfaces;
using System.Configuration;
using System.Configuration.Install;
using System.Linq;
using System.Diagnostics;
using System.Globalization;
using System.Net;

namespace DataTransfer
{
	/// <summary>
	/// Класс для работы с данными
	/// </summary>
	public class DataClass : IDataClass
	{
		// Название службы
		private string serviceName = "SGKService";
		
		#region Хранение данных
		// Хранения значений аналоговых сигналов
		private Dictionary<int, AnalogSignal> AnalogSignals;
		// Хранения значений дискретных сигналов
		private Dictionary<int, DiscreteSignal> DiscreteSignals;
		#endregion
		
		#region Объекты 
		// Чтение данных по Modbus
		private IModbusReader modbusReader;
		// Работа с БД
		private IDataBase dbSource;
		#endregion
		
		#region Переменные для передачи данных
		// Адрес устройства Modbus
		private byte slaveId;
		// Дискретные сигналы для сокетов
		private byte[] discreteBytes = null;
		// Аналоговые сигналы для сокетов
		private byte[] analogBytes = null;
		#endregion
		
		#region Конфигурация
		// Конфигурации файлов с данными
		private AppSettingsSection modbusSettings;
		// Конфигурации порта
		private AppSettingsSection dbSettings;
		// Конфигурация клиента
		private AppSettingsSection serviceSettings;
		#endregion
		
		// Периода записи в БД
		private int periodDBWrite;
		// Журнал событий
		private EventLog eventLog = new EventLog();
		
		
		#region Получение периода записи в БД
		public int GetDBPeriod()
		{
			Int32.TryParse(dbSettings.Settings["DBPeriod"].Value, out periodDBWrite);
			return periodDBWrite;
		}
		#endregion
		
		#region Конструкторы класса		
		public DataClass()
		{
		
			// Считывание данных
			GetConfigFile();
			GetEventConfig();
			GetConfigDB();
			GetConfigModbus();
			
		}
		
		/*
		static DataClass()
		{
			// Считывание данных
			DataClass dataClass = new DataClass();
			dataClass.GetConfigFile();
			dataClass.GetEventConfig();
			dataClass.GetConfigDB();
			dataClass.GetConfigModbus();
			
		}*/
		#endregion

		#region Объявление структур конфингурации
		private void GetConfigFile()
		{
			// Путь к конфигурации 
            string exePath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "SGK509ClientWPF.exe");
            // Откртытие конфигурационного файла
            System.Configuration.Configuration appConfig = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None).HasFile ? ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None) : ConfigurationManager.OpenExeConfiguration(exePath);
            // Считывание конфигурации БД
			dbSettings = (AppSettingsSection)appConfig.GetSection("DBSettings");
			// Считывание конфигурации Modbus
			modbusSettings = (AppSettingsSection)appConfig.GetSection("ModbusSettings");
			// Считывание конфигурации Клиента
			serviceSettings = (AppSettingsSection)appConfig.GetSection("ServiceSettings");
			
		}
		#endregion
		
		#region Настройка журнала событий
		public void GetEventConfig()
		{
			// Имя машины
			string machineName;
			// Если IP не указан, то используем локальный адрес
			if(String.IsNullOrEmpty(serviceSettings.Settings["IP"].Value))
				machineName = "127.0.0.1";
			else
				// Иначе берем тот, что указан
				machineName = serviceSettings.Settings["IP"].Value;
			// Получаем DNS из IP адреса
			string hostName = Dns.GetHostEntry(machineName).HostName.Split('.')[0];
			
			EventSourceCreationData creationData = new EventSourceCreationData(serviceName,serviceName);
			creationData.MachineName = hostName;
				
			// Если журнал существует
			if (EventLog.SourceExists(serviceName, hostName))
            {
				// Считываем источник журнала
				string logName = EventLog.LogNameFromSourceName(serviceName, hostName);
				// Если не совпадает с нужным
				if (logName != serviceName)
				{
					// Удаляем источник
					EventLog.DeleteEventSource(serviceName, hostName);
					// Создаем нужный источник
					EventLog.CreateEventSource(creationData);
				}
				
			} else {
				// Создаем журнал
                EventLog.CreateEventSource(creationData);        
			}
			// Имя журнала 
            eventLog.Log = serviceName;
            // Имя источника
            eventLog.Source = serviceName;
            // Имя компьютера
            eventLog.MachineName = hostName;
		}
		#endregion
				
		#region Загрузка параметров для Modbus
		private void GetConfigModbus()
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
							modbusSettings.Settings["MBSerialPort"].Value, 
							Convert.ToInt32(modbusSettings.Settings["MBBaudRate"].Value),
							(System.IO.Ports.Parity) Enum.Parse(typeof(System.IO.Ports.Parity), 
							modbusSettings.Settings["MBParity"].Value),
							Convert.ToInt16(modbusSettings.Settings["MBDataBits"].Value),
							(System.IO.Ports.StopBits) Enum.Parse(typeof(System.IO.Ports.StopBits), 
							modbusSettings.Settings["MBStopBit"].Value));
						eventLog.WriteEntry("modbusReader инициализирован");
					}
					catch (Exception ex)
					{
						eventLog.WriteEntry("Configuratin : " + ex.Message, EventLogEntryType.Error);
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
		private void GetConfigDB()
		{
			dbSource = new DataBase();
			dbSource.SetDBConnectionParameters(
				dbSettings.Settings["DBDataSource"].Value,
				dbSettings.Settings["DBName"].Value,
				dbSettings.Settings["DBUser"].Value,
				dbSettings.Settings["DBPassword"].Value);
			
		}
		#endregion
		
		#region Получения размера данных для передачи через сокет из считанных значений
		public int GetSizeBytes()
		{
			return discreteBytes.Length + analogBytes.Length;
		}
		
		public int GetDiscreteSize()
		{
			return discreteBytes.Length;
		}
		
		public int GetAnalogSize()
		{
			return analogBytes.Length;
		}
		#endregion
		
		#region Получения размера данных для передачи через сокет из Базы данных
		public int GetInitAnalogSize()
		{
			return dbSource.GetAnalogSize("confAnalog");
		}
		
		public int GetInitDiscreteSize()
		{
			return dbSource.GetDiscreteSize("confDiscrete");
		}
		
		public int GetInitSize()
		{
			return dbSource.GetDiscreteSize("confDiscrete") + dbSource.GetAnalogSize("confAnalog");
		}
		#endregion
		
		#region Получение адреса службы
		public string GetServiceIPAddress()
		{
			return serviceSettings.Settings["IP"].Value;
		}
		#endregion
		
		#region Получение порта службы
		public int GetServicePort()
		{
			int port;
			Int32.TryParse(serviceSettings.Settings["Port"].Value, out port);
			return port;
		}
		#endregion
		
		#region Запись сигналов в БД
		public void WriteDBSignals(object sender, EventArgs e)
		{
			foreach(KeyValuePair <int, DiscreteSignal> item in DiscreteSignals)
			{
				try
				{
					// Значение сигнала
					DiscreteSignal signal = (DiscreteSignal) item.Value;
					// Запись в БД
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
		
		#region Основной поток опроса данных через Modbus	
		public void MainThread()
		{
          
 			#region Чтение дискретных сигналов
 			// Заполняем структуру для дискретных сигналов
 			try
 			{
 				DiscreteSignals = new Dictionary<int, DiscreteSignal> (dbSource.GetParams("confDiscrete"));
 			}
 			catch(Exception ex)
 			{
 				eventLog.WriteEntry(ex.ToString());
		
 			}
           	// Все значения в один массив
           	BitArray discreteBits = new BitArray(DiscreteSignals.Count);
           	// Перебираем все элементы дискретных сигналов
           	for (int i = 0; i <= DiscreteSignals.Count - 1; i++)
           	{
           		// Записываем дату
           		// DiscreteSignals.ElementAt(i).Value.Timestamp = DateTime.Now;
           		// Записываем значение переменной по адресу Modbus 
	           	try
	           	{
	           		DiscreteSignals.ElementAt(i).Value.Value =  	
	           			modbusReader.ReadDiscrete(
	           						slaveId, 
	           						Convert.ToUInt16(DiscreteSignals.ElementAt(i).Value.Modbus_address));
	           		// Переписываем в массив бит
	           		discreteBits[i] = DiscreteSignals.ElementAt(i).Value.Value;
	           	}
	           	catch (Exception ex)
	           	{
	           		eventLog.WriteEntry("Не могу считать дискретное значение Modbus: " + DiscreteSignals.ElementAt(i).Value.Modbus_address + " \n" + ex.Message);
	           	}
           	}
           		
           	// Создаем массив байт для упаковки дискретных значений
           	try
           	{
           		discreteBytes = new byte[ discreteBits.Length >> 3 + ((discreteBits.Length & 7)==0 ? 0 : 1 ) ];
           		eventLog.WriteEntry(discreteBytes.Length.ToString());
           	}
           	catch (Exception ex)
           	{
           		eventLog.WriteEntry(ex.Message);
           	}
           	// Упаковываем данные в байты
           	discreteBits.CopyTo(discreteBytes, 0);
           	#endregion
           	
           	#region Чтение дискретных сигналов
           	// Заполняем структуру для аналоговых сигналов
           	AnalogSignals = dbSource.GetParams("confAnalog", "dictTypes");
           	// Перебираем все элементы
           	int bytesCounter = 0;
           	for (int i = 0; i <= AnalogSignals.Count - 1; i++ )
           	{
           		AnalogSignals.ElementAt(i).Value.Timestamp = DateTime.Now;
           		try
           		{
           			AnalogSignals.ElementAt(i).Value.Value = 
           					modbusReader.ReadAnalog(
           						slaveId,
           						Convert.ToUInt16(AnalogSignals.ElementAt(i).Value.Modbus_address),
           						Convert.ToUInt16(AnalogSignals.ElementAt(i).Value.Size));
           			
           			byte[] floatToBytes = BitConverter.GetBytes(AnalogSignals.ElementAt(i).Value.Value);
           			
           			foreach (byte itemByte in floatToBytes)
           			{
           				analogBytes[bytesCounter] = itemByte;
           				bytesCounter++;
           			}
           		}
           		catch (Exception ex)
           		{
           			eventLog.WriteEntry("Не могу считать аналоговое значение Modbus: " + AnalogSignals.ElementAt(i).Value.Modbus_address + " \n" + ex.Message);
           		}
           	}
           	#endregion
         }
		#endregion
		
		#region Массив данных для передачи через сокет
		public byte[] GetSocketData()
		{
			byte[] ret = new byte[discreteBytes.Length + analogBytes.Length];
			Array.Copy(discreteBytes, 0, ret, 0, discreteBytes.Length);
			Array.Copy(analogBytes, 0, ret, discreteBytes.Length, analogBytes.Length);
			return ret;
		}
		#endregion
	}
}
