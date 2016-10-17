/*
 * Created by SharpDevelop.
 * User: Technical department
 * Date: 17.10.2016
 * Time: 12:13
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Text;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using SGK509;
using SGKService;
using System.Configuration;
using System.ServiceProcess;
using System.Configuration.Install;
using System.Diagnostics;
using System.Collections.Specialized;
using System.IO.Ports;


namespace SGK509
{
	/// <summary>
	/// Description of MainForm.
	/// </summary>
	public partial class MainForm : Form
	{
		
		private Configuration appConfig;        		// Переменная для чтения конфигурации
        private ServiceController controller;   		// Переменная для работы со службой
        private SlaveSettings slaveSettings;    		// Переменная для конфигурации файлов с данными
        private AppSettingsSection SerialPortSection;  	// Переменная для конфигурации порта
        private EventLog events = new EventLog();		// Переменная для записи событий
        private const string serviceName = "SGKService";// Переменная для имени службы
        
        #region Конструктор для приложения
		public MainForm()
		{
			InitializeComponent();
			CheckService(serviceName);					// Проверка существования службы
			ConfigurationInit();						// Инициализация конфигурации
			// Заполнение протоколов передачи
			ComboBoxInit(cbProtocol, new string[] {"Modbus RTU", "Modbus TCP"}, "Protocol");
			// Заполнение доступных портов
			//ComboBoxInit(cbPort, SerialPort.GetPortNames(), "PortName");
            // Заполнение скорости передачи
			//ComboBoxInit(cbBaudRate, new string[] { "1200", "2400", "4800", "9600", "19200", "38400", "57600", "115200", "230400" }, "BaudRate");
            // Заполнение четности портов
			//ComboBoxInit(cbParity, Enum.GetNames(typeof (Parity)), "Parity");
            // Заполнение Стоп-Битов
			//ComboBoxInit(cbStopBit, Enum.GetNames(typeof (StopBits)), "StopBits");
			// Заполнение Битов Данных
			//ComboBoxInit(cbDataBits, new string[] { "4", "5", "6", "7", "8", }, "DataBits");
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
            slaveSettings = (SlaveSettings)appConfig.GetSection("SlaveSettings");
            SerialPortSection = (AppSettingsSection)appConfig.GetSection("SerialPortSettings");

        }
        #endregion
        
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
        
        #region Проверить запущена ли служба ModbusRTUService
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
                
        #region Проверить установлена ли служба ModbusRTUService
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
        
        
        
        
	}
}
