/*
 * Created by SharpDevelop.
 * User: Technical department
 * Date: 06.11.2016
 * Time: 18:56
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Interfaces;
using System.IO.Ports;
using Modbus.Device;
using Modbus.Utility;

namespace ModbusReader
{
	/// <summary>
	/// Класс для Modbus RTU.
	/// </summary>
	public class ModbusRTUReader : IModbusReader
	{
		private static SerialPort serialPort;
		private static IModbusSerialMaster master;
		
		#region Конструктор класса и создание Modbus-устройства
		public ModbusRTUReader(string portName, int baudRate, Parity parity, int dataBits, StopBits stopBits)
		{
			serialPort = new SerialPort(portName, baudRate, parity, dataBits, stopBits);
			// Открываем порт если он не открыт
			if (!serialPort.IsOpen)
				serialPort.Open();
				
			// Создаем Modbus-устройство
			master = ModbusSerialMaster.CreateRtu(serialPort);
		}
		#endregion
		
		#region Чтение дискретного сигнала
		public bool ReadDiscrete(byte slaveId, ushort address)
		{
			return master.ReadInputs(slaveId, address, 1)[0];
		}
		#endregion
		
		#region Чтение дискретных сигналов
		public bool[] ReadDiscretes(byte slaveId, ushort address, ushort Count)
		{
			return master.ReadInputs(slaveId, address, Count);
		}
		#endregion
		
		#region Чтение дробного аналогового сигнала
		public float ReadAnalog(byte slaveId, ushort address, ushort size)
		{
			float ret = 0;
			if (size == 2)
			{
				ushort[] registers = master.ReadInputRegisters(slaveId, address, 1);
				ret = Convert.ToSingle(registers);
			}
			else
			
			if(size == 4)
			{
				ushort[] registers = master.ReadInputRegisters(slaveId, address, 2);
				ret = ModbusUtility.GetSingle(registers[0], registers[1]);
			}
			return ret;
		}
		#endregion
		
		#region Чтение аналоговых сигналов
		public float[] ReadAnalogs(byte slaveId, ushort address, ushort Count)
		{
			float[] retValue = new float[Count];
			for (ushort i = 0; i < Count; i++)
			{
				ushort[] registers = master.ReadInputRegisters(slaveId, address, 2);
				retValue[i] = ModbusUtility.GetSingle(registers[0], registers[1]);
			}
			return retValue;
		}
		#endregion
		/*
		#region Чтение аналогового сигнала
		public ushort ReadAnalog(byte slaveId, ushort address)
		{
			return master.ReadInputRegisters(slaveId, address, 1)[0];
		}
		#endregion
		
		#region Чтение аналоговых сигналов
		public ushort[] ReadAnalogs(byte slaveId, ushort address, ushort Count)
		{
			return master.ReadInputRegisters(slaveId, address, Count);
		}
		#endregion*/
	}
}
