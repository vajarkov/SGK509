/*
 * Created by SharpDevelop.
 * User: Technical department
 * Date: 04.11.2016
 * Time: 16:49
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;

namespace DataTypes
{
	/// <summary>
	/// Общее описание для сигналов
	/// </summary>
	/// <param name="Num">Номер канала в списке конфигурации</param>
	/// <param name="Datetime">Время измерения канала</param>
	/// <param name="Modbus_address">Modbus адрес канала</param>
	/// <param name="Value">Значение параметра</param>
	public abstract class Signal
	{
		public int Num { get; set; }
		public DateTime Timestamp { get; set; }
		public int Modbus_address { get; set; }
	}
}