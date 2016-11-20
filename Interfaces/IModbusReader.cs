/*
 * Created by SharpDevelop.
 * User: Technical department
 * Date: 06.11.2016
 * Time: 18:26
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace Interfaces
{
	/// <summary>
	/// Интерфейс для чтения данных по Modbus
	/// </summary>
	public interface IModbusReader
	{
		/// <summary>
		/// Метод для чтения дискретного сигнала
		/// </summary>
		/// <param name="slaveId">Адрес Modbus устройства</param>
		/// <param name="address">Адрес параметра</param>
		/// <returns>Значение параметра</returns>
		bool ReadDiscrete(byte slaveId, ushort address);
		
		/// <summary>
		/// Метод для чтения дискретных сигналов
		/// </summary>
		/// <param name="slaveId">Адрес Modbus устройства</param>
		/// <param name="startAddress">Адрес первого параметра</param>
		/// <param name="Count">Количество сигналов</param>
		/// <returns>Массив значений параметров</returns>
		bool[] ReadDiscretes(byte slaveId, ushort startAddress, ushort Count);
		
		
		/// <summary>
		/// Метод для считывания целочисленного аналогового сигнала
		/// </summary>
		/// <param name="slaveId">Адрес Modbus устройства</param>
		/// <param name="address">Адрес сигнала</param>
		/// <returns>Значение параметра</returns>
		ushort ReadAnalog(byte slaveId, ushort address);
		
		/// <summary>
		/// Метод для считывания дробного аналогового сигнала
		/// </summary>
		/// <param name="slaveId">Адрес Modbus устройства</param>
		/// <param name="address">Адрес сигнала</param>
		/// <returns>Значение параметра</returns>
		float ReadAnalog(byte slaveId, ushort address);
		
		/// <summary>
		/// Метод для считывания аналоговых сигналов
		/// </summary>
		/// <param name="slaveId">Адрес Modbus устройства</param>
		/// <param name="startAddress">Адрес первого параметра</param>
		/// <param name="Count">Количество сигналов</param>
		/// <returns>Значение параметра</returns>
		float[] ReadAnalogs(byte slaveId, ushort startAddress, ushort Count);
		
		/*
		
		/// <summary>
		/// Метод для считывания аналогового сигнала
		/// </summary>
		/// <param name="slaveId">Адрес Modbus устройства</param>
		/// <param name="address">Адрес сигнала</param>
		/// <returns>Значение параметра</returns>
		ushort ReadAnalog(byte slaveId, ushort address);
		
		/// <summary>
		/// Метод для считывания аналоговых сигналов
		/// </summary>
		/// <param name="slaveId">Адрес Modbus устройства</param>
		/// <param name="startAddress">Адрес первого параметра</param>
		/// <param name="Count">Количество сигналов</param>
		/// <returns>Значение параметра</returns>
		ushort[] ReadAnalogs(byte slaveId, ushort startAddress, ushort Count);*/
	}
}
