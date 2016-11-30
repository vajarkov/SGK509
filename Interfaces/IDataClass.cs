/*
 * Created by SharpDevelop.
 * User: Technical department
 * Date: 26.11.2016
 * Time: 13:33
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Interfaces
{
	/// <summary>
	/// Интерфейс обработки данных
	/// </summary>
	public interface IDataClass
	{
		
		/// <summary>
		/// Считывание из конфигурации период опроса
		/// </summary>
		/// <returns>Время в секундах</returns>
		int GetDBPeriod();
		
		
		/// <summary>
		/// Данные о размере передаваемых данных в сокет
		/// </summary>
		/// <returns>Общий размер данныхы</returns>
		int GetSizeBytes();
		
		/// <summary>
		/// Данные о размере передаваемых данных в сокет
		/// </summary>
		/// <returns>Размер дискретных данных</returns>
		int GetDiscreteSize();
		
		/// <summary>
		/// Данные о размере передаваемых данных в сокет 
		/// </summary>
		/// <returns>Размер аналоговых данных</returns>
		int GetAnalogSize();
		
		/// <summary>
		/// Данные о размере передаваемых данных в сокет из БД
		/// </summary>
		/// <returns>Общий размер данныхы</returns>
		int GetInitSize();
		
		/// <summary>
		/// Данные о размере передаваемых данных в сокет из БД
		/// </summary>
		/// <returns>Размер дискретных данных</returns>
		int GetInitDiscreteSize();
		
		/// <summary>
		/// Данные о размере передаваемых данных в сокет из БД
		/// </summary>
		/// <returns>Размер аналоговых данных</returns>
		int GetInitAnalogSize();
		
		/// <summary>
		/// Получение IP-адреса службы
		/// </summary>
		/// <returns>IP-адрес</returns>
		string GetServiceIPAddress();
		
		/// <summary>
		/// Получение порта сокета для службы
		/// </summary>
		/// <returns>номер порта</returns>
		int GetServicePort();
		
		/// <summary>
		/// Запись в БД сигнала для таймера службы
		/// </summary>
		/// <param name="sender">Таймер</param>
		/// <param name="e">Обработчик событий</param>
		void WriteDBSignals(object sender, EventArgs e);
		
		/// <summary>
		/// Основной поток для опроса значений по Modbus
		/// </summary>
		void MainThread();
		
		/// <summary>
		/// Конфигурация журнала событий
		/// </summary>
		void GetEventConfig();
		
		/// <summary>
		/// Массив байт для передачи через сокет
		/// </summary>
		/// <returns>Дискретные и аналоговые сигналы</returns>
		byte[] GetSocketData();
		
		/// <summary>
		/// Получение имени службы
		/// </summary>
		/// <returns>Строка с названием службы</returns>
		string GetServiceName();
		
		/// <summary>
		/// Получение имени компьютера, где установлена служба
		/// </summary>
		/// <returns>Строка с именем компьютера</returns>
		string GetHostName();
	}
}
